(function () {

	"use strict";
	Backbone.Components = {};

	var viewsContext = ".jComponentsTmpls";

	Backbone.Components.Popup = Marionette.LayoutView.extend({
		defaults: {
			onRender: function () { }
		},
		template: viewsContext + " .jPopupTmpl",
		eventNamespace: ".popup-events",
		regions: {
			body: ".jBody"
		},
		initialize: function (settings) {
			this.eventNamespace += this.cid;
			this.options = _.extend({}, this.defaults, settings);

			// закрываем попап, если закрывается внутренняя вьюшка
			this.listenToOnce(this.body, "show", function () {
				this.listenToOnce(this.body.currentView, "destroy", function () {
					this.destroy();
				});
			});

		},
		onRender: function () {

			_.delay(function (view) {
				$(document).on("click" + view.eventNamespace, function (e) {

					// переписать, буээ

					if ($(e.target).parents().filter(function () {
						return this === view.el;
					}).length === 0) {
						view.destroy();
						return;
					}
				});

			}, 1, this);



			this.options.onRender(this.$el);

		},
		onDestroy: function () {
			$(document).off(this.eventNamespace);
		}
	});

	Backbone.Components.Modal = Backbone.Components.Popup.extend({
		defaults: {

		},
		template: viewsContext + " .jModalTmpl",
		eventNamespace: ".modal-events",
		backdropEl: $("<div/>", {
			"class": "modal-backdrop fade in"
		}),
		onRender: function () {
			var that = this;

			$(window).on("resize" + this.eventNamespace, function () {
				that.resize();
			});

			this.$el.show().addClass("in");
			this.backdropEl.appendTo("body");
			this.backdropEl.on("click" + this.eventNamespace, function () {
				that.destroy();
			});
			this.$el.find(".close").on("click" + this.eventNamespace, function() {
				that.destroy();
			});

		},
		onDestroy: function () {
			$(document).off(this.eventNamespace);
			this.backdropEl.remove();
		},
		resize: function () {
			this.$el.css("margin-top", -this.$el.height() / 2 + "px");
			this.$el.css("margin-left", -this.$el.width() / 2 + "px");
		}
	});

	Backbone.Components.PagePreloader = Marionette.ItemView.extend({
		defaults: {
			lines: 15, // The number of lines to draw
			length: 35, // The length of each line
			width: 2, // The line thickness
			radius: 60, // The radius of the inner circle
			corners: 1, // Corner roundness (0..1)
			rotate: 0, // The rotation offset
			direction: 1, // 1: clockwise, -1: counterclockwise
			color: '#000', // #rgb or #rrggbb or array of colors
			speed: 1.4, // Rounds per second
			trail: 42, // Afterglow percentage
			shadow: false, // Whether to render a shadow
			className: "spinner",
			hwaccel: true
		},
		template: viewsContext + " .jPagePreloaderTmpl",
		initialize: function (settings) {
			this.options = _.extend({}, this.defaults, settings);
		},
		onRender: function () {
			var spinner = new Spinner(this.options);
			spinner.spin();
			this.el.appendChild(spinner.el);
		}
	});

})();


