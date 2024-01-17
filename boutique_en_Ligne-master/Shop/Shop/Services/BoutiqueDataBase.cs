using Shop.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Services
{
    public class BoutiqueDataBase
    {
        private readonly SQLiteConnection _baseDeDonnees;

        public BoutiqueDataBase(string cheminBaseDeDonnees)
        {
            _baseDeDonnees = new SQLiteConnection(cheminBaseDeDonnees);
            _baseDeDonnees.CreateTable<Categorie>();
            _baseDeDonnees.CreateTable<Produit>();
            _baseDeDonnees.CreateTable<LigneCommande>();
            _baseDeDonnees.CreateTable<Commande>();

        }



        // Public method to get the instance of the database

        // Opérations sur les catégories
        public List<Categorie> ObtenirCategories()
        {
            return _baseDeDonnees.Table<Categorie>().ToList();
        }
        // Opérations sur les produits par catégorie
        public List<Produit> ObtenirProduitsParCategorie(Categorie c)
        {
            var produits = _baseDeDonnees.Table<Produit>().Where(p => p.IdCategorie == c.Id).ToList();
            return produits;
        }

        public void AjouterCategorie(Categorie categorie)
        {
            _baseDeDonnees.Insert(categorie);
        }

        public void ModifierCategorie(Categorie categorie)
        {
            // Mettre à jour la catégorie
            _baseDeDonnees.Update(categorie);

            
        }


        public void SupprimerCategorie(int idCategorie)
        {
            // Récupérer la catégorie
            var categorie = _baseDeDonnees.GetWithChildren<Categorie>(idCategorie, recursive: true);

            // Supprimer les produits associés
            foreach (var produit in categorie.produits)
            {
                _baseDeDonnees.Delete(produit);
            }

            // Supprimer la catégorie elle-même
            _baseDeDonnees.Delete(categorie);
        }



        // Opérations sur les produits
        public List<Produit> ObtenirProduits()
        {
            return _baseDeDonnees.Table<Produit>().ToList();
        }

        public void AjouterProduit(Produit produit)
        {
            _baseDeDonnees.Insert(produit);
        }

        public void ModifierProduit(Produit produit)
        {
            // Mettre à jour le produit
            _baseDeDonnees.Update(produit);

            // Mettre à jour la référence de catégorie dans le produit
            var categorie = _baseDeDonnees.Find<Categorie>(produit.IdCategorie);
            if (categorie != null)
            {
                produit.IdCategorie = categorie.Id;
                _baseDeDonnees.Update(produit);
            }
        }

        public void SupprimerProduit(int idProduit)
        {
            _baseDeDonnees.Delete<Produit>(idProduit);
        }

        public void modifierCommande(Commande c)
        {
            _baseDeDonnees.Update(c);
        }
        // Opérations sur les lignes de commande
        public void AjouterLigneCommande(LigneCommande ligneCommande)
        {
            _baseDeDonnees.Insert(ligneCommande);
        }

        public List<LigneCommande> ObtenirLesLignesCommande(int id)
        {
            var lignes = _baseDeDonnees.Table<LigneCommande>().Where(p => p.IdCommande == id).ToList();
            return lignes;
        }
       



        public List<Commande> ObtenirToutesLesCommandes()
        {
            return _baseDeDonnees.Table<Commande>().ToList();
        }

        // Add this method to your BoutiqueDataBase class
        public Commande ObtenirCommandeParId(int idCommande)
        {
            return _baseDeDonnees.Find<Commande>(idCommande);
        }


        // Opérations sur les commandes
        public void AjouterCommande(Commande commande)
        {
            _baseDeDonnees.Insert(commande);
        }



    }
}
