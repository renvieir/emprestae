<?php

$app->get("/makeFriends/:id1/:id2", "createFriendship");
$app->get("/removeFriends/:id1/:id2", "deleteFriendship");
$app->get("/getFriends/:id1", "getFriends");

function createFriendship($i1, $i2) {
	
	global $friendTable, $userTable;

	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$id1 = $json->friend1;
	$id2 = $json->friend2;
	*/

	$response["status"] = 1;
	$dbh = getConnection();
	$id1 = $i1; $id2 = $i2;

	try {
		/* if the ids exist and are not friends, add a new friendship */
		$sql = "insert into $friendTable (idusuario_a, idusuario_b) select
					A.idusuario, B.idusuario from $userTable A, $userTable B
						where A.idusuario = :id1 and B.idusuario = :id2 and
							(select	count(*) from $friendTable where
								(idusuario_a = :id1 and idusuario_b = :id2) or
								(idusuario_a = :id2 and idusuario_b = :id1))=0";

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

function deleteFriendship($i1, $i2) {
	
	global $friendTable;
	
	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$id1 = $json->friend1;
	$id2 = $json->friend2;
	*/

	$response["status"] = 1;
	$dbh = getConnection();
	$id1 = $i1; $id2 = $i2;

	$sql = "DELETE FROM $friendTable where idusuario_a = :id1 and
														idusuario_b = :id2";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->bindParam(":id2", $id2);

	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
}

function getFriends($i1) {

	global $friendTable;
	
	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$id1 = $json->friend1;
	*/

	$response["status"] = 1;
	$dbh = getConnection();
	$id1 = $i1;

	$sql = "SELECT idusuario_b AS friend FROM $friendTable WHERE
				idusuario_a = :id1 UNION SELECT idusuario_a AS friend
					FROM $friendTable WHERE idusuario_b = :id1";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->execute();

	$arr = Array(); $i = 0;
	while ($friendId = $stmt->fetch(PDO::FETCH_ASSOC))
		$arr[$i++] = $friendId["friend"];

	$response["friends"] = (empty($arr)) ? null : $arr;
	closeConnection($dbh);
	echo json_encode($response);
}

?>
