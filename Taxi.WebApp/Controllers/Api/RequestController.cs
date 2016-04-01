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

namespace Taxi.WebApp.Controllers.Api
{
    public class RequestController : ApiBaseController
    {
		public RequestController(EntityContext context)
			: base(context)
		{ }

		[HttpPost]
		public object Calculate(OrderRequest req) 
		{
			if (req != null)
			{
				return new { Price = 250 };
			}
			return new { Price = 100 };
		}

		public HttpResponseMessage Get(string username)
		{
		    HttpContext.Current.AcceptWebSocketRequest(new ChatWebSocketHandler(username));
			var res = Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
			res.Content = new StringContent(String.Format("{0}-ho-ho-ho", username));
			return res;
		}
		
		class ChatWebSocketHandler : WebSocketHandler
		{
		    private static WebSocketCollection _chatClients = new WebSocketCollection();
		    private string _username;
		
		    public ChatWebSocketHandler(string username)
		    {
		        _username = username;
		    }
		
		    public override void OnOpen()
		    {
		        _chatClients.Add(this);
		    }
		
		    public override void OnMessage(string message)
		    {
		        _chatClients.Broadcast(_username + ": " + message);
		    }
		}

    }
}
