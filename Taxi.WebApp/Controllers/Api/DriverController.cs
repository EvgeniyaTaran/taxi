using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Taxi.DataAccess;
using Taxi.Entities;
using Taxi.WebApp.Models.Dtos;

namespace Taxi.WebApp.Controllers.Api
{
	public class DriverController : ApiBaseController
    {
		public DriverController(EntityContext context)
			: base(context)
		{ }
    }
}
