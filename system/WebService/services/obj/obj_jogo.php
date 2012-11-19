<?php

$app->post("/createGame", "createObjGame");
$app->put("/updateGame", "updateObjGame");
$app->get("/getGameInfo/:appID/:data/:iv", "getObjGameInfo");
$app->get("/getAllGames", "getAllObjGames");
$app->get("/getSimilarGames/:appID/:data/:iv", "getSimilarObjGames");
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

	$appID = $json->appID;
	$crypt_data = $json->data;
	$iv = $json->iv;
	$json = json_decode(decrypt_data($appID, $crypt_data, $iv));

	$title = $json->titulo; $platform = $json->plataforma;
	$house = $json->produtora;
	//$imPath = createImage($title, $json->image, false);
	$imPath = null;

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
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

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

	$appID = $json->appID;
	$crypt_data = $json->data;
	$iv = $json->iv;
	$json = json_decode(decrypt_data($appID, $crypt_data, $iv));

	$title = $json->titulo; $platform = $json->plataforma;
	$house = $json->produtora; $id = $json->idJogo;
	//$imPath = createImage($title, $json->image, false);
	$imPath = null;

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
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getObjGameInfo($appID, $data, $iv) {

	global $gameTable;

	$json = json_decode(decrypt_data($appID, $data, $iv));
	$id = $json->idJogo;
	$response["status"] = 0;

	$dbh = getConnection();
	$sql = "select * from $gameTable where idJogo = :id";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	/* get all Game information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["jogos"] = storeElements("jogo", $tmp);
	if ($response["jogos"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getAllObjGames() {

	global $gameTable;

	$response["status"] = 0;
	$dbh = getConnection();

	$sql = "select * from $gameTable";
	$stmt = $dbh->prepare($sql);
	$stmt->execute();

	/* get user information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_ASSOC);
	$response["jogos"] = storeElements("jogo", $tmp);
	if ($response["jogos"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getSimilarObjGames($appID, $data, $iv) {

	global $gameTable;
	
	$json = json_decode(decrypt_data($appID, $data, $iv));
	$titulo = $json->titulo;
	$response["status"] = 0;

	$dbh = getConnection();
	$sql = "select * from $gameTable where titulo like '%$titulo%'";
	$stmt = $dbh->prepare($sql);
	$stmt->execute();

	/* get user information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_ASSOC);
	$response["jogos"] = storeElements("jogo", $tmp);
	if ($response["jogos"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

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

	$appID = $json->appID;
	$crypt_data = $json->data;
	$iv = $json->iv;
	$json = json_decode(decrypt_data($appID, $crypt_data, $iv));

	$id = $json->idJogo;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "delete from $gameTable where idJogo = :id";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

?>
