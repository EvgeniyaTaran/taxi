App.Controllers.Cars = {
	show: function () {
		var view = new App.Views.Cars({ collection: App.Collections.cars });
		App.mainRegion.show(view);
	},
	activeForHome: function (region) {
		App.Collections.Cars.getActive(5).done(function (result) {
			var view = new App.Views.Home.Cars({
				collection: result
			});
			region.show(view);
		});
	}
};

App.Controllers.Car = {
	show: function (id) {
		var model = App.Collections.cars.get(id);
		var view = new App.Views.Cars({ collection: App.Collections.cars });
		App.mainRegion.show(view);
	}
};