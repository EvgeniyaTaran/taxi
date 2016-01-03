using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
    public class CarBrand : IEntity
    {
        public int Id { get; set; }
		public int Num { get; set; }

        public string Name { get; set; }

		public string PhotoPath { get; set; }

		[JsonIgnore]
        public List<Country> Countries { get; set; } 
    }
}
