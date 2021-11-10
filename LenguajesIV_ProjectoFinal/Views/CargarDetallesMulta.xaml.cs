using LenguajesIV_ProjectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LenguajesIV_ProjectoFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CargarDetallesMulta : ContentPage
    {
        public IList<Detalle_Multa> ListaDeInfraccionesAgregadas { get; set; } //lleva registro de los detalles agregados
        public IList<Infracciones> ListaDeInfracciones { get; set; }

        public CargarDetallesMulta()
        {
            ListaDeInfraccionesAgregadas = new List<Detalle_Multa>();
            ListaDeInfracciones = new ObservableCollection<Infracciones>();
         
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 1, descripcion_infraccion = "Manejar en estado de ebriedad", precio_tentativo_infraccion = 1600, severidad_infraccion = "grave" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 2, descripcion_infraccion = "Exceso de velocidad", precio_tentativo_infraccion = 1500, severidad_infraccion = "grave" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 3, descripcion_infraccion = "Uso del celular al volante", precio_tentativo_infraccion = 1250, severidad_infraccion = "intermedio" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 4, descripcion_infraccion = "Ausencia de matafuegos", precio_tentativo_infraccion = 700, severidad_infraccion = "leve" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 5, descripcion_infraccion = "No utilizar del cinturon de seguridad", precio_tentativo_infraccion = 1000, severidad_infraccion = "intermedio" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 6, descripcion_infraccion = "Estacionamiento en rampa de discapacitados", precio_tentativo_infraccion = 1300, severidad_infraccion = "intermedio" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 7, descripcion_infraccion = "Cruzar semáforo en rojo", precio_tentativo_infraccion = 1550, severidad_infraccion = "grave" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 8, descripcion_infraccion = "No utilizar las luces correspondientes", precio_tentativo_infraccion = 1400, severidad_infraccion = "intermedio" });
            
            InitializeComponent();
            BindingContext = this;
          
        }
        private void Atras(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(NuevaMulta)}");
        }

        private async void Siguiente(object sender, EventArgs e)
        {
            if (ListaDeInfraccionesAgregadas.Count > 0)
            {
                Application.Current.Properties["listaDetalles"] = ListaDeInfraccionesAgregadas;
                await Shell.Current.GoToAsync($"//{nameof(TomarFoto)}");
            }
            else {
                await DisplayAlert("Atencion!", "Debe agregar al menos una infraccion a la multa", "ok");
            }
            
        }

        private async void Agregar_Detalle(object sender, EventArgs e)
        {
            try
            {
                Detalle_Multa detalle = new Detalle_Multa();
                //Nose si a esto te referis con: "agregar cod multa a detalles antes de grabar" en el HACK
                //  A la multa cuando la grabas en la base de datos, te devuelve un codigo, ese es el codigo autoincremental
                // lo que hay que hacer es En MarcarEnElMapa, grabar la multa, el codigo que te devuelva el metodo, ponerselos al detalle en cod_multa ANTES  de grabarlos
                detalle.cod_multa = ((Multas)Application.Current.Properties["Multa"]).cod_multa; //El cod multa de este objeto siempre va a ser 0, porque nunca le definiste uno, al momento de grabarlo en la bd SIN EL COD 0, el metodo de grabar te devuelve con que valor lo grabo en ese campo, ese usas como codigo

                detalle.descripcion_infraccion = ((Infracciones)this.InfraccionPicker.SelectedItem).descripcion_infraccion.ToString();
                detalle.cod_infracion = ((Infracciones)this.InfraccionPicker.SelectedItem).cod_infraccion;
                detalle.subtotal_detalle_multa = Convert.ToInt32(this.txtPrecio.Text);
                detalle.testimonio_agente = this.txtTestimonio.Text;
                detalle.observacion_detalle_multa = this.txtObservaciones.Text;
                //el cod de la multa se lo agregamos cuando insertemos la multa en la BD y nos devuelva el codigo!

                //agrego detalle a la lista
                ListaDeInfraccionesAgregadas.Add(detalle);

                //reseteo campos
                this.InfraccionPicker.SelectedItem = null;
                this.txtPrecio.Text = "";
                this.txtObservaciones.Text = "";
                this.txtTestimonio.Text = "";

                await DisplayAlert("Correcto", "Se agrego el detalle a la lista", "ok");
                await Shell.Current.GoToAsync($"//{nameof(NuevaMulta)}");
                await Shell.Current.GoToAsync($"//{nameof(CargarDetallesMulta)}"); //dios me perdone por la chanchada
            }
            catch (Exception)
            {

                await DisplayAlert("Advertencia!", "Complete todos los campos antes de continuar", "OK");
            }
            


        }

        private void InfraccionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rellenar con precio sugerido
            try
            {
                this.txtPrecio.Text = Convert.ToString(((Infracciones)InfraccionPicker.SelectedItem).precio_tentativo_infraccion);

            }
            catch (Exception)
            {

                this.txtPrecio.Text = "";
            } 
        }

        private  async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Eliminar elemento de la lista
            ListaDeInfraccionesAgregadas.Remove((Detalle_Multa)e.SelectedItem);
            await DisplayAlert("Correcto!", "Elemento eliminado de la lista!", "ok");

            await Shell.Current.GoToAsync($"//{nameof(NuevaMulta)}");//again
            await Shell.Current.GoToAsync($"//{nameof(CargarDetallesMulta)}");
        }
    }
}