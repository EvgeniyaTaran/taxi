using System.Web;
using System.Web.Optimization;

namespace Taxi.WebApp
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/libs")
				.Include("~/js/libs/jquery-2.1.0.js")
				.Include("~/js/libs/jquery.cookie.js")
				.Include("~/js/libs/lodash.js")
				.Include("~/js/libs/jsrender.js")
				.Include("~/js/libs/spin.js")
				.IncludeDirectory("~/js/libs/backbone.marionette", "*.js", true)
				.IncludeDirectory("~/js/libs/select2", "*.js", true)
				.IncludeDirectory("~/js/practices", "*.js", true));


			bundles.Add(new ScriptBundle("~/bundles/app")
				.Include("~/js/app/router.js")
				.Include("~/js/app/app.js")
				.IncludeDirectory("~/js/app/models", "*.js", true)
				.Include("~/js/app/app.collections.js")
				.IncludeDirectory("~/js/app/views", "*.js", true)
				.IncludeDirectory("~/js/app/controllers", "*.js", true));

			//bundles.Add(new ScriptBundle("~/bundles/bootstrap")
			//	.IncludeDirectory("~/js/bootstrap", "*.js", true));

			bundles.Add(new StyleBundle("~/css").Include(
					  "~/css/libs/bootstrap.css",
					  "~/css/layout.css"
					  ));
		}
	}
}
