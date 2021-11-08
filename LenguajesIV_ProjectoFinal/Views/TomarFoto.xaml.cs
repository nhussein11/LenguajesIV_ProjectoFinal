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
        }

        private void Capturar(object sender, EventArgs e)
        {

        }

        private void Atras(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(CargarDetallesMulta)}");
        }

        private void Siguiente(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(MarcarEnElMapa)}");
        }
    }
}