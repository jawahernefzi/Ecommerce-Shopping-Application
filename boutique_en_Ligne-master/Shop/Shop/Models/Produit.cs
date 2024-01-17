using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
    public class Produit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Description { get; set; }

        public decimal Prix { get; set; }

        public byte[] UrlImage { get; set; }


        [ForeignKey(typeof(Categorie))]
        public int IdCategorie { get; set; }

    }
}
