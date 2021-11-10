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
        public  VerMultas()
        {
            InitializeComponent();
            BindingContext = this;
            //Nico: Metodo para traerse las multas de ese agente
            //traerse el infractor de una multa
            //traerse el vehiculo de una multa
            //traerse la ubicacion de una multa
            //NO hace falta los detalles 
            //crear Obj multa con esos datos y meterlo en ListadoMultasRealizadas
            //Debe ser asincrónico --> me llevo todo a una función:
            Cargar_Multas_Realizadas();


        }
        public async void Cargar_Multas_Realizadas() 
        {
            int cod_agente_activo = ((Agentes)Application.Current.Properties["DatosUsuario"]).cod_agente;
            //Nico: Metodo para traerse las multas de ese agente
            List<Multas> lista_multas = await App.SQLiteDB.Get_MultasxAgente_Async(cod_agente_activo);
            foreach (Multas multa in lista_multas)
            {
                MultasRealizadas multaRealizada = new MultasRealizadas
                {
                    nombre_agente = ((Agentes)Application.Current.Properties["DatosUsuario"]).nombre_agente,
                    apellido_agente = ((Agentes)Application.Current.Properties["DatosUsuario"]).apellido_agente,
                    fecha_multa = multa.fecha_multa,
                    lugar_multa = multa.lugar_multa,
                    dni_infractor = (await App.SQLiteDB.Get_InfractorxMulta_Async(multa.cod_infractor)).dni_infractor,
                    patente_dominio_vehiculo = (await App.SQLiteDB.Get_VehiculoxMulta_Async(multa.cod_vehiculo)).patente_dominio_vehiculo,
                    latitud_ubicacion= (await App.SQLiteDB.Get_UbicacionxMulta_Async(multa.cod_multa)).latitud_ubicacion,
                    longitud_ubicacion = (await App.SQLiteDB.Get_UbicacionxMulta_Async(multa.cod_multa)).longitud_ubicacion,
                };
                this.ListadoMultasRealizadas.Add(multaRealizada);
            }
            //NO hace falta los detalles --> OK 
        }
        private void Abrir_Ubicacion_Multa(object sender, SelectedItemChangedEventArgs e)
        {
            // con la lat y long de la multa -> abrir google maps

        }
    }
}