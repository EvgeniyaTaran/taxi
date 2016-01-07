(function () {
	var app = new Backbone.Marionette.Application();
	app.Models = {};
	app.Controllers = {};
	app.Views = {};
	app.Behaviors = {};
	window.App = app;

	App.addInitializer(function () {

		App.router = new AppRouter();

		Backbone.history.start({
			pushState: true,
			root: "/admin/"
		});

		Backbone.history.bindLinks({ ignore: ".jDefaultLink" });

	});

	App.on("start", function (options) {
		console.log("start");
	});

	//App.on("before:start", function () {
	//    alert("PreStrart");
	//});

	App.Helpers = {
		showPreloader: function () {
			var top = $("body").scrollTop();
			if (top > 0) {
				$(".jPreloader").css("padding-top", top);
			}
			$(".jPreloader").show();
		},
		hidePreloader: function () {
			$(".jPreloader").hide();
		},
		initPreloader: function () {
			var opts = {
				lines: 11,
				length: 0,
				width: 27,
				radius: 52,
				scale: 0.5,
				corners: 1,
				color: '#0c7cd5',
				opacity: 0.3,
				rotate: 0,
				direction: 1,
				speed: 1,
				trail: 60,
				fps: 20,
				zIndex: 2e9,
				className: 'spinner',
				top: '50%',
				left: '50%',
				shadow: false,
				hwaccel: false,
				position: 'absolute'
			}
			var spinContainer = $(".jPreloader")[0];
			var spinner = new Spinner(opts).spin(spinContainer);
			$(".jPreloader").hide();
		},
		bindEvents: function () {
			//$(window).on("scroll", function () {
			//	var top = $(window).scrollTop();
			//	if (top > 114) {
			//		$(".jMainRegion").css({
			//			//position: "fixed"
			//		});
			//	}
			//});
			$(".jMenuTab").on("click", function (e) {
				$(".jMenuTab").removeClass("active");
				$(e.currentTarget).addClass("active");
			});
		},
		//triggerEvent: function (entity) {
		//	switch (entity) {
		//		case "block":
		//			Backbone.trigger(App.Triggers.addNewBlock);
		//			break;
		//		case "group":
		//			Backbone.trigger(App.Triggers.addNewGroup);
		//			break;
		//		case "question":
		//			Backbone.trigger(App.Triggers.addNewQuestion);
		//			break;
		//		case "panel":
		//			Backbone.trigger(App.Triggers.addNewPanel);
		//			break;
		//		case "control":
		//			Backbone.trigger(App.Triggers.addNewControl);
		//			break;
		//		case "controlItem":
		//			Backbone.trigger(App.Triggers.addNewControlItem);
		//			break;
		//	}
		//},
		getData: function (collection) {
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

			var state = [];

			collection.each(function (view) {
				var result = {
					sysId: view.model.sysId
				};
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
						value = getDataFromSelect2(view.model, control, name);
					}

					result[name] = formatValue(view.model, name, value);

				}, view));
				state.push(result);
			});
			return state;
		}
	};

	App.Helpers.initPreloader();
	App.Helpers.bindEvents();

	App.addRegions({
		mainRegion: ".jMainRegion",
		modalRegion: ".jModalRegion"
	});
})();