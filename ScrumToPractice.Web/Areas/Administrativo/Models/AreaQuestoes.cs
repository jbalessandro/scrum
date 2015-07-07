using ScrumToPractice.Domain.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ScrumToPractice.Web.Areas.Administrativo.Models
{
    public class AreaQuestoes
    {
        public IEnumerable<SelectListItem> Areas { get; set; }
        public IEnumerable<Questao> Questoes { get; set; }
    }
}