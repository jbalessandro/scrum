using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System.Linq;
using System.Data.Entity;

namespace ScrumToPractice.Web.Tests.Services
{
    [TestClass]
    public class Questoes
    {
        [TestMethod]
        public void IncluirQuestao()
        {
            // Arrange
            var questao = new Questao
            {
                AlteradoPor = 1,
                Descricao = "Qual o tamanho ideal do Time Scrum?",
                IdArea = 1,
                MultiplaEscolha = false,
                ComentarioScrum = "O tamanho ideal do Time Scrum é de 3 a 9 pessoas"
            };

            var service = new QuestaoService();

            // Act
            service.Gravar(questao);

            // Assert
            Assert.AreEqual(1, questao.Id);
        }

        [TestMethod]
        public void ObterQuestao()
        {
            // Arrange
            var service = new QuestaoService();

            // Act
            var id = service.Find(1).Id;

            // Assert
            Assert.AreEqual(1, id);
        }

        [TestMethod]
        public void ListarQuestoes()
        {
            // Arrange
            var service = new QuestaoService();

            // Act
            var questoes = service.Listar();

            // Assert
            Assert.AreEqual(1, questoes.Count());
        }
    }
}
