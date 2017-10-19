(function (angular) {
    angular.module('customer.controller', ['customer.service'])
        .controller('customerRegisterController', ['$scope', '$customerService', '$location', function ($scope, $customerService, $location) {
            $scope.model = {};
            $scope.completed = false;
            $scope.register = function (model) {
                $scope.promise = $customerService.register(model).then(function (response) {
                    $scope.model = response.Value;
                    $scope.commandResult = response;
                    if (response.Succeeded) {
                        $location.path('/dashboard');
                        return;
                    }
                });
            };

        }])
        .controller('profileController', ['$scope', '$customerService', '$location', function ($scope, $customerService, $location) {
            $scope.profile = {};
            $scope.completed = false;
            $scope.promise = $customerService.profile().then(function (response) {
                $scope.profile = response;
                $scope.completed = true;
            });

        }])
        ;
})(angular);