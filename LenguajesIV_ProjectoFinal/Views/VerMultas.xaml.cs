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
            ListadoMultasRealizadas = new List<MultasRealizadas>();
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
            
            List<Multas> lista_multas = await App.SQLiteDB.Get_MultasxAgente_Async(cod_agente_activo);
            foreach (Multas multa in lista_multas)
            {
                string nombre_agente = ((Agentes)Application.Current.Properties["DatosUsuario"]).nombre_agente;
                string dni_infractor = (await App.SQLiteDB.Get_InfractorxMulta_Async(multa.cod_infractor)).dni_infractor; //funciona bien
                string patente_dominio_vehiculo = (await App.SQLiteDB.Get_VehiculoxMulta_Async(multa.cod_vehiculo)).patente_dominio_vehiculo; //funciona bien
                string longitud_ubicacion = (await App.SQLiteDB.Get_UbicacionxMulta_Async(multa.cod_multa)).longitud_ubicacion; // aca esta fallando 
                string apellido_agente = ((Agentes)Application.Current.Properties["DatosUsuario"]).apellido_agente;
                string fecha_multa = multa.fecha_multa;
                string latitud_ubicacion = (await App.SQLiteDB.Get_UbicacionxMulta_Async(multa.cod_multa)).latitud_ubicacion; // aca esta fallando
                string lugar_multa = multa.lugar_multa;
                /*Falla en las ubicaciones, las estas grabando en la bd? con el cod de la multa? el error que tira es porque devuevle null y no sabe a donde ir a buscar la propiedad de la latitud/longitud*/
               MultasRealizadas multaRealizada = new MultasRealizadas
                {
                    nombre_agente = nombre_agente,
                    apellido_agente = apellido_agente,
                    fecha_multa = fecha_multa,
                    lugar_multa = lugar_multa,
                    dni_infractor = dni_infractor,
                    patente_dominio_vehiculo = patente_dominio_vehiculo,
                    latitud_ubicacion= latitud_ubicacion,
                    longitud_ubicacion = longitud_ubicacion
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

        public   async void ActualizarLista(object sender, EventArgs e)
        {
            Cargar_Multas_Realizadas();
            await Shell.Current.GoToAsync($"//{nameof(Perfil)}"); //old but gold truco de "actualizar" vista
            await Shell.Current.GoToAsync($"//{nameof(VerMultas)}");
            
            // esto desp lo ponemos en el constructor de la vista, por ahora hasta que lo arreglemos, queda en el boton 
        }
    }
}