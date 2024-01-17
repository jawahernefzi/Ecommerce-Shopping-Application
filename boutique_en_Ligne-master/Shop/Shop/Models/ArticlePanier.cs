using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Models
{
    public class ArticlePanier
    {
        public int IdProduit { get; set; }
        public string NomProduit { get; set; }
        public decimal PrixUnitaire { get; set; }
        public int Quantite { get; set; }
        public decimal Total => PrixUnitaire * Quantite;
    }
}
