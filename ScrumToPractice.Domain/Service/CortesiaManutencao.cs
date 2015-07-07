using ScrumToPractice.Domain.Abstract;
using ScrumToPractice.Domain.Models;
using System;
using System.Linq;

namespace ScrumToPractice.Domain.Service
{
    /// <summary>
    /// Manutencao dos simulados de cortesia
    /// </summary>
    public class CortesiaManutencao
    {
        private IBaseService<Cortesia> serviceCortesia;
        private IParametro serviceParametro;

        public CortesiaManutencao()
        {
            serviceCortesia = new CortesiaService();
            serviceParametro = new ParametroService();
        }
        
        /// <summary>
        /// Remove cortesias anteriores a uma determinada data
        /// Esta da eh oriunda de um determinado parametro
        /// </summary>
        public void CleanCortesia()
        {
            // numero de dias a excluir em cortesia (CriadoEm)
            var dataMaximaExclusao = DateTime.Today.Date.AddDays(GetNumDiasManutencao() * -1);

            if (dataMaximaExclusao < DateTime.Today.Date)
            {
                var cortesias = serviceCortesia.Listar().Where(x => x.CriadoEm <= dataMaximaExclusao).AsEnumerable();

                foreach (var item in cortesias)
                {
                    serviceCortesia.Excluir(item.Id);
                }
            }
        }

        private int GetNumDiasManutencao()
        {
            var parametro = serviceParametro.Find("CORTESIA_MANUTENCAO_DIAS");

            if (parametro != null)
            {
                return Convert.ToInt32(parametro.Valor);
            }
            return 0;
        }
    }
}
