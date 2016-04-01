(function () {
    var app = new Backbone.Marionette.Application();
    app.Models = {};
    app.Controllers = {};
    app.Behaviors = {};
    app.Views = {};

    window.App = app;

    App.addInitializer(function () {

        App.router = new AppRouter();

        Backbone.history.start({
            pushState: true
        });

        Backbone.history.bindLinks({ ignore: ".jDefaultLink" });

    });

    App.on("start", function (options) {
    });

    App.addRegions({
        mainRegion: ".jMainRegion",
        modalRegion: ".jModalRegion"
    });
})();
