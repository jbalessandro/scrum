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
        public void CriarSimulado()
        {
            // Arrange
            ISimuladoCortesia cortesia;
            cortesia = new CortesiaSimulado();

            // Act
            var idNovoSimulado = cortesia.CriarSimulado();
            var simulado = cortesia.GetSimulado(idNovoSimulado);

            // Assert
            Assert.IsTrue(simulado.QuestoesSimuladas.Count() > 0);
        }

        [TestMethod]
        public void GetSimulado()
        {
            // Arrange
            ISimuladoCortesia simulado;
            simulado = new CortesiaSimulado();

            // Act
            var questaoCortesia = simulado.GetQuestao(1, 1);
            
            // Assert
            Assert.IsNotNull(questaoCortesia.RespostaUsuario);
        }
    }
}
