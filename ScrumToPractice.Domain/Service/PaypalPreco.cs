using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using System;
using System.Linq;

namespace ScrumToPractice.Domain.Service
{   
    public class PaypalPreco: IPreco
    {
        private const string _codigoParametro = "PAYPAL_PRICE_30D";
        IBaseService<Parametro> _parametro;

        public PaypalPreco()
        {
            _parametro = new ParametroService();
        }

        public decimal GetPrecoMensal()
        {
            return Convert.ToDecimal(getParametro().Valor);
        }

        public void SetPrecoMensal(decimal valor, int idUsuario)
        {
            // valida
            if (valor <=0)
            {
                throw new ArgumentException("Valor inválido");
            }

            var parametro = getParametro();
            if (parametro == null)
            {
                parametro = new Parametro();
                parametro.Codigo = _codigoParametro;
            }

            parametro.Valor = valor.ToString();
            parametro.AlteradoPor = idUsuario;
            parametro.AlteradoEm = DateTime.Now;
            _parametro.Gravar(parametro);
        }

        private Parametro getParametro()
        {
            return _parametro.Listar().Where(x => x.Codigo == _codigoParametro).FirstOrDefault();
        }
    }
}
