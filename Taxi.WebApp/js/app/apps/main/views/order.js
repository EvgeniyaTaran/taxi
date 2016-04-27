App.Views.OrderLayout = Marionette.LayoutView.extend({
	template: ".jOrderLayoutTmpl",
	templateHelpers: {},
	_fromAddress: null,
	_toAddress: null,
	regions: {
		cars: ".jActiveCars"
	},
	ui: {
		refresh: ".jRefreshCars",
		time: ".jTime",
		mapRegion: ".jMapRegion",
		from: ".jFrom",
		to: ".jTo"
	},
	modelEvents: {
		"change": "render"
	},
	events: {
		"click @ui.refresh": "refreshCars",
		"change @ui.time": "afterTime",
		"change @ui.from": "afterMap",
		"change @ui.to": "afterMap"
	},
	onRender: function () {
		console.log("Hello Evgeniya");
	},
	afterTime: function () {
		this.ui.mapRegion.slideDown();
	},
	afterMap: function () {
		if (this.ui.from.val() && this.ui.to.val()) {
			var data = {
				period: this.ui.time.val()
			};

			App.Controllers.Request.calculatePrice(data);
		}
	}
});