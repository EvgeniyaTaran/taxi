App.Models.Address = Backbone.ExtModel.extend({
	defaults: {},
	init: function () {
		this.bindItem("street", App.Collections.streets, "streetId");
	}
});

App.Models.Addresses = Backbone.Collection.extend({
	url: "/api/address",
	model: App.Models.Address
});