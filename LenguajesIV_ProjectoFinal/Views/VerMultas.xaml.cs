using LenguajesIV_ProjectoFinal.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

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
            


        }
        public async void Cargar_Multas_Realizadas() 
        {
            int cod_agente_activo = ((Agentes)Application.Current.Properties["DatosUsuario"]).cod_agente;
            //Nico: Metodo para traerse las multas de ese agente
            // No esta trayendo las multas este metodo no l opuedo ver en eldebug,debe ser algo en el return, trata de hacerlo por partes
            // lo que le cambies ahi seguro lo tenes que cambiar en Get_InfractorxMulta_Async Get_VehiculoxMulta_Async Get_UbicacionxMulta_Async
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
           
        }
        private async  void Abrir_Ubicacion_Multa(object sender, SelectedItemChangedEventArgs e)
        {
            double lat = Convert.ToDouble(((MultasRealizadas)e.SelectedItem).latitud_ubicacion);
            double lon = Convert.ToDouble(((MultasRealizadas)e.SelectedItem).longitud_ubicacion);
            await Map.OpenAsync(lat, lon, new MapLaunchOptions
            {
                Name = ((MultasRealizadas)e.SelectedItem).lugar_multa,
                NavigationMode = NavigationMode.Default

            });


        }

        private async void ActualizarLista(object sender, EventArgs e)
        {
             Cargar_Multas_Realizadas();
        }
    }
}