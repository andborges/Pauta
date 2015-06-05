'use strict';

angular.module('pautaApp.controllers', ['mobileit.directives'])
    .controller('AuthCtrl', ['$scope', '$rootScope', 'api', 'crypt', function ($scope, $rootScope, api, crypt) {
		$scope.username = "andborges";
		$scope.password = "1q2w3e4r";

		$scope.login = function() {
			$scope.message = "";

			var encryptedInfo = crypt.encrypt($scope.username + "|" + $scope.password);
			
			api.call("auth", "EncryptedInfo=" + encodeURIComponent(encryptedInfo), { stopLoading: false }, function(data) {
				if (data.SessionId) {
					$rootScope.sessionId = data.SessionId;
					$rootScope.mbl.screen.go('/classes');
				} else {
					$scope.message = "Usuário ou senha inválido";
				}
			});
		}
    }])
	.controller('ClassesCtrl', ['$scope', '$rootScope', 'api', function ($scope, $rootScope, api) {
		api.call("class", "date=12/25/2013", { beginLoading: false }, function(data) {
			$scope.classes = data;
			$scope.ready = true;

			$rootScope.mbl.page.refreshScroll();
		});
		
		$scope.showClass = function(classId, discipline, classRoom) {
			$rootScope.classId = classId;
			$rootScope.discipline = discipline;
			$rootScope.classRoom = classRoom;
			$rootScope.mbl.screen.go('/class');
		}
    }])
	.controller('ClassCtrl', ['$scope', '$rootScope', 'api', 'crypt', function ($scope, $rootScope, api, crypt) {
		api.call("class/" + $rootScope.classId, null, {}, function(data) {
			$scope.myclass = data;
			$scope.ready = true;

			$rootScope.mbl.page.refreshScroll();
		});
		
		$scope.save = function() {
			var attendances = "";

			angular.forEach($scope.myclass.Attendances, function (attendance, i) {
				var value = attendance.Attendant ? "1" : "0";
				attendances += attendance.StudentNumber + ":" + value + ";";
			});
			
			api.call("class/save/" + $rootScope.classId, "attendances=" + encodeURIComponent(crypt.encrypt(attendances)), {}, function(data) {
				if(data) {
					navigator.notification.alert("A chamada foi salva com sucesso", null, "Chamada salva");
				}
			});
		}
	}]);