<?php

//require "bd_connection.php";

$app->post("/createUser", "createUser");
$app->put("/updateUser", "updateUser");
$app->delete("/removeUser", "removeUser");
$app->post("/checkUser", "checkUser");
$app->get("/getUserInfo/:email", "getUserInfo");
$app->get("/getAllUsers/:email", "getAllUsers");

function createUser() {

	global $userTable;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$name = $json->nome; $email = $json->email; $pwd = $json->senha;
	$lat = $json->addressLat; $long = $json->addressLong;
	$imPath = createImage($email, $json->image, true);

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "insert into $userTable (email, nome, senha, addressLat, addressLong,
				imagePath) values (:email, :name, :pwd, :lat, :long, :imPath)";

	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":email", $email);
		$stmt->bindParam(":name", $name);
		$stmt->bindParam(":pwd", $pwd);
		$stmt->bindParam(":lat", $lat);
		$stmt->bindParam(":long", $long);
		$stmt->bindParam(":imPath", $imPath);
		$stmt->execute();
	} catch (PDOException $e) {
		$response["status"] = 0;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function updateUser() {

	global $userTable;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$name = $json->nome; $email = $json->email; $pwd = $json->senha;
	$lat = $json->addressLat; $long = $json->addressLong;
	$imPath = createImage($email, $json->image, true);

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "update $userTable set email = :email, nome = :name, senha = :pwd,
				addressLat = :lat, addressLong = :long, imagePath = :imPath
					where email = :email";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":email", $email);
	$stmt->bindParam(":name", $name);
	$stmt->bindParam(":pwd", $pwd);
	$stmt->bindParam(":lat", $lat);
	$stmt->bindParam(":long", $long);
	$stmt->bindParam(":imPath", $imPath);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function removeUser() {

	global $userTable;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$email = $json->email;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "delete from $userTable where email = :email";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":email", $email);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function checkUser() {

	global $userTable;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$email = $json->email; $pwd = $json->senha;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "SELECT * from $userTable where email = :email and senha = :pwd";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":email", $email);
	$stmt->bindParam(":pwd", $pwd);
	$stmt->execute();

	$res = $stmt->fetchAll(PDO::FETCH_ASSOC);
	if (count($res) == 0)
		$response["status"] = 0;

	closeConnection($dbh);
	echo json_encode($response);
	return;

}

function getUserInfo($mail) {

	global $userTable;

	$response["status"] = 0;
	$email = $mail;
	$dbh = getConnection();
	$sql = "select idusuario, nome, email, addressLat, addressLong, imagePath
										from $userTable where email = :email";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":email", $email);
	$stmt->execute();

	/* get user information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["users"] = storeElements("user", $tmp);
	if ($response["users"])
		$response["status"] = 1;

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function getAllUsers($mail) {

	global $userTable;

	$response["status"] = 0;
	$email = $mail;
	$dbh = getConnection();
	$sql = "select idusuario, nome, email, addressLat, addressLong, imagePath
										from $userTable where email != :email";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":email", $email);
	$stmt->execute();

	/* get user information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_ASSOC);
	$response["users"] = storeElements("user", $tmp);
	if ($response["users"])
		$response["status"] = 1;

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

?>
