using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.Entities.Helpers
{
	public static class JsonSettings
	{
		private static JsonSerializerSettings _settings;

		public static JsonSerializerSettings Default()
		{
			return _settings ?? (_settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				TypeNameHandling = TypeNameHandling.Auto,
				NullValueHandling = NullValueHandling.Ignore,
				TypeNameAssemblyFormat = FormatterAssemblyStyle.Full,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
		}

		private static JsonSerializerSettings _settingsWithType;

		public static JsonSerializerSettings DefaultWithType()
		{
			return _settingsWithType ?? (_settingsWithType = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				TypeNameHandling = TypeNameHandling.Objects,
				NullValueHandling = NullValueHandling.Ignore,
				TypeNameAssemblyFormat = FormatterAssemblyStyle.Full,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
		}
	}
}
