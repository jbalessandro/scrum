using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;
using ScrumToPractice.Domain.Abstract;

namespace ScrumToPractice.Web.Tests.Services.Simulados
{
    [TestClass]
    public class UnitTest1
    {
        private ICliente service;

        public UnitTest1()
        {
            service = new ClienteService();
        }

        [TestMethod]
        public void IncluirCliente()
        {
            // Arrange
            var cliente = new Cliente
            {
                Email = "jb.alessandro@gmail.com",
                Nome = "JOSE ALESSANDRO LIMA BATISTA",
                CriadoEm = DateTime.Now,
                Ativo = true,
                ExpiraEm = DateTime.Now.AddMonths(1),
                Observacao = "teste",
                PagoEm = DateTime.Today.Date,
                ValorPago = 9.99M
            };

            // Act
            int id = service.Gravar(cliente);
            Cliente clienteGravado = service.Find(id);

            // Assert
            Assert.AreEqual(clienteGravado.Nome, cliente.Nome);
        }
    }
}
