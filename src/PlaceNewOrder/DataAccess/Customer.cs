using System;
namespace PlaceNewOrder.DataAccess
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Courriel { get; set; }
    }
}