using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;
using System;
using System.Linq;

namespace ScrumToPractice.Domain.Service
{
    public class UsuarioService: IBaseService<Usuario>, ILogin
    {
        private IBaseRepository<Usuario> repository;

        public UsuarioService()
        {
            repository = new EFRepository<Usuario>();
        }

        public IQueryable<Usuario> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Usuario item)
        {
            // formata
            item.Email = item.Email.ToLower().Trim();
            item.Nome = item.Nome.ToUpper().Trim();

            // valida
            if (repository.Listar().Where(x => x.Nome == item.Nome && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Usuário já cadastrado");
            }

            if (repository.Listar().Where(x => x.Login == item.Login && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Login já utilizado por outro usuário");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                item.CriadoEm = DateTime.Now;
                return repository.Incluir(item).Id;
            }
            else
            {
                return repository.Alterar(item).Id;
            }
        }

        public Usuario Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var usuario = repository.Find(id);

                if (usuario != null)
                {
                    usuario.Ativo = false;
                    return repository.Alterar(usuario);
                }

                return usuario;
            }
        }

        public Usuario Find(int id)
        {
            return repository.Find(id);
        }

        public Usuario ValidaLogin(string login, string senha)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(senha))
            {
                var usuario = repository.Listar().Where(x => x.Ativo == true && x.Login == login && x.Senha == senha).FirstOrDefault();
                if (usuario != null)
                {
                    return usuario;
                }
            }

            return null;
        }

        public int GetIdUsuario(string login)
        {
            var usuario = repository.Listar().Where(x => x.Ativo == true && x.Login == login).FirstOrDefault();

            if (usuario != null)
            {
                return usuario.Id;
            }

            return 0;
        }
    }
}
