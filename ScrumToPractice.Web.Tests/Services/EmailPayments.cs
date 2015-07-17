using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Service;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Web.Tests.Services
{
    [TestClass]
    public class EmailPayments
    {
        [TestMethod]
        public void SendMailPayment()
        {
            // Arrange
            var sendEmail = new EmailPayment();
            var payment = new Payment();

            // Act
            var status = sendEmail.EnviarEmail(payment);

            // Assert
            Assert.AreEqual(EmailPayment.StatusEmail.Enviado, status);
        }
    }
}
