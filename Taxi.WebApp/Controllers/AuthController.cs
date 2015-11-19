using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taxi.DataAccess;

namespace Taxi.WebApp.Controllers
{
	public class AuthController : BaseController
    {
		public AuthController(EntityContext context)
			: base(context)
		{ }

        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
    }
}