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
				throw new Exception(String.Format("There is no such cacr with id = {0}", id));
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

		[HttpPut]
		public object Update(int id, Car dto)
		{
			var car = Db.Cars.FirstOrDefault(c => c.Number == dto.Number);
			if (car == null)
			{
				throw new Exception(String.Format("There is no car with id = {0}", id));
			}
			car.CarModelId = dto.CarModelId;
			car.Color = dto.Color;
			car.Name = dto.Name;
			car.Number = dto.Number;
			car.IsActive = dto.IsActive;
			car.TechDataId = dto.TechDataId;
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

		///////////////////////////////
		/*[HttpPost]
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

		[HttpPut]
		public object Save(CarDto dto)
		{
			var car = Db.Cars.FirstOrDefault(c => c.Number == dto.Number);
			if (car == null)
			{
				throw new Exception(String.Format("The car with number {0} has already existed", dto.Number));
			}
			car.CarModelId = dto.ModelId;
			car.Name = dto.Name;
			car.Number = dto.Number;
			car.IsActive = true;

			Db.SaveChanges();
			return new { car };
		}

		[HttpDelete]
		public string Delete(int id)
		{
			var car = Db.Cars.FirstOrDefault(c => c.Id == id);
			if (car == null)
			{
				throw new Exception(String.Format("The car with id = {0} has already existed", id));
			}
			Db.Cars.Remove(car);
			Db.SaveChanges();
			return "Ok";
		}

		[HttpGet]
		public object GetById(int id)
		{
			var car = Db.Cars.FirstOrDefault(c => c.Id == id);
			if (car == null)
			{
				throw new Exception(String.Format("The car with id = {0} has already existed", id));
			}
			return new { car };
		}

		[HttpGet]
		public object GetByNumber(string number)
		{
			var car = Db.Cars.FirstOrDefault(c => c.Number == number);
			if (car == null)
			{
				throw new Exception(String.Format("The car with id = {0} has already existed", id));
			}
			return new { car };
		}

		[HttpGet]
		public object GetAll()
		{
			var cars = Db.Cars.Where(c => c.IsActive).ToList();
			return new { cars };
		}*/
	}
}
