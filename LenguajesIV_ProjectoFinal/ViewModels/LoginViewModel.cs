using LenguajesIV_ProjectoFinal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LenguajesIV_ProjectoFinal.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand IniciarSesionCommand => new Command(OnLoginClicked);
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public bool ValidarCampos() {
            return usuario.Length > 0
                    && !string.IsNullOrEmpty(usuario)
                    && contraseña.Length > 0
                    && !string.IsNullOrEmpty(contraseña);
        }
        private bool ValidarUsuario()
        {
            //Buscar en bd credenciales
            
            return true;
        }

        private async void OnLoginClicked(object obj)
        {
            if (this.ValidarCampos())
            {
                if (this.ValidarUsuario())
                {
                    //iniciar sesion
                    App.Current.Properties["usuario"] = this.usuario;
                    App.Current.Properties["isLogged"] = true;
                    await Shell.Current.GoToAsync($"//{nameof(Perfil)}");


                }
                else {
                    await Application.Current.MainPage.DisplayAlert("Error en el Inicio de Sesion", "Usuario y/o Contraseña incorrectos", "Ok");

                }
            }
            else {
                await Application.Current.MainPage.DisplayAlert("Error en el Inicio de Sesion", "Complete el formulario correctamente", "Ok");
            }
            
        }

        
    }
}
