using System.Windows.Input;
using Xamarin.Forms;
using Shop.Services;
using Shop.Models;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.ViewModels
{
    public class PanierViewModel : BaseViewModel
    {
        private ObservableCollection<ArticlePanier> _articlesPanier;
        private ArticlePanier _selectedItem;
        public decimal _total;

        public decimal Total
        {
            get { return _total; }
            set { SetProperty(ref _total, value); }
        }
        public ArticlePanier SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        public ObservableCollection<ArticlePanier> Articles
        {
            get
            {
                return _articlesPanier;
            }
            set
            {
                _articlesPanier = value;
                OnPropertyChanged(nameof(Articles));
                // Whenever the Articles collection changes, recalculate the total
                CalculerTotal();
            }
        }

        private Panier _panier;

        public Panier Panier
        {
            get { return _panier; }

        }
        public ICommand RetirerArticleCommand { get; }
        public ICommand PasserCommandeCommand { get; }
        public ICommand ViderPanierCommand { get; }

        public PanierViewModel()
        {

            _panier = App.shoppingCart;  // Ensure that App.shoppingCart is initialized
            Console.WriteLine($"PanierViewModel created. Panier count: {_panier.Articles.Count}");

            _articlesPanier = new ObservableCollection<ArticlePanier>(_panier?.Articles ?? new List<ArticlePanier>());
            CalculerTotal();

            RetirerArticleCommand = new Command<int>(RetirerArticle);
            PasserCommandeCommand = new Command(PasserCommande);
            ViderPanierCommand = new Command(ViderPanier);
            IncrementQuantityCommand = new Command<int>(IncrementQuantity);
            DecrementQuantityCommand = new Command<int>(DecrementQuantity);
            CalculerTotalCommand = new Command(CalculerTotal);
        }



        // Commandes liées aux méthodes du Panier
        public ICommand IncrementQuantityCommand { get; }
        public ICommand DecrementQuantityCommand { get; }
        public ICommand CalculerTotalCommand { get; }
        private void RetirerArticle(int idProduit)
        {
            App.shoppingCart.RetirerArticle(idProduit);
            RefreshPanier();
        }
        private async void PasserCommande()
        {
            // Prompt the user for their name
            string customerName = await Application.Current.MainPage.DisplayPromptAsync("Confirmation", "Please write your name:");

            if (!string.IsNullOrEmpty(customerName))
            {
                // User entered a name, proceed with adding the command
                Console.WriteLine($"Commande confirmed by {customerName}");
                AjouterCommande(customerName);
                ViderPanier();  // This line should clear the panier
            }
            else
            {
                // User canceled or entered an empty name
                Console.WriteLine("Commande canceled");
            }
        }










        private void ViderPanier()
        {
            Console.WriteLine("ViderPanier method called."); // Add debugging output
            Articles = new ObservableCollection<ArticlePanier>();

        }
        // Actualise la liste d'articles après chaque modification
        private void RefreshPanier()
        {
            Articles = new ObservableCollection<ArticlePanier>(_panier.Articles);
            OnPropertyChanged(nameof(Articles));
        }
        private void IncrementQuantity(int idProduit)
        {
            _panier.IncrementQuantity(idProduit);
            RefreshPanier();
        }

        private void DecrementQuantity(int idProduit)
        {
            _panier.DecrementQuantity(idProduit);
            RefreshPanier();
        }

        private void CalculerTotal()
        {
            Total = _panier.CalculerTotal(Articles.ToList());
            Console.WriteLine($"Total du panier: {Total:C}");
            Console.WriteLine($"CalculerTotal called. Articles count: {Articles.Count}");

            OnPropertyChanged(nameof(Total));
        }
        private void AjouterCommande(String name)
        {

            if (name != null)
            {
                // Créer une nouvelle commande à partir des articles dans le panier
                Commande nouvelleCommande = new Commande();
                nouvelleCommande.NomClient = name;

                // Ajouter la commande à la base de données pour obtenir l'Id auto-incrémenté
                App.mydataBase.AjouterCommande(nouvelleCommande);

                // Créer une liste pour les lignes de commande
                List<LigneCommande> lignesCommande = new List<LigneCommande>();

                // Obtenez l'Id de la commande fraîchement ajoutée
                int commandeId = nouvelleCommande.Id;

                // Ajouter les lignes de commande associées à la commande
                foreach (var article in Panier.Articles)
                {
                    LigneCommande nouvelleLigneCommande = new LigneCommande
                    {
                        IdProduit = article.IdProduit,
                        Quantite = article.Quantite,
                        IdCommande = commandeId
                    };

                    // Ajoutez la ligne de commande à la liste
                    lignesCommande.Add(nouvelleLigneCommande);

                    try
                    {
                        // Sauvegardez la ligneCommande dans la base de données
                        App.mydataBase.AjouterLigneCommande(nouvelleLigneCommande);
                        Console.WriteLine(App.mydataBase.ObtenirLesLignesCommande(commandeId));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }

                // Assigner la liste de lignes de commande à la nouvelle commande
                nouvelleCommande.LignesCommande = lignesCommande;

                // Mettez à jour la commande dans la base de données
                App.mydataBase.modifierCommande(nouvelleCommande);

                Console.WriteLine($"Commande added for {name}");
                Console.WriteLine($"Loaded ligne {nouvelleCommande.LignesCommande[0].Quantite}");
                ViderPanier();  // This line should clear the panier

            }
            else
            {
                Console.WriteLine("Error: Customer name is null.");
            }
        }













    }




















}


