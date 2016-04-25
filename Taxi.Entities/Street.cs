﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
	public class Street: IEntity
	{
		public int Id { get; set; }
		public int Num { get; set; }

		public BaseLocale Ru { get; set; }
		public BaseLocale En { get; set; }
		public StreetType Type { get; set; }

		public Street() 
		{
			Ru = new BaseLocale();
			En = new BaseLocale();
		}

	}

	public enum StreetType
	{
		Street = 0,
		Alley = 1, // проулок
		Avenue = 2, // проспект
		BlindAlley = 3, // тупик
		Boulevard = 4
	}
}
