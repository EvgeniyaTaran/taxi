App.Models.CarModel = Backbone.ExtModel.extend({
	defaults: {

	},
	init: function () {
		this.bindItem("carBrand", App.Collections.carBrands, "carBrandId");
		this.bindCollection("cars", App.Collections.cars, "carModelId");
	}
});

App.Models.CarModels = Backbone.Collection.extend({
	url: "/api/carmodels",
	model: App.Models.CarModel
});