App.Models.Car = Backbone.ExtModel.extend({
	defaults: {

	},
	init: function () {
		this.bindItem("carModel", App.Collections.carModels, "carModelId");
	}
});

App.Models.Cars = Backbone.Collection.extend({
	url: "/api/cars",
	model: App.Models.Car,
	getActive: function (count) {
		var that = this;
		return $.get("/api/cars/getactive", { count: count }).then(function (cars) {
			that.add(cars, { merge: true });
		});
	}
});