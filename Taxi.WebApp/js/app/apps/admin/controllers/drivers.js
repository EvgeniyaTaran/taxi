App.Controllers.Drivers = {
	show: function () {
		var view = new App.Views.Drivers({ collection: App.Collections.drivers });
		App.mainRegion.show(view);
	}
};

App.Controllers.Driver = {
	show: function (id) {
		var model = App.Collections.drivers.get(id);
		var view = new App.Views.DriverLayout({ model: model });
		App.mainRegion.show(view);
	}
};