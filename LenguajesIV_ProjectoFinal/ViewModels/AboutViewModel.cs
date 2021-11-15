using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LenguajesIV_ProjectoFinal.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Inicio";
        }

        public ICommand OpenWebCommand { get; }
    }
}