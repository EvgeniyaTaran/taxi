using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Taxi.DataAccess;
using Taxi.WebApp.Models.Dtos;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

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
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
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
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
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
		public JsonResult AndroidLogin(DriverLogInDto dto)
		{
			try
			{
				var driver = UserManager.FindByName(dto.Login);
				if (driver != null)
				{
					var res = UserManager.CheckPassword(driver, dto.Password);
					if (res)
					{
						return Json(driver.Id);
					}
					// TODO: realize own usermanager with PasswordLogin method

					return Json(String.Empty);
				}
				return Json(String.Empty);
			}
			catch (Exception e)
			{
				return Json(null);
			}
		}
    }
}