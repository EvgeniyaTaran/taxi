App.Models.CarBrand = Backbone.ExtModel.extend({
	defaults: {

	},
	init: function () {
		this.bindCollection("carModels", App.Collections.carModels, "carBrandId");
	}
});

App.Models.CarBrands = Backbone.Collection.extend({
	url: "/api/carbrands",
	model: App.Models.CarBrand
});