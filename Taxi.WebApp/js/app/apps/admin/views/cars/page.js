App.Views.CarLayout = Marionette.LayoutView.extend({
	template: ".jCarLayoutTmpl",
	behaviors: {
		Edit: {}
	},
	regions: {
	},
	ui: {
	},
	modelEvents: {
		"change": "render"
	},
	events: {
	},
	onRender: function () {
		this.changeStatus.show(new App.Views.ActionChangeStatus({
			model: this.model
		}));
	}
});