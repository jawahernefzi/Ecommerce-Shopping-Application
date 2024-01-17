using Shop.Models;
using Shop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace Shop.ViewModels
{
    public class UserProductsPage : BaseViewModel
    {
        private Produit _selectedItem;
        private ObservableCollection<Produit> _products;
        private Categorie _selectedCategory;

        public ObservableCollection<Produit> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }
        public Categorie SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }
        public Produit SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public  void OnAppearing()
        {
            LoadProductsForSelectedCategory();
        }
        public Command ProductSelected;

        public UserProductsPage( Categorie SelectedItem)
        {
            SelectedCategory = SelectedItem;


            LoadProductsForSelectedCategory();
            AjouterAuPanierCommand = new Command<Produit>(AjouterAuPanier);

            ProductSelected = new Command<Produit>(OnProductSelected);
        }
        public UserProductsPage()
        {

        }
        private void LoadProductsForSelectedCategory()
        {
            if (SelectedCategory != null)
            {
                // Retrieve products based on the selected category ID
                List<Produit> products = App.mydataBase.ObtenirProduitsParCategorie(SelectedCategory);

                // Update the collection of products in your ViewModel
                Products = new ObservableCollection<Produit>(products);

                // Add some debug output
                Debug.WriteLine($"Loaded {Products.Count} products for category {SelectedCategory.Nom}");

            }
            OnPropertyChanged(nameof(Products));
            OnPropertyChanged(nameof(SelectedCategory));


        }
        public Command AjouterAuPanierCommand { get; }

        private void AjouterAuPanier(Produit p)
        {
            if (p != null)
            {
                App.ShoppingCart.AjouterArticle(p.Id, p.Nom, p.Prix, 1);
                Application.Current.MainPage.DisplayAlert("Information", "Product added to the cart ", "OK");

            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Information", " Problem in adding product to cart", "OK");
            }
        }

        
        public async void OnProductSelected(Produit selectedProduct)
        {
            if (selectedProduct != null)
            {
                // Navigate to the ProductDetailsPage with the selected product
                await Shell.Current.Navigation.PushAsync(new ProductDetailsPage(selectedProduct));
            }
        }



    }
}
