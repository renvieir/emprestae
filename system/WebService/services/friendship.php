<?php

$app->post("/requestFriend", "requestFriendship");
$app->put("/acceptFriend", "addFriendship");
$app->delete("/removeFriends", "deleteFriendship");
$app->get("/getFriends/:id1", "getFriends");
$app->get("/getFriendsRequest/:id1", "getFriendsRequest");

function requestFriendship() {
	
	global $friendTable, $userTable;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$id1 = $json->id1; $id2 = $json->id2;

	$response["status"] = 1;
	$dbh = getConnection();

	try {
		/* if the ids exist and are not friends, add a new friendship */
		$sql = "insert into $friendTable (idusuario_a, idusuario_b) select
					A.idusuario, B.idusuario from $userTable A, $userTable B
						where A.idusuario = :id1 and B.idusuario = :id2 and
							(select	count(*) from $friendTable where
								(idusuario_a = :id1 and idusuario_b = :id2) or
								(idusuario_a = :id2 and idusuario_b = :id1)
							) = 0";

		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":id1", $id1);
		$stmt->bindParam(":id2", $id2);
		$stmt->execute();
	} catch (PDOException $e) { /* couldn't insert */
		$response["status"] = 0;
	}

	closeConnection($dbh);
	echo json_encode($response);
}

function addFriendship () {

	global $friendTable, $userTable;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$id1 = $json->id1; $id2 = $json->id2;

	$response["status"] = 1;
	$dbh = getConnection();

	try {
		/* if the ids exist and are not friends, add a new friendship */
		$sql = "update $friendTable set status = 1 where (idusuario_a = :id1 and
					idusuario_b = :id2) or (idusuario_a = :id2 and
													idusuario_b = :id1)";

		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":id1", $id1);
		$stmt->bindParam(":id2", $id2);
		$stmt->execute();
	} catch (PDOException $e) { /* couldn't insert */
		$response["status"] = 0;
	}

	closeConnection($dbh);
	echo json_encode($response);
}

function deleteFriendship() {
	
	global $friendTable, $userTable;
	
	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$id1 = $json->id1; $id2 = $json->id2;

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "DELETE FROM $friendTable where (idusuario_a = :id1 and
				idusuario_b = :id2) or (idusuario_a = :id2 and
														idusuario_b = :id1)";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->bindParam(":id2", $id2);

	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
}

function getFriends($id1) {

	global $friendTable;
	
	$response["status"] = 0;
	$dbh = getConnection();

	$sql = "SELECT idusuario, nome, email, addressLat, addressLong, imagePath
				from usuario, (SELECT idusuario_b AS friend FROM $friendTable
					WHERE idusuario_a = :id1 and status = 1 UNION SELECT
						idusuario_a AS friend FROM $friendTable WHERE
							idusuario_b = :id1 and status = 1) as tmp WHERE
													idusuario = tmp.friend";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->execute();

	/* get user information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_ASSOC);
	$response["users"] = storeElements("user", $tmp);
	if ($response["users"])
		$response["status"] = 1;

	closeConnection($dbh);
	echo json_encode($response);
}

function getFriendsRequest($id1) {

	global $friendTable;
	
	$response["status"] = 0;
	$dbh = getConnection();

	$sql = "SELECT idusuario, nome, email, addressLat, addressLong, imagePath
				from usuario, (SELECT idusuario_b AS friend FROM $friendTable
					WHERE idusuario_a = :id1 and status = 1 UNION SELECT
						idusuario_a AS friend FROM $friendTable WHERE
							idusuario_b = :id1 and status = 0) as tmp WHERE
													idusuario = tmp.friend";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->execute();

	/* get user information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_ASSOC);
	$response["users"] = storeElements("user", $tmp);
	if ($response["users"])
		$response["status"] = 1;

	closeConnection($dbh);
	echo json_encode($response);
}

?>
