using Shop.Models;
using Xamarin.Forms;

namespace Shop.ViewModels
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        private Produit _product;

        public Produit Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        public Command AddToCartCommand { get; }
        public ProductDetailsViewModel()
        {

        }
        public ProductDetailsViewModel(Produit selectedProduct)
        {
            Product = selectedProduct;

            // Command to add the product to the shopping cart
            AddToCartCommand = new Command(AjouterAuPanier);
        }

        private void AjouterAuPanier()
        {
            // Implement the logic to add the product to the shopping cart
            if (Product != null)
            {
                App.ShoppingCart.AjouterArticle(Product.Id, Product.Nom, Product.Prix, 1);
                AfficherMessage("Produit ajouté au panier");
            }
            else
            {
                AfficherMessage("Erreur : Impossible d'ajouter le produit au panier");
            }
        }

        private void AfficherMessage(string message)
        {
            // Use Xamarin.Forms' DisplayAlert to show a pop-up message
            Application.Current.MainPage.DisplayAlert("Information", message, "OK");
        }
    }
}
