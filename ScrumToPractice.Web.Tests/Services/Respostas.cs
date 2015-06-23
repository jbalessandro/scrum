using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System.Linq;
using System.Data.Entity;

namespace ScrumToPractice.Web.Tests.Services
{
    [TestClass]
    public class Respostas
    {
        [TestMethod]
        public void IncluirResposta()
        {
            // Arrange
            var resposta = new Resposta
            {
                AlteradoPor = 1,
                Descricao = "1-10",
                IdQuestao = 1,
            };

            var service = new RespostaService();

            // Act
            var id = service.Gravar(resposta);

            // Assert
            Assert.AreEqual(1, id);
        }

        [TestMethod]
        public void FindResposta()
        {
            // Arrange
            var service = new RespostaService();

            // Act
            var id = service.Find(1).Id;

            // Assert
            Assert.AreEqual(1, id);
        }
    }
}
