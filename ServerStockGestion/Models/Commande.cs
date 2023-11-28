using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServerStockGestion
{
    [Table("Commandes")]
    public class Commande
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int QuantiteCom { get; set; }
        [Required]
        public DateTime DateCom { get; set; }
        [Required]
        public int IdProd{ get; set; }
        [ForeignKey("IdProd")]
        public Produit Prod { get; set; }
    }
}