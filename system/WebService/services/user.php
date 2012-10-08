<?php

//require "bd_connection.php";

$app->get("/createUser/:name/:email/:pwd", "createUser");
$app->get("/updateUser/:name/:email/:pwd", "updateUser");
$app->get("/getUserInfo/:email", "getUserInfo");
$app->get("/removeUser/:email", "removeUser");

function createUser($nome, $mail, $pass) {

	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$name = $json->name;
	$email = $json->email;
	$pwd = $json->pwd;
	*/

	$name = $nome; $email = $mail; $pwd = $pass;
	$dbh = getConnection();
	$sql = "insert into usuario (email, nome, senha) values
														(:email, :name, :pwd)";

	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":email", $email);
		$stmt->bindParam(":name", $name);
		$stmt->bindParam(":pwd", $pwd);
		$stmt->execute();
	} catch (PDOException $e) {
		echo json_encode(returnMsg("status", 0));
		return;
	}

	closeConnection($dbh);
	echo json_encode(returnMsg("status", 1));
	return;
}

function updateUser($nome, $mail, $pass) {

	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$name = $json->name;
	$email = $json->email;
	$pwd = $json->pwd;
	*/

	$name = $nome; $email = $mail; $pwd = $pass;
	$dbh = getConnection();
	$sql = "update usuario set email = :email, nome = :name, senha = :pwd
														where email = :email";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":email", $email);
	$stmt->bindParam(":name", $name);
	$stmt->bindParam(":pwd", $pwd);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode(returnMsg("status", 1));
	return;
}

function getUserInfo($mail) {

	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$email = $json->email;
	*/

	$response["status"] = 0;

	$email = $mail;
	$dbh = getConnection();
	$sql = "select * from usuario where email = :email";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":email", $email);
	$stmt->execute();

	/* get user information as a associative array */
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

function removeUser($mail) {

	/* lendo dados da mensagem com json
	$request = Slim::getInstance()->request();
	$body = $request->getBody();
	if (!$body) { // fail!
		$response["status"] = $status;
		echo json_encode($response);
		return;
	}

	$json = json_decode($request->getBody());
	$email = $json->email;
	*/

	$email = $mail;
	$dbh = getConnection();
	$sql = "delete from usuario where email = :email";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":email", $email);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode(returnMsg("status2", 1));
	return;
}

?>
