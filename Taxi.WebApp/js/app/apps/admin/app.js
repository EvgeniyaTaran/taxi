(function () {
	var app = new Backbone.Marionette.Application();
	app.Models = {};
	app.Controllers = {};
	app.Views = {};
	app.Behaviors = {};
	window.App = app;

	App.addInitializer(function () {

		App.router = new AppRouter();

		Backbone.history.start({
			pushState: true,
			root: "/admin/"
		});

		Backbone.history.bindLinks({ ignore: ".jDefaultLink" });

	});

	App.on("start", function (options) {
		console.log("start");
	});

	//App.on("before:start", function () {
	//    alert("PreStrart");
	//});


	App.addRegions({
		mainRegion: ".jMainRegion",
		modalRegion: ".jModalRegion"
	});
})();