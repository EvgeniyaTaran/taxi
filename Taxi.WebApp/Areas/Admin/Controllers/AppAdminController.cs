﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taxi.DataAccess;
using Taxi.WebApp.Areas.Admin.Models;
using Taxi.WebApp.Controllers;

namespace Taxi.WebApp.Areas.Admin.Controllers
{
	public class AppAdminController : BaseController
    {
		public AppAdminController(EntityContext context)
			: base(context)
		{ }

        public ActionResult Index()
        {
			var vm = new AppAdminViewModel 
			{
				Brands = Db.CarBrands.ToList(),
				Cabs = Db.Cabs.ToList(),
				Cars = Db.Cars.ToList(),
				Models = Db.CarModels.ToList(),
				Drivers = Db.Drivers.ToList()
			};
            return View(vm);
        }
    }
}