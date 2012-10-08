<?php

	function getConnection() {

		$dsn = "mysql:host=localhost;dbname=emprestae_db";
		$user = "emprestae";
		$pass = "emprestaep";

		try {
			// make connection
			$dbh = new PDO($dsn, $user, $pass);

			// set the error attribute to be handled
			$dbh->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

		} catch (PDOException $e) {
			echo "Conection failed: " . $e->getMessage() . "\n";
			die();
		}

		return $dbh;
	}

	function closeConnection($dbh) {

		$dbh = NULL;
	}

	function returnMsg($key, $value) {
	
		$response[$key] = $value;
		return $response;
	}
?>
