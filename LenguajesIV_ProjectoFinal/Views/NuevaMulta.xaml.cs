using LenguajesIV_ProjectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LenguajesIV_ProjectoFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevaMulta : ContentPage
    {
        public NuevaMulta()
        {
            InitializeComponent();
            this.HoraPicker.Time = DateTime.Now.TimeOfDay;     
        }

        private async void Validar_Infractor(object sender, EventArgs e)
        {
            //Buscar dni en bd
            Infractores infractor = new Infractores();
            infractor = await App.SQLiteDB.Get_Infractores_byDNI_Async(txtdni.Text);
            //Si encuentro
            //cargar datos en los campos
            if (await App.SQLiteDB.Get_Infractores_byDNI_Async(txtdni.Text) != null)
            {
                txtnombre.Text = infractor.nombre_infractor;
                txtapellido.Text = infractor.apellido_infractor;
                txttelefono.Text = infractor.telefono_infractor.ToString();
                txtdomicilio.Text = infractor.domicilio_infractor;
                Application.Current.Properties["infractor"] = infractor;
                Application.Current.Properties["infractor_nuevo"] = false;
            }
            else
            {
                await DisplayAlert("No esta registrado", "ingrese los datos para registrarlo junto con la multa","ok");
                Application.Current.Properties["infractor_nuevo"] = true;
            }
        }

        private async void Validar_Vehiculo(object sender, EventArgs e)
        {
            Vehiculos vehiculo = new Vehiculos();
            vehiculo = await App.SQLiteDB.Get_VehiculosbyPatente_Async(txtPatente.Text);
            if (vehiculo != null)
            {
                txtModelo.Text = vehiculo.modelo_vehiculo;
                txtCaracteristicas.Text = vehiculo.caracteristicas_vehiculo;
                Application.Current.Properties["vehiculo"] = vehiculo;
                Application.Current.Properties["vehiculo_nuevo"] = false;
            }
            else {
                await DisplayAlert("No esta registrado", "ingrese los datos para registrarlo junto con la multa", "ok");
                Application.Current.Properties["vehiculo_nuevo"] = true;
            }
        }


        private async void Siguiente_Pantalla(object sender, EventArgs e)
        {
            //Validar que esten ingresados los campos y sean validos
            //Siempre se crea la multa
            Multas multa = new Multas();

            //se agrega fecha y hora
            multa.fecha_multa = this.FechaPicker.Date.ToString("{MMM d, yyyy}");
            multa.fecha_multa = this.HoraPicker.Time.ToString();
            //Paso la multa a propiedades
            Application.Current.Properties["Multa"] = multa;
            if ((bool)Application.Current.Properties["infractor_nuevo"] == true)
            {
                Infractores infractor = new Infractores();
                infractor.apellido_infractor = this.txtapellido.Text;
                infractor.dni_infractor = (this.txtdni.Text);
                infractor.nombre_infractor = this.txtdomicilio.Text;
                infractor.telefono_infractor = Convert.ToInt32(this.txttelefono.Text);
                infractor.domicilio_infractor = this.txtdomicilio.Text;
                Application.Current.Properties["infractor"] = infractor;
            }
            if ((bool)Application.Current.Properties["vehiculo_nuevo"] == true)
            {
                Vehiculos vehiculo = new Vehiculos();
                vehiculo.patente_dominio_vehiculo = this.txtPatente.Text;
                vehiculo.modelo_vehiculo = this.txtModelo.Text;
                vehiculo.caracteristicas_vehiculo = this.txtCaracteristicas.Text;
                Application.Current.Properties["vehiculo"] = vehiculo;
            }
            //Navegar a siguiente pantalla
            await Shell.Current.GoToAsync($"///{nameof(CargarDetallesMulta)}");
        }
    }
}