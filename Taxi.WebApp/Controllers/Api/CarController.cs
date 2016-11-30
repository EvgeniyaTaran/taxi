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

		[HttpGet]
		public List<Car> GetAll() 
		{
			return Db.Cars.Where(c => c.IsActive).ToList();
		}

		[HttpGet]
		public Car GetById(int id)
		{
			var car = Db.Cars.FirstOrDefault(c => c.Id == id);
			if (car == null)
			{
				throw new Exception(String.Format("There is no such car with id = {0}", id));
			}
			return car;
		}

		[HttpGet]
		public object GetByNumber(string number)
		{
			var car = Db.Cars.FirstOrDefault(c => c.Number == number);
			if (car == null)
			{
				throw new Exception(String.Format("The car with number = {0} has already existed", number));
			}
			return new { car };
		}

		[HttpPost]
		public object Create(Car dto)
		{
			var car = Db.Cars.FirstOrDefault(c => c.Number == dto.Number);
			if (car != null)
			{
				throw new Exception(String.Format("The car with number {0} has already existed", dto.Number));
			}
			car = new Car
			{
				CarModelId = dto.CarModelId,
				Number = dto.Number,
				IsActive = false
			};
			Db.Cars.Add(car);
			Db.SaveChanges();
			return new { car };
		}

		[HttpPut]
		public object Save(Car dto)
		{
			var car = Db.Cars.FirstOrDefault(c => c.Id == dto.Id);
			if (car == null)
			{
				throw new Exception(String.Format("There is no car with id = {0}", dto.Id));
			}
			if (Db.Cars.Any(c => c.Number == dto.Number && c.Id != dto.Id))
			{
				throw new Exception(String.Format("The car with number {0} has already existed", dto.Number));
			}
			car.CarModelId = dto.CarModelId;
			car.Color = dto.Color;
			car.Number = dto.Number;
			car.IsActive = dto.IsActive;
			car.TechDataId = dto.TechDataId;
			car.En = dto.En;
			car.Ru = dto.Ru;
			Db.SaveChanges();
			return new { car };
		}

		[HttpDelete]
		public object Delete(int id) 
		{
			var car = Db.Cars.FirstOrDefault(c => c.Id == id);
			if (car == null)
			{
				throw new Exception(String.Format("There is no car with id = {0}", id));
			}
			Db.Cars.Remove(car);
			Db.SaveChanges();
			return new { Success = true };
		}

		//[HttpGet]
		//public object Filter(FilterCarParams filterParams)
		//{

		//}
	}
}
