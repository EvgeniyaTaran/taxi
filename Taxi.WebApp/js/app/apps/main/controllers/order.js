App.Controllers.Order = {
	create: function () {
		var view = new App.Views.OrderLayout();
		App.mainRegion.show(view);
	},
	calculatePrice: function (data) {
		$.post("/api/order/calculate", { order: data })
			.done(function (response) {
				console.log(response);
			})
			.fail(function (response) {
				alert("Error");
			})
	}
};