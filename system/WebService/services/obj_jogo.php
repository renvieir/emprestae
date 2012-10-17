<?php

$app->post("/createGame", "createObjGame");
$app->put("/updateGame", "updateObjGame");
$app->get("/getGameInfo/:id", "getObjGameInfo");
$app->get("/getAllGames/:id", "getAllObjGames");
$app->delete("/removeGame", "removeObjGame");

function createObjGame() {
	
	global $gameTable;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$title = $json->titulo; $platform = $json->plataforma;
	$house = $json->produtora;
	$imPath = createImage($title, $json->image, false);

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "insert into $gameTable (titulo, plataforma, produtora, imagePath)
								values (:title, :platform, :house, :imPath)";
	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":title", $title);
		$stmt->bindParam(":platform", $platform);
		$stmt->bindParam(":house", $house);
		$stmt->bindParam(":imPath", $imPath);
		$stmt->execute();
	} catch (PDOException $e) {
		$response["status"] = 0;
		return;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function updateObjGame() {

	global $gameTable;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$title = $json->titulo; $platform = $json->plataforma;
	$house = $json->produtora; $id = $json->idJogo;
	$imPath = createImage($title, $json->image, false);

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "update $gameTable set titulo = :title, plataforma = :platform,
				produtora = :house, imagePath = :imPath where idJogo = :id";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->bindParam(":platform", $platform);
	$stmt->bindParam(":house", $house);
	$stmt->bindParam(":imPath", $imPath);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function getObjGameInfo($id) {

	global $gameTable;

	$response["status"] = 0;
	$dbh = getConnection();

	$sql = "select * from $gameTable where idJogo = :id";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	/* get all Game information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["objects"] = storeElements("object", $tmp);
	if ($response["objects"])
		$response["status"] = 1;

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function getAllObjGames($id) {

	global $gameTable;

	$response["status"] = 0;
	$dbh = getConnection();

	$sql = "select * from $gameTable where idJogo != :id";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	/* get user information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_ASSOC);
	$response["objects"] = storeElements("object", $tmp);
	if ($response["objects"])
		$response["status"] = 1;

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function removeObjGame() {

	global $gameTable;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$id = $json->idJogo;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "delete from $gameTable where idJogo = :id";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

?>
