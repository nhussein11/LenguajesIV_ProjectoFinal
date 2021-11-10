using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LenguajesIV_ProjectoFinal.Services;
using LenguajesIV_ProjectoFinal.Models;
using Plugin.Messaging;

namespace LenguajesIV_ProjectoFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComunicarseConSuperior : ContentPage
    {
        public ComunicarseConSuperior()
        {
            InitializeComponent();
            Agentes usuario_conectado = (Agentes)Application.Current.Properties["DatosUsuario"];
            this.txtDe.Text = usuario_conectado.email_agente;
            
        }

        private async void EnviarCorreo(object sender, EventArgs e)
        {
            try
            {
                var emailMessenger = CrossMessaging.Current.EmailMessenger;
                if (emailMessenger.CanSendEmail)
                {

                    // Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc.
                    var email = new EmailMessageBuilder()
                      .To(this.txtPara.Text)
                      .Subject(this.txtAsunto.Text)
                      .Body(this.txtMensaje.Text)
                      .Build();

                    emailMessenger.SendEmail(email);
                }
                //var mensaje = new EmailMessage(this.txtAsunto.Text, this.txtMensaje.Text, this.txtPara.Text);
                //mensaje.BodyFormat = EmailBodyFormat.PlainText;
                //await Email.ComposeAsync(mensaje);
            }
            catch (Exception)
            {

                await DisplayAlert("Error!", "El servicio de mensajeria no esta disponible por el momento", "OK");
            }
            
        }

        private void actualizar_dni(object sender, FocusEventArgs e)
        {
            Agentes usuario_conectado = (Agentes)Application.Current.Properties["DatosUsuario"];
            this.txtDe.Text = usuario_conectado.email_agente;
        }
    }
}