using LenguajesIV_ProjectoFinal.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace LenguajesIV_ProjectoFinal.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}