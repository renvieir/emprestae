<?php

$app->post("/createFilm", "createObjFilm");
$app->put("/updateFilm", "updateObjFilm");
$app->get("/getFilmInfo/:appID/:data/:iv", "getObjFilmInfo");
$app->get("/getAllFilms", "getAllObjFilms");
$app->get("/getSimilarFilms/:appID/:data/:iv", "getSimilarObjFilms");
$app->delete("/removeFilm", "removeObjFilm");

function createObjFilm() {

	global $filmTable;

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

	$title = $json->titulo; $director = $json->diretor;
	$house = $json->distribuidora;
	//$imPath = createImage($title, $json->image, false);
	$imPath = null;

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "insert into $filmTable (titulo, diretor, distribuidora, imagePath)
								values (:title, :director, :house, :imPath)";
	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":title", $title);
		$stmt->bindParam(":director", $director);
		$stmt->bindParam(":house", $house);
		$stmt->bindParam(":imPath", $imPath);
		$stmt->execute();
	} catch (PDOException $e) {
		$response["status"] = 0;
	}

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function updateObjFilm() {

	global $filmTable;

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

	$title = $json->titulo; $director = $json->diretor;
	$house = $json->distribuidora; $id = $json->idFilme;
	//$imPath = createImage($title, $json->image, false);
	$imPath = null;

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "update $filmTable set titulo = :title, diretor = :director,
			distribuidora = :house, imagePath = :imPath where idFilme = :id";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->bindParam(":director", $director);
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

function getAllObjFilms() {

	global $filmTable;

	$response["status"] = 0;
	$dbh = getConnection();

	$sql = "select * from $filmTable";
	$stmt = $dbh->prepare($sql);
	$stmt->execute();

	/* get user information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_ASSOC);
	$response["filmes"] = storeElements("filme", $tmp);
	if ($response["filmes"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getSimilarObjFilms($appID, $data, $iv) {

	global $filmTable;

	$json = json_decode(decrypt_data($appID, $data, $iv));
	$titulo = $json->titulo;
	$response["status"] = 0;

	$dbh = getConnection();
	$sql = "select * from $filmTable where titulo like '%$titulo%' ";
	$stmt = $dbh->prepare($sql);
	$stmt->execute();

	/* get user information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_ASSOC);
	$response["filmes"] = storeElements("filme", $tmp);
	if ($response["filmes"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getObjFilmInfo($appID, $data, $iv) {

	global $filmTable;

	$json = json_decode(decrypt_data($appID, $data, $iv));
	$id = $json->idFilme;
	$response["status"] = 0;

	$dbh = getConnection();
	$sql = "select * from $filmTable where idFilme = :id";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	/* get all Film information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["filmes"] = storeElements("filme", $tmp);
	if ($response["filmes"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function removeObjFilm() {

	global $filmTable;

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

	$id = $json->idFilme;

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "delete from $filmTable where idFilme = :id";
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
