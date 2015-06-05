pautaApp.config(function($routeProvider) {
	$routeProvider
	.when('/', {
		templateUrl : 'pages/auth.html',
		controller  : 'AuthCtrl'
	})
	.when('/classes', {
		templateUrl : 'pages/classes.html',
		controller  : 'ClassesCtrl'
	})
	.when('/class', {
		templateUrl : 'pages/class.html',
		controller  : 'ClassCtrl'
	})
});