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
    public class MarridgesController : BaseController
    {
	    public MarridgesController(EntityContext db) : base(db)
	    {
	    }

	    [HttpGet]
        public ActionResult Index()
	    {
		    var cars = Db.Cars.Where(x => x.IsForMarrige).Include(x => x.Owner).ToList();

		    var vm = new MarrigesViewModel()
		    {
			    Cars = cars
		    };

            return View(vm);
        }
    }
}