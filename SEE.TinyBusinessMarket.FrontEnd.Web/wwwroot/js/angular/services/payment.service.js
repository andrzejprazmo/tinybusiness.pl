(function (angular) {
    angular
        .module('payment.service', [])
        .service('$paymentService', ['$http', '$q', function ($http, $q) {

            this.add = function (productId) {
                var deferred = $q.defer();
                return $http.get('Payment/Add', { params: { productId: productId } }).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

            this.payu = function (model) {
                var deferred = $q.defer();
                return $http.post('Payment/Start', model).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

            this.remitance = function (model) {
                var deferred = $q.defer();
                return $http.post('Payment/Remitance', model).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

            this.product = function (productId) {
                var deferred = $q.defer();
                return $http.get('Product/Details', { params: { productId: productId } }).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

        }]);
})(angular);