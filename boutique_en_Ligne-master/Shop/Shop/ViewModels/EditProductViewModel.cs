using Shop.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Shop.ViewModels
{
    // ViewModel for UpdateProductPage
    public class EditProductViewModel : BaseViewModel
    {
        private Produit _product;

        public Produit Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        private ObservableCollection<Categorie> _categories;

        public ObservableCollection<Categorie> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        private Categorie _selectedCategory;

        public Categorie SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public ICommand UpdateCommand { get; }

        public EditProductViewModel(Produit product)
        {
            Product = product??new Produit();
            LoadCategories();

            // Initialize your commands
            UpdateCommand = new Command(OnUpdateCommand);


        }

        private void LoadCategories()
        {
            // Load categories from the database
            Categories = new ObservableCollection<Categorie>(App.mydataBase.ObtenirCategories());

            // Set the selected category of the product
            if (Product.IdCategorie != 0)
            {
                SelectedCategory = Categories.FirstOrDefault(c => c.Id == Product.IdCategorie);
            }
        }



        private async Task<byte[]> ConvertStreamToByteArray(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        private void OnUpdateCommand()
        {
            try
            {
                if(Product.Id==0)
                {
                    Product.IdCategorie = SelectedCategory.Id;
                    App.mydataBase.AjouterProduit(Product);

                }
                else
                {
                    Product.IdCategorie = SelectedCategory.Id;

                    // Save changes to the database
                    App.mydataBase.ModifierProduit(Product);

                }
                

                // Navigate back to the previous page
                Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Handle exception appropriately
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
       

    }
}
