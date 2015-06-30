﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;
using ScrumToPractice.Domain.Abstract;

namespace ScrumToPractice.Domain.Service
{
    /// <summary>
    /// Manutencao dos simulados de cortesia
    /// </summary>
    public class CortesiaManutencao
    {
        private IBaseService<Cortesia> serviceCortesia;
        private ICortesia cortesia;

        public CortesiaManutencao()
        {
            serviceCortesia = new CortesiaService();
            cortesia = new CortesiaService();
        }
        
        /// <summary>
        /// Remove cortesias anteriores a uma determinada data
        /// Esta da eh oriunda de um determinado parametro
        /// </summary>
        public void CleanCortesia()
        {
            // numero de dias a excluir em cortesia (CriadoEm)
            var dataMaximaExclusao = DateTime.Today.Date.AddDays(cortesia.GetNumDiasManutencao() * -1);

            if (dataMaximaExclusao < DateTime.Today.Date)
            {
                var cortesias = serviceCortesia.Listar().Where(x => x.CriadoEm <= dataMaximaExclusao).AsEnumerable();

                foreach (var item in cortesias)
                {
                    serviceCortesia.Excluir(item.Id);
                }
            }
        }
    }
}
