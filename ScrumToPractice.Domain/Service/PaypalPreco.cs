using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using System;
using System.Linq;

namespace ScrumToPractice.Domain.Service
{   
    /// <summary>
    /// Preco para pagamento via Paypal
    /// </summary>
    public class PaypalPreco: IPreco
    {
        private const string _codigoParametro = "PAYPAL_PRICE_30D";
        IBaseService<Parametro> _parametro;

        public PaypalPreco()
        {
            _parametro = new ParametroService();
        }

        /// <summary>
        /// Preco para assinatura de 30 dias
        /// </summary>
        /// <returns></returns>
        public decimal GetPrecoMensal()
        {
            return Convert.ToDecimal(getParametro().Valor);
        }

        /// <summary>
        /// Define o preco para 30 dias de uso
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="idUsuario"></param>
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
