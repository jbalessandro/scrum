
using System.ComponentModel.DataAnnotations;
namespace ScrumToPractice.Domain.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int IdCliente { get; set; }

        public string AddressCountry { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountryCode { get; set; }
        public string ContactPhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PayerBusinessName { get; set; }
        public string PayerEmail { get; set; }
        public string PayerId { get; set; }

        public string TxnId { get; set; }
        public string PaymentStatus { get; set; }
        public decimal Tax { get; set; }
        public decimal McGross { get; set; }
    }
}
