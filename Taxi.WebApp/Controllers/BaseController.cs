using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taxi.DataAccess;

namespace Taxi.WebApp.Controllers
{
    public class BaseController : Controller
    {
		private EntityContext _db;
		public EntityContext Db { get { return _db; } }

		public BaseController(EntityContext context)
		{
			_db = context;
		}
    }
}