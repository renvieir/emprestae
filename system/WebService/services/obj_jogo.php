<?php

$app->get("/createGame/:titulo/:plataforma/:produtora", "createObjGame");
$app->get("/updateGame/:titulo/:plataforma/:produtora", "updateObjGame");
$app->get("/getGameInfo/:titulo", "getObjGameInfo");
$app->get("/removeGame/:titulo", "removeObjGame");

function createObjGame($titulo, $plataforma, $produtora) {

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
	$dbh = getConnection();
	$sql = "insert into objJogo (titulo, plataforma, produtora) values
												(:title, :platform, :house)";

	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":title", $title);
		$stmt->bindParam(":platform", $platform);
		$stmt->bindParam(":house", $house);
		$stmt->execute();
	} catch (PDOException $e) {
		echo json_encode(returnMsg("status", 0));
		return;
	}

	closeConnection($dbh);
	echo json_encode(returnMsg("status", 1));
	return;
}

function updateObjGame($titulo, $plataforma, $produtora) {

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
	$dbh = getConnection();
	$sql = "update objJogo set titulo = :title, plataforma = :platform,
								produtora = :house where titulo = :title";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->bindParam(":platform", $platform);
	$stmt->bindParam(":house", $house);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode(returnMsg("status", 1));
	return;
}

function getObjGameInfo($titulo) {

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
	$sql = "select * from objJogo where titulo = :title";
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
	$dbh = getConnection();
	$sql = "delete from objJogo where titulo = :title";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode(returnMsg("status", 1));
	return;
}

?>
