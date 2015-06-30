﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ICorSimulado
    {
        IEnumerable<CorSimulado> GetSimulados(int idCortesia);
        CorSimulado Find(int idCortesia, int idQuestao);
    }
}
