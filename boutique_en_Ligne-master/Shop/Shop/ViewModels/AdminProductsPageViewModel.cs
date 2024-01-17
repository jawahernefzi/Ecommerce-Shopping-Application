using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shop.Models;
using Shop.Views;
using Xamarin.Forms;

namespace Shop.ViewModels
{
    public class AdminProductsPageViewModel : BaseViewModel
    {
        private ObservableCollection<Produit> _products;
        private Produit _selectedItem;

        public Produit SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public ObservableCollection<Produit> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        public Command<Produit> DeleteProductCommand { get; }
        public Command AddProductCommand { get; }
        public Command EditProductCommand { get; }

        public AdminProductsPageViewModel()
        {
            DeleteProductCommand = new Command<Produit>(OnDeleteProduct);
            AddProductCommand = new Command(OnAddProduct);
            EditProductCommand = new Command<Produit>(OnEditProduct);
        }

        private void LoadProducts()
        {
            List<Produit> products = App.mydataBase.ObtenirProduits();

            // Verify that 'Nom' property is correctly set in each Produit object
            foreach (var product in products)
            {
                Console.WriteLine($"Product Name: {product.Nom}");
            }

            Products = new ObservableCollection<Produit>(products);
        }

        public void OnAppearing()
        {
            LoadProducts();
        }

        private async void OnDeleteProduct(Produit selectedProduct)
        {
            if (selectedProduct != null)
            {
                bool result = await Application.Current.MainPage.DisplayAlert("Supprimer le produit", $"Êtes-vous sûr de vouloir supprimer le produit {selectedProduct.Nom} ?", "Oui", "Non");

                if (result)
                {
                    App.mydataBase.SupprimerProduit(selectedProduct.Id);
                    LoadProducts();

                    await Application.Current.MainPage.DisplayAlert("Produit supprimé", "Le produit a été supprimé avec succès.", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Sélectionnez un produit", "Veuillez sélectionner un produit avant de supprimer.", "OK");
            }
        }

        private async void OnAddProduct()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditProduct(null));
        }

        private async void OnEditProduct(Produit selectedProduct)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditProduct(selectedProduct));
        }
    }
}
