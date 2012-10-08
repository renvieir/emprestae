<?php

$app->get("/createLoan/:id1/:id2/:objId/:objType/:stDate/:endDate",
															"createEmprestimo");
$app->get("/updateLoan/:id1/:id2/:endDate", "updateEmprestimo");
$app->get("/getLoan/:id1/:id2", "getEmprestimo");
$app->get("/changeLoanStatus/:id1/:id2/:status", "changeEmprestimoStatus");
$app->get("/removeLoan/:id1/:id2", "removeEmprestimo");


/* date format: yyyy/mm/dd */
function createEmprestimo($id1, $id2, $objId, $objType, $stDate, $endDate){

	global $loanTable;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "insert into $loanTable (fk_idUser1, fk_idUser2, idObj, tipoObj,
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

/* atualiza data de devolucao */
function updateEmprestimo($id1, $id2, $endDate){

	global $loanTable;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "update $loanTable set dtDevolucao = :endDate where
									fk_idUser1 = :id1 and fk_idUser2 = :id2";
	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":id1", $id1);
		$stmt->bindParam(":id2", $id2);
		$stmt->bindParam(":endDate", $endDate);
		$stmt->execute();
	} catch(PDOException $e) {
		$response["status"] = 0;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

/* retorna um json cujos elementos são as colunas e os valores do emprestimo */
function getEmprestimo($id1, $id2) {

	global $loanTable;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "select * from $loanTable where fk_idUser1 = :id1 and
															fk_idUser2 = :id2";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->bindParam(":id2", $id2);
	$stmt->execute();

	/* get laon information as a associative array */
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

function changeEmprestimoStatus($id1, $id2, $status) {

	global $loanTable;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "update $loanTable set status = :status where fk_idUser1 = :id1 and
															fk_idUser2 = :id2";
	try {
		$stmt = $dbh->prepare($sql);
		$stmt->bindParam(":id1", $id1);
		$stmt->bindParam(":id2", $id2);
		$stmt->bindParam(":status", $status);
		$stmt->execute();
	} catch(PDOException $e) {
		$response["status"] = 0;
	}

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

function removeEmprestimo($id1, $id2) {

	global $loanTable;

	$response["status"] = 1;
	$dbh = getConnection();
	$sql = "delete from $loanTable where fk_idUser1 = :id1 and fk_idUser2=:id2";
	$stmt = $dbh->prepare($sql);
	$stmt->bindParam(":id1", $id1);
	$stmt->bindParam(":id2", $id2);
	$stmt->execute();

	closeConnection($dbh);
	echo json_encode($response);
	return;
}

?>
