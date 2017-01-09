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
			if (!context.CarModels.Any())
			{
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

				context.SaveChanges();
			}

			GenerateDrivers(context);

			GenerateCars(context);

			if (!context.Photos.Any())
			{
				GenerateTestImgs(context);
			}

			GenerateDefUSers(context);
			GenOrders(context);
		}

		private void GenOrders(EntityContext db)
		{
			var user = db.Users.FirstOrDefault(u => !(u is Driver)) ?? new WebUser
			{
				Sex = Sex.Male,
				BirthDate = DateTime.Now.AddYears(-27),
				Email = "alexroverandom@gmail.com",
				FirstName = "Tester",
				Surname = "Testerson",
				PhoneNumber = "0989882872",
				PasswordHash = "ANin9+1rNiQh6Hz45rUnBG1Acr9mFsRMHGh0nYjRSSz573LjnS5uTZGbGyMkJDqE3Q==",
				UserName = "alexroverandom@gmail.com",
				Photos = new List<WebUserPhoto>
					{
						new WebUserPhoto
						{
							Description = "Фото с первой поездки",
							FileName = "user-test-1.png",
							IsMain = true,
							Num = 0
						}
					},
				PhoneNumberConfirmed = true,
				EmailConfirmed = true,
				SecurityStamp = Guid.NewGuid().ToString()
			};

			db.Users.AddOrUpdate(user);
			db.SaveChanges();
			if (!db.Cabs.Any())
			{
				var driver1 = db.Users.FirstOrDefault(u => u is Driver);
				var driver2 = db.Users.FirstOrDefault(u => u is Driver && u.Id != driver1.Id);
				var car1 = db.Cars.FirstOrDefault();
				var car2 = db.Cars.FirstOrDefault(x => x.Id != car1.Id);
				var cab1 = new Cab
				{
					CarId = car1.Id,
					DriverId = driver1.Id
				};
				var cab2 = new Cab
				{
					CarId = car2.Id,
					DriverId = driver2.Id
				};

				db.Cabs.Add(cab1);
				db.Cabs.Add(cab2);
				db.SaveChanges();

				var order1 = db.Orders.FirstOrDefault();
				var order2 = db.Orders.FirstOrDefault(x => x.Id != order1.Id);

				order1.CabId = cab1.Id;
				order1.ClientComment = "прелестно";
				order1.Status = OrderStatus.Closed;
				order2.ClientComment = "ужас";
				order2.CabId = cab2.Id;
				order2.Status = OrderStatus.Closed;

				user.Orders = new List<Order>
				{
					order1, order2
				};

				db.SaveChanges();
			}

			
		}

		private void GenerateDefUSers(EntityContext db)
		{


		}

		private void GenerateTestImgs(EntityContext context)
		{
			var photos = new List<CarPhoto>
			{
				new CarPhoto
				{
					IsMain = true,
					CarId = 2,
					FileName = "test1.png",
					Num = 0
				},
				new CarPhoto
				{
					IsMain = true,
					CarId = 10,
					FileName = "test2.png",
					Num = 0
				}
			};
			context.Photos.AddRange(photos);
			context.SaveChanges();
		}

		private void GenerateDrivers(EntityContext db)
		{
			if (!db.Drivers.Any())
			{
				for (int i = 1; i < 21; i++)
				{
					db.Drivers.Add(CreateDriver(i));
				}
				db.SaveChanges();
			}
		}

		private Driver CreateDriver(int num)
		{
			var driver = new Driver
			{
				PasswordHash = "ANin9+1rNiQh6Hz45rUnBG1Acr9mFsRMHGh0nYjRSSz573LjnS5uTZGbGyMkJDqE3Q==",
				Email = $"driver{num}@gmail.com",
				UserName = $"driver{num}",
				BirthDate = DateTime.Now.AddYears(-40 + num)
			};
			return driver;
		}

		private void GenerateCars(EntityContext db)
		{
			if (!db.Cars.Any())
			{
				var models = db.CarModels.ToList();
				var drivers = db.Drivers.ToList();
				var r = new Random();
				foreach (var carModel in models)
				{
					var index = r.Next(0, drivers.Count - 1);
					var driver = drivers.ElementAt(index);
					var car = new Car()
					{
						CarModelId = carModel.Id,
						OwnerId = driver.Id,
						Number = Guid.NewGuid().ToString(),
						Color = _getRandomColor(r),
						Ru = new BaseLocale { Name = $"{carModel.Brand.Ru.Name} {carModel.Ru.Name}" },
						En = new BaseLocale { Name = $"{carModel.Brand.En.Name} {carModel.En.Name}" }
					};
					db.Cars.Add(car);
				}
				db.SaveChanges();
			}
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
			//context.CarBrands.AddRange(brands);
			//context.SaveChanges();
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
