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
					En = new BaseLocale
					{
						Name = "Corolla"
					},
					Ru = new BaseLocale
					{
						Name = "Corolla"
					},
					Brand = brands.ElementAt(0)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Camry"
					},
					Ru = new BaseLocale
					{
						Name = "Camry"
					},
					Brand = brands.ElementAt(0)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Astra"
					},
					Ru = new BaseLocale
					{
						Name = "Astra"
					},
					Brand = brands.ElementAt(1)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Vectra"
					},
					Ru = new BaseLocale
					{
						Name = "Vectra"
					},
					Brand = brands.ElementAt(1)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Focus"
					},
					Ru = new BaseLocale
					{
						Name = "Focus"
					},
					Brand = brands.ElementAt(2)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Mondeo"
					},
					Ru = new BaseLocale
					{
						Name = "Mondeo"
					},
					Brand = brands.ElementAt(2)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Civic"
					},
					Ru = new BaseLocale
					{
						Name = "Civic"
					},
					Brand = brands.ElementAt(3)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Pilot"
					},
					Ru = new BaseLocale
					{
						Name = "Pilot"
					},
					Brand = brands.ElementAt(3)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "S660"
					},
					Ru = new BaseLocale
					{
						Name = "S660"
					},
					Brand = brands.ElementAt(3)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Benz"
					},
					Ru = new BaseLocale
					{
						Name = "Benz"
					},
					Brand = brands.ElementAt(4)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Aveo"
					},
					Ru = new BaseLocale
					{
						Name = "Aveo"
					},
					Brand = brands.ElementAt(5)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Lacetti"
					},
					Ru = new BaseLocale
					{
						Name = "Lacetti"
					},
					Brand = brands.ElementAt(5)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Evanda"
					},
					Ru = new BaseLocale
					{
						Name = "Evanda"
					},
					Brand = brands.ElementAt(5)
				},
				new CarModel 
				{ 
					En = new BaseLocale
					{
						Name = "Camaro"
					},
					Ru = new BaseLocale
					{
						Name = "Camaro"
					},
					Brand = brands.ElementAt(5)
				}
			};
			context.Countries.AddRange(_createCountries());
			context.CarBrands.AddRange(brands);
			context.CarModels.AddRange(models);

			foreach (var m in models)
			{
				var car = new Car
				{
					Model = m,
					Number = Guid.NewGuid().ToString(),
					Color = _getRandomColor(r),
					Ru = new BaseLocale { Name = String.Format("{0} {1}", m.Brand.Ru.Name, m.Ru.Name) },
					En = new BaseLocale { Name = String.Format("{0} {1}", m.Brand.En.Name, m.En.Name) }
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
				new CarBrand { Ru = new BaseLocale{Name = "Toyota"}, En = new BaseLocale{Name = "Toyota"}},
				new CarBrand { Ru = new BaseLocale{Name = "Opel"}, En = new BaseLocale{Name = "Opel"}},
				new CarBrand { Ru = new BaseLocale{Name = "Ford"}, En = new BaseLocale{Name = "Ford"}},
				new CarBrand { Ru = new BaseLocale{Name = "Honda"}, En = new BaseLocale{Name = "Honda"}},
				new CarBrand { Ru = new BaseLocale{Name = "Mersedes"}, En = new BaseLocale{Name = "Mersedes"}},
				new CarBrand { Ru = new BaseLocale{Name = "Chevrolet"}, En = new BaseLocale{Name = "Chevrolet"}},
				new CarBrand { Ru = new BaseLocale{Name = "Nissan"}, En = new BaseLocale{Name = "Nissan"}}
			};
			context.CarBrands.AddRange(brands);
			context.SaveChanges();
			return brands;
		}

		private List<Country> _createCountries() 
		{
			var countries = new List<Country>
			{
				new Country
				{
					En = new BaseLocale{Name = "Ukraine", ShortName = "Ua"},
					Ru = new BaseLocale{Name = "Украина", ShortName = "Ua"}
				},
				new Country
				{
					En = new BaseLocale{Name = "Germany", ShortName = "Gr"},
					Ru = new BaseLocale{Name = "Германия", ShortName = "Gr"}
				},
				new Country
				{
					En = new BaseLocale{Name = "Poland", ShortName = "Po"},
					Ru = new BaseLocale{Name = "Польша", ShortName = "Po"}
				}
			};
			return countries;
		}
    }
}
