App.Controllers.Order = {
	newOrder: function () {
		var view = new App.Views.Order.OrderLayout();
		App.mainRegion.show(view);
	},
	calculatePrice: function (data) {
		$.post("/api/order/calculate", data)
			.done(function (response) {

			})
			.fail(function (response) {
				alert("Error");
			})
	}
};