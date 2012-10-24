<?php

//require "bd_connection.php";

$app->post("/createUser", "createUser");
$app->put("/updateUser", "updateUser");
$app->delete("/removeUser", "removeUser");
$app->post("/checkUser", "checkUser");
$app->get("/getUserInfo/:email", "getUserInfo");
$app->get("/getAllUsersBut/:email", "getAllUsersBut");
$app->get("/getAllUsersByEmail/:email", "getAllUsersByEmail");
$app->get("/getCloseUsers/:userId/:lat/:long", "getCloseUsers");

function createUser() {

	global $userTable;

	/* verifica se existe alguma informacao no corpo da mensagem */
	$request = Slim::getInstance()->request();
	$json = readRequestBody($request);
	if (!$json) {
		$response["status"] = 0;
		$response["aqui"] = 1;
		echo json_encode($response);
		return;
	}

	/* lendo dados do json */
	$name = $json->nome; $email = $json->email; $pwd = $json->senha;
	$lat = $json->addressLat; $long = $json->addressLong;

//	$imPath = createImage($email, $json->image, true);

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "insert into $userTable (email, nome, senha, addressLat, addressLong,
				imagePath) values (:email, :name, :pwd, :lat, :long, null)";

	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":email", $email);
		$stmt->bindParam(":name", $name);
		$stmt->bindParam(":pwd", $pwd);
		$stmt->bindParam(":lat", $lat);
		$stmt->bindParam(":long", $long);

//		$stmt->bindParam(":imPath", $imPath);

		$stmt->execute();
	} catch (PDOException $e) {
		$response["status"] = 0;
		$response["aqui"] = 0;
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
	$imPath = createImage($email, $json->imagePath, true);

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

function getAllUsersBut($mail) {

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

function getAllUsersByEmail($mail) {

	global $userTable;

	$response["status"] = 0;
	$email = $mail;
	$dbh = getConnection();
	$sql = "select idusuario, nome, email, addressLat, addressLong, imagePath
										from $userTable where email like '%$mail%'";
	$stmt = $dbh->prepare($sql);
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

function getCloseUsers($id1, $userLat, $userLong) {

	global $friendTable;
	
	$response["status"] = 0;
	$dbh = getConnection();

	$sql = "SELECT idusuario, nome, email, addressLat, addressLong, imagePath
				from usuario, (SELECT idusuario_b AS friend FROM $friendTable
					WHERE idusuario_a = :id1 UNION SELECT idusuario_a AS friend
						FROM $friendTable WHERE idusuario_b = :id1) as tmp WHERE
							idusuario = tmp.friend";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->execute();

	/* get user information as a associative array */
	$elements = $stmt->fetchAll(PDO::FETCH_ASSOC);
	$type = "user";
	if (count($elements) != 0) {
		$arr = Array();
		$i = 0;
		$coordUser = coordenadasEsfericas($userLat, $userLong);
		echo "ok2\n";
		foreach ($elements as $elem) {
			$arr[$i] = Array();
			$arr[$i][$type] = Array();
			foreach ($elements[$i] as $key => $value) {
				if ($key == "addressLat") {
					$friendLat = $elements[$i]["addressLat"];
					$friendLong = $elements[$i]["addressLong"];
					$coordFriend=coordenadasEsfericas($friendLat, $friendLong);

					if( !distBetweenUsers($coordUser, $coordFriend) ) {
						$arr[$i][$type]	= null;
						unset($arr[$i]);
						break;
					}
				}
				$arr[$i][$type][$key] = ($value) ? $value: null;
			}
			$i++;
		}
	}

	$response["users"] = $arr;
	if ($response["users"])
		$response["status"] = 1;

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function coordenadasEsfericas ($lat, $long) {

	$lat *= pi()/180.0;
	$long *= pi()/180.0;

	$coord = Array();
	$coord["x"] = sin($long)*cos($lat);
	$coord["y"] = cos($long)*cos($lat);
	$coord["z"] = sin($lat);

	return $coord;
}

function distBetweenUsers ($coordUser, $coordFriend) {

	$alpha = acos( $coordUser["x"]*$coordFriend["x"] +
		$coordUser["y"]*$coordFriend["y"] + $coordUser["z"]*$coordFriend["z"] );
	
	$raioTerra = 6378;
	$raioPadrao = 100;

	if ($alpha*$raioTerra <= $raioPadrao)
		return 1;
	
	return 0;
}

?>
