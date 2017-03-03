// app-trips.js

(function(){

    //Creating module
    angular.module("app-trips", ["simpleControls", "ngRoute"])
    .config(function ($routeProvider) {

        $routeProvider.when("/", {
            controller: "tripsController",
            controllerAs: "vm",
            templateUrl: "/views/tripsView.html"
        });

        $routeProvider.when("/editor", {
            controller: "tripEditController",
            controllerAs: "vm",
            templateUrl: "/views/tripEditorView.html"
        });

        $routeProvider.otherwise({ redirectTo: "/" });
    });
})();