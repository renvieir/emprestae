<?php

	/* tables */

	$userTable = "usuario";
	$friendTable = "amizade";
	$filmTable = "objFilme";
	$gameTable = "objJogo";
	$bookTable = "objLivro";
	$loanTable = "emprestimo";

	function getConnection() {

		$dsn = "mysql:host=localhost;dbname=emprestae_db";
		$user = "emprestae";
		$pwd = "emprestaep";

		try {
			// make connection
			$dbh = new PDO($dsn, $user, $pwd);

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

?>
