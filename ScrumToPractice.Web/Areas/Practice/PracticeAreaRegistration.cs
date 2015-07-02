using System.Web.Mvc;

namespace ScrumToPractice.Web.Areas.Practice
{
    public class PracticeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Practice";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Practice_default",
                "Practice/{controller}/{action}/{id}",
                new { controller = "Practice", action = "Index", id = UrlParameter.Optional },
                new [] {"ScrumToPractice.Web.Areas.Practice.Controllers"}
            );
        }
    }
}