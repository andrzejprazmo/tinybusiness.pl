(function (angular) {
    angular.module('product.controller', ['dashboard.service'])
        .controller('productController', ['$scope', '$location', '$dashboardService', function ($scope, $location, $dashboardService) {
            $scope.dashboard = {};

            $scope.promise = $dashboardService.details().then(function (response) {
                $scope.dashboard = response;
            });
        }])
        ;
})(angular);