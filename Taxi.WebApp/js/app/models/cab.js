App.Models.Cab = Backbone.ExtModel.extend({
	defaults: {},
	init: function () {
		this.bindItem("car", App.Collections.cars, "carId");
		this.bindItem("driver", App.Collections.drivers, "driverId");
	}
});

App.Models.Cabs = Backbone.Collection.extend({
	url: "/api/cab",
	model: App.Models.Cab
});