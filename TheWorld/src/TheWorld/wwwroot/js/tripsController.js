(
function () {
    "use strict";

    //getting the existing module
    angular.module("app-trips")
           .controller("tripsController", tripsController);

    function tripsController()
    {
        var vm = this;
        vm.trips = [{
            name : "Lucian trips",
            created: new Date()
        }, {
            name: "Lucian trips 2",
            created: new Date()
        }
        ];
    }
}
)();