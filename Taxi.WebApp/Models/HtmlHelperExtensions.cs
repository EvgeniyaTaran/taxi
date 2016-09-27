using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Taxi.WebApp.Models
{
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString IsActiveLink(this HtmlHelper helper, string url)
		{
			return @();
		}

	}
}