<?php

$app->get("/createBook/:titulo/:autor/:edicao/:editora", "createObjBook");
$app->get("/updateBook/:titulo/:autor/:edicao/:editora", "updateObjBook");
$app->get("/getBookInfo/:titulo", "getObjBookInfo");
$app->get("/removeBook/:titulo", "removeObjBook");

function createObjBook($titulo, $autor, $edicao, $editora) {

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
	$dbh = getConnection();
	$sql = "insert into objLivro (titulo, autor, edicao, editora) values
												(:title, :author, :ed, :house)";

	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":title", $title);
		$stmt->bindParam(":author", $author);
		$stmt->bindParam(":ed", $ed);
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

function updateObjBook($titulo, $autor, $edicao, $editora) {

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
	$dbh = getConnection();
	$sql = "update objLivro set titulo = :title, autor = :author, edicao = :ed,
									editora = :house where titulo = :title";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->bindParam(":author", $author);
	$stmt->bindParam(":ed", $ed);
	$stmt->bindParam(":house", $house);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode(returnMsg("status", 1));
	return;
}

function getObjBookInfo($titulo) {

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
	$sql = "select * from objLivro where titulo = :title";
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
	$sql = "delete from objLivro where titulo = :title";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode(returnMsg("status", 1));
	return;
}

?>
