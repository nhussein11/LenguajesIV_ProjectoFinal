﻿using LenguajesIV_ProjectoFinal.ViewModels;
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
    public partial class TomarFoto : ContentPage
    {
        public TomarFoto()
        {
            InitializeComponent();
            BindingContext = new FotoViewModel();
            Application.Current.Properties["path_foto"] =null;

        }

        private void Atras(object sender, EventArgs e)
        {

            Shell.Current.GoToAsync($"//{nameof(CargarDetallesMulta)}");
        }

        private void Siguiente(object sender, EventArgs e)
        {
            if (Application.Current.Properties["path_foto"] != null)
            {
                Shell.Current.GoToAsync($"//{nameof(MarcarEnElMapa)}");

            }
            else {
                DisplayAlert("Atencion!", "Debe adjuntar una foto valida", "OK");
            }
        }
    }
}