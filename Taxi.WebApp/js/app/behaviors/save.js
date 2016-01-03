(function () {
	App.Behaviors.Save = Backbone.Marionette.Behavior.extend({
		events: {
			"click .jSave": "save"
		},
		modelEvents: {
			"change": "modelChanged"
		},
		modelChanged: function (model, options) {
			if (options && options.sender === this.view) {
				return;
			}
			if (this.view.isDestroyed) return;
			this.view.render();
		},
		save: function () {
			var that = this;
			this.view.model.fill(this.view);
			if (this.view.model.hasChanged()) {
				App.Helpers.showPreloader();
				this.view.model.save(null, {
					wait: true,
					success: function () {
						App.Helpers.hidePreloader();
						that.view._isChanged = false;
					},
					error: function (data) {
						App.Helpers.hidePreloader();
						alert(data);
					}
				});
			}
		}
	});	
})();
