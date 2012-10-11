<?php

$app->get("/createFilm/:titulo/:diretor/:distribuidora", "createObjFilm");
$app->get("/updateFilm/:titulo/:diretor/:distribuidora", "updateObjFilme");
$app->get("/getFilmInfo/:titulo", "getObjFilmInfo");
$app->get("/removeFilm/:titulo", "removeObjFilm");

function createObjFilm($titulo, $diretor, $distribuidora) {

	global $filmTable;

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

	$title = $titulo; $director = $diretor; $house = $distribuidora;
	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "insert into $filmTable (titulo, diretor, distribuidora) values
												(:title, :director, :house)";

	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":title", $title);
		$stmt->bindParam(":director", $director);
		$stmt->bindParam(":house", $house);
		$stmt->execute();
	} catch (PDOException $e) {
		$response["status"] = 0;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function updateObjFilm($titulo, $diretor, $distribuidora) {

	global $filmTable;

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

	$title = $titulo; $director = $diretor; $house = $distribuidora;
	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "update $filmTable set titulo = :title, diretor = :director,
								distribuidora = :house where titulo = :title";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->bindParam(":director", $director);
	$stmt->bindParam(":house", $house);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function getObjFilmInfo($titulo) {

	global $filmTable;

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
	$sql = "select * from $filmTable where titulo = :title";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->execute();

	/* get all Film information as a associative array */
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

function removeObjFilm($titulo) {

	global $filmTable;

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
	$sql = "delete from $filmTable where titulo = :title";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

?>

