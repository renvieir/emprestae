<?php

$app->post("/createBook", "createObjBook");
$app->put("/updateBook", "updateObjBook");
$app->get("/getBookInfo/:appID/:data/:iv", "getObjBookInfo");
$app->get("/getAllBooks", "getAllObjBooks");
$app->get("/getSimilarBooks/:appID/:data/:iv", "getSimilarObjBooks");
$app->delete("/removeBook", "removeObjBook");

function createObjBook() {

	global $bookTable;

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

	$title = $json->titulo; $author = $json->autor; $ed = $json->edicao;
	$house = $json->editora;
	//$imPath = createImage($title, $json->image, false);
	$imPath = null;

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "insert into $bookTable (titulo, autor, edicao, editora, imagePath)
								values (:title, :author, :ed, :house, :imPath)";

	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":title", $title);
		$stmt->bindParam(":author", $author);
		$stmt->bindParam(":ed", $ed);
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

function updateObjBook() {

	global $bookTable;

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

	$title = $json->titulo; $author = $json->autor; $ed = $json->edicao;
	$house = $json->editora; $id = $json->idLivro;
	//$imPath = createImage($title, $json->image, false);
	$imPath = null;

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "update $bookTable set titulo = :title, autor = :author, edicao= :ed,
				editora = :house, imagePath = :imPath where idLivro = :id";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":title", $title);
	$stmt->bindParam(":author", $author);
	$stmt->bindParam(":ed", $ed);
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

function getObjBookInfo($appID, $data, $iv) {

	global $bookTable;
	
	$json = json_decode(decrypt_data($appID, $data, $iv));
	$id = $json->idLivro;
	$response["status"] = 0;

	$dbh = getConnection();
	$sql = "select * from $bookTable where idLivro = :id";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	/* get all book information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["livros"] = storeElements("livro", $tmp);
	if ($response["livros"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getAllObjBooks() {

	global $bookTable;

	$response["status"] = 0;
	$dbh = getConnection();

	$sql = "select * from $bookTable";
	$stmt = $dbh->prepare($sql);
	$stmt->execute();

	/* get all book information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["livros"] = storeElements("livro", $tmp);
	if ($response["livros"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getSimilarObjBooks($appID, $data, $iv) {

	global $bookTable;
	
	$json = json_decode(decrypt_data($appID, $data, $iv));
	$titulo = $json->titulo;
	$response["status"] = 0;
	$dbh = getConnection();

	$sql = "select * from $bookTable where titulo like '%$titulo%'";
	$stmt = $dbh->prepare($sql);
	$stmt->execute();

	/* get all book information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["livros"] = storeElements("livro", $tmp);
	if ($response["livros"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function removeObjBook() {

	global $bookTable;

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

	$id = $json->idLivro;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "delete from $bookTable where idLivro = :id";

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
