(function (angular) {
    angular.module('ngTemplate', []).directive('ngTemplate', ['$templateRequest', '$compile', function ($templateRequest, $compile) {
        'use strict';
        return {
            restrict: 'E',
            scope: {
                src: '=src',
            },
            transclude: true,
            compile: function (scope, element, attr) {
                scope.src = scope.src + '?v=' + Date.now();
                return function (scope, $element) {
                    var templateUrl = scope.src + '?v=' + Date.now();
                    $templateRequest(templateUrl, true).then(function (response) {
                        $element.html($compile(response)(scope.$parent));
                    });
                };
            },
        };
    }])
})(angular);