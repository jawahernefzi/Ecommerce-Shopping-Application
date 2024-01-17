using Xamarin.Essentials;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Shop.Models;
using Shop.ViewModels;

namespace Shop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProduct : ContentPage
    {
        EditProductViewModel viewModel;

        public EditProduct(Produit selectedProduct)
        {
            InitializeComponent();
            BindingContext = viewModel = new EditProductViewModel(selectedProduct);
            LoadExistingImage();
        }

        private void LoadExistingImage()
        {
            // Get the view model from the BindingContext
            if (viewModel.Product.UrlImage != null && viewModel.Product.UrlImage.Length > 0)
            {
                // Set the image control to display the existing image
                productImage.Source = ImageSource.FromStream(() => new MemoryStream(viewModel.Product.UrlImage));
            }
        }

        private async void OnSelectImageClicked(object sender, EventArgs e)
        {
            try
            {
                // Use Xamarin.Essentials to pick an image from the gallery
                var result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    // Update the image control with the selected image
                    productImage.Source = ImageSource.FromStream(() => result.OpenReadAsync().Result);

                    // Convert the selected image to a byte array and save it in the view model
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await result.OpenReadAsync().Result.CopyToAsync(ms);
                        viewModel.Product.UrlImage = ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., permission denied, canceled)
                // Log or display an error message
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
