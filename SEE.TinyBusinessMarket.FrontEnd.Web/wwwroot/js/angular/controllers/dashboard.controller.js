(function (angular) {
    angular.module('dashboard.controller', ['dashboard.service'])
        .controller('dashboardController', ['$scope', '$location', '$dashboardService', function ($scope, $location, $dashboardService) {
            $scope.completed = false;
            $scope.dashboard = {};

            $scope.promise = $dashboardService.details().then(function (response) {
                $scope.dashboard = response;
                $scope.completed = true;
            });
        }])
        ;
})(angular);