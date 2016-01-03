(function () {
	App.Behaviors.Modal = Backbone.Marionette.Behavior.extend({
		ui: {
			close: ".jClose"
		},
		events: {
			"click .jClose": function () {
				var that = this;
				var res = that.checkForChanges(that.view);
				var isChanged = false;
				for (var prop in res) {
					if ((!that.view.isContentView || !that.view.isContentView()) && that.view.model.hasOwnProperty(prop) && res[prop] !== that.view.model[prop]) {
						isChanged = true;
					}
				}
				if (isChanged) {
					if (confirm("Info is changed. Do you want to save changes?")) {
						this.view.model.fill(this.view);
						if (this.view.model.hasChanged()) {
							App.Helpers.showPreloader();
							this.view.model.save(null, {
								wait: true,
								success: function () {
									App.Helpers.hidePreloader();
									that.view._isChanged = false;
									that.view.destroy();
								},
								error: function (data) {
									App.Helpers.hidePreloader();
									alert(data);
									that.view.destroy();
								}
							});
						}
						else {
							this.view.destroy();
						}
					} else {
						this.view.destroy();
					}
				} else {
					this.view.destroy();
				}
			}
		},
		checkForChanges: function (view) {
			var that = this;
			var getDataFromSelect2 = function (model, control, name) {
				var data = control.select2("data");
				if (_.isArray(data)) {
					if (data.length === 0) {
						return _.isArray(model.attributes[name]) ? [] : null;
					}
					return _.map(data, "id");
				}
				return data ? data.id : null;
			};

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

			function getResult(view, options) {
				var result = {};
				view.$el.find("[name]").each(_.bind(function (ind, v) {
					var control = $(v);
					var value = control.val();
					var name = control.attr("name");
					if (control.is(":checkbox")) {
						var isChecked = control.prop("checked");
						if (view.$el.find("[name=" + name + "]").length > 1) {
							var values = result[name] || [];
							if (isChecked) {
								values.push(value);
							}
							value = values;
						} else {
							value = isChecked;
						}
					} else if (control.is(":radio")) {
						value = view.$el.find("[name=" + name + "]:checked").val();
					} else if (control.data("select2")) {
						value = getDataFromSelect2(that.view.model, control, name);
					}
					result[name] = formatValue(that.view.model, name, value);
				}, that));
				return result;
			};
			return getResult(view);
		}
	});
})();