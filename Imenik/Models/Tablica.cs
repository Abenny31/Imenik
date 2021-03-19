using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Imenik.Models
{
    public class Tablica
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(20)]
        public string Ime { get; set; }
        [Required]
        [MaxLength(30)]
        public string Prezime { get; set; }
        [Required]
        public string Broj { get; set; }
        [Required]
        [MaxLength(30)]
        public string Adresa { get; set; }
    }
}
