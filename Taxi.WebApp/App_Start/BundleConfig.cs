using BundleTransformer.Core.Transformers;
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
				.Include(
					"~/js/libs/jquery-2.1.0.js",
					"~/js/libs/jsrender.js",
					"~/js/libs/lodash.js",
					"~/js/libs/backbone.marionette/backbone.js",
					"~/js/libs/backbone.marionette/backbone.marionette.js",
					"~/js/libs/backbone.marionette/backbone.routefilter.js",
					"~/js/libs/select2/select2.js",
					"~/js/libs/select2/select2_locale_ru.js",
					"~/js/libs/spin.js",
					"~/js/libs/bootstrap/tooltip.js",
					"~/js/libs/bootstrap/*.js"));

			bundles.Add(new ScriptBundle("~/bundles/jquery")
				.Include(
					"~/js/libs/jquery-2.1.0.js",
					"~/js/libs/spin.js")
			);

			bundles.Add(new ScriptBundle("~/bundles/pages")
				.Include(
					"~/js/pages/auth.js")
			);

			bundles.Add(new ScriptBundle("~/bundles/js/areas/admin")
				.Include("~/js/areas/admin/layout.js"));

			bundles.Add(new ScriptBundle("~/bundles/js/areas/operator")
				.Include("~/js/areas/operator/layout.js"));

			bundles.Add(new ScriptBundle("~/bundles/apps").Include(
				"~/js/practices/hideOnClickExcept.js",
				"~/js/practices/utils.js",
				"~/js/practices/backbone.extensions.js",
				"~/js/practices/models/mixins.js",
				"~/js/practices/models/enums.js",
				"~/js/practices/views/helpers.js",
				"~/js/practices/views/templateHelpers.js",
				"~/js/practices/views/components.js",
				"~/js/practices/behaviors/settings.js",
				"~/js/practices/behaviors/sortable.js"));

			bundles.Add(new ScriptBundle("~/bundles/app")
				.Include("~/js/app/apps/main/router.js")
				.Include("~/js/app/apps/main/app.js")
				.IncludeDirectory("~/js/app/behaviors/", "*.js", true)
				.IncludeDirectory("~/js/app/models", "*.js", true)
				.IncludeDirectory("~/js/app/apps/main/models", "*.js", true)
				.Include("~/js/app/apps/main/app.collections.js")
				.IncludeDirectory("~/js/app/apps/main/views", "*.js", true)
				.IncludeDirectory("~/js/app/apps/main/controllers", "*.js", true));

			bundles.Add(new ScriptBundle("~/bundles/adminApp")
				.Include("~/js/app/apps/admin/router.js")
				.Include("~/js/app/apps/admin/app.js")
				.IncludeDirectory("~/js/app/behaviors/", "*.js", true)
				.IncludeDirectory("~/js/app/models", "*.js", true)
				.IncludeDirectory("~/js/app/apps/admin/models", "*.js", true)
				.Include("~/js/app/apps/admin/app.collections.js")
				.IncludeDirectory("~/js/app/apps/admin/views", "*.js", true)
				.IncludeDirectory("~/js/app/apps/admin/controllers", "*.js", true));

			bundles.Add(new ScriptBundle("~/bundles/operatorApp")
				.Include("~/js/app/apps/operator/router.js")
				.Include("~/js/app/apps/operator/app.js")
				.IncludeDirectory("~/js/app/behaviors/", "*.js", true)
				.IncludeDirectory("~/js/app/models", "*.js", true)
				.IncludeDirectory("~/js/app/apps/operator/models", "*.js", true)
				.Include("~/js/app/apps/admin/app.collections.js")
				.IncludeDirectory("~/js/app/apps/admin/views", "*.js", true)
				.IncludeDirectory("~/js/app/apps/admin/controllers", "*.js", true));


			var libs = new StyleBundle("~/bundles/css/libs/")
				.Include("~/css/bootstrap.less")
				.Include("~/css/libs/select2/*.css");
			libs.Transforms.Add(new StyleTransformer());
			libs.Transforms.Add(new CssMinify());
			bundles.Add(libs);

			AddCssBundle(bundles, "~/bundles/css/overrides/",
				"~/css/override/navbar.less"
				);

			AddCssBundle(bundles, "~/bundles/css/commons/",
				"~/css/sections/layout.less"
				);
			AddCssBundle(bundles, "~/bundles/css/operator/commons/",
				"~/css/sections/operator/layout.less"
				);
			AddCssBundle(bundles, "~/bundles/css/admin/commons/",
				"~/css/sections/admin/layout.less"
				);
#if !DEBUG
			   BundleTable.EnableOptimizations = true;
#endif

			bundles.UseCdn = true;
		}

		private static void AddCssBundle(BundleCollection bundles, string name, params string[] includes)
		{
			var bundle = new Bundle(name).Include(includes);
			bundle.Transforms.Add(new StyleTransformer());
			bundle.Transforms.Add(new CssMinify());
			bundles.Add(bundle);
		}

	}
}
