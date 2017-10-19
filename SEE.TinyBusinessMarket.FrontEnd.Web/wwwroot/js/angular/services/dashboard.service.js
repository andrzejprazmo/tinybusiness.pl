(function (angular) {
    angular
        .module('dashboard.service', [])
        .service('$dashboardService', ['$http', '$q', function ($http, $q) {

            this.details = function () {
                var deferred = $q.defer();
                return $http.get('Dashboard/Details').then(function (response) {
                    deferred.resolve(response.data);
                    return deferred.promise;
                }, function (response) {
                    deferred.reject(response);
                    return deferred.promise;
                });
            };

        }]);
})(angular);