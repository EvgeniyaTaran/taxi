App.Views.CarListItem = Marionette.ItemView.extend({
	template: ".jCarListItemTmpl",
	behaviors: {
		Delete: {}
	}
});

App.Views.Cars = Marionette.CompositeView.extend({
	template: ".jCarsLayoutTmpl",
	templateHelpers: {
		getCarModels: function () {
			return App.Collections.carModels.models;	//.where({ carBrandId: this.carModel.carBrandId });
		},
		getCarBrands: function () {
			return App.Collections.carBrands.models;
		}
	},
	childView: App.Views.CarListItem,
	childViewContainer: ".jContent",
	ui: {
		create: ".jCreate",
		number: ".jNumber",
		brand: ".jBrand",
		model: ".jModel",
		form: ".jForm",
		show: ".jShowForm",
		hide: ".jHideForm"
	},
	events: {
		"click @ui.create": "create",
		"click @ui.show": "showForm",
		"click @ui.hide": "hideForm",
		"change @ui.brand": "refreshModels"
	},
	create: function () {
		var modelId = this.ui.model.val();
		var number = this.ui.number.val();
		if (modelId && number) {
			App.Helpers.showPreloader();
			App.Collections.cars.create({
				number: number,
				carModelId: modelId
			},
			{
				wait: true,
				success: function (model, response) {
					App.Helpers.hidePreloader();
					App.Collections.cars.remove(App.Collections.cars.findWhere({ number: model.number }));
					App.Collections.cars.add(new App.Models.Car(response.car));
					App.router.navigate("/cars/" + response.car.id + "/", { trigger: true });
				},
				error: function (response) {
					App.Helpers.hidePreloader();
				}
			});
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
	refreshModels: function () {
		this.ui.model.find("option").hide();
		var newItems = this.ui.model.find("option[data-brandId='" + this.ui.brand.val() + "']");
		newItems.show();
		this.ui.model.val(newItems.data("id"));
	},
	onRender: function () {
		this.refreshModels();
	}
});