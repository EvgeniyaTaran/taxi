(function () {
	Backbone.Marionette.Renderer.render = function (template, data, helpers) {
		var tmpl = Backbone.Marionette.TemplateCache.get(template);
		return tmpl.render(data, helpers);
	};

	Backbone.Marionette.TemplateCache.prototype.compileTemplate = function (rawTemplate) {
		return $.templates(rawTemplate);
	};

	Backbone.Marionette.TemplateCache.prototype.loadTemplate = function (templateId) {
		var myTemplate = $(templateId).html();
		return myTemplate;
	};


	
	Backbone.Marionette.ItemView.prototype.attachElContent = function (html) {


		// Нужно заменить элемент, т.к. он уже привязан к дому, 
		// а если не менять - то он будет только во вьюшке, но не будет отражен в доме.
		var $oldel = this.$el;
		this.setElement(html, true);
		$oldel.replaceWith(this.$el).remove();

		return this;
	};


	Backbone.Marionette.CompositeView.prototype.attachElContent = function (html) {


		// Нужно заменить элемент, т.к. он уже привязан к дому, 
		// а если не менять - то он будет только во вьюшке, но не будет отражен в доме.
		var $oldel = this.$el;
		this.setElement(html, true);
		$oldel.replaceWith(this.$el).remove();

		return this;
	};
	

	var stringify = function (jqObj) {
		return $("<div>").append(jqObj.clone()).html();
	};

	var parseEl = function (tmplEl) {

		var str = tmplEl.data("el");
		if (!str) {
			return "<div />";
		}
		var split = str.split(".");

		var el = $("<" + split[0] + " />");

		_.each(split.slice(1), function (className) {
			el.addClass(className);
		});


		return stringify(el);
	};


	Backbone.Marionette.CollectionView.prototype.attachBuffer = function (collectionView, buffer) {

		var $oldel = collectionView.$el;
		var el;
		if (this.childView) {
			el = parseEl($(this.childView.prototype.template)); //todo:fallback to tagName if undefined
		} else if (this.tagName) {
			var cls = this.className;

			el = "<" + this.tagName;
			if (cls) {
				el += " class=" + cls;
			}

			el += " />";
		} else {
			el = "<div />";
		}

		collectionView.setElement(el, true);
		$oldel.replaceWith(collectionView.$el).remove();


		collectionView.$el.append(buffer);
	};
	
	Backbone.Model.prototype.fill = function () {

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

		return function (view, options) { //todo:refactor

			var result = {};

			view.$el.find("[name]").each(_.bind(function (ind, v) {
				var control = $(v);

				var value = control.val();
				var name = control.attr("name");

				if (control.is(":checkbox")) {

					var isChecked = control.prop("checked");

					if (view.$el.find("[name=" + name + "]").length > 1) { //checkbox list


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
					value = getDataFromSelect2(this, control, name);
				}

				result[name] = formatValue(this, name, value);

			}, this));

			this.set(result, _.extend({}, {
				sender: view
			}, options));
		};
	}();

	Backbone.Collection.prototype.withIds = function (arr) {
		return this.filter(function (el) {
			return _.contains(arr, el.id);
		});
	};

	/**
	 * Практика для реализации серверной фильтарции. Отфильтрованные элементы подмердживаются в коллекцию.
	 * @param  {[type]} data Должен безопасно сериализоваться ф-цией $.param(obj). Т.к. Collection.fetch() делает GET запрос.
	 * @return {[type]} Возвращает Deferred объект.
	 */
	Backbone.Collection.prototype.fetchByFilter = function (data) {

		return this.fetch({
			data: data,
			merge: true,
			remove: false
		}).then(_.bind(function (items) {

			var itemIds = _.map(items, function (item) {
				return item.id;
			});

			return this.withIds(itemIds);

		}, this));

	};

	Backbone.Collection.prototype.getOrFetch = function (id) {

		var model = this.get(id);

		if (model) {
			return $.when(model);
		}

		return this.fetch({
			data: {
				id: id
			},
			merge: true,
			remove: false
		}).then(_.bind(function (item) {

			return this.get(item.id);

		}, this));

	};


	Backbone.MemoryCollection = Backbone.Collection.extend({
		sync: function (method, model, options) {
			options.success.apply();
		}
	});

	Backbone.FilteredCollection = Backbone.Collection.extend({
		initialize: function (models, options) {

			if (_.isUndefined(options.source)) {
				throw new Error("Filtered collection source not defined.");
			}

			this.comparator = options.source.comparator;

			this.options = _.defaults(options, {
				matches: function () {
					return true;
				},
				events: []
			});

			//1-st filter by matches
			this.update();

			this.options.source.on("all", function (event, model) {


				if (event === "add" || event === "remove") {

					if (options.matches(model)) {
						this[event](model);
					}

				}
				if (event === "reset") {
					this.update();
				}
				if (_.contains(options.events, event)) {

					if (options.matches(model)) {
						this.add(model);
					} else {
						this.remove(model);
					}


				}
			}.bind(this));
		},
		update: function () {
			this.reset(this.options.source.filter(this.options.matches));
		}
	});

	Backbone.ExtModel = Backbone.Model.extend({
		initialize: function (options) {
			if (typeof this.init === "function") {
				this.init(options);
			}

			this.definePropertiesForAttributes();

			this.on("change", function () {
				this.definePropertiesForAttributes();
			});

		},
		defineModelProperty: function (propertyName, getter, setter) {

			var prop = {
				get: getter,
				set: setter,
				enumerable: true,
				configurable: true
			};

			Object.defineProperty(this, propertyName, prop);
			Object.defineProperty(this.attributes, propertyName, prop);
		},
		bindItem: function (propertyName, collection, keyName) {
			var obj = this;

			this.defineModelProperty(propertyName, function () {
				return collection.get(obj.get(keyName));
			});
		},
		bindCollection: function (name, collection, matches) {

			var func;

			if (_.isString(matches)) {
				func = function (model) {
					return model.get(matches) === this.id;
				}.bind(this);
			} else {
				func = matches;
			}


			this.set(name, new Backbone.FilteredCollection(null, {
				source: collection,
				matches: func
			}));

			this.on("change:" + name, function () {

				this.get(name).reset(collection.filter(func));

			}.bind(this));


		},
		definePropertiesForAttributes: function () {
			var obj = this;

			_.forEach(obj.attributes, function (v, i) {
				if (!obj.hasOwnProperty(i))
					Object.defineProperty(obj, i, {
						get: function () {
							return obj.get(i);
						},
						set: function (value) {
							obj.set(i, value);
						},
						configurable: true
					});
			});
		},
		toJSON: function (options) {
			if (_.has(options, "emulateHTTP")) {
				var res = {};
				_.forIn(this.attributes, function (value, key) {
					if (_.isArray(value) || _.isNumber(value) || _.isString(value) || _.isBoolean(value) || _.isNull(value)) {
						res[key] = value;
					} else if (!(value instanceof Backbone.Model) && !(value instanceof Backbone.Collection)) {
						res[key] = value;
					}
				});
				return res;
			} else {
				return _.clone(this.attributes);
			}
		}
	});

	/**
	 * Переопределяет дефолтное поведение ссылок заменяя
	 * его на навигацию с помощь Backbone.Router
	 * @param  {Object} router Backbone.Router
	 * @param  {Object} options [ignore]=".default-link" селектор элементов для
	 * которых не переопределяется поведение
	 */
	Backbone.History.prototype.bindLinks = function (options) {
		"use strict";
		//пока забиндит и такие ссылки: //google.com, а так быть не должно.
		$(document).on("click.router-event-namespace", "a[href^='/']", function (e) {

			var $target = $(e.currentTarget),
				isMetaPressed = e.altKey || e.ctrlKey || e.metaKey || e.shiftKey;

			if (!isMetaPressed && !$target.is(options.ignore)) {

				e.preventDefault();

				var href = $target.attr("href");
				var pattern = new RegExp("^" + Backbone.history.root);

				var url = href.replace(pattern, "");

				Backbone.history.navigate(url, {
					trigger: true
				});

				return false;
			}
		});
	};





	//Для того, чтобы работали несколько контроллеров в одном апп.роутере.
	//https://gist.github.com/vermilion1/5525972
	Marionette.AppRouter.prototype.processAppRoutes = function (controller, appRoutes) {
		var routeNames = _.keys(appRoutes).reverse(); // Backbone requires reverted order of routes

		_.each(routeNames, function (route) {
			var methodName = appRoutes[route];
			var params = methodName.split('#');
			var ctrl = controller;
			var method;

			if (params.length > 1) {
				var ctrlName = params[0];
				ctrl = this[ctrlName] || this.options[ctrlName];
				methodName = params[1];
				if (!ctrl || !methodName) {
					throw new Error('Please specify correct route');
				}
			}
			method = ctrl[methodName];

			if (!method) {
				throw new Error('Method "' + methodName + '" was not found on the controller');
			}

			this.route(route, "", function () {
				this.trigger('before:route:change');
				method.apply(ctrl, arguments);
			});

		}, this);
	};
})();