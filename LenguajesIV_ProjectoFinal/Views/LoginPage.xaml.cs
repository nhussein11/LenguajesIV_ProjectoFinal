using LenguajesIV_ProjectoFinal.ViewModels;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }


        private async void TapGestureRecognizer_Registrase(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Registrarse)}");
        }
    }
}