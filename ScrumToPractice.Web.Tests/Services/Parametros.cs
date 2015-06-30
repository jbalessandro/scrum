﻿using System;
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

            var service = new ParametroService();
            
            // Act
            parametro1.Id = service.Gravar(parametro1);
            parametro2.Id = service.Gravar(parametro2);            

            // Assert
            Assert.AreEqual(1, parametro1.Id);
            Assert.AreEqual(2, parametro2.Id);
        }
    }
}