using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taxi.DataAccess;
using Taxi.WebApp.Models;

namespace Taxi.WebApp.Controllers
{
	public class AppController : BaseController
	{
		public AppController(EntityContext context)
			: base(context)
		{ }

		public ActionResult Index()
		{
			var cars = Db.Cars.ToList();
			var models = Db.CarModels.ToList();
			var brands = Db.CarBrands.ToList();
			var vm = new LayoutViewModel()
			{
				Cars = cars,
				CarModels = models,
				CarBrands = brands,
			};
			return View(vm);
		}
	}
}