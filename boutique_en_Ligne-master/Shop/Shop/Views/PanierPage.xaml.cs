using Xamarin.Forms;
using Shop.ViewModels;

namespace Shop.Views
{
    public partial class PanierPage : ContentPage
    {
        public PanierPage()
        {
            InitializeComponent();
            BindingContext = new PanierViewModel();
        }
    }
}
