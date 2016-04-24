App.Controllers.Request = {
	create: function () {
		var view = new App.Views.RequestLayout();
		App.mainRegion.show(view);
	},
	calculatePrice: function (data) {
		$.post("/api/request/calculate", { "": data })
			.done(function (response) {
				console.log(response);
			})
			.fail(function (response) {
				alert("Error");
			})
	}
};