var AppRouter = Marionette.AppRouter.extend({
    parseId: function (id) {
        if (id) {
            id = _.parseInt(id);
        }
        return id;
    },

    //добавить в практики
    route: function (route, name, callback) {
        var router = this;
        if (!callback) callback = this[name];

        var f = function () {
            callback.apply(router, arguments);
        };
        return Backbone.Router.prototype.route.call(this, route, name, f);
    },
    routes: {
		"": "order",
        "cars/": "cars",
        "cars/:id/": "car",
		"order/": "order"
    },
    index: function () {
    	App.Controllers.Home.show();
    },
    cars: function (id) {
    	App.Controllers.Cars.show();
    },
    car: function (id) {
        App.Controllers.Car.show(this.parseId(id));
    },
    order: function () {
    	App.Controllers.Order.newOrder();
    }
});
