using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Taxi.WebApp.Areas.Operator.Controllers
{
    public class DeskController : Controller
    {
        // GET: Operator/Desk
        public ActionResult Index()
        {
            return View();
        }
    }
}