(function (angular) {
    angular
        .module('modal.dialog', [])
        .factory('ModalDialog', ['$animate', '$document', '$compile', '$controller', '$http', '$templateRequest', '$q', '$rootScope', function ($animate, $document, $compile, $controller, $http, $templateRequest, $q, $rootScope) {
            function ModalDialog() {
                var self = this;

                var getTemplate = function getTemplate(template, templateUrl) {
                    var deferred = $q.defer();
                    if (template) {
                        deferred.resolve(template);
                    } else if (templateUrl) {
                        templateUrl = templateUrl + '?v=' + Date.now();
                        $templateRequest(templateUrl, true).then(function (template) {
                            deferred.resolve(template);
                        }, function (error) {
                            deferred.reject(error);
                        });
                    } else {
                        deferred.reject("No template or templateUrl has been specified.");
                    }
                    return deferred.promise;
                };

                self.show = function (options) {
                    var deferred = $q.defer();
                    getTemplate(options.template, options.templateUrl).then(function (template) {
                        var modalScope = (options.scope || $rootScope).$new();

                        var inputs = {
                            $scope: modalScope,
                        };
                        if (options.inputs) angular.extend(inputs, options.inputs);

                        var linkFn = $compile(template);
                        var modalElement = linkFn(modalScope);
                        inputs.$element = modalElement;
                        inputs.$callback = options.callback;

                        var modalController = $controller(options.controller, inputs, false, options.controllerAs);
                        $(modalElement).modal();

                    }).then(null, function (error) {
                        deferred.reject(error);
                    });
                    return deferred.promise;

                };
            };

            return new ModalDialog();
        }]);
    ;
})(angular);