using ScrumToPractice.Domain.Models;
using System.Collections.Generic;

namespace ScrumToPractice.Web.Areas.Administrativo.Models
{
    public class QuestaoRespostas
    {
        public Questao Questao { get; set; }
        public IEnumerable<Resposta> Respostas { get; set; }
    }
}