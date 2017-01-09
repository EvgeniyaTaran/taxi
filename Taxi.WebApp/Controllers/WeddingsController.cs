using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taxi.DataAccess;
using Taxi.WebApp.Models;

namespace Taxi.WebApp.Controllers
{
    public class WeddingsController : BaseController
    {
	    public WeddingsController(EntityContext db) : base(db)
	    {
	    }

	    [HttpGet]
        public ActionResult Index()
	    {
		    var cars = Db.Cars.Where(x => x.IsForMarrige).Include(x => x.Owner).Include(x => x.Photos).ToList();

		    var models = Db.CarModels.Where(m => m.Cars.Any(c => c.IsForMarrige)).ToList();
		    var brands = Db.CarBrands.Where(b => b.Models.SelectMany(m => m.Cars).Any(c => c.IsForMarrige));

		    var vm = new WeddingsViewModel()
		    {
			    Cars = cars.OrderByDescending(x => x.Photos.Any()),
				CarModels = models,
				CarBrands = brands
		    };

            return View(vm);
        }
    }
}