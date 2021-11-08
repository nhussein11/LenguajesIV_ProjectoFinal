using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using LenguajesIV_ProjectoFinal.Models;
using System.Collections.ObjectModel;

namespace LenguajesIV_ProjectoFinal.ViewModels
{
    class CargarDetalleMultaViewModel
    {
        public IList<Infracciones> ListaDeInfracciones { get; set; }
        public CargarDetalleMultaViewModel() {
            ListaDeInfracciones = new ObservableCollection<Infracciones>();
            //buscar infracciones en BD y cargarlas aca o hardcodearlas(?
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 1, descripcion_infraccion = "Manejar ebrio", precio_tentativo_infraccion = 1235, severidad_infraccion = "grave" });
        }
    }
}
