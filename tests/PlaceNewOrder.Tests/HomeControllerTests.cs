using Microsoft.AspNetCore.Mvc;
using PlaceNewOrder.Controllers;
using PlaceNewOrder.Models;
using System;
using Xunit;
using ExpectedObjects;

namespace PlaceNewOrder.Tests
{
    public class HomeControllerTests
    {
        private readonly HomeController _target;

        public HomeControllerTests()
        {
            _target = new HomeController();
        }

        [Fact]
        public void DoitRetournerSurLaPageIndex()
        { 
            var order = new PlaceOrderViewModel
            {
                Nom = "Minet",
                Prenom = "Gros",
                Adresse = "Sous la cage de Titi",
                Courriel = "gros.minet@ilovebirds.com",
                Produit = "pince à oiseaux",
                Quantite = 1
            };

            var result = _target.PlaceOrder(order) as ViewResult;

            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public void NeDoitPasRetournerDeModeleEnCasDeSucces()
        { 
            var order = new PlaceOrderViewModel
            {
                Nom = "Minet",
                Prenom = "Gros",
                Adresse = "Sous la cage de Titi",
                Courriel = "gros.minet@ilovebirds.com",
                Produit = "pince à oiseaux",
                Quantite = 1
            };

            var result = _target.PlaceOrder(order) as ViewResult;

            Assert.Null(result.Model);
        }

        [Fact]
        public void DoitRetournerLeModeleEnCasDeNomAbsent()
        { 
            var order = new PlaceOrderViewModel
            {
                Prenom = "Gros",
                Adresse = "Sous la cage de Titi",
                Courriel = "gros.minet@ilovebirds.com",
                Produit = "pince à oiseaux",
                Quantite = 1
            };

            var result = _target.PlaceOrder(order) as ViewResult;

            Assert.Equal("Index", result.ViewName);
            Assert.NotNull(result.Model);
            Assert.IsAssignableFrom<PlaceOrderViewModel>(result.Model);

            var resultExpected = ((PlaceOrderViewModel)result.Model).ToExpectedObject();
            resultExpected.ShouldEqual(order);
        }
    }
}
