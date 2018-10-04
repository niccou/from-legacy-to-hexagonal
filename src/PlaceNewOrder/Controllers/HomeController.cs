using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlaceNewOrder.DataAccess;
using PlaceNewOrder.Models;

namespace PlaceNewOrder.Controllers
{
    public class HomeController : Controller
    {
        private CustomerRepository _customerRepository;
        private OrderRepository _orderRepository;

        public HomeController()
        {
            _customerRepository = new CustomerRepository();
            _orderRepository = new OrderRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlaceOrder(PlaceOrderViewModel newOrder)
        {
            // -- If this is a new customer, create the customer record --
            if (string.IsNullOrEmpty(newOrder.Nom))
            {
                ModelState.AddModelError(nameof(newOrder.Nom), "Le nom ne peut être vide.");
                return View("Index", newOrder);
            }

            //var crepo = new CustomerRepository();

            Customer c;

            if (newOrder.CustomerId != Guid.Empty)
            {
                c = _customerRepository.GetCustomerById(newOrder.CustomerId);
            }
            else
            {
                c = _customerRepository.GetCustomerByName(newOrder.Nom);
            }

            if (c == null)
            {
                var newCustomer = new Customer();
                newCustomer.Nom = newOrder.Nom;
                newCustomer.Prenom = newOrder.Nom;
                newCustomer.Courriel = newOrder.Nom;
                newCustomer.Adresse = newOrder.Nom;

                c = _customerRepository.SaveNewCustomer(newCustomer);
            }

            // -- Create the order for the customer. --
            if (string.IsNullOrEmpty(newOrder.Produit))
            {
                ModelState.AddModelError(nameof(newOrder.Produit), "Le produit ne peut être vide.");
                return View("Index", newOrder);
            }

            if (newOrder.Quantite == 0)
            {
                ModelState.AddModelError(nameof(newOrder.Produit), "La quantité ne peut être inférieure à un.");
                return View("Index", newOrder);
            }
            
            //var orepo = new OrderRepository();

            Order o = _orderRepository.CreateNewOrder(c);

            o.Prod = newOrder.Produit;
            o.Qty = newOrder.Quantite;

            _orderRepository.Save(o);

            return View(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
