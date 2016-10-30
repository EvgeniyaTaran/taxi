using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taxi.DataAccess;
using Taxi.WebApp.Models.Dtos;

using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Taxi.WebApp.Models;
using Taxi.DataAccess;

namespace Taxi.WebApp.Controllers
{
	public class AuthController : BaseController
    {
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;

		public AuthController(EntityContext context)
			: base(context)
        {
		}

		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager;// ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager;// ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult AndroidLogin(DriverLogInDto dto)
		{
			// TODO: realize own usermanager with PasswordLogin method
			var driver = Db.Drivers.FirstOrDefault();

			Response.StatusCode = (int) HttpStatusCode.OK;
			//return Content(driver?.Id);
			return Content(Guid.NewGuid().ToString());
		}
    }
}