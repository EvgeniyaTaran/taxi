using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
    public class CarType : IEntity
    {
        public int Id { get; set; }
		public int Num { get; set; }

		//[JsonIgnore]
		//public ICollection<Car> Cars { get; set; }
    }
}
