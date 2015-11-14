using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities
{
    public class MapPoint: IEntity
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
