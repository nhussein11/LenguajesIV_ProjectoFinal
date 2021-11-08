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
        /*Metodos que creo necesitamos
            Buscar infractor por dni
            Buscar vehiculo por matricula
            Buscar cod de vehiculo por matricula
            Buscar cod de agente por usuario
            Grabar multa en bd -> retornar cod con el que se graba
            Grabar infractor en bd
            Grabar vehiculo en bd
         */

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
            //Navegar a siguiente pantalla
            await Shell.Current.GoToAsync($"///{nameof(CargarDetallesMulta)}");

        }

        private void Validar_Vehiculo(object sender, EventArgs e)
        {
            //buscar en bd patente
            string patente = this.txtPatente.Text;
            //cargar en campos los datos encontrados
            // si no encuentra dato --> displayAlert("No esta registrado, ingrese los datos para registrarlo junto con la multa")
                //Crear Vehiculo si es necesario
                Vehiculos auto = new Vehiculos();
                auto.patente_dominio_vehiculo = this.txtPatente.Text;
                auto.modelo_vehiculo = this.txtModelo.Text;
                auto.caracteristicas_vehiculo = this.txtCaracteristicas.Text;
                //Mando el Objeto Auto a las propiedades y le pongo un null a cod_auto
                Application.Current.Properties["auto"] = auto;
                Application.Current.Properties["cod_auto"] = null;
            //si encontre el auto, mando el codigo del auto  a las propiedades
            //De esta forma se cuando hay que registrar o no
                Application.Current.Properties["cod_auto"] = 1;

        }
        private void Validar_Infractor(object sender, EventArgs e) {
            //Buscar dni en bd
            //Si encuentro
            //cargar datos en los campos
                //Mando el codigo a propiedades
                     Application.Current.Properties["cod_infractor"] = 1;
            // si no encuentra dato -->  displayAlert("No esta registrado, ingrese los datos para registrarlo junto con la multa")
            //Crear Infractor si es necesario
            Infractores infractor = new Infractores();
                    infractor.apellido_infractor = this.txtapellido.Text;
                    infractor.dni_infractor = Convert.ToInt32(this.txtdni.Text);
                    infractor.nombre_infractor = this.txtdomicilio.Text;
                    infractor.telefono_infractor = Convert.ToInt32(this.txttelefono.Text);
                    infractor.domicilio_infractor = this.txtdomicilio.Text;
                //Mando infractor a Propiedades y el codigo en null
                Application.Current.Properties["infractor"] = infractor;
                Application.Current.Properties["cod_infractor"] = null;


        }

    }
}