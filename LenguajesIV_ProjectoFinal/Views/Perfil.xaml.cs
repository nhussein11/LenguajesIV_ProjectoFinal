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
            //metodo para Buscar datos de este usuario en la bd en base a nombre de usuario

        }

        private void GuardarDatos(object sender, EventArgs e)
        {
            //metodo que haga update en la tabla agentes, donde el usuario sea (string)Application.Current.Properties["usuario"]
            //No puede cambiar su nombre de usuario, nos hace la vida mas facil
        }
    }
}