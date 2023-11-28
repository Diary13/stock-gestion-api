using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServerStockGestion
{
    [Table("Produits")]
    public class Produit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Libelle { get; set; }
        [Required]
        public int PrixUnitaire { get; set; }
        [Required]
        public int Quantite { get; set; }
        public string Image { get; set; }
        public int IdFour { get; set; }
        [ForeignKey("IdFour")]
        public Fournisseur Four { get; set; }
    }
}