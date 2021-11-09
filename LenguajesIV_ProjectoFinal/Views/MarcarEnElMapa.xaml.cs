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
            
            //NO esta grabando
            if ((bool)Application.Current.Properties["infractor_nuevo"] == true)
            {
                await App.SQLiteDB.SaveInfractoresAsync((Infractores)Application.Current.Properties["infractor"]); //tira error expresion no soportada
            }
            if ((bool)Application.Current.Properties["vehiculo_nuevo"] == true)
            {
                await App.SQLiteDB.SaveVehiculosAsync((Vehiculos)Application.Current.Properties["vehiculo"]); // lo mismo aca 
            }
            //Inserto, en caso de ser necesario, los detalles
            foreach (var detalle in (IList<Detalle_Multa>)Application.Current.Properties["listaDetalles"]) {
                await App.SQLiteDB.SaveDetalle_MultaAsync(detalle);
            }

            //Al final de todo mostrar alert que se guardo todo correcto
            //Enviar correo
            //limpiar variables de Properties

        }

        private void Atras(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(TomarFoto)}");
        }
    }
}