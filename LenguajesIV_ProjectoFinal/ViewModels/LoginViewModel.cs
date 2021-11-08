using LenguajesIV_ProjectoFinal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using LenguajesIV_ProjectoFinal.Services;
using LenguajesIV_ProjectoFinal.Models;
using System.Threading.Tasks;

namespace LenguajesIV_ProjectoFinal.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        
        public ICommand IniciarSesionCommand => new Command(OnLoginClicked);
        public  string usuario { get; set; }
        public  string contraseña { get; set; }
        public bool ValidarCampos() {
            return usuario.Length > 0
                    && !string.IsNullOrEmpty(usuario)
                    && contraseña.Length > 0
                    && !string.IsNullOrEmpty(contraseña);
        }
        public async Task<bool> ValidarUsuarioAsync()
        {
            //Buscar en bd credenciales
            Agentes agen = new Agentes();
            agen = await App.SQLiteDB.Get_Agentes_byUserandPassword_Async(this.usuario, this.contraseña);
            if (agen != null)
            {
                return true;
            }
            else { return false; }
        }

        private async void OnLoginClicked(object obj)
        {
            if (this.ValidarCampos())
            {
                if (await this.ValidarUsuarioAsync())
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
