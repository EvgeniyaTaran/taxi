App.Controllers.Orders = {
	show: function () {
		var view = new App.Views.Orders({ collection: App.Collections.orders });
		App.mainRegion.show(view);
	}
};

App.Controllers.Order = {
	show: function (id) {
		var model = App.Collections.orders.get(id);
		var view = new App.Views.Order({ model: model });
		App.mainRegion.show(view);
	}
};