using LenguajesIV_ProjectoFinal.Views;
using LenguajesIV_ProjectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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
        public string correo { get; set; }
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
                 &&  correo.Length > 0
                && !string.IsNullOrEmpty(correo)
                );

        }
        public async Task<bool> ValidarRegistroAsync() {
            // consultar a bd --> traer usuarios
            var agenList = await App.SQLiteDB.Get_Agentes_Async();
            // validar nombre de usuario y dni --> sean unicos
            foreach (var agente in agenList) {
                if (agente.user_agente == this.usuario)
                {
                    return false;
                }
                else if (agente.dni_agente == Convert.ToInt32(this.dni))
                {
                    return false;
                }
            }
            return true;
        }
        private async void Registrar_Usuario()
        {
            if (ValidarCampos())
            {
                if (await this.ValidarRegistroAsync())
                {
                    //Guardar nuevo registro en la bd
                    Agentes agente_nuevo = new Agentes {
                        nombre_agente = this.nombre,
                        apellido_agente = this.apellido,
                        dni_agente = Convert.ToInt32(this.dni),
                        email_agente=this.correo,
                        user_agente = this.usuario,
                        password_agente=this.contraseña
                    };
                    await App.SQLiteDB.SaveAgentesAsync(agente_nuevo);
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
