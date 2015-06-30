using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Service;
using ScrumToPractice.Domain.Abstract;
using System.Linq;

namespace ScrumToPractice.Web.Tests.Services
{
    [TestClass]
    public class Cortesias
    {
        [TestMethod]
        public void ObterCortesia()
        {
            // Arrange
            ICortesia cortesia;
            cortesia = new CortesiaService();

            // Act
            var simulado = cortesia.GetSimulado();

            // Assert
            Assert.IsTrue(simulado.Questoes.Count() > 0);
        }
    }
}
