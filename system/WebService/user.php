<?php

	require "connection.php";

	$app->post("createUser", "createUser");
	$app->post("login", "login");

	/* return status if the user was created */
	function createUser() {
	
		$request = Slim::getInstance()->request();
		$body = $request->getBody();
	}

	/* return status and user ID */
	function login() {
	
		$status = 1;
		$userID = null;
		$param = null;

		$request = Slim::getInstance()->request();
		$body = $request->getBody();
		if ($body)
			$param = json_decode($body);

		if ($param) {
			$user = $param->user;
			$pwd = $param->pass;
			$table = "usuario";

			$sql = "SELECT FROM ?";
			$dbh = getConnection();
		}
	}
?>
