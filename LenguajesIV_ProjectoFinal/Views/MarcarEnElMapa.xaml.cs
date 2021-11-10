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
        private void Atras(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(TomarFoto)}");
        }
        
        private async void GrabarMulta(object sender, EventArgs e)
        {
            //Guardo la ubicacion, lo pongo dentro de una funcion por prolijidad nomás pero si queres meterlo aca adentro no pasa nada
            Get_Location();
            //Inserto infractor
            if ((bool)Application.Current.Properties["infractor_nuevo"])
            {
                await App.SQLiteDB.SaveInfractoresAsync((Infractores)Application.Current.Properties["infractor"]); //tira error expresion no soportada
            }
            //Inserto vehiculo
            if ((bool)Application.Current.Properties["vehiculo_nuevo"])
            {
                await App.SQLiteDB.SaveVehiculosAsync((Vehiculos)Application.Current.Properties["vehiculo"]); // lo mismo aca 
            }
            //Inserto la multa
            Multas multa_a_insertar = new Multas();
            multa_a_insertar = (Multas)Application.Current.Properties["Multa"];
            multa_a_insertar.cod_infractor = ((Infractores)Application.Current.Properties["infractor"]).cod_infractores;
            multa_a_insertar.cod_vehiculo = ((Vehiculos)Application.Current.Properties["vehiculo"]).cod_vehiculo;
            multa_a_insertar.cod_agente = ((Agentes)Application.Current.Properties["DatosUsuario"]).cod_agente;
            multa_a_insertar.path_dni_infractorXmulta = (string)Application.Current.Properties["path_foto"];
            /*ACA faltaria una mas que sería para el cod_ubicacion, que recien lo vamos a tener cuando veamos lo del mapa*/
            //multa_a_insertar.cod_ubicacion
            //o  al reves: ubicacion_multa.cod_multa = ((Multas)Application.Current.Properties["Multa"]).cod_multa;

            //Inserto, en caso de ser necesario, los detalles
            foreach (var detalle in (IList<Detalle_Multa>)Application.Current.Properties["listaDetalles"]) {
                //Antes de insertar los detalles,ponerles en cod_multa el codigo de la multa que se acaba de registrar
                await App.SQLiteDB.SaveDetalle_MultaAsync(detalle);
            }
            
            await DisplayAlert("Atencion!", "Se guardaron correctamente los datos", "OK");

            //mail al superior:
            try
            {

                int cod_multa = 0;
                string agente_apellido = ((Agentes)Application.Current.Properties["DatosUsuario"]).apellido_agente;
                string agente_nombre = ((Agentes)Application.Current.Properties["DatosUsuario"]).nombre_agente;
                string apellido_infractor = ((Infractores)Application.Current.Properties["infractor"]).apellido_infractor;
                string nombre_infractor = ((Infractores)Application.Current.Properties["infractor"]).nombre_infractor;
                string patente_vehiculo = ((Vehiculos)Application.Current.Properties["vehiculo"]).patente_dominio_vehiculo;
                string modelo_vehiculo = ((Vehiculos)Application.Current.Properties["vehiculo"]).modelo_vehiculo;
                string descricpion_vehiculo = ((Vehiculos)Application.Current.Properties["vehiculo"]).caracteristicas_vehiculo;
                string motivo_descripcion_importe = "";

                foreach (var detalle in (IList<Detalle_Multa>)Application.Current.Properties["listaDetalles"])
                {
                    motivo_descripcion_importe += "infraccion: " + detalle.descripcion_infraccion + ". Observaciones: " + detalle.observacion_detalle_multa + ". Importe: " + detalle.subtotal_detalle_multa + "\n";
                }

                string Body = $"Se registro con exito la multa codigo: {cod_multa}, labrada por el agente: {agente_apellido}, {agente_nombre}.\n El infractor fue: {apellido_infractor}, {nombre_infractor}.\n A borde de un vehiculo dominio: {patente_vehiculo}, modelo: {modelo_vehiculo} cuya descripcion coincide con: {descricpion_vehiculo}.\n La multa esta dada en consta de los siguientes conceptos:\n {motivo_descripcion_importe}";
                var mensaje = new EmailMessage("Nueva Multa generada", Body, "superior@salta.com.ar");
                mensaje.BodyFormat = EmailBodyFormat.PlainText;
                EmailAttachment foto_dni = new EmailAttachment((string)Application.Current.Properties["path_foto"]);
                mensaje.Attachments.Add(foto_dni);
                await Email.ComposeAsync(mensaje);
            }
            catch (Exception)
            {
                await DisplayAlert("Error!", "El servicio de mensajeria no esta disponible por el momento", "OK");
            }


            //limpiar variables de Properties 
            Application.Current.Properties["infractor_nuevo"] = false;
            Application.Current.Properties["vehiculo_nuevo"] = false;
            Application.Current.Properties["infractor"] = null;
            Application.Current.Properties["vehiculo"] = null;
            Application.Current.Properties["Multa"] = null;
            Application.Current.Properties["listaDetalles"] = null;
            Application.Current.Properties["DatosUsuario"] = null;
            Application.Current.Properties["path_foto"] = null;
           // Los entrys tambien los podriamos borrar 

        }
        public async void Get_Location()
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }
            Ubicaciones ubi = new Ubicaciones
            {
                latitud_ubicacion = location.Latitude.ToString(),
                longitud_ubicacion = location.Longitude.ToString(),
                cod_multa = ((Multas)Application.Current.Properties["Multa"]).cod_multa
            };
            await App.SQLiteDB.SaveUbicacionesAsync(ubi);
        }
    }
}