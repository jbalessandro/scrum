using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System.Linq;
using System.Data.Entity;

namespace ScrumToPractice.Web.Tests.Services
{
    [TestClass]
    public class Parametros
    {
        [TestMethod]
        public void IncluirParametro()
        {
            // Arrange
            var parametro = new Parametro
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = 1,
                Codigo = "CORTESIA_QUESTOES",
                Valor = "10"
            };

            var service = new ParametroService();

            // Act
            parametro.Id = service.Gravar(parametro);

            // Assert
            Assert.AreEqual(1, parametro.Id);
        }
    }
}
