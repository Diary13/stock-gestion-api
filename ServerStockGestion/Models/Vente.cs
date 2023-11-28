using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServerStockGestion
{
    [Table("Ventes")]
    public class Vente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantite { get; set; }
        //mba ialana @ilay mametaka 0 dia tsy natao required
        public int IdFact { get; set; }
        [ForeignKey("IdFact")]
        public Facture Fact { get; set; }
        [Required]
        public int IdProd { get; set; }
        [ForeignKey("IdProd")]
        public Produit Prod { get; set; }
    }
}