using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Repository;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Service
{
    public class UsuarioService: IBaseService<Usuario>
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
    }
}
