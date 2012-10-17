<?php

$app->get("/getUserObjs/:userID", "getUserObjs");
$app->post("/addUserObj", "addUserObj");
$app->delete("/removeUserObj", "removeUserObj");

/* return object ids in a list */
function getUserObjs($userID) {

	global $patrimonio;

	$response["status"] = 1;
	$dbh = getConnection();

	foreach ($patrimonio as $data) {
	
		$objType = $data[0]; $possui = $data[1];
		$table = "obj$objType";
		$sql = "select $table.* from $table left join $possui on
						fk_idUser = :userID where fk_id$objType = id$objType";

		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":userID", $userID);
		$stmt->execute();

		$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
		$response[$objType] = storeElements("object", $tmp);
		if ($response[$objType])
			$response["status"] = 1;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function removeUserObj() {

	global $patrimonio;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$userId = $json->userId; $objId = $json->objId; $objType = $json->objType;

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

function addUserObj() {

	global $patrimonio;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$userId = $json->userId; $objId = $json->objId; $objType = $json->objType;

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
		$sql = "insert into $possui select idusuario, id$obj from
					usuario, obj$obj where idusuario = :userID and
						id$obj = :objID and (select count(*) from $possui where
							fk_idUser = :userID and fk_id$obj = :objID) = 0";
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
