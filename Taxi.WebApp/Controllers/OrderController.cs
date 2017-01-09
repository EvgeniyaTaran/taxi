using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taxi.DataAccess;
using Taxi.Entities;
using Taxi.Services;
using Taxi.WebApp.Models.Dtos;
using Taxi.WebApp.Properties;

namespace Taxi.WebApp.Controllers
{
	public class OrderController : BaseController
	{
		private readonly CalculateService _calcService;

		public OrderController(EntityContext context)
			: base(context)
		{
			_calcService = new CalculateService(Settings.Default.BaseCost, Settings.Default.PerKilometer);
		}

		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Calculate(OrderDto dto)
		{
			if (dto != null)
			{
				var coordsFrom = new GeoCoordinates
				{
					Latitude = Convert.ToDouble(dto.GeoCoordinatesFromLat),
					Longitude = Convert.ToDouble(dto.GeoCoordinatesFromLng)
				};

				var coordsTo = new GeoCoordinates
				{
					Latitude = Convert.ToDouble(dto.GeoCoordinatesToLat),
					Longitude = Convert.ToDouble(dto.GeoCoordinatesToLng)
				};

				var price = _calcService.CalculatePrice(new List<GeoCoordinates> {coordsFrom, coordsTo});

				if (dto.TaxiClass == TaxiClass.Comfort)
				{
					price *= Settings.Default.ComfortIndex;
				}

				return Json(new {Price = price, Currency = "грн"});
			}
			return Json("Error");
		}

		[HttpPost]
		public Order Create(OrderDto dto)
		{
			var coordsFrom = new GeoCoordinates
			{
				Latitude = Convert.ToDouble(dto.GeoCoordinatesFromLat),
				Longitude = Convert.ToDouble(dto.GeoCoordinatesFromLng)
			};

			var coordsTo = new GeoCoordinates
			{
				Latitude = Convert.ToDouble(dto.GeoCoordinatesToLat),
				Longitude = Convert.ToDouble(dto.GeoCoordinatesToLng)
			};

			var addressFrom = Db.Addresses.FirstOrDefault(a => Math.Abs(a.Coords.Latitude - coordsFrom.Latitude) < 0.000001 && Math.Abs(a.Coords.Longitude - coordsFrom.Longitude) < 0.000001);
			var addressTo = Db.Addresses.FirstOrDefault(a => Math.Abs(a.Coords.Latitude - coordsTo.Latitude) < 0.000001 && Math.Abs(a.Coords.Longitude - coordsTo.Longitude) < 0.000001);

			var client = String.IsNullOrWhiteSpace(dto.ClientId) ? null : Db.Clients.FirstOrDefault(c => c.Id == dto.ClientId);

			var order = new Order
			{
				Addresses = new List<OrderAddress>
				{
					new OrderAddress
					{
						Num = 0,
						Type = OrderAddressType.From,
						Address = addressFrom ?? new Address
						{
							Coords = coordsFrom,
							Name = dto.AddressFrom
						}
					},
					new OrderAddress
					{
						Num = 1,
						Type = OrderAddressType.To,
						Address = addressTo ?? new Address
						{
							Coords = coordsTo,
							Name = dto.AddressTo
						}
					}
					
				},
				ClientId = client?.Id,
				Status = OrderStatus.New,
				TaxiType = dto.TaxiClass
			};

			Db.Orders.Add(order);
			Db.SaveChanges();

			return order;
		}

		
	}
}