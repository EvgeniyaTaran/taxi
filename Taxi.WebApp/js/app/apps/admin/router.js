﻿var AppRouter = Marionette.AppRouter.extend({
	parseId: function (id) {
		if (id) {
			id = _.parseInt(id);
		}
		return id;
	},

	//добавить в практики
	route: function (route, name, callback) {
		var router = this;
		if (!callback) callback = this[name];

		var f = function () {
			callback.apply(router, arguments);
		};
		return Backbone.Router.prototype.route.call(this, route, name, f);
	},
	routes: {
		"": "index",
		"orders/": "orders",
		"cars/": "cars",
		"cabs/": "cabs"
	},
	index: function () {
		App.Controllers.Cars.show();
	},
	cars: function (id) {
		App.Controllers.Cars.show();
	},
	cabs: function (id) {
		App.Controllers.Cabs.show(this.parseId(id));
	},
	orders: function () {
		App.Controllers.Orders.newOrder();
	}
});
