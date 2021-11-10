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
            // hardcodearlas(?
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 1, descripcion_infraccion = "Manejar en estado de ebriedad", precio_tentativo_infraccion = 1600, severidad_infraccion = "grave" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 2, descripcion_infraccion = "Exceso de velocidad", precio_tentativo_infraccion = 1500, severidad_infraccion = "grave" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 3, descripcion_infraccion = "Uso del celular al volante", precio_tentativo_infraccion = 1250, severidad_infraccion = "intermedio" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 4, descripcion_infraccion = "Ausencia de matafuegos", precio_tentativo_infraccion = 700, severidad_infraccion = "leve" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 5, descripcion_infraccion = "No utilizar del cinturon de seguridad", precio_tentativo_infraccion = 1000, severidad_infraccion = "intermedio" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 6, descripcion_infraccion = "Estacionamiento en rampa de discapacitados", precio_tentativo_infraccion = 1300, severidad_infraccion = "intermedio" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 7, descripcion_infraccion = "Cruzar semáforo en rojo", precio_tentativo_infraccion = 1550, severidad_infraccion = "grave" });
            ListaDeInfracciones.Add(new Infracciones { cod_infraccion = 8, descripcion_infraccion = "No utilizar las luces correspondientes", precio_tentativo_infraccion = 1400, severidad_infraccion = "intermedio" });
        }
    }
}
