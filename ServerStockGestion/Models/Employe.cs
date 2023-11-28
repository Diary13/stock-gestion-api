using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServerStockGestion
{
    [Table("Employes")]
    public class Employe
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Le nom est trop long")]
        public string Nom { get; set; }
        [Required, MaxLength(40, ErrorMessage = "Le prénom est trop long")]
        public string Prenom { get; set; }
        [Required, MaxLength(60)]
        public string Adresse { get; set; }
        [Required, MaxLength(10)]
        public string Tel { get; set;}
        [Required, DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        [Required, DataType(DataType.Password)]
        public string MDP { get; set; }
    }
}