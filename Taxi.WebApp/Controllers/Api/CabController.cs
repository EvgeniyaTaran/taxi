﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Taxi.DataAccess;

namespace Taxi.WebApp.Controllers.Api
{
	public class CabController : ApiBaseController
    {
		public CabController(EntityContext context)
			: base(context)
		{ }
    }
}
