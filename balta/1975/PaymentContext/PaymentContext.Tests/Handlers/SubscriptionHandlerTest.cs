using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTest
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(),
                                                  new FakeEmailService());

            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "dafsadf";
            command.LastName  = "asdfsadfasdf";
            command.Address = "sdfasdfs";
            command.Document = "99999999999";
            command.BarCode = "13123123123";
            command.BoletoNumber = "123123123";
            command.PaymentNumber = "12434131";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "dfsadfsdf";
            command.PayerDocument = "123123123123";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "email@email.com";
            command.Street = "sdfasdf";
            command.Number = "13123123";
            command.Neighborhood = "213123";
            command.City = "sdfasdfa";
            command.State = "as";
            command.Country = "safsadf";
            command.ZipCode = "13234-112";

            handler.Handler(command);

            Assert.AreEqual(false, handler.Valid);
        }

    }
}