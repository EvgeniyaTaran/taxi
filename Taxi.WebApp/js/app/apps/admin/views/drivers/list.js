App.Views.DriverListItem = Marionette.ItemView.extend({
	template: ".jDriverListItemTmpl",
	behaviors: {
		Delete: {}
	}
});

App.Views.Drivers = Marionette.CompositeView.extend({
	template: ".jDriversLayoutTmpl",
	childView: App.Views.DriverListItem,
	childViewContainer: ".jContent",
	ui: {
		create: ".jCreate",
		email: ".jEmail",
		form: ".jForm",
		show: ".jShowForm",
		hide: ".jHideForm"
	},
	events: {
		"click @ui.create": "create",
		"click @ui.show": "showForm",
		"click @ui.hide": "hideForm"
	},
	create: function () {
		var email = this.ui.email.val();
		if (email) {
			if (App.Helpers.validateEmail(email)) {
				App.Helpers.showPreloader();
				App.Collections.drivers.create({
					email: email
				},
				{
					wait: true,
					success: function (model, response) {
						App.Helpers.hidePreloader();
						App.Collections.drivers.remove(App.Collections.drivers.findWhere({ email: model.email }));
						App.Collections.drivers.add(new App.Models.Driver(response.driver));
						App.router.navigate("/drivers/" + response.driver.id + "/", { trigger: true });
					},
					error: function (response) {
						App.Helpers.hidePreloader();
					}
				});
			}
		}
	},
	showForm: function (e) {
		this.ui.show.hide();
		this.ui.hide.show();
		this.ui.form.slideDown();
	},
	hideForm: function (e) {
		this.ui.hide.hide();
		this.ui.show.show();
		this.ui.form.slideUp();
	},
	onRender: function () {
		//this.refreshModels();
	}
});