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
			var complexData = that.getComplexTypeData(this.view);
			this.view.model.fill(this.view, complexData);
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
		},
		getComplexTypeData: function (view) {

			var formatValue = function (model, name, rawValue) {

				var modelValue = model.attributes[name];

				if (_.isNumber(modelValue)) {
					return _.parseInt(rawValue);
				}

				if (_.isString(rawValue)) {
					rawValue = rawValue.trim();
				}

				return rawValue;

			};
			var result = {};
			var els = view.$el.find("[data-complexname]");

			if (els.length === 0) {
				return;
			}

			els.each(_.bind(function (ind, v) {
				var control = $(v);
				var value = control.val();
				var field = control.data("complexname").split(".");
				if (!result[field[0]]) {
					result[field[0]] = {};
				}

				if (field[2]) {
					if (!result[field[0]][field[1]]) {
						result[field[0]][field[1]] = {};
					}

					result[field[0]][field[1]][field[2]] = formatValue(view.model, field[2], value);
				} else {
					result[field[0]][field[1]] = formatValue(view.model, field[1], value);
				}
			}, view.model));

			return result;
		}
	});	
})();
