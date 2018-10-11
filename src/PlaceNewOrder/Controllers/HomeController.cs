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
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;

        public HomeController(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public HomeController() : this(new CustomerRepository(), new OrderRepository()) { }

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

            var orderCustomer = new OrderCustomer
            {
                Id = newOrder.CustomerId,
                Nom = newOrder.Nom,
                Prenom = newOrder.Prenom,
                Courriel = newOrder.Courriel,
                Adresse = newOrder.Adresse,
            };

            Customer customer = GetOrCreateCustomer(orderCustomer);

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

            SaveOrder(newOrder, customer);

            return View(nameof(Index));
        }

        private void SaveOrder(PlaceOrderViewModel newOrder, Customer customer)
        {
            Order order = _orderRepository.CreateNewOrder(customer);

            order.Prod = newOrder.Produit;
            order.Qty = newOrder.Quantite;

            _orderRepository.Save(order);
        }

        private Customer GetOrCreateCustomer(OrderCustomer orderCustomer)
        {
            Customer customer;

            if (orderCustomer.Id != Guid.Empty)
            {
                customer = _customerRepository.GetCustomerById(orderCustomer.Id);
            }
            else
            {
                customer = _customerRepository.GetCustomerByName(orderCustomer.Nom);
            }

            if (customer == null)
            {
                var newCustomer = new Customer();
                newCustomer.Lastname = orderCustomer.Nom;
                newCustomer.Firstname = orderCustomer.Prenom;
                newCustomer.Email = orderCustomer.Courriel;
                newCustomer.Address = orderCustomer.Adresse;

                customer = _customerRepository.SaveNewCustomer(newCustomer);
            }

            return customer;
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
