(function () {
	var Layout = {
		_activeClass: "active",
		ui: {
			menuTab: ".jMenuTab"
		},
		bindEvents: function () {
			var that = this;
			this.ui.menuTab.on("click", function (e) {
				that.ui.menuTab.removeClass(that._activeClass);
				$(e.currentTarget).addClass(that._activeClass);
			});
		},
		init: function () {
			this.bindEvents();
		}
	}
	Layout.init();
})();