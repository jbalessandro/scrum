using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System;

namespace ScrumToPractice.Web.Tests.Services
{
    [TestClass]
    public class Parametros
    {
        [TestMethod]
        public void IncluirParametro()
        {
            // Arrange
            var parametro1 = new Parametro
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = 1,
                Codigo = "NUM_QUESTOES_CORTESIA",
                Valor = "10"
            };

            var parametro2 = new Parametro
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = 1,
                Codigo = "CORTESIA_MANUTENCAO_DIAS",
                Valor = "30"
            };

            var parametro3 = new Parametro
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = 1,
                Codigo = "NOTA_MINIMA",
                Valor = "85"
            };

            var parametro4 = new Parametro
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = 1,
                Codigo = "PRAZO_ACESSO_PAGO",
                Valor = "1"
            };

            var parametro5 = new Parametro
            {
                AlteradoEm = DateTime.Now,
                AlteradoPor = 1,
                Codigo = "PAYPAL_PRICE_30D",
                Valor = "30.00"
            };

            var service = new ParametroService();
            
            // Act
            parametro1.Id = service.Gravar(parametro1);
            parametro2.Id = service.Gravar(parametro2);
            parametro3.Id = service.Gravar(parametro3);
            parametro4.Id = service.Gravar(parametro4);

            // Assert
            Assert.AreEqual(1, parametro1.Id);
            Assert.AreEqual(2, parametro2.Id);
            Assert.AreEqual(3, parametro3.Id);
            Assert.AreEqual(4, parametro4.Id);
        }
    }
}
