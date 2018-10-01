using System;
using System.ComponentModel.DataAnnotations;

namespace PlaceNewOrder.Models
{
    public class PlaceOrderViewModel
    {
        public Guid CustomerId  { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        public string Courriel { get; set; }
        [Required]
        public string Produit { get; set; }
        [Required]
        public int Quantite { get; set; }
    }
}