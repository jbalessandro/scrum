using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using System.Linq;
using System.Data.Entity;

namespace ScrumToPractice.Web.Tests.Services
{
    [TestClass]
    public class Areas
    {
        [TestMethod]
        public void IncluirArea()
        {
            // Arrange
            var area = new Area
            {
                AlteradoPor = 1,
                Descricao = "Theory",
            };
            var service = new AreaService();

            // Act
            var id = service.Gravar(area);

            // Assert
            Assert.AreEqual(1, id);
        }

        [TestMethod]
        public void FindArea()
        {
            // Arrange
            var service = new AreaService();

            // Act
            var area = service.Find(1);

            // Assert
            Assert.AreEqual("THEORY", area.Descricao);
        }

        [TestMethod]
        public void ListarArea()
        {
            // Arrange
            var service = new AreaService();

            // Act
            var areas = service.Listar();

            // Assert
            Assert.AreEqual(1, areas.Count());
        }
    }
}
