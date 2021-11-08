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
    public partial class CargarDetallesMulta : ContentPage
    {
        public List<Detalle_Multa> ListaDeInfraccionesAgregadas { get; set; } //lleva registro de los detalles agregados

        public CargarDetallesMulta()
        {
            ListaDeInfraccionesAgregadas = new List<Detalle_Multa>();
            InitializeComponent();
        }
        /*Metodos que necesitamos
         Grabar detalle en bd
         */

        private void Atras(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(NuevaMulta)}");
        }

        private void Siguiente(object sender, EventArgs e)
        {
            //Paso la lista de detalles para mostrar en la otra pantalla
            App.Current.Properties["detallesMulta"] = ListaDeInfraccionesAgregadas;
        }

        private void Agregar_Detalle(object sender, EventArgs e)
        {
            Detalle_Multa detalle = new Detalle_Multa();

            detalle.descripcion_infraccion = ((Infracciones)this.InfraccionPicker.SelectedItem).descripcion_infraccion.ToString();
            detalle.cod_infracion = ((Infracciones)this.InfraccionPicker.SelectedItem).cod_infraccion;
            detalle.subtotal_detalle_multa = Convert.ToInt32(this.txtPrecio.Text);
            detalle.testimonio_agente = this.txtTestimonio.Text;
            detalle.observacion_detalle_multa = this.txtObservaciones.Text;
            //cod multa 
            int cod_multa = (int)Application.Current.Properties["codigoMulta"];
            detalle.cod_multa = cod_multa;
            ListaDeInfraccionesAgregadas.Add(detalle);

            this.InfraccionPicker.SelectedItem = null;
            this.txtPrecio.Text = "";
            this.txtObservaciones.Text = "";
            this.txtTestimonio.Text = "";
            

        }

        private void InfraccionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtPrecio.Text = Convert.ToString(((Infracciones)InfraccionPicker.SelectedItem).precio_tentativo_infraccion);

            }
            catch (Exception)
            {

                this.txtPrecio.Text = "";
            } 
        }
    }
}