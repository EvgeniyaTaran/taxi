App.Controllers.Cabs = {
	show: function () {
		var view = new App.Views.Cabs({ collection: App.Collections.cabs });
		App.mainRegion.show(view);
	}
};

App.Controllers.Cab = {
	show: function (id) {
		var model = App.Collections.cabs.get(id);
		var view = new App.Views.Cab({ model: model });
		App.mainRegion.show(view);
	}
};