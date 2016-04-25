App.Models.Street = Backbone.ExtModel.extend({
	defaults: {

	},
	init: function () {
		this.bindItem("carModel", App.Collections.carModels, "carModelId");
		this.bindCollection("addresses", App.Collections.addresses, "streetId");
	}
});

App.Models.Streets = Backbone.Collection.extend({
	url: "/api/street",
	model: App.Models.Street
});