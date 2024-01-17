using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
    public class LigneCommande
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Produit))]
        public int IdProduit { get; set; }

        public int Quantite { get; set; }
        [ForeignKey(typeof(Commande))]
        public int IdCommande { get; set; }

    }
}
