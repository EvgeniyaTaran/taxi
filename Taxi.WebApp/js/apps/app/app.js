(function () {
    var app = new Backbone.Marionette.Application();
    app.Models = {};
    app.Controllers = {};

    app.Views = {};

    app.Views.Order = {};
    app.Views.Car = {};

    app.Constants = {};

    app.Globals = {

    };

    window.App = app;

    App.addInitializer(function () {

        App.router = new AppRouter();

        //App.router.on("route", function (route, params) {
        //	console.log("Different Page: " + route);
        //});

        Backbone.history.start({
            pushState: true
        });

        Backbone.history.bindLinks({ ignore: ".jDefaultLink" });

    });

    App.on("start", function (options) {
    });

    //App.on("before:start", function () {
    //    alert("PreStrart");
    //});


    App.addRegions({
        mainRegion: ".jMainRegion",
        modalRegion: ".jModalRegion"
    });
})();
