(function (angular) {
    angular
        .module('customer.service', [])
        .service('$customerService', ['$http', '$q', function ($http, $q) {

            this.register = function (model) {
                var deferred = $q.defer();
                return $http.post('Customer/Register', model).then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

            this.profile = function () {
                var deferred = $q.defer();
                return $http.get('Customer/Details').then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };
        }]);
})(angular);