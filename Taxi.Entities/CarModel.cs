using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
    public class CarModel: IEntity
    {
        public int Id { get; set; }
		public int Num { get; set; }

        public string Name { get; set; }

		public int CarBrandId { get; set; }
        [JsonIgnore]
        public CarBrand Brand { get; set; }
    }
}
