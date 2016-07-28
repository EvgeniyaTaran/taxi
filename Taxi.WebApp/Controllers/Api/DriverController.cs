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

		[HttpPost]
		public string LogIn(DriverLogInDto dto)
		{
			var driver = Db.Drivers.FirstOrDefault(d => d.Id == dto.DriverId);
			if (driver == null)
			{
				throw new Exception($"There is no such driver with id = {dto.DriverId}");
			}
			var car = Db.Cars.FirstOrDefault(d => d.Id == dto.CarId);
			if (car == null)
			{
				throw new Exception($"There is no such car with id = {dto.CarId}");
			}

			var cab = Db.Cabs.FirstOrDefault(c => c.DriverId == dto.DriverId && c.CarId == dto.CarId);
			if (cab == null)
			{
				cab = new Cab
				{
					Car = car,
					Driver = driver
				};
				Db.Cabs.Add(cab);
			}
			cab.Coords = new GeoCoordinates
			{
				Lattitude = dto.Lattitude,
				Longitude = dto.Longitude
			};
			Db.SaveChanges();
			return "Ok";
		}
    }
}
