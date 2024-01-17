using Shop.Models;
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
    public partial class Products : ContentPage
    {
        readonly UserProductsPage  _viewModel;


        public Products(Categorie SelectedItem)
        {
            InitializeComponent();
            BindingContext = _viewModel = new UserProductsPage(SelectedItem);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }




        private void OnProductSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Produit selectedProduct)
            {
                // Call the ProductSelected method in the ViewModel
                ((UserProductsPage)BindingContext).OnProductSelected(selectedProduct);

                // Clear the selection to allow selecting the same item again
                ((CollectionView)sender).SelectedItem = null;
            }
        }

    }
}