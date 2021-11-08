using LenguajesIV_ProjectoFinal.Models;
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
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
            this.txtusuario.Text = (string)Application.Current.Properties["usuario"];
            this.txtcontraseña.Text = (string)Application.Current.Properties["contrasena"];
            //Como tiene que ser asincronico llamo a un procedimiento de estas caracteristicas:
            Llenar_campos_usuarioactivo(this.txtusuario.Text, this.txtcontraseña.Text);

        }

        private async void GuardarDatos(object sender, EventArgs e)
        {
            //metodo que haga update en la tabla agentes, donde el usuario sea (string)Application.Current.Properties["usuario"]
            //No puede cambiar su nombre de usuario, nos hace la vida mas facil
            Agentes agente_conectado = new Agentes {
                cod_agente =((Agentes) Application.Current.Properties["DatosUsuario"]).cod_agente,
                user_agente = ((Agentes)Application.Current.Properties["DatosUsuario"]).user_agente,
                password_agente= this.txtcontraseña.Text,
                nombre_agente = this.txtnombre.Text,
                apellido_agente = this.txtapellido.Text,
                dni_agente = Convert.ToInt32(this.txtdni.Text),
                domicilio_agente = this.txtdomicilio.Text,
                telefono_agente = Convert.ToInt64(this.txttelefono.Text),
                email_agente = this.txtcorreo.Text.ToString()
            };
            await App.SQLiteDB.SaveAgentesAsync(agente_conectado);
           
        }
        public async void Llenar_campos_usuarioactivo(string user, string contra) 
        {
            Agentes agente_conectado = new Agentes();
            agente_conectado = await App.SQLiteDB.Get_Agentes_byUserandPassword_Async(this.txtusuario.Text, this.txtcontraseña.Text);
            this.txtnombre.Text = agente_conectado.nombre_agente;
            this.txtapellido.Text = agente_conectado.apellido_agente;
            this.txtdni.Text = agente_conectado.dni_agente.ToString();
            this.txtdomicilio.Text = agente_conectado.domicilio_agente;
            if (agente_conectado.telefono_agente != 0) { this.txttelefono.Text = agente_conectado.telefono_agente.ToString(); }
            else{ this.txttelefono.Text = ""; }
            this.txtcorreo.Text = agente_conectado.email_agente;
        }
    }
}