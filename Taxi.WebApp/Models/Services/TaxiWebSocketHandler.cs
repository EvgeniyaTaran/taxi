using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi.WebApp.Models.Services
{
	public class TaxiWebSocketHandler: WebSocketHandler
	{
		private static WebSocketCollection _drivers = new WebSocketCollection();
		private string _driverId;
		
		public TaxiWebSocketHandler(string driverId)
		{
			_driverId = driverId;
		}
		
		public override void OnOpen()
		{
		    _drivers.Add(this);
		}
		
        //public override void OnMessage(string message)
        //{
        //   // _drivers.Broadcast();
        //}
	}
}