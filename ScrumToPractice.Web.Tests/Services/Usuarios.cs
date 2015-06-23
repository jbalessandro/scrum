using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrumToPractice.Domain.Service;
using ScrumToPractice.Domain.Models;
using System.Linq;
using System.Data.SqlClient;

namespace ScrumToPractice.Web.Tests.Services
{
    [TestClass]
    public class Usuarios
    {
        [TestMethod]
        public void ListarUsuarios()
        {
            // Arrange
            var listar = new UsuarioService();

            // Act
            var usuarios = listar.Listar().ToList();

            // Assert
            Assert.AreEqual(1, usuarios.Count);
        }

        [TestMethod]
        public void IncluirUsuario()
        {
            // Arrange
            var usuario = new Usuario
            {
                Email = "jb.alessandro@gmail.com",
                Nome = "JOSE",
                Login = "JOSE",
                Senha = "b8c7p2c6"
            };

            // Act
            var idUsuario = new UsuarioService().Gravar(usuario);

            // Assert
            Assert.AreEqual(idUsuario, 1);
        }

        [TestMethod]
        public void ObterUsuario()
        {
            // Arrange
            var usuarioService = new UsuarioService();

            // Act
            var usuario = usuarioService.Find(1);

            // Assert
            Assert.AreEqual("JOSE", usuario.Nome);
        }

        [TestMethod]
        public void AlterarUsuario()
        {
            // Arrange
            var service = new UsuarioService();
            var usuario = service.Find(3);

            // Act
            int id = service.Gravar(usuario);

            // Assert
            Assert.AreEqual(3, id);
        }
    }
}
