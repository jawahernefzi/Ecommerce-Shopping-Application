using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Services
{
    public class Panier
    {
        public List<ArticlePanier> Articles { get; set; }

        public Panier()
        {
            Articles = new List<ArticlePanier>();
        }

        public void AjouterArticle(int idProduit, string nomProduit, decimal prixUnitaire, int quantite)
        {
            // Vérifier si l'article est déjà dans le panier
            var articleExist = Articles.FirstOrDefault(a => a.IdProduit == idProduit);

            if (articleExist != null)
            {
                // L'article existe déjà, mettre à jour la quantité
                articleExist.Quantite += quantite;
            }
            else
            {
                // Ajouter un nouvel article au panier
                Articles.Add(new ArticlePanier
                {
                    IdProduit = idProduit,
                    NomProduit = nomProduit,
                    PrixUnitaire = prixUnitaire,
                    Quantite = quantite
                });
            }
        }

        public void RetirerArticle(int idProduit)
        {
            // Retirer l'article du panier
            var article = Articles.FirstOrDefault(a => a.IdProduit == idProduit);

            if (article != null)
            {
                Articles.Remove(article);
            }
        }

        public decimal CalculerTotal(List<ArticlePanier> articles)
        {
            // Calculer le total du panier
            return articles.Sum(article => article.PrixUnitaire * article.Quantite);
        }


        public void ViderPanier()
        {
            // Vider le panier
            Articles.Clear();
        }

        public void IncrementQuantity(int idProduit)
        {
            var article = Articles.FirstOrDefault(a => a.IdProduit == idProduit);
            if (article != null)
            {
                article.Quantite++;
            }
        }

        public void DecrementQuantity(int idProduit)
        {
            var article = Articles.FirstOrDefault(a => a.IdProduit == idProduit);
            if (article != null && article.Quantite > 1)
            {
                article.Quantite--;
            }
        }
    }
}
