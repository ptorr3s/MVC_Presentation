using System.Web;
using System.Web.Optimization;

namespace Web {
	public class BundleConfig {
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles) {
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
							"~/Scripts/jquery.signalR-{version}.js",
							"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
				"~/Scripts/knockout-*",
				"~/Scripts/knockout.mapping-*"));

			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
			bundles.Add(new ScriptBundle("~/Content/js").Include("~/Content/Site.js"));
		}
	}
}