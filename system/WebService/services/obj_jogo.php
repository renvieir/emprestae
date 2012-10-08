<?php

$app->get("/createGame/:titulo/:plataforma/:produtora", "createObjGame");
$app->get("/updateGame/:titulo/:plataforma/:produtora", "updateObjGame");
$app->get("/getGameInfo/:titulo", "getObjGameInfo");
$app->get("/removeGame/:titulo", "removeObjGame");

function createObjGame($titulo, $plataforma, $produtora) {
	
	global $gameTable;

	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$title = $json->title;
	$director = $json->director;
	$ed = $json->ed;
	$house = $json->house;
	*/

	$title = $titulo; $platform = $plataforma; $house = $produtora;
	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "insert into $gameTable (titulo, plataforma, produtora) values
												(:title, :platform, :house)";

	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":title", $title);
		$stmt->bindParam(":platform", $platform);
		$stmt->bindParam(":house", $house);
		$stmt->execute();
	} catch (PDOException $e) {
		$response["status"] = 0;
		return;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function updateObjGame($titulo, $plataforma, $produtora) {

	global $gameTable;

	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$title = $json->title;
	$director = $json->director;
	$ed = $json->ed;
	$house = $json->house;
	*/

	$title = $titulo; $platform = $plataforma; $house = $produtora;
	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "update $gameTable set titulo = :title, plataforma = :platform,
									produtora = :house where titulo = :title";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->bindParam(":platform", $platform);
	$stmt->bindParam(":house", $house);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function getObjGameInfo($titulo) {

	global $gameTable;

	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$title = $json->title;
	*/

	$response["status"] = 0;

	$title = $titulo;
	$dbh = getConnection();
	$sql = "select * from $gameTable where titulo = :title";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->execute();

	/* get all Game information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	if (!empty($tmp)) {
		foreach ($tmp[0] as $key => $value)
			$response[$key] = ($value) ? $value: null;
		$response["status"] = 1;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function removeObjGame($titulo) {

	global $gameTable;

	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$title = $json->title;
	*/

	$title = $titulo;
	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "delete from $gameTable where titulo = :title";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

?>
