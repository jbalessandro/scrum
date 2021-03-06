﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Service;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ICliente: IBaseService<Cliente>
    {
        decimal Pagar(int idCliente);
    }
}
