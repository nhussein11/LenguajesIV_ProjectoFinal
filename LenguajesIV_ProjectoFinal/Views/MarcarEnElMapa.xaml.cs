﻿using System;
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
            //aca van a estar los datos de la localizacion tambien
            
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
            //Al final de todo mostrar alert que se guardo todo correcto
            await DisplayAlert("Atencion!", "Se guardaron correctamente los datos", "OK");

            //mail al superior:
            //mail_superior_coninfo_multa(multa_a_insertar, (Infractores)Application.Current.Properties["infractor"], (Vehiculos)Application.Current.Properties["vehiculo"], (IList<Detalle_Multa>)Application.Current.Properties["listaDetalles"], (Agentes)Application.Current.Properties["DatosUsuario"]);

            //limpiar variables de Properties 
            Application.Current.Properties["infractor_nuevo"] = false;
            Application.Current.Properties["vehiculo_nuevo"] = false;
            Application.Current.Properties["infractor"] = null;
            Application.Current.Properties["vehiculo"] = null;
            Application.Current.Properties["Multa"] = null;
            Application.Current.Properties["listaDetalles"] = null;
            Application.Current.Properties["DatosUsuario"] = null;
            Application.Current.Properties["path_foto"] = null;
            //y ¿todos los entry usados tmb?

        }


        /*
         public void mail_superior_coninfo_multa(Multas multa, Infractores infractor, Vehiculos vehiculo, IList<Detalle_Multa> detalles_multa, Agentes agente) {
            var renglon = "";
            foreach (var detalle in detalles_multa) {
                renglon = renglon + " - " + detalle.descripcion_infraccion;
            }
            try
            {
                var mensaje = new EmailMessage(
                    "Nueva Multa Registrada. Día: "+multa.fecha_multa+" .Hora: "+multa.hora_multa, 
                    "Se ha registrado una multa, cod: "+multa.cod_multa+" , por los siguientes motivos: "+renglon+" . El infrator es: "+ infractor.apellido_infractor+" . El vehiculo involucrado es: "+vehiculo.modelo_vehiculo, 
                    //O algo asi:
                    municipalidad_salta@gmail.com);
                mensaje.BodyFormat = EmailBodyFormat.PlainText;
                await Email.ComposeAsync(mensaje);
            }
            catch (Exception)
            {

                await DisplayAlert("Error!", "El servicio de mensajeria no esta disponible por el momento", "OK");
            }
        }
         */

    }
}