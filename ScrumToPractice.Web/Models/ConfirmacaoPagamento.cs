using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Web.Models
{
    public class ConfirmacaoPagamento
    {
        public Payment Payment { get; set; }
        public Cliente Cliente { get; set; }
    }
}