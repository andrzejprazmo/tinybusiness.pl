(function (angular) {
    angular.module('admin.controller', ['admin.service'])
        .controller('adminController', ['$scope', '$location', '$adminService', function ($scope, $location, $adminService) {
        }])
        .controller('adminCustomersController', ['$scope', '$location', '$adminService', function ($scope, $location, $adminService) {
            $scope.customers = [];
            $scope.request = {
                PageIndex: 0,
                PageSize: 10,
            };

            $scope.promise = $adminService.customers($scope.request).then(function (response) {
                $scope.request = response.Request;
                $scope.customers = response.List;
            });
        }])
        .controller('adminCustomerDetailsController', ['$scope', '$location', '$adminService', '$routeParams', 'ModalDialog', function ($scope, $location, $adminService, $routeParams, ModalDialog) {
            $scope.customer = {};
            $scope.orders = [];
            $scope.licences = [];

            $scope.sendProForma = function (item) {
                ModalDialog.show({
                    templateUrl: 'html/popup/send.proforma.html',
                    controller: "adminCustomerProFormaController",
                    callback: function (result) {
                        window.location.reload();
                    },
                    inputs: {
                        model: item
                    }
                });
            };

            $scope.applyLicence = function (item) {
                ModalDialog.show({
                    templateUrl: 'html/popup/apply.licence.html',
                    controller: "adminCustomerLicenceController",
                    callback: function (result) {
                        $scope.load();
                    },
                    inputs: {
                        model: item
                    }
                });
            };

            $scope.password = function (customer) {
                ModalDialog.show({
                    templateUrl: 'html/popup/generate.password.html',
                    controller: "adminCustomerPasswordController",
                    callback: function (result) {
                        $scope.load();
                    },
                    inputs: {
                        model: customer
                    }
                });
            };

            $scope.load = function () {
                $scope.promise = $adminService.customerDetails($routeParams.customerId).then(function (response) {
                    $scope.customer = response;
                    return $adminService.customerOrders($routeParams.customerId).then(function (response) {
                        $scope.orders = response;
                        return $adminService.customerLicences($routeParams.customerId).then(function (response) {
                            $scope.licences = response;
                        });
                    });
                });
            }

            $scope.load();
        }])
        .controller('adminCustomerLicenceController', ['$element', '$scope', '$adminService', 'model', '$callback', function ($element, $scope, $adminService, model, $callback) {
            $scope.model = model;
            $scope.model.SellDate = {
                Value: moment().format("YYYY-MM-DD")
            };
            $scope.apply = function (model) {
                $adminService.finishOrder(model).then(function (response) {
                    if (response.Succeeded) {
                        $element.modal('hide');
                        $callback(true);
                    }
                    $scope.commandResult = response;
                });
            };
        }])
        .controller('adminCustomerProFormaController', ['$element', '$scope', '$adminService', 'model', '$callback', function ($element, $scope, $adminService, model, $callback) {
            $scope.model = model;
            $scope.send = function (model) {
                $adminService.sendProForma(model.Id).then(function (response) {
                    if (response.Succeeded) {
                        $element.modal('hide');
                        $callback(true);
                    }
                    $scope.commandResult = response;
                });
            };
        }])
        .controller('adminCustomerPasswordController', ['$element', '$scope', '$adminService', 'model', '$callback', function ($element, $scope, $adminService, model, $callback) {
            $scope.model = model;
            $scope.password = function (model) {
                $adminService.generatePassword(model.Id).then(function (response) {
                    if (response.Succeeded) {
                        $element.modal('hide');
                        $callback(true);
                    }
                    $scope.commandResult = response;
                });
            };
        }])
        ;
})(angular);