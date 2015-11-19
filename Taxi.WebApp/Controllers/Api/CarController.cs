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
	public class CarController : ApiBaseController
    {
		public CarController(EntityContext context)
			: base(context)
		{ }

		[HttpPost]
		public object Create(CarDto dto)
		{
			var car = Db.Cars.FirstOrDefault(c => c.Number == dto.Number);
			if (car == null)
			{
				throw new Exception(String.Format("The car with number {0} has already existed", dto.Number));
			}
			car = new Car 
			{
				CarModelId = dto.ModelId,
				Name = dto.Name,
				Number = dto.Number,
				IsActive = true
			};
			Db.Cars.Add(car);
			Db.SaveChanges();
			return new { car };
		}


    }
}
