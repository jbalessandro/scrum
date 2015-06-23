using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumToPractice.Domain.Service
{
    public interface IBaseService<T> where T: class
    {
        IQueryable<T> Listar();
        int Gravar(T item);
        T Excluir(int id);
        T Find(int id);
    }
}
