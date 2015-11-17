namespace Taxi.DataAccess.Migrations
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using Taxi.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<Taxi.DataAccess.EntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Taxi.DataAccess.EntityContext context)
        {
			var r = new Random();
			var brands = _createCarBrands(context);

			var models = new List<CarModel>
			{
				new CarModel 
				{ 
					Name = "Corolla",
					Brand = brands.ElementAt(0)
				},
				new CarModel 
				{ 
					Name = "Camry",
					Brand = brands.ElementAt(0)
				},
				new CarModel 
				{ 
					Name = "Astra",
					Brand = brands.ElementAt(1)
				},
				new CarModel 
				{ 
					Name = "Vectra",
					Brand = brands.ElementAt(1)
				},
				new CarModel 
				{ 
					Name = "Focus",
					Brand = brands.ElementAt(2)
				},
				new CarModel 
				{ 
					Name = "Mondeo",
					Brand = brands.ElementAt(2)
				},
				new CarModel 
				{ 
					Name = "Civic",
					Brand = brands.ElementAt(3)
				},
				new CarModel 
				{ 
					Name = "Pilot",
					Brand = brands.ElementAt(3)
				},
				new CarModel 
				{ 
					Name = "S660",
					Brand = brands.ElementAt(3)
				},
				new CarModel 
				{ 
					Name = "Benz",
					Brand = brands.ElementAt(4)
				},
				new CarModel 
				{ 
					Name = "Aveo",
					Brand = brands.ElementAt(5)
				},
				new CarModel 
				{ 
					Name = "Lacetti",
					Brand = brands.ElementAt(5)
				},
				new CarModel 
				{ 
					Name = "Evanda",
					Brand = brands.ElementAt(5)
				},
				new CarModel 
				{ 
					Name = "Camaro",
					Brand = brands.ElementAt(5)
				}
			};

			context.CarBrands.AddRange(brands);
			context.CarModels.AddRange(models);

			foreach (var m in models)
			{
				var car = new Car
				{
					Model = m,
					Number = Guid.NewGuid().ToString(),
					Color = _getRandomColor(r),
					Name = String.Format("{0} {1}", m.Brand.Name, m.Name)
				};
				context.Cars.Add(car);
			}
			context.SaveChanges();
        }

		private CarColor _getRandomColor(Random r)
		{
			var values = Enum.GetValues(typeof(CarColor)).Cast<CarColor>();
			return values.ElementAt(r.Next(0, values.Count() - 1));
		}

		private List<CarBrand> _createCarBrands(EntityContext context)
		{
			var brands = new List<CarBrand>
			{
				new CarBrand { Name = "Toyota"},
				new CarBrand { Name = "Opel"},
				new CarBrand { Name = "Ford"},
				new CarBrand { Name = "Honda"},
				new CarBrand { Name = "Mersedes"},
				new CarBrand { Name = "Chevrolet"},
				new CarBrand { Name = "Nissan"}
			};
			context.CarBrands.AddRange(brands);
			context.SaveChanges();
			return brands;
		}
    }
}
