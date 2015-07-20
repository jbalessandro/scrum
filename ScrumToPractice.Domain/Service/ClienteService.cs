using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;
using System;
using System.Linq;

namespace ScrumToPractice.Domain.Service
{
    public class ClienteService: ICliente
    {
        private IBaseRepository<Cliente> repository;

        public ClienteService()
        {
            repository = new EFRepository<Cliente>();
        }

        public IQueryable<Cliente> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Cliente item)
        {
            // valida
            if (item.Id == 0)
            {
                item.CriadoEm = DateTime.Now;
                item.Ativo = true;
            }

            if (item.ValorPago <= 0)
            {
                // TODO: implementar exception de pagamento nao efetuado
                // de forma que o metodo que chamou este metodo
                // redirecione o cliente para o pagamento
            }

            if (item.PagoEm == DateTime.MinValue || item.PagoEm > DateTime.Now)
            {
                throw new ArgumentException("Date of payment invalid");
            }

            if (item.ExpiraEm == DateTime.MinValue)
            {
                // por omissao libero 30 dias
                item.ExpiraEm = item.PagoEm.AddMonths(1);
            }

            if (string.IsNullOrEmpty(item.Observacao))
            {
                item.Observacao = string.Empty;
            }

            item.Nome = item.Nome.ToUpper().Trim();
            item.Email = item.Email.ToLower().Trim();

            // grava
            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }
            return repository.Alterar(item).Id;
        }

        public Cliente Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var cliente = repository.Find(id);
                if (cliente != null)
                {
                    cliente.Ativo = false;
                    Gravar(cliente);
                }
                return cliente;
            }
        }

        public Cliente Find(int id)
        {
            return repository.Find(id);
        }

        public decimal Pagar(int idCliente)
        {
            throw new NotImplementedException();
        }
    }
}
