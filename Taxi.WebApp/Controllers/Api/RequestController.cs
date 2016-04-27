//using Microsoft.AspNet.SignalR.WebSockets;
using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Taxi.DataAccess;
using Taxi.Entities;
using Taxi.WebApp.Models.Services;

namespace Taxi.WebApp.Controllers.Api
{
    public class RequestController : ApiBaseController
    {
		public RequestController(EntityContext context)
			: base(context)
		{ }


		public HttpResponseMessage Get(string username)
		{
			var handler = new TaxiWebSocketHandler(username);
			HttpContext.Current.AcceptWebSocketRequest(handler);
			var res = Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
			res.Content = new StringContent(String.Format("{0}-ho-ho-ho", username));
			return res;
		}

    }
}
