using System.Web.Mvc;

namespace Taxi.WebApp.Areas.Operator
{
    public class OperatorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Operator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
			context.MapRoute(
				"Desk",
				"Operator/",
				new { action = "Index", controller = "Desk" }
			);

            context.MapRoute(
                "Operator_default",
                "Operator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}