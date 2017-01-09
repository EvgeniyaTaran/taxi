using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Entities;

namespace Taxi.Services
{
	public class CalculateService
	{
		private readonly float _baseCost;
		private readonly float _perKm;
		private const float RoadIndex = 2f;

		public CalculateService(float baseCost, float perKilometer)
		{
			_baseCost = baseCost;
			_perKm = perKilometer;
		}

		public double CalculatePrice(List<GeoCoordinates> points)
		{
			if (points.Count < 2)
			{
				throw new Exception("We need minimum 2 points to create a simple route");
			}

			double distance = 0;

			if (points.Count == 2)
			{
				distance = _getDistance(points.First(), points.Last());
			}
			else
			{
				for (int i = 0; i < points.Count - 1; i++)
				{
					distance += _getDistance(points.ElementAt(i), points.ElementAt(i + 1));
				}
			}

			var price = _baseCost + (distance * RoadIndex / 1000) * _perKm;

			return price;
		}

		private double _getDistance(GeoCoordinates from, GeoCoordinates to)
		{
			var gaussFrom = GeoCoordsTransformer.ToGaussProjection(from);
			var gaussTo = GeoCoordsTransformer.ToGaussProjection(to);

			var distance = Math.Pow(Math.Pow(gaussFrom.X - gaussTo.X, 2) + Math.Pow(gaussFrom.Y - gaussTo.Y, 2), 0.5);

			return distance;
		}
	}
}
