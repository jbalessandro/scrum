﻿using System.Web.Mvc;

namespace ScrumToPractice.Web.Areas.Administrativo
{
    public class AdministrativoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administrativo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Administrativo_default",
                "Administrativo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new [] {"ScrumToPractice.Web.Areas.Administrativo.Controllers"}
            );
        }
    }
}