using Xunit;
using Moq;
using PlaceNewOrder.Controllers;
using PlaceNewOrder.DataAccess;
using PlaceNewOrder.Models;
using System;

namespace PlaceNewOrder.Tests
{
    public class PlaceOrderTests
    {
        private readonly HomeController _target;
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly PlaceOrderViewModel _newOrderWithCustomerId;
        private readonly PlaceOrderViewModel _newOrderWithoutCustomerId;
        private readonly Guid _customerId;

        public PlaceOrderTests()
        {
            _customerId = Guid.NewGuid();
            _newOrderWithCustomerId = new PlaceOrderViewModel
            {
                CustomerId = _customerId,
                Nom = "Minet",
                Prenom = "Gros",
                Adresse = "Sous la cage de Titi",
                Courriel = "gros.minet@ilovebirds.com",
                Produit = "pince � oiseaux",
                Quantite = 1
            };

            _newOrderWithoutCustomerId = new PlaceOrderViewModel
            {
                CustomerId = Guid.Empty,
                Nom = "Minet",
                Prenom = "Gros",
                Adresse = "Sous la cage de Titi",
                Courriel = "gros.minet@ilovebirds.com",
                Produit = "pince � oiseaux",
                Quantite = 1
            };

            _customerRepository = new Mock<ICustomerRepository>();
            _orderRepository = new Mock<IOrderRepository>();
            _target = new HomeController(_customerRepository.Object, _orderRepository.Object);
        }

        [Fact]
        public void SearchCustommerById()
        {
            //Given
            var customer = new Customer
            {
                Id = _customerId,
                Lastname = nameof(Customer.Lastname),
                Firstname = nameof(Customer.Firstname),
                Email = nameof(Customer.Email),
                Address = nameof(Customer.Address)
            };

            _customerRepository.Setup(m => m.GetCustomerById(_customerId)).Returns(customer).Verifiable($"{nameof(ICustomerRepository.GetCustomerById)} not called.");
            _orderRepository.Setup(m => m.CreateNewOrder(customer)).Returns(new Order());

            //When
            _target.PlaceOrder(_newOrderWithCustomerId);

            //Then
            AssertCustomerByIdCalledOnce();
            _customerRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public void SearchCustommerByNameIfNoCustomerId()
        {
            //Given
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Lastname = nameof(Customer.Lastname),
                Firstname = nameof(Customer.Firstname),
                Email = nameof(Customer.Email),
                Address = nameof(Customer.Address)
            };

            _customerRepository.Setup(m => m.GetCustomerByName("Minet")).Returns(customer).Verifiable($"{nameof(ICustomerRepository.GetCustomerById)} not called.");
            _orderRepository.Setup(m => m.CreateNewOrder(customer)).Returns(new Order());

            //When
            _target.PlaceOrder(_newOrderWithoutCustomerId);

            //Then
            AssertCustomerByNameCalledOnce();
            _customerRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public void CreateCustomerIfNotFoundById()
        {
            //Given
            _customerRepository.Setup(m => m.GetCustomerById(_customerId)).Returns<Customer>(null);
            _customerRepository.Setup(m => m.SaveNewCustomer(It.Is<Customer>(c =>
                    c.Lastname == _newOrderWithCustomerId.Nom &&
                    c.Firstname == _newOrderWithCustomerId.Prenom &&
                    c.Address == _newOrderWithCustomerId.Adresse &&
                    c.Email == _newOrderWithCustomerId.Courriel
                )))
                .Returns((Customer c) =>
                {
                    c.Id = Guid.NewGuid();
                    return c;
                }).Verifiable();
            _orderRepository.Setup(m => m.CreateNewOrder(It.IsAny<Customer>())).Returns(new Order());

            //When
            _target.PlaceOrder(_newOrderWithCustomerId);

            //Then
            AssertCustomerByIdCalledOnce();
            AssertSaveNewCustomerCalledOnce();
            _customerRepository.VerifyNoOtherCalls();
        }

        private void AssertSaveNewCustomerCalledOnce()
        {
            _customerRepository.Verify(m => m.SaveNewCustomer(It.IsAny<Customer>()), Times.Once());
        }

        [Fact]
        public void CreateCustomerIfNotFoundByName()
        {
            //Given
            _customerRepository.Setup(m => m.GetCustomerByName("Minet")).Returns<Customer>(null);
            _customerRepository.Setup(m => m.SaveNewCustomer(It.Is<Customer>(c =>
                    c.Lastname == _newOrderWithoutCustomerId.Nom &&
                    c.Firstname == _newOrderWithoutCustomerId.Prenom &&
                    c.Address == _newOrderWithoutCustomerId.Adresse &&
                    c.Email == _newOrderWithoutCustomerId.Courriel
                )))
                .Returns((Customer c) =>
                {
                    c.Id = Guid.NewGuid();
                    return c;
                }).Verifiable();
            _orderRepository.Setup(m => m.CreateNewOrder(It.IsAny<Customer>())).Returns(new Order());

            //When
            _target.PlaceOrder(_newOrderWithoutCustomerId);

            //Then
            AssertCustomerByNameCalledOnce();
            AssertSaveNewCustomerCalledOnce();
            _customerRepository.VerifyNoOtherCalls();
        }

        private void AssertCustomerByNameCalledOnce()
        {
            _customerRepository.Verify(m => m.GetCustomerByName(It.IsAny<string>()), Times.Once());
        }

        private void AssertCustomerByIdCalledOnce()
        {
            _customerRepository.Verify(m => m.GetCustomerById(It.IsAny<Guid>()), Times.Once());
        }

    }
}