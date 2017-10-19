(function (angular) {
    angular.module('payment.controller', ['payment.service'])
        .controller('paymentController', ['$scope', '$location', '$paymentService', '$routeParams', function ($scope, $location, $paymentService, $routeParams) {
            $scope.model = {
            };
            $scope.completed = false;

            $scope.promise = $paymentService.add($routeParams.productId).then(function (response) {
                $scope.model = response;
                $scope.model.PaymentType = 'REMITANCE';
                $scope.completed = true;
            });

            $scope.submit = function (model) {
                if (model.PaymentType == 'PAYU') {
                    $scope.payu(model);
                }
                else if (model.PaymentType == 'REMITANCE') {
                    $scope.remitance(model);
                }
            };

            $scope.payu = function (model) {
                $scope.promise = $paymentService.payu(model).then(function (response) {
                    $scope.model = response.Value;
                    $scope.commandResult = response;
                    $scope.model.PaymentType = 'PAYU';
                    if (response.Succeeded) {
                        var url = $scope.model.RedirectUrl;
                        window.location.href = url;

                    }
                });
            };
            $scope.remitance = function (model) {
                $scope.promise = $paymentService.remitance(model).then(function (response) {
                    $scope.model = response.Value;
                    $scope.commandResult = response;
                    $scope.model.PaymentType = 'REMITANCE';
                    if (response.Succeeded) {
                        // show "Operation completed" message
                        $location.path('/payment/' + $routeParams.productId + '/completed');
                    }
                });
            };
        }])
        .controller('paymentCompletedController', ['$scope', '$location', '$paymentService', '$routeParams', function ($scope, $location, $paymentService, $routeParams) {
            $scope.product = {};
            $scope.completed = false;

            $scope.promise = $paymentService.product($routeParams.productId).then(function (response) {
                $scope.product = response;
                $scope.completed = true;
            });
        }])
        ;
})(angular);