using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Service;

namespace ScrumToPractice.Web.Tests.Services
{
    [TestClass]
    public class Precos
    {
        IPreco preco;

        public Precos()
        {
            preco = new PaypalPreco();
        }

        [TestMethod]
        public void SetPreco()
        {
            // Arrange
            decimal valormensal = 30M;

            // Act
            preco.SetPrecoMensal(valormensal, 1);

            // Assert
            Assert.AreEqual(30M, preco.GetPrecoMensal());            
        }

        [TestMethod]
        public void GetPreco()
        {
            // Arrange
            decimal valorMensal;

            // Act
            valorMensal = preco.GetPrecoMensal();

            // Assert
            Assert.AreEqual(30, valorMensal);
        }
    }
}
