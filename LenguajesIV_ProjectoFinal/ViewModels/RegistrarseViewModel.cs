using LenguajesIV_ProjectoFinal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading;
namespace LenguajesIV_ProjectoFinal.ViewModels
{
    class RegistrarseViewModel:BaseViewModel
    {
        public ICommand RegistrarUsuarioCommand => new Command(Registrar_Usuario);
        public string nombre { get ;set;}
        public string apellido { get; set; }
        public string dni { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public bool ValidarCampos()
        {

            return (
                nombre.Length > 0
                && !string.IsNullOrEmpty(nombre)
                && apellido.Length > 0
                && !string.IsNullOrEmpty(apellido)
                && Convert.ToInt32(dni) > 10000000
                && usuario.Length > 0
                && !string.IsNullOrEmpty(usuario)
                && contraseña.Length > 0
                && !string.IsNullOrEmpty(contraseña)
                );

        }
        public bool ValidarRegistro() {
            // consultar a bd --> traer usuarios
            // validar nombre de usuario --> sea unico 
            // validar que NO haya otro usuario con el mismo DNI

            return true;
        }
        private async void Registrar_Usuario()
        {
            if (ValidarCampos())
            {
                if (this.ValidarRegistro())
                {
                    //Guardar nuevo registro en la bd
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                else
                {

                    await Application.Current.MainPage.DisplayAlert("Error en Registro", "Usted ya se encuentra registrado", "Ok");

                }

            }
            else { 
                    await Application.Current.MainPage.DisplayAlert("Error en Registro", "Complete el formulario correctamente", "Ok");

            }
        }
        }
    }
