(function (angular) {
    angular
        .module('admin.service', [])
        .service('$adminService', ['$http', '$q', function ($http, $q) {

            this.customers = function (request) {
                var deferred = $q.defer();
                return $http.post('Admin/Customers', request).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

            this.customerDetails = function (customerId) {
                var deferred = $q.defer();
                return $http.get('Admin/CustomerDetails', { params: { customerId: customerId } }).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

            this.customerOrders = function (customerId) {
                var deferred = $q.defer();
                return $http.get('Admin/CustomerOrders', { params: { customerId: customerId } }).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

            this.customerLicences = function (customerId) {
                var deferred = $q.defer();
                return $http.get('Admin/CustomerLicences', { params: { customerId: customerId } }).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

            this.finishOrder = function (model) {
                var deferred = $q.defer();
                return $http.post('Admin/FinishOrder', model).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

            this.sendProForma = function (proFormaId) {
                var deferred = $q.defer();
                return $http.post('Admin/SendProForma', { Id: proFormaId }).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

            this.generatePassword = function (customerId) {
                var deferred = $q.defer();
                return $http.post('Admin/CustomerPassword', { Id: customerId } ).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

        }]);
})(angular);