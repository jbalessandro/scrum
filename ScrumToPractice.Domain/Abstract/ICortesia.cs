﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Domain.Abstract
{
    public interface ISimuladoCortesia
    {
        int CriarSimulado();
        SimuladoCortesia GetSimulado(int id);
        
        QuestaoCortesia GetQuestao(int idSimulado);
        QuestaoCortesia GetQuestao(int idCortesia, int idQuestao);

        QuestaoCortesia GetProximaQuestao(int idSimulado);
        QuestaoCortesia GetProximaQuestao(int idCortesia, int idQuestaoAtual = 0);

        QuestaoCortesia GetQuestaoAnterior(int idSimulado);
        QuestaoCortesia GetQuestaoAnterior(int idCortesia, int idQuestaoAtual);

        QuestaoCortesia ResponderDepois(int idCortesia, int idQuestao);

        int GetNumQuestoes();        
        
        void GravarRespostaUsuario(int idCorSimulado, IEnumerable<int> selecionadas);

        CortesiaResultado GetResultado(int idCortesia);
    }
}
