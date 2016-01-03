(function () {
	App.Behaviors.Delete = Backbone.Marionette.Behavior.extend({
		ui: {
			del: ".jDelete",
			popup: ".jDeletePopup",
			confirm: ".jDeleteConfirm"
		},
		_hideClass: "hide",
		_class: null,
		events: {
			"click .jDelete": function (e) {
				e.stopPropagation();
				$(".jDeletePopup").hide();
				var item = $(e.currentTarget).parent();
				$("body").css("overflow", "hidden");
				$(".jStructure").css("overflow", "hidden");
				var topBody = $("body").scrollTop();
				var topStructure = $(".jStructure").scrollTop();
				var top = (topBody + topStructure) > 0 ? (topBody + topStructure) : 0;
				var that = this;
				that.ui.popup.css({
					width: item.width() + 10,
					height: item.height() + 10,
					"padding-left": Math.round((item.width() + 10 - 100) / 2),
					"padding-right": Math.round((item.width() + 10 - 100) / 2),
					"margin-top": -(top + 5)
				});
				that.ui.popup.show();
				that.ui.popup.css({
					width: item.width() + 10
				})
			},
			"mouseenter .jItem": function (e) {
				e.preventDefault();
				e.stopPropagation();
				var that = this;
				that.ui.del.removeClass(that._hideClass);
				//setTimeout(function () {
				//	if (that.ui.del) {
				//		$(that.ui.del).addClass(that._hideClass);
				//	}
				//}, 1500);
			},
			"mouseleave .jItem": function (e) {
				e.preventDefault();
				e.stopPropagation();
				var that = this;
				that.ui.del.addClass(that._hideClass);
				//setTimeout(function () {
				//	if (that.ui.del) {
				//		$(that.ui.del).addClass(that._hideClass);
				//	}
				//}, 200);
			},
			"click @ui.popup": function (e) {
				$("body").css("overflow", "auto");
				$(".jStructure").css("overflow", "auto");
				this.ui.popup.hide();
			},
			"click @ui.confirm": function (e) {
				e.stopPropagation();
				$("body").css("overflow", "auto");
				$(".jStructure").css("overflow", "auto");
				this.ui.popup.hide();
				var that = this;
				App.Helpers.showPreloader();
				this.view.model.destroy({
					data: {
						pollId: App.Helpers.activePollId
					},
					processData: true,
					wait: true
				})
				.done(function (data) {
					App.Helpers.hidePreloader();
					that.view.destroy();
					App.Helpers.triggerEvent(data.entity);
				})
				.fail(function (xhr) {
					App.Helpers.hidePreloader();
					console.log("Error: entity is not deleted");
				});
			}
		},
		modelEvents: {
			"change": "modelChanged"
		},
		modelChanged: function (model, options) {
			if (options && options.sender === this.view) {
				return;
			}
			this.view.render();
		}
	});
})();