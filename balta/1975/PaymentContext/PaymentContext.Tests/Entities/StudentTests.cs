using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {

        private readonly Student _student;

        private readonly Name _name;
        private readonly Document _doc;
        private readonly Address _address;
        private readonly Email _email;

        public StudentTests()
        {
            _name = new Name("First", "Last");
            _doc = new Document("11111111111", EDocumentType.CPF);
            _email = new Email("asdasd@asd.asd");
            _address = new Address("Rua 1", "11", "asd", "city", "sp", "br", "12312312");
            _student = new Student(_name, _doc, _email);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenActiveSubscription()
        {
            var payment = new PayPalPayment("1232133",
                                            DateTime.Now,
                                            DateTime.Now.AddDays(5),
                                            10,
                                            10,
                                            "Corp",
                                            _doc,
                                            _address,
                                            _email);   
            var subscription = new Subscription(null);
            subscription.AddPayment(payment);

            _student.AddSubscription(subscription);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            var subscription = new Subscription(null);
            _student.AddSubscription(subscription);
            
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12333",
                                            DateTime.Now,
                                            DateTime.Now.AddDays(5),
                                            10,
                                            10,
                                            "Corp",
                                            _doc,
                                            _address,
                                            _email);            
            subscription.AddPayment(payment);

            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}