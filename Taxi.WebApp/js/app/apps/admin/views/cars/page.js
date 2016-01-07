App.Views.CarLayout = Marionette.LayoutView.extend({
	template: ".jCarLayoutTmpl",
	behaviors: {
		Save: {}
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
		
	}
});