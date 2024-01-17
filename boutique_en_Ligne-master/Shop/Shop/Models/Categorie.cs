using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
    public class Categorie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nom { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Produit> produits { get; set; }


    }
}
