using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxi.Entities;
using Taxi.Services.Models;

namespace Taxi.Services
{
    public class GeoCoordsTransformer
    {
	    public static SimpleCoords ToGaussProjection(GeoCoordinates coords)
	    {
			// Перевод географических координат (широты и долготы) точки в прямоугольные
			// координаты проекции Гаусса-Крюгера (на примере координат Москвы).

			// Географические координаты точки (в градусах)
		    double dLon = coords.Longitude;
			double dLat = coords.Latitude;

			// Номер зоны Гаусса-Крюгера (если точка рассматривается в системе
			// координат соседней зоны, то номер зоны следует присвоить вручную)
			int zone = (int)(dLon / 6.0 + 1);

			// Параметры эллипсоида Красовского
			double a = 6378245.0;          // Большая (экваториальная) полуось
			double b = 6356863.019;        // Малая (полярная) полуось
			double e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);  // Эксцентриситет
			double n = (a - b) / (a + b);        // Приплюснутость


			// Параметры зоны Гаусса-Крюгера
			double F = 1.0;                   // Масштабный коэффициент
			double Lat0 = 0.0;                // Начальная параллель (в радианах)
			double Lon0 = (zone * 6 - 3) * Math.PI / 180;  // Центральный меридиан (в радианах)
			double N0 = 0.0;                  // Условное северное смещение для начальной параллели
			double E0 = zone * 1e6 + 500000.0;    // Условное восточное смещение для центрального меридиана

			// Перевод широты и долготы в радианы
			double Lat = dLat * Math.PI / 180.0;
			double Lon = dLon * Math.PI / 180.0;

			// Вычисление переменных для преобразования
			double sinLat = Math.Sin(Lat);
			double cosLat = Math.Cos(Lat);
			double tanLat = Math.Tan(Lat);

			double v = a * F * Math.Pow(1 - e2 * Math.Pow(sinLat, 2), -0.5);
			double p = a * F * (1 - e2) * Math.Pow(1 - e2 * Math.Pow(sinLat, 2), -1.5);
			double n2 = v / p - 1;
			double M1 = (1 + n + 5.0 / 4.0 * Math.Pow(n, 2) + 5.0 / 4.0 * Math.Pow(n, 3)) * (Lat - Lat0);
			double M2 = (3 * n + 3 * Math.Pow(n, 2) + 21.0 / 8.0 * Math.Pow(n, 3)) * Math.Sin(Lat - Lat0) * Math.Cos(Lat + Lat0);
			double M3 = (15.0 / 8.0 * Math.Pow(n, 2) + 15.0 / 8.0 * Math.Pow(n, 3)) * Math.Sin(2 * (Lat - Lat0)) * Math.Cos(2 * (Lat + Lat0));
			double M4 = 35.0 / 24.0 * Math.Pow(n, 3) * Math.Sin(3 * (Lat - Lat0)) * Math.Cos(3 * (Lat + Lat0));
			double M = b * F * (M1 - M2 + M3 - M4);
			double I = M + N0;
			double II = v / 2 * sinLat * cosLat;
			double III = v / 24 * sinLat * Math.Pow(cosLat, 3) * (5 - Math.Pow(tanLat, 2) + 9 * n2);
			double IIIA = v / 720 * sinLat * Math.Pow(cosLat, 5) * (61 - 58 * Math.Pow(tanLat, 2) + Math.Pow(tanLat, 4));
			double IV = v * cosLat;
			double V = v / 6 * Math.Pow(cosLat, 3) * (v / p - Math.Pow(tanLat, 2));
			double VI = v / 120 * Math.Pow(cosLat, 5) * (5 - 18 * Math.Pow(tanLat, 2) + Math.Pow(tanLat, 4) + 14 * n2 - 58 * Math.Pow(tanLat, 2) * n2);

			// Вычисление северного и восточного смещения (в метрах)
			double N = I + II * Math.Pow(Lon - Lon0, 2) + III * Math.Pow(Lon - Lon0, 4) + IIIA * Math.Pow(Lon - Lon0, 6);
			double E = E0 + IV * (Lon - Lon0) + V * Math.Pow(Lon - Lon0, 3) + VI * Math.Pow(Lon - Lon0, 5);

			return new SimpleCoords(N, E);
		}

	}
}
