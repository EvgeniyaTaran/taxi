App.Controllers.Cars = {
	show: function () {
		var view = new App.Views.Cars({ collection: App.Collections.cars });
		App.mainRegion.show(view);
	}
};

App.Controllers.Car = {
	show: function (id) {
		var model = App.Collections.cars.get(id);
		var view = new App.Views.CarLayout({ model: model });
		App.mainRegion.show(view);
	},
	create: function () {
		
	}
};