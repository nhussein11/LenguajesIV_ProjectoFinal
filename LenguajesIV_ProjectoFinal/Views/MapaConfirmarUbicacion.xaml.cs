using LenguajesIV_ProjectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace LenguajesIV_ProjectoFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapaConfirmarUbicacion : ContentPage
    {
        public MapaConfirmarUbicacion()
        {
            InitializeComponent();

            double lat = Convert.ToDouble(Application.Current.Properties["latitud"]);
            double lon = Convert.ToDouble(Application.Current.Properties["longitud"]);
            var map = new Map(MapSpan.FromCenterAndRadius(
                new Position(lat, lon),
                Distance.FromMiles(0.5)))
            {
                IsShowingUser = true,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            Pin ubicacion = new Pin
            {
                Type = PinType.Place,
                Position = new Position(lat, lon),
                Label = "Tu ubicacion",
                Address = ((Multas)Application.Current.Properties["Multa"]).lugar_multa

            };
            map.Pins.Add(ubicacion);
            
        }
    }
}