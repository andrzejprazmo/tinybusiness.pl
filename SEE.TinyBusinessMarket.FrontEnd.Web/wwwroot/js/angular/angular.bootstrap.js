(function (angular) {
    angular
        .module('tinyBusiness', [
            'ngRoute',
            'ngLocale',
            'cgBusy',
            'ui.mask',
            'ngTemplate',
            'modal.dialog',
            'customer.controller',
            'dashboard.controller',
            'payment.controller',
            'admin.controller'
        ])
        .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            var template = function (path) {
                return path + '?v=' + Date.now();
            };
            $routeProvider
                .when('/dashboard', { templateUrl: template('html/dashboard/index.html'), controller: 'dashboardController' })
                .when('/customer/register', { templateUrl: template('html/customer/register.html'), controller: 'customerRegisterController' })
                .when('/payment/:productId/start', { templateUrl: template('html/payment/start.html'), controller: 'paymentController' })
                .when('/payment/:productId/completed', { templateUrl: template('html/payment/completed.html'), controller: 'paymentCompletedController' })
                .when('/profile', { templateUrl: template('html/customer/profile.html'), controller: 'profileController' })
                .when('/admin', { templateUrl: template('html/admin/index.html'), controller: 'adminController' })
                .when('/admin/customers', { templateUrl: template('html/admin/customers.html'), controller: 'adminCustomersController' })
                .when('/admin/customers/details/:customerId', { templateUrl: template('html/admin/customer.details.html'), controller: 'adminCustomerDetailsController' })
                .when('/', { redirectTo: '/dashboard' });
            $locationProvider.hashPrefix('');
        }])
        .run(['$rootScope', '$q', function($rootScope, $q) {

        }])
        ;
})(angular);