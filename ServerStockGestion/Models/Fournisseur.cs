using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServerStockGestion
{
    [Table("Fournisseurs")]
    public class Fournisseur
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Le nom est trop long")]
        public string Nom { get; set; }
        [Required, MaxLength(80, ErrorMessage = "La localité est trop longue")]
        public string Localite { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        [Required, MaxLength(10)]
        public string Tel { get; set; }
    }
}