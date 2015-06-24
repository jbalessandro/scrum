using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Web.Areas.Administrativo.Models
{
    public class QuestaoRespostas
    {
        public Questao Questao { get; set; }
        public IEnumerable<Resposta> Respostas { get; set; }
    }
}