﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LenguajesIV_ProjectoFinal.Models;
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
            //Aca recupero todos los objetos desde properties vehiculo/cod_vehiculo infractor/cod_infractor multa y detalles
            //aca van a estar los datos de la localizacion tambien
            //los inserto a todos
            //Inserto, en caso de ser necesario, los datos del infractor nuevo y del vehiculo neuvo dentro de la bdd
            //Cuando guardes la multa, guarda tambien el path de la foto del DNI ->FotoViewModel
            
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
                await App.SQLiteDB.SaveDetalle_MultaAsync(detalle);
            }
            await DisplayAlert("Atencion!", "Se guardaron correctamente los datos", "OK");
            
            //Al final de todo mostrar alert que se guardo todo correcto
            //Enviar correo
            //limpiar variables de Properties y ¡todos los entry usados!

        }

        private void Atras(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(TomarFoto)}");
        }
    }
}