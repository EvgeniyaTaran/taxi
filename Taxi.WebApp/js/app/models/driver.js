﻿App.Models.Driver = Backbone.ExtModel.extend({
	defaults: {
	},
	init: function () {
		this.bindCollection("cabs", App.Collections.cabs, "driverId");
	}
});

App.Models.Cars = Backbone.Collection.extend({
	url: "/api/driver",
	model: App.Models.Car
});