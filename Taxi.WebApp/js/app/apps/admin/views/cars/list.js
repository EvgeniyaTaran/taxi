App.Views.CarListItem = Marionette.ItemView.extend({
	template: ".jCarListItemTmpl"
});

App.Views.Cars = Marionette.CompositeView.extend({
	template: ".jCarsLayoutTmpl",
	childView: App.Views.CarListItem,
	childViewContainer: ".jContent",
	ui: {
		create: ".jCreate",
		title: ".jTitle"
		//date: ".jDate"
	},
	events: {
		"click @ui.create": "create"
	},
	create: function () {
		var title = this.ui.title.val();
		App.Collections.actions.create({
			title: title
		}, {
			wait: true,
			success: function (model, response) {
				App.router.navigate("/actions/" + model.id + "/", { trigger: true });
			}
		});
	}
});