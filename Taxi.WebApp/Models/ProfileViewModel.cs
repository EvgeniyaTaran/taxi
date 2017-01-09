using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taxi.Entities;

namespace Taxi.WebApp.Models
{
	public class ProfileViewModel : LayoutViewModel
	{
		public List<Order> Orders { get; set; }
		public WebUser CurrentUser { get; set; }
	}
}