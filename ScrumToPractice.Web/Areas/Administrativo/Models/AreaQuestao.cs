using ScrumToPractice.Domain.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ScrumToPractice.Web.Areas.Administrativo.Models
{
    public class AreaQuestao
    {
        public IEnumerable<SelectListItem> Areas { get; set; }
        public Questao Questao { get; set; }

    }
}