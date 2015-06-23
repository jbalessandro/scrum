﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumToPractice.Domain.Models;

namespace ScrumToPractice.Web.Areas.Administrativo.Models
{
    public class AreaQuestao
    {
        public IEnumerable<SelectListItem> Areas { get; set; }
        public Questao Questao { get; set; }

    }
}