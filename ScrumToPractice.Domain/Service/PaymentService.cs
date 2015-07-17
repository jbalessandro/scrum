using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Repository;

namespace ScrumToPractice.Domain.Service
{
    public class PaymentService: IBaseService<Payment>
    {
        private IBaseRepository<Payment> repository;

        public PaymentService()
        {
            repository = new EFRepository<Payment>();
        }

        public IQueryable<Payment> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Payment item)
        {
            if (item.IdCliente == 0)
            {
                throw new ArgumentException("Cliente inválido");
            }

            return repository.Incluir(item).Id;
        }

        public Payment Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK
                return null;
            }            
        }

        public Payment Find(int id)
        {
            return repository.Find(id);
        }
    }
}
