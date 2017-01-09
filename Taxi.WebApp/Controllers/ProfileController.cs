using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Taxi.DataAccess;
using Taxi.Entities;
using Taxi.WebApp.Models;

namespace Taxi.WebApp.Controllers
{
	[Authorize]
	public class ProfileController : BaseController
    {
	    public ProfileController(EntityContext db) : base(db)
	    {
	    }

	    [HttpGet]
        public ActionResult Index()
	    {
			//var user = Db.Users.FirstOrDefault(x => x is Driver);
			if (!User.Identity.IsAuthenticated)
			{
				throw new Exception("403 Forbidden");
			}
			var userId = User.Identity.GetUserId();

			var user = Db.Users.Include(u => u.Orders.Select(x => x.Addresses.Select(h => h.Address))).FirstOrDefault(u => u.Id == userId);

		    var cabIds = user.Orders.Select(x => x.CabId).ToList();
		    var orderIds = user.Orders.Select(x => x.Id).ToList();

		    var cabs = Db.Cabs.Include(x => x.Car).Include(x => x.Driver).Where(x => cabIds.Contains(x.Id)).ToList();

		    var addresses = Db.OrderAddresses.Where(x => orderIds.Contains(x.OrderId)).Select(x => x.Address).ToList();

		    var vm = new ProfileViewModel
		    {
			    CurrentUser = user
		    };
            return View(vm);
        }
    }
}