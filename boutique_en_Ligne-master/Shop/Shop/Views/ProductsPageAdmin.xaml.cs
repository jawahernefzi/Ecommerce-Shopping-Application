using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPageAdmin : ContentPage
    {
        private readonly AdminProductsPageViewModel _viewModel;

        public ProductsPageAdmin()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AdminProductsPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}