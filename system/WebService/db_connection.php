<?php

	function getConnection() {
	
		$dsn = "mysql:host=localhost;dbname=emprestae_db";
		$user = "emprestae";
		$pwd = "emprestaep";

		try {

			$dbh = new PDO($dsn, $user, $pwd);
			$dbh->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

		} catch (PDOException $e) {

			$err = "Connection Failed: " . $e->getMessage() . "<br/>";
			die($err);
		}

		return $dbh;
	}

?>
