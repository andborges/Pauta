var pautaApp = angular.module('pautaApp', ['ngRoute', 'ngAnimate', 'mobileit.directives', 'pautaApp.controllers']);

pautaApp.run(function(setup) {
	setup.initialize(
		{
			server: "http://staging.mbl-it.com/pauta", // Staging
			// server = "http://localhost:5992", // Localhost
			// server = "http://192.168.56.1:5992", // GenyMotion localhost

			firstPage: "/",
			
			encryptionInfo: {
								password: "ZskU37awLVsxgMqyS",
								salt: "PautaComponentFramework",
								passwordIterations: 2,
								initialVector: "QSLbm97h@mls87wI",
								keySize: 256 / 32
							}
		}
	);
});