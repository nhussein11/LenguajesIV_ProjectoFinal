using LenguajesIV_ProjectoFinal.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LenguajesIV_ProjectoFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class VerMultas : ContentPage
    {
        public IList<MultasRealizadas> ListadoMultasRealizadas { get; set; }
        public VerMultas()
        {
            InitializeComponent();
            BindingContext = this;
            //Nico: Metodo para traerse las multas de ese agente
            //traerse el infractor de una multa
            //traerse el vehiculo de una multa
            //traerse la ubicacion de una multa
            //NO hace falta los detalles 
            //crear Obj multa con esos datos y meterlo en ListadoMultasRealizadas

        }

        private void Abrir_Ubicacion_Multa(object sender, SelectedItemChangedEventArgs e)
        {
            // con la lat y long de la multa -> abrir google maps 
            //e.SelectedItem tiene el obj que seleccionaste 
        }
    }
}