<?php

$app->get("/makeFriends/:id1/:id2", "createFriendship");
$app->get("/rmFriends/:id1/:id2", "deleteFriendship");
$app->get("/getFriends/:id1", "getFriends");

function createFriendship($i1, $i2) {
	
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

	$id1 = $i1; $id2 = $i2;
	$dbh = getConnection();

	/* check if the friendship already exists */
	$sql = "SELECT * FROM amizade where
							(idusuario_a = :id1 and idusuario_b = :id2) or
							(idusuario_a = :id2 and idusuario_b = :id1)";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->bindParam(":id2", $id2);
	$tmp = $stmt->execute();
	$arr = $stmt->fetchAll();
	if (count($arr) != 0) {
		echo json_encode(returnMsg("status", $status));
		return;
	}

	/* add friendship if not exists */
	$sql = "INSERT INTO amizade (idusuario_a, idusuario_b) VALUE
															(:id1, :id2)";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->bindParam(":id2", $id2);

	$tmp = $stmt->execute();
	if (!$tmp) { // couldn't insert
		echo json_encode(returnMsg("status", $status));
		return;
	}

	$status = 1;
	$json = json_encode(returnMsg("status", $status));

	closeConnection($dbh);
	echo $json;
}

function deleteFriendship($i1, $i2) {
	
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

	$id1 = $i1; $id2 = $i2;

	$dbh = getConnection();

	$sql = "DELETE FROM amizade where idusuario_a = :id1 and idusuario_b= :id2";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->bindParam(":id2", $id2);

	print_r($stmt);
	$tmp = $stmt->execute();
	if (!$tmp) { // couldn't delete the row
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$status = 1;
	$response["status"] = $status;
	$json = json_encode($response);

	closeConnection($dbh);
	echo $json;
}

function getFriends($i1) {

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

	$id1 = $i1;
	$dbh = getConnection();
	$sql = "SELECT IF(idusuario_a = :id1, idusuario_b, idusuario_a) as friend
				FROM amizade where idusuario_a = :id1 or idusuario_b = :id1";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->execute();

	$arr = Array(); $i = 0;
	while ($friendId = $stmt->fetch(PDO::FETCH_ASSOC))
		$arr[$i++] = $friendId["friend"];

	if(empty($arr)) {
		$response["status"] = 0;
		$json = json_encode($response);
		closeConnection($dbh);
		echo $json;
		return;
	}

	$response["friends"] = $arr;
	$response["status"] = 1;
	$json = json_encode($response);

	closeConnection($dbh);
	echo $json;
}

?>
