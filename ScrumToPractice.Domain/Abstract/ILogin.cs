﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ILogin
    {
        bool ValidaLogin(string login, string senha);
    }
}
