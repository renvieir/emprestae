<?php

$app->get("/getUserObjs/:userID", "getUserObjs");
$app->get("/addUserObj/:userID/:objID/:objType", "addUserObj");
$app->get("/removeUserObj/:userID/:objID/:objType", "removeUserObj");

/* return object ids in a list */
function getUserObjs($userID) {

	$patrimonio = Array("A" => ["Livro", "possuiLivro"],
						"B" => ["Jogo", "possuiJogo"],
						"C" => ["Filme", "possuiFilme"]);

	$response["status"] = 1;
	$dbh = getConnection();

	foreach ($patrimonio as $p) {
		$obj = $p[0]; $possui = $p[1];
		$sql = "select * from $possui where fk_idUser = :userID";
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":userID", $userID);
		$stmt->execute();
		$objIDs = Array();
		$i = 0;
		/* FETCH_NUM make the array indexed */
		while ($row = $stmt->fetch(PDO::FETCH_NUM))
			$objIDs[$i++] = $row[1];
		if (!empty($objIDs))
			$response[$obj] = array_values($objIDs);
		else
			$response[$obj] = null;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function removeUserObj($userId, $objId, $objTitulo) {

	$patrimonio = Array("A" => ["Livro", "possuiLivro"],
						"B" => ["Jogo", "possuiJogo"],
						"C" => ["Filme", "possuiFilme"]);

	$objType = strtoupper($objType);
	/* the objType doesn't exist */
	if ( !isset($patrimonio[$objType]) ) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	$response["status"] = 1;
	$dbh = getConnection();
	$obj = $patrimonio[$objType][0];
	$possui = $patrimonio[$objType][1];

	$sql = "delete from $possui where fk_idUser = :userID and
														fk_id$obj = :objID";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":userID", $userId);
	$stmt->bindParam(":objID", $objId);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function addUserObj($userId, $objId, $objType) {

	$patrimonio = Array("A" => ["Livro", "possuiLivro"],
						"B" => ["Jogo", "possuiJogo"],
						"C" => ["Filme", "possuiFilme"]);

	$objType = strtoupper($objType);
	/* the objType doesn't exist */
	if ( !isset($patrimonio[$objType]) ) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	$response["status"] = 1;
	$dbh = getConnection();
	$obj = $patrimonio[$objType][0];
	$possui = $patrimonio[$objType][1];

	try {
		/* if the ids exist in their tables and are not linked, add a new row */
		$sql = "insert into $possui select idusuario, idJogo from
					usuario, obj$obj where idusuario = :userID and
						id$obj = :objID and (select count(*) from $possui where
							fk_idUser = :userID and fk_idJogo = :objID) = 0";
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":userID", $userId);
		$stmt->bindParam(":objID", $objId);
		$stmt->execute();
	} catch (PDOException $e) { /* couldn't insert */
		$response["status"] = 0;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

?>
