using LenguajesIV_ProjectoFinal.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LenguajesIV_ProjectoFinal.ViewModels
{
    class FotoViewModel:Foto
    {
        public Command CapturarCommand { get; set; }
        public FotoViewModel() {

            CapturarCommand = new Command(TomarFoto);
        }
        private async void TomarFoto() {
            var camara = new StoreCameraMediaOptions();
            camara.PhotoSize = PhotoSize.Full;
            camara.SaveToAlbum = true;
            var foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(camara);
            if (foto != null) {

                Foto_img = ImageSource.FromStream(() => { return foto.GetStream(); });
                //guardo el path a la foto dentro del sistema de archivos
                //deberiamos guardar este path en la bd en multas
                //luego en el correo en la opcion Attachments lo adjuntamos como archivo
                Application.Current.Properties["path_foto"] = foto.AlbumPath;
            }
        
        }
    }
}
