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
            
            //Inserto infractor
            if ((bool)Application.Current.Properties["infractor_nuevo"])
            {
                await App.SQLiteDB.SaveInfractoresAsync((Infractores)Application.Current.Properties["infractor"]); 
            }
            //Inserto vehiculo
            if ((bool)Application.Current.Properties["vehiculo_nuevo"])
            {
                await App.SQLiteDB.SaveVehiculosAsync((Vehiculos)Application.Current.Properties["vehiculo"]); 
            }
            //Inserto la multa
            Multas multa_a_insertar = new Multas();
            multa_a_insertar.lugar_multa = ((Multas)Application.Current.Properties["Multa"]).lugar_multa;
            multa_a_insertar.hora_multa = ((Multas)Application.Current.Properties["Multa"]).hora_multa;
            multa_a_insertar.fecha_multa = ((Multas)Application.Current.Properties["Multa"]).fecha_multa;
            multa_a_insertar.path_dni_infractorXmulta = (string)Application.Current.Properties["path_foto"];
            //FKs de la multa
            multa_a_insertar.cod_infractor = ((Infractores)Application.Current.Properties["infractor"]).cod_infractores;
            multa_a_insertar.cod_vehiculo = ((Vehiculos)Application.Current.Properties["vehiculo"]).cod_vehiculo;
            multa_a_insertar.cod_agente = ((Agentes)Application.Current.Properties["DatosUsuario"]).cod_agente;

            /*Se me hace que no esta mostrando el metodo en VerMultas porque no estas registrando ninguna multa*/
            await App.SQLiteDB.SaveMultassAsync(multa_a_insertar);
            int cod_multa_insertada = (await App.SQLiteDB.Get_Cod_UltimaMulta_Async()).cod_multa;
            //Actualizo la FK de la ubicacion registrada
            ((Ubicaciones)Application.Current.Properties["Ubicacion"]).cod_multa = cod_multa_insertada;
            //Update en la bdd
            await App.SQLiteDB.SaveUbicacionesAsync(((Ubicaciones)Application.Current.Properties["Ubicacion"]));
            
            //Inserto, en caso de ser necesario, los detalles
            foreach (var detalle in (IList<Detalle_Multa>)Application.Current.Properties["listaDetalles"]) {
                detalle.cod_multa = cod_multa_insertada;
                
                await App.SQLiteDB.SaveDetalle_MultaAsync(detalle);
            }

            await DisplayAlert("Atencion!", "Se guardaron correctamente los datos, se procedera a redactarse un correo al Superior", "OK");

            //mail al superior:
            try
            {

                int cod_multa = (await App.SQLiteDB.Get_Cod_UltimaMulta_Async()).cod_multa;
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


            //Comento esto porque estaba dando un error
            //al cargar nueva multa -> toda accion que realizabas despues tiraba error de instancia nula -> seguro es por esto 
            /*
             
             Application.Current.Properties["infractor_nuevo"] = false;
            Application.Current.Properties["vehiculo_nuevo"] = false;
            Application.Current.Properties["infractor"] = null;
            Application.Current.Properties["vehiculo"] = null;
            Application.Current.Properties["Multa"] = null;
            Application.Current.Properties["listaDetalles"] = null;
            Application.Current.Properties["DatosUsuario"] = null;
            Application.Current.Properties["path_foto"] = null;
             
             */

            //Me voy a la pantalla de ver multas
            await Shell.Current.GoToAsync($"//{nameof(VerMultas)}");

        }

        private async void ObtenerUbicacion(object sender, EventArgs e)
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {

                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)

                }
                );
                this.txtLat.Text = Convert.ToString(location.Latitude);
                this.txtLong.Text = Convert.ToString(location.Longitude);
                //para pasar ubicacion a otras paginas
                Application.Current.Properties["latitud"] = Convert.ToString(location.Latitude);
                 Application.Current.Properties["longitud"] = Convert.ToString(location.Longitude);


            }
            else {
                this.txtLat.Text = Convert.ToString(location.Latitude);
                this.txtLong.Text = Convert.ToString(location.Longitude);
                //para pasar ubicacion a otras paginas
                Application.Current.Properties["latitud"] = Convert.ToString(location.Latitude);
                Application.Current.Properties["longitud"] = Convert.ToString(location.Longitude);
            }
            await Map.OpenAsync(Convert.ToDouble(Application.Current.Properties["latitud"]), Convert.ToDouble(Application.Current.Properties["longitud"]), new MapLaunchOptions
            {
                Name = "Tu ubicacion",
                NavigationMode = NavigationMode.Default

            });
            // Como lo veas mejor 
            Ubicaciones ubicacionXmulta = new Ubicaciones
            {
                latitud_ubicacion = this.txtLat.Text,
                longitud_ubicacion = this.txtLong.Text,
            };
            Application.Current.Properties["Ubicacion"] = ubicacionXmulta;
        } 
    } 
}
