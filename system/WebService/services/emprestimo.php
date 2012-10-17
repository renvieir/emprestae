<?php

$app->post("/createEmp", "createEmprestimo");
$app->get("/getEmpPorMim/:id1", "getEmpPorMim");
$app->get("/getEmpDeMim/:id1", "getEmpDeMim");
$app->delete("/removeEmp", "removeEmprestimo");

$app->put("/updateEmpDate", "updateEmprestimo");
$app->put("/changeEmpStatus", "changeEmprestimoStatus");


/* date format: yyyy/mm/dd */
function createEmprestimo(){

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
	echo json_encode($response);
	return;
}

/* Objetos que o usuario emprestou de outros usuarios */
function getEmpPorMim($id) {

	global $loanTable;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "select * from $loanTable where fk_idUser2 = :id";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	/* get laon information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["emprestimos"] = storeElements("emprestimo", $tmp);
	if ($response["emprestimos"])
		$response["status"] = 1;

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

/* Objetos que o usuario emprestou para outros usuarios */
function getEmpDeMim($id) {

	global $loanTable;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "select * from $loanTable where fk_idUser1 = :id";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $id);
	$stmt->execute();

	/* get laon information as a associative array */
	$tmp = $stmt->fetchAll(PDO::FETCH_CLASS);
	$response["emprestimos"] = storeElements("emprestimo", $tmp);
	if ($response["emprestimos"])
		$response["status"] = 1;

	closeConnection($dbh);
	echo json_encode($response);
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
	$idEmp = $json->idemprestimo;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "delete from $loanTable where idemprestimo = :id";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id", $idEmp);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
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
	echo json_encode($response);
	return;
}

/* status tem que ser um inteiro: 0 ou 1 */
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
	echo json_encode($response);
	return;
}

?>
