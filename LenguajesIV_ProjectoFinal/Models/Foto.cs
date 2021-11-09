using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace LenguajesIV_ProjectoFinal.Models
{
    public class Foto : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string nombre = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
        private ImageSource foto;

        public ImageSource Foto_img
        {
            get { return foto; }
            set { foto = value;
                OnPropertyChanged();
            }
        }

    }
}
