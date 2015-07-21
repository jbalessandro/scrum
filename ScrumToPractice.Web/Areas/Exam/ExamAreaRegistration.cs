using System.Web.Mvc;

namespace ScrumToPractice.Web.Areas.Exam
{
    public class ExamAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Exam";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Exam_default",
                "Exam/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "ScrumToPractice.Web.Areas.Exam.Controllers" }
            );

            context.MapRoute(
                "Exam",
                "Exam/{chave}",
                new { controller = "Exam", action = "Index", chave = @"\d+" },
                new[] { "ScrumToPractice.Web.Areas.Exam.Controllers" }
            );

        }
    }
}