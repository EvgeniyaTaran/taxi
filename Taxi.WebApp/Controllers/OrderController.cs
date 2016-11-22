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
			var coordsFrom = new GeoCoordinates
			{
				Latitude = Convert.ToDouble(dto.GeoCoordinatesFromLat),
				Longitude = Convert.ToDouble(dto.GeoCoordinatesFromLng)
			};

			//var addressFrom = Db.Addresses.FirstOrDefault(a => a.Coords == coordsFrom);

			var coordsTo = new GeoCoordinates
			{
				Latitude = Convert.ToDouble(dto.GeoCoordinatesToLat),
				Longitude = Convert.ToDouble(dto.GeoCoordinatesToLng)
			};

			//var addressTo = Db.Addresses.FirstOrDefault(a => a.Coords == coordsTo);

			var client = String.IsNullOrEmpty(dto.ClientId) ? null : Db.Clients.FirstOrDefault(c => c.Id == dto.ClientId);

			var order = new Order
			{
				AddressFrom = new Address   //addressFrom ?? new Address
				{
					Coords = coordsFrom,
					Name = dto.AddressFrom
				},
				AddressTo = new Address    //addressTo ?? new Address
				{
					Coords = coordsFrom,
					Name = dto.AddressFrom
				},
				Client = client,
				
			};

			return order;
		}

		
	}
}