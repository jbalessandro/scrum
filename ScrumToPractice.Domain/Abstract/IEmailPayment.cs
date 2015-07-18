using ScrumToPractice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumToPractice.Domain.Abstract
{
    public interface IEmailPayment
    {
        ScrumToPractice.Domain.Service.EmailPayment.StatusEmail EnviarEmail(Cliente cliente);
    }
}
