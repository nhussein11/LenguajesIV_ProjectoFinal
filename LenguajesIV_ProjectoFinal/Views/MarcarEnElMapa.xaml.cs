using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LenguajesIV_ProjectoFinal.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LenguajesIV_ProjectoFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarcarEnElMapa : ContentPage
    {
        public MarcarEnElMapa()
        {
            InitializeComponent();
        }

        private async void GrabarMulta(object sender, EventArgs e)
        {
            //aca van a estar los datos de la localizacion tambien
            
            
            if ((bool)Application.Current.Properties["infractor_nuevo"] == true)
            {
                await App.SQLiteDB.SaveInfractoresAsync((Infractores)Application.Current.Properties["infractor"]); //tira error expresion no soportada
            }
            if ((bool)Application.Current.Properties["vehiculo_nuevo"] == true)
            {
                await App.SQLiteDB.SaveVehiculosAsync((Vehiculos)Application.Current.Properties["vehiculo"]); // lo mismo aca 
            }
            //Cuando guardes la multa, guarda tambien el path de la foto del DNI ->FotoViewModel
            //Falta insertar la multa -> luego de insertar tomar cod

            foreach (var detalle in (IList<Detalle_Multa>)Application.Current.Properties["listaDetalles"]) {
                //Antes de insertar los detalles,ponerles en cod_multa el codigo de la multa que se acaba de registrar
                await App.SQLiteDB.SaveDetalle_MultaAsync(detalle);
            }

            await DisplayAlert("Correcto!", "la infraccion fue creada exitosamente, Se procedera a enviarse un correo a superior", "OK");


            int cod_multa = 0; //reemplazar por el codigo de la multa insertada
            string agente_apellido = ((Agentes)Application.Current.Properties["DatosUsuario"]).apellido_agente;
            string agente_nombre = ((Agentes)Application.Current.Properties["DatosUsuario"]).nombre_agente;
            string apellido_infractor = ((Infractores)Application.Current.Properties["infractor"]).apellido_infractor;
            string nombre_infractor = ((Infractores)Application.Current.Properties["infractor"]).nombre_infractor;
            string patente_vehiculo = ((Vehiculos)Application.Current.Properties["vehiculo"]).patente_dominio_vehiculo; // lo mismo aca 
            string modelo_vehiculo = ((Vehiculos)Application.Current.Properties["vehiculo"]).modelo_vehiculo; // lo mismo aca 
            string descricpion_vehiculo = ((Vehiculos)Application.Current.Properties["vehiculo"]).caracteristicas_vehiculo; // lo mismo aca 
            string motivo_descripcion_importe="";
       
            foreach (var detalle in (IList<Detalle_Multa>)Application.Current.Properties["listaDetalles"]) {
                motivo_descripcion_importe+="infraccion: "+detalle.descripcion_infraccion+",Observaciones: "+detalle.observacion_detalle_multa+",Importe: "+detalle.subtotal_detalle_multa+ "\n";
            }

                string Body = $"Se registro con exito la multa codigo:{cod_multa},labrada por el agente:{agente_apellido},{agente_nombre}.El infractor fue:{apellido_infractor},{nombre_infractor}.A borde de un vehiculo dominio:{patente_vehiculo}, Modelo:{modelo_vehiculo} cuya descripcion coincide con:{descricpion_vehiculo}. La multa esta dada en consta de los sigueintes conceptos:{motivo_descripcion_importe}";
            var mensaje = new EmailMessage("Nueva Multa generada", Body, "superior@salta.com.ar");
            mensaje.BodyFormat = EmailBodyFormat.PlainText;
            EmailAttachment foto_dni = new EmailAttachment((string)Application.Current.Properties["path_foto"]);
            mensaje.Attachments.Add(foto_dni);
            await Email.ComposeAsync(mensaje);

            Application.Current.Properties["vehiculo"] = null;
            Application.Current.Properties["multa"] = null;
            Application.Current.Properties["infractor"] = null;
            Application.Current.Properties["listaDetalles"] = null;
            Application.Current.Properties["path_foto"] = null;

        }

        private void Atras(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(TomarFoto)}");
        }
    }
}