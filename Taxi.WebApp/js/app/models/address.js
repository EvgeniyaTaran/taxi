App.Models.Address = Backbone.ExtModel.extend({
	defaults: {},
	init: function () {
		this.bindManyToMany("tagGroups", App.Collections.tagGroups, "tagGroupIds", "tagIds");
	}
});

App.Models.Addresses = Backbone.Collection.extend({
	url: "/api/address",
	model: App.Models.Address
});