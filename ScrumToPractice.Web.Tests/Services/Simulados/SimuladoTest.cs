using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System.Collections.Generic;

namespace ScrumToPractice.Web.Tests.Services.Simulados
{
    [TestClass]
    public class SimuladoTest
    {
        private ISimulado service;

        public SimuladoTest()
        {
            service = new SimuladoService();
        }

        [TestMethod]
        public void NovoSimulado()
        {
            // Arrange
            Simulado simulado;

            // Act
            simulado = service.GetNovoSimulado(1);

            // Assert
            Assert.AreNotEqual(null, simulado);
            Assert.AreNotEqual(0, simulado.NumQuestoes);
        }

        [TestMethod]
        public void FindSimulado()
        {
            // Arrange
            Simulado simulado;

            // Act
            simulado = service.Find(2);

            // Assert
            Assert.AreEqual(2, simulado.NumQuestoes);
        }

        [TestMethod]
        public void GetQuestaoSimulado()
        {
            // Arrange
            QuestaoSimulado questao;

            // Act
            questao = service.GetQuestao(2, 1);

            // Assert
            Assert.AreEqual(1, questao.QuestaoUsuario.IdQuestao);
        }

        [TestMethod]
        public void GetProximaQuestaoSimulado()
        {
            // Arrange
            QuestaoSimulado questao;

            // Act
            questao = service.GetProximaQuestao(2, 1);

            // Assert
            Assert.AreEqual(2, questao.QuestaoUsuario.Id);
        }

        [TestMethod]
        public void GetQuestaoAnteriorSimulado()
        {
            // Arrange
            QuestaoSimulado questao;

            // Act
            questao = service.GetQuestaoAnterior(2, 2);

            // Assert
            Assert.AreEqual(1, questao.QuestaoUsuario.Id);
        }

        [TestMethod]
        public void ResponderDepoisSimulado()
        {
            // Arrange
            QuestaoSimulado questao;

            // Act
            service.ResponderDepois(2, 1);
            questao = service.GetQuestao(2, 1);

            // Assert
            Assert.AreEqual(true, questao.QuestaoUsuario.ResponderDepois);
        }

        [TestMethod]
        public void GravarRespostaUsuarioSimulado()
        {
            // Arrange
            var selecionadas = new List<int> { 2 };

            // Act
            service.GravarRespostasUsuario(2, selecionadas);
            var questao = service.GetQuestao(2, 1);

            // Assert
            Assert.AreEqual(false, questao.QuestaoUsuario.ResponderDepois);
            
        }

        [TestMethod]
        public void GetResultadoSimulado()
        {
            // Arrange
            SimuladoResultado resultado;

            // Act
            resultado = service.GetResultado(2);

            // Assert
            Assert.AreEqual(50, resultado.Resultado);
        }
    }
}
