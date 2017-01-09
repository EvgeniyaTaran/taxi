(function() {
	var ui = {
		tabs: {
			all: $(".jTab")
		},
		blocks: {
			all: $(".jBlock"),
			info: $(".jInfo"),
			history: $(".jHistory"),
			stat: $(".jStat")
		}
	};

	var classes = {
		active: "active",
		hide: "hide"
	}

	function bindEvents() {
		ui.tabs.all.click(function(e) {
			var item = $(this);
			ui.tabs.all.toggleClass(classes.active, false);
			item.toggleClass(classes.active, true);
			var data = item.data("tab");
			ui.blocks.all.toggleClass(classes.hide, true);
			ui.blocks.all.filter(function() {
				return $(this).data("tab") === data;
			}).toggleClass(classes.hide, false);
		});
	}

	bindEvents();
})();