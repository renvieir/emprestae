<?php

$app->post("/requestEmp", "solicitaEmprestimo");
$app->put("/acceptEmp", "aceitaEmprestar");

$app->get("/getEmpPorMim/:appID/:data/:iv", "getEmpPorMim");
$app->get("/getEmpDeMim/:appID/:data/:iv", "getEmpDeMim");
$app->get("/getEmpRequestDeMim/:appID/:data/:iv", "getEmpRequestDeMim");

$app->delete("/removeEmp", "removeEmprestimo");

$app->put("/updateEmpDate", "updateEmprestimo");
$app->put("/changeEmpStatus", "changeEmprestimoStatus");


/* date format: yyyy/mm/dd; id1 emprestou de id2 */
function solicitaEmprestimo(){

	global $loanTable, $patrimonio;

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

	$id1 = $json->fk_idUser1; $id2 = $json->fk_idUser2;
	$objId = $json->idObj; $objType = $json->tipoObjeto;
	$stDate = $json->dtEmprestimo; $endDate = $json->dtDevolucao;

	/* the objType doesn't exist */
	if ( !isset($patrimonio[$objType]) ) {
		$response["status"] = 0;
		echo json_encode($response);
		return;
	}

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "insert into $loanTable (fk_idUser1, fk_idUser2, idObj, tipoObjeto,
				dtEmprestimo, dtDevolucao, status) values
					(:id1, :id2, :objId, :objType, :stDate, :endDate, false)";
	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":id1", $id1);
		$stmt->bindParam(":id2", $id2);
		$stmt->bindParam(":objId", $objId);
		$stmt->bindParam(":objType", $objType);
		$stmt->bindParam(":stDate", $stDate);
		$stmt->bindParam(":endDate", $endDate);
		$stmt->execute();
	} catch(PDOException $e) {
		$response["status"] = 0;
	}

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function aceitaEmprestar() {

	global $loanTable;

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

	$idEmp = $json->idemprestimo;

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "update $loanTable set status = 1 where idemprestimo = :idEmp";
	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":idEmp", $idEmp);
		$stmt->execute();
	} catch(PDOException $e) {
		$response["status"] = 0;
	}

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

/* Objetos que o usuario emprestou de outros usuarios */
function getEmpPorMim($appID, $data, $iv) {

	global $loanTable;

	$json = json_decode(decrypt_data($appID, $data, $iv));
	$id = $json->idusuario;
	$response["status"] = 1;

	$dbh = getConnection();
	$sql = "select * from $loanTable where fk_idUser1 = :id and status != 0";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	/* get laon information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["emprestimos"] = storeElements("emprestimo", $tmp);
	if ($response["emprestimos"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

/* Objetos que o usuario emprestou para outros usuarios */
function getEmpDeMim($appID, $data, $iv) {

	global $loanTable;

	$json = json_decode(decrypt_data($appID, $data, $iv));
	$id = $json->idusuario;
	$response["status"] = 1;

	$dbh = getConnection();
	$sql = "select * from $loanTable where fk_idUser2 = :id and status != 0";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	/* get laon information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["emprestimos"] = storeElements("emprestimo", $tmp);
	if ($response["emprestimos"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function getEmpRequestDeMim($appID, $data, $iv) {

	global $loanTable;
	
	$json = json_decode(decrypt_data($appID, $data, $iv));
	$id = $json->idusuario;
	$response["status"] = 1;

	$dbh = getConnection();
	$sql = "select * from $loanTable where fk_idUser2 = :id and status = 0";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	/* get laon information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["emprestimos"] = storeElements("emprestimo", $tmp);
	if ($response["emprestimos"])
		$response["status"] = 1;

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

function removeEmprestimo() {

	global $loanTable;

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

	$idEmp = $json->idemprestimo;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "delete from $loanTable where idemprestimo = :id";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $idEmp);
	$stmt->execute();

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

/* atualiza data de devolucao */
function updateEmprestimo(){

	global $loanTable;

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

	$idEmp = $json->idemprestimo; $endDate = $json->dtDevolucao;

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "update $loanTable set dtDevolucao = :endDate where
														idemprestimo = :idEmp";
	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":idEmp", $idEmp);
		$stmt->bindParam(":endDate", $endDate);
		$stmt->execute();
	} catch(PDOException $e) {
		$response["status"] = 0;
	}

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

/* status tem que ser um inteiro: 2 ou 3 */
function changeEmprestimoStatus() {

	global $loanTable;

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

	$idEmp = $json->idemprestimo; $status = $json->status;

	$response["status"] = 1;
	$dbh = getConnection();

	$sql = "update $loanTable set status = :status where idemprestimo = :idEmp";
	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":idEmp", $idEmp);
		$stmt->bindParam(":status", $status);
		$stmt->execute();
	} catch(PDOException $e) {
		$response["status"] = 0;
	}

	closeConnection($dbh);
	$json = json_encode($response);
	$data = encrypt_data($appID, $json);
	echo json_encode($data);

	return;
}

?>
