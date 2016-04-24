using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taxi.DataAccess;

namespace Taxi.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(EntityContext context)
			: base(context)
		{ }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderDto order)
        {
            
        }
    }
}