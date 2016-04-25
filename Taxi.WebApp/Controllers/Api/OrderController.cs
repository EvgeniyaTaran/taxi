using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Taxi.DataAccess;
using Taxi.Entities;

namespace Taxi.WebApp.Controllers.Api
{
	public class OrderController : ApiBaseController
    {
		public OrderController(EntityContext context)
			: base(context)
		{ }

		[HttpPost]
		public object Calculate(Order order)
		{
			if (order != null)
			{
				return new { Price = 250 };
			}
			return new { Price = 100 };
		}
    }
}
