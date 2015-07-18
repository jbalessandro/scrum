using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using System;

namespace ScrumToPractice.Domain.Service
{
    public class Pagamento: IPagamento
    {
        private IBaseService<Cliente> serviceCliente;
        private IBaseService<Payment> servicePayment;
        private IParametro serviceParametro;
        private IEmailPayment serviceEmail;
        private Payment _payment;

        public Pagamento()
        {
            serviceCliente = new ClienteService();
            servicePayment = new PaymentService();
            serviceParametro = new ParametroService();
            serviceEmail = new EmailPayment();
        }

        /// <summary>
        /// Inclui um novo cliente no sistema quando do pagamento e retorna o Id do Cliente
        /// </summary>
        /// <param name="payment">Dados do pagamento recebido</param>
        /// <returns>Id do cliente</returns>
        public int NovoPagamento(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentException("Invalid payment");
            }

            // variavel da classe
            _payment = payment;

            // grava um novo cliente
            var cliente = IncluirCliente();
            _payment.IdCliente = cliente.Id;

            // grava informacoes do pagamento
            _payment.Id = servicePayment.Gravar(_payment);

            // envia e-mail ao cliente
            serviceEmail.EnviarEmail(cliente);

            // retorna ID do cliente
            return _payment.IdCliente;
        }

        private Cliente IncluirCliente()
        {
            var idCliente = serviceCliente.Gravar(new Cliente
            {
                Ativo = true,
                CriadoEm = DateTime.Now,
                Email = _payment.PayerEmail,
                ExpiraEm = DateTime.Today.Date.AddMonths(serviceParametro.GetPrazoAcessoPago()).AddDays(1),
                Nome = _payment.FirstName,
                Observacao = "PAYPAL",
                PagoEm = DateTime.Now,
                ValorPago = _payment.McGross,
                Chave = Guid.NewGuid().ToString("N")
            });

            return serviceCliente.Find(idCliente);
        }
    }
}
