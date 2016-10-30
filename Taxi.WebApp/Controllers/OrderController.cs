using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taxi.DataAccess;
using Taxi.Entities;
using Taxi.WebApp.Models.Dtos;

namespace Taxi.WebApp.Controllers
{
	public class OrderController : BaseController
	{
		public OrderController(EntityContext context)
			: base(context)
		{ }

		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public Order Create(OrderDto dto)
		{
			var order = new Order
			{
				AddressFrom = new Address
				{
					
				}, 
			};

			return order;
		}

		
	}
}