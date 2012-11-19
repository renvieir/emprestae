<?php

$app->post("/createUser", "createUser");
$app->put("/updateUser", "updateUser");
$app->delete("/removeUser", "removeUser");
$app->post("/checkUser", "checkUser");
$app->get("/getUserInfo/:appID/:data/:iv", "getUserInfo");
$app->get("/getAllUsersBut/:appID/:data/:iv", "getAllUsersBut");
$app->get("/getAllUsersByName/:appID/:data/:iv", "getAllUsersByName");
$app->get("/getCloseUsers/:appID/:data/:iv", "getCloseUsers");

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

	$appID = $json->appID;
	$crypt_data = $json->data;
	$iv = $json->iv;
	$json = json_decode(decrypt_data($appID, $crypt_data, $iv));

	$name = $json->nome; $email = $json->email; $pwd = $json->senha;
	$lat = $json->addressLat; $long = $json->addressLong;

	//$imPath = createImage($email, $json->image, true);
	$imPath = null;

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
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

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

	$appID = $json->appID;
	$crypt_data = $json->data;
	$iv = $json->iv;
	$json = json_decode(decrypt_data($appID, $crypt_data, $iv));

	$name = $json->nome; $email = $json->email; $pwd = $json->senha;
	$lat = $json->addressLat; $long = $json->addressLong;
	$idUser = $json->idusuario;
	
	//$imPath = createImage($email, $json->imagePath, true);
	$imPath = null;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "update $userTable set email = :email, nome = :name, senha = :pwd,
				addressLat = :lat, addressLong = :long, imagePath = :imPath
					where idusuario = :idUser";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":email", $email);
	$stmt->bindParam(":name", $name);
	$stmt->bindParam(":pwd", $pwd);
	$stmt->bindParam(":lat", $lat);
	$stmt->bindParam(":long", $long);
	$stmt->bindParam(":imPath", $imPath);
	$stmt->bindParam(":idUser", $idUser);
	$stmt->execute();

	$response['id'] = $idUser;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

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

	$appID = $json->appID;
	$crypt_data = $json->data;
	$iv = $json->iv;
	$json = json_decode(decrypt_data($appID, $crypt_data, $iv));

	$email = $json->email;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "delete from $userTable where email = :email";

	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":email", $email);
	$stmt->execute();

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

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

	$appID = $json->appID;
	$crypt_data = $json->data;
	$iv = $json->iv;
	$json = json_decode(decrypt_data($appID, $crypt_data, $iv));

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
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getUserInfo($appID, $data, $iv) {

	global $userTable;

	$json = json_decode(decrypt_data($appID, $data, $iv));
	$email = $json->email;

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
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getAllUsersBut($appID, $data, $iv) {

	global $userTable;

	$json = json_decode(decrypt_data($appID, $data, $iv));
	$response["status"] = 0;
	$email = $json->$email;

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
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getAllUsersByName($appID, $data, $iv) {

	global $userTable;

	$json = json_decode(decrypt_data($appID, $data, $iv));
	$response["status"] = 0;
	$name = $json->nome;

	$dbh = getConnection();
	$sql = "select idusuario, nome, email, addressLat, addressLong, imagePath
										from $userTable where nome like '%$name%'";
	$stmt = $dbh->prepare($sql);
	$stmt->execute();

	/* get user information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_ASSOC);
	$response["users"] = storeElements("user", $tmp);
	if ($response["users"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getCloseUsers($appID, $data, $iv) {

	global $friendTable;
	
	$json = json_decode(decrypt_data($appID, $data, $iv));
	
	$response["status"] = 0;
	$id1 = $json->idusuario;
	$userLat = $json->addressLat;
	$userLong = $json->addressLong;

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
		foreach ($elements as $elem) {
			$arr[$i] = Array();
			$arr[$i][$type] = Array();
			
			/* verifica proximidade */
			$friendLat = $elements[$i]["addressLat"];
			$friendLong = $elements[$i]["addressLong"];
			$coordFriend=coordenadasEsfericas($friendLat, $friendLong);

			if( !distBetweenUsers($coordUser, $coordFriend) )
				continue;

			echo $elements[$i]['idusuario'];
			foreach ($elements[$i] as $key => $value)
				$arr[$i][$type][$key] = ($value) ? $value: null;
			$i++;
		}
	}

	if ($i == 0) $arr = null;
	$response["users"] = $arr;
	if ($response["users"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

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
