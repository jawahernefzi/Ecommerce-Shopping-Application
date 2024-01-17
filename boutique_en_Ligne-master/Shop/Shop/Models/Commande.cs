using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
    public class Commande
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string NomClient { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<LigneCommande> LignesCommande { get; set; }
    }
}
