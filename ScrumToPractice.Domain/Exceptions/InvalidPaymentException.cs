using System;

namespace ScrumToPractice.Domain.Exceptions
{
    public class InvalidPaymentException: Exception
    {
        public InvalidPaymentException()
        {
        }

        public InvalidPaymentException(string message)
            : base(message)
        {
        }
    }
}
