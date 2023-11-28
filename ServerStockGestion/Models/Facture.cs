using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServerStockGestion
{
    [Table("Factures")]
    public class Facture
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateFact { get; set; }
    }
}