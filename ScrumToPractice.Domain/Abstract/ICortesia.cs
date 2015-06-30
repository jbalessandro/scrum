﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ICortesia
    {
        SimuladoCortesia GetSimulado();
        int GetNumQuestoes();
    }
}
