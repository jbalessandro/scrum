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
            var questaoCortesia = simulado.GetQuestao(6, 1);
            var questaoCortesiaById = simulado.GetQuestao(81);
            
            // Assert
            Assert.AreEqual(questaoCortesia.QuestaoUsuario.RespostasUsuario.Count(), 4);
            Assert.AreEqual(questaoCortesia.QuestaoUsuario.RespostasUsuario.Last().IdResposta, 4);
            Assert.AreEqual(questaoCortesiaById.QuestaoUsuario.Id, 81);
        }

        [TestMethod]
        public void GetProximaQuestao()
        {
            // Arrange
            ISimuladoCortesia simulado;
            simulado = new CortesiaSimulado();

            // Act
            var proximaQuestao = simulado.GetProximaQuestao(6, 2);
            var proximaQuestaoById = simulado.GetProximaQuestao(80);

            // Assert
            Assert.AreEqual(proximaQuestao.QuestaoUsuario.Id, 81);
            Assert.AreEqual(proximaQuestaoById.QuestaoUsuario.Id, 81);

        }

        [TestMethod]
        public void GetQuestaoAnterior()
        {
            // Arrange
            ISimuladoCortesia simulado;
            simulado = new CortesiaSimulado();

            // Act
            var questaoAnterior = simulado.GetQuestaoAnterior(6, 1);
            var questaoAnteriorById = simulado.GetQuestaoAnterior(81);

            // Assert
            Assert.AreEqual(questaoAnterior.QuestaoUsuario.Id, 80);
            Assert.AreEqual(questaoAnteriorById.QuestaoUsuario.Id, 80);
        }

        [TestMethod]
        public void GetCorrecaoCortesia()
        {
            // Arrange
            ISimuladoCortesia simulado;
            simulado = new CortesiaSimulado();

            // Act
            var resultado = simulado.GetResultado(135);

            // Assert
            Assert.AreEqual(2, resultado.Correcao.Count());
            Assert.AreNotEqual(null, resultado.Correcao.First().Questao);
            Assert.IsTrue(resultado.Correcao.First().SelecaoAluno.Count() > 0);
            Assert.IsTrue(resultado.Correcao.First().SelecaoSistema.Count() > 0);
        }
    }
}
