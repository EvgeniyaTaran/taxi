﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Entities;

namespace Taxi.DataAccess
{
    public class EntityContext: IdentityDbContext<WebUser>
    {
		public DbSet<Car> Cars { get; set; }
		//public DbSet<CarType> CarTypes { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderAddress> OrderAddresses { get; set; }
		public DbSet<TechData> TechDataSet { get; set; }
		public DbSet<Address> Addresses { get; set; }

		public DbSet<Route> Routes { get; set; }
		public DbSet<Road> Roads { get; set; }
		public DbSet<RoutePoint> RoutePoints { get; set; }
		public DbSet<RoadPoint> RoadPoints { get; set; }
		//public DbSet<MapPoint> MapPoints { get; set; }

		public DbSet<CarBrand> CarBrands { get; set; }
		public DbSet<CarModel> CarModels { get; set; }
		public DbSet<Country> Countries { get; set; }

		public DbSet<Driver> Drivers { get; set; }
		public DbSet<Manager> Managers { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<OrderRequest> Requests { get; set; }
		public DbSet<Cab> Cabs { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<Street> Streets { get; set; }
		//public DbSet<Settings> Settings { get; set; }



		public EntityContext()
			: base("default")
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//modelBuilder.HasDefaultSchema("Taxi");
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			base.OnModelCreating(modelBuilder);
		}

		public static EntityContext Create()
		{
			return new EntityContext();
		}
    }
}
