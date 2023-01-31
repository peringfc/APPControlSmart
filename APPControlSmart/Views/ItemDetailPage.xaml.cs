using APPControlSmart.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace APPControlSmart.Views
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