<?php

$app->get("/getUserObjs/:userID", "getUserObjs");
$app->get("/addUserObj/:userID/:objID/:objTitulo", "addUserObj");
$app->get("/removeUserObj/:userID/:objID/:objTitulo", "removeUserObj");

/* return object ids in a list */
function getUserObjs($userID) {

	$tablesPatrimonio = Array("Livro" => "possuiLivro",
								"Jogo" => "possuiJogo",
								"Filme" => "possuiFilme");

	$dbh = getConnection();
	foreach ($tablesPatrimonio as $obj => $possui) {
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
	$response["status"] = 1;
	echo json_encode($response);
	return;
}

function removeUserObj($userId, $objId, $objTitulo) {

	$tablesPatrimonio = Array("Livro" => "possuiLivro",
								"Jogo" => "possuiJogo",
								"Filme" => "possuiFilme");

	$dbh = getConnection();
	foreach ($tablesPatrimonio as $obj => $possui) {
		$sql = "select * from obj$obj where id$obj = :objID and
													titulo = :objTitulo";
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":objID", $objId);
		$stmt->bindParam(":objTitulo", $objTitulo);
		$stmt->execute();
		$arr = $stmt->fetchAll(PDO::FETCH_NUM);
		if(!empty($arr)) break;
	}

	/* object is not in the stock */
	if(empty($arr)) {
		closeConnection($dbh);
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	$sql = "delete from $possui where fk_idUser = :userID and
														fk_id$obj = :objID";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":userID", $userId);
	$stmt->bindParam(":objID", $objId);
	$stmt->execute();

	closeConnection($dbh);
	$response["status"] = 1;
	echo json_encode($response);
	return;
}

?>
