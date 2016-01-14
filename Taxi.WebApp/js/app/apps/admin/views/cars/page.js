App.Views.CarLayout = Marionette.LayoutView.extend({
	template: ".jCarLayoutTmpl",
	templateHelpers: {
		getCarModels: function () {
			return App.Collections.carModels.models;	//.where({ carBrandId: this.carModel.carBrandId });
		},
		getCarBrands: function () {
			return App.Collections.carBrands.models;
		}
	},
	behaviors: {
		Save: {}
	},
	regions: {
	},
	ui: {
		brand: ".jBrand",
		model: ".jModel"
	},
	modelEvents: {
		"change": "render"
	},
	events: {
		"change @ui.brand": "refreshModels"
	},
	onRender: function () {
		this.ui.model.find("option").hide();
		this.ui.model.find("option[data-brandId='" + this.model.carModel.carBrandId + "']").show();
		//this.ui.brand.select2({});
		//this.ui.model.select2({});
	},
	refreshModels: function () {
		//var collection = App.Collections.carModels.where({ carBrandId: parseInt(this.ui.brand.val()) });
		//this.ui.model.select2("val", collection);
		this.ui.model.find("option").hide();
		var newItems = this.ui.model.find("option[data-brandId='" + this.ui.brand.val() + "']");
		newItems.show();
		this.ui.model.val(newItems.data("id"));
	}
});