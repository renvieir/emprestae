<?php

$app->get("/createBook/:titulo/:autor/:edicao/:editora", "createObjBook");
$app->get("/updateBook/:titulo/:autor/:edicao/:editora", "updateObjBook");
$app->get("/getBookInfo/:titulo", "getObjBookInfo");
$app->get("/removeBook/:titulo", "removeObjBook");

function createObjBook($titulo, $autor, $edicao, $editora) {

	global $boojTable;

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
	$author = $json->author;
	$ed = $json->ed;
	$house = $json->house;
	*/

	$title = $titulo; $author = $autor; $ed = $edicao; $house = $editora;
	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "insert into $bookTable (titulo, autor, edicao, editora) values
												(:title, :author, :ed, :house)";

	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":title", $title);
		$stmt->bindParam(":author", $author);
		$stmt->bindParam(":ed", $ed);
		$stmt->bindParam(":house", $house);
		$stmt->execute();
	} catch (PDOException $e) {
		$response["status"] = 0;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function updateObjBook($titulo, $autor, $edicao, $editora) {

	global $boojTable;

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
	$author = $json->author;
	$ed = $json->ed;
	$house = $json->house;
	*/

	$title = $titulo; $author = $autor; $ed = $edicao; $house = $editora;
	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "update $bookTable set titulo = :title, autor = :author, edicao= :ed,
									editora = :house where titulo = :title";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->bindParam(":author", $author);
	$stmt->bindParam(":ed", $ed);
	$stmt->bindParam(":house", $house);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function getObjBookInfo($titulo) {

	global $boojTable;

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
	$sql = "select * from $bookTable where titulo = :title";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->execute();

	/* get all book information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS)[0];

	if (!empty($tmp)) {
		foreach ($tmp[0] as $key => $value)
			$response[$key] = ($value) ? $value: null;
		$response["status"] = 1;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function removeObjBook($titulo) {

	global $boojTable;

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
	$sql = "delete from $bookTable where titulo = :title";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

?>
