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
            var cliente = new Cliente { Email = "jb.alessandro@gmail.com", ExpiraEm = DateTime.Today.Date.AddMonths(1).AddDays(1) };

            // Act
            var status = sendEmail.EnviarEmail(cliente);

            // Assert
            Assert.AreEqual(EmailPayment.StatusEmail.Enviado, status);
        }
    }
}
