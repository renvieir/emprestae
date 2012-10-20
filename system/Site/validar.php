<?php

	if ($_POST["action"] == "Cadastrar") {
		$_SESSION["page"] = "login.php";
		cadastrar();
	}

	else if ($_POST["action"] == "Edit") {

		require "user_control.php";

		$_SESSION["update"] = 1;
		cadastrar();
	}

	else if ($_POST["action"] == "Login")
		checkLogin();

	else if ($_POST["action"] == "Submit")
		signUp();

	else if ($_POST["action"] == "Adicionar Amizade")
		addFriend();

	else if ($_POST["action"] == "Cancelar Amizade")
		rmFriend();

	else if ($_POST["action"] == "Adicionar Objeto")
		addObj();

	else if ($_POST["action"] == "Remover Objeto")
		rmObj();

	function cadastrar() {
	
		$url = "cadastrar.php";
		header("Location: $url");
	}

	function checkLogin() {

		require "user_control.php";

		$user = $_POST["user"];
		$pwd = $_POST["pwd"];

		$arr = Array("email" => $user, "senha" => $pwd);
		$context = createMsgContext($arr, "POST");
		$json = file_get_contents("http://localhost/sd/emprestei/system/WebService/checkUser", false, $context);

		$status = json_decode($json);
		if ($status->status == 1) {

			$json = file_get_contents("http://localhost/sd/emprestei/system/WebService/getUserInfo/$user");
			$tmp = json_decode($json);
			$status = $tmp->users[0]->user;

			print_r($status);
			$_control = new User($status->nome, $status->email, $status->idusuario, $status->imagePath);
			$_SESSION["user"] = $_control;
			$url = "index.php";
		}
		else {
			$_SESSION["chances"] = 1;
			// to get the client url that made the request
			$url = $_SERVER["HTTP_REFERER"];
		}

		header("Location: $url");
	}

	/* control the signUp and update from users */
	function signUp() {

		require "user_control.php";
	
		$user = $_POST["user"];
		$pwd = $_POST["pwd"];
		$name = $_POST["name"];

		$arr = Array("email" => $user, "senha" => $pwd, "nome" => $name,
			"addressLat" => null, "addressLong" => null, "imagePath" => null);

		if (!isset($_SESSION["update"])) {
			$context = createMsgContext($arr, "POST");
			$url = "http://localhost/sd/emprestei/system/WebService/createUser";
			$url_return = "cadastrar.php";
		}
		else {
			$context = createMsgContext($arr, "PUT");
			$url = "http://localhost/sd/emprestei/system/WebService/updateUser";
			$url_return = "index.php";
		}

		$json = file_get_contents($url, false, $context);

		$status = json_decode($json);
		if ($_SESSION["update"]) {
			$_SESSION["user"]->setEmail($user);
			$_SESSION["user"]->setName($name);
			unset($_SESSION["update"]);
		}
		else {
			if ($status->status == 1)
				$_SESSION["status"] = 1;
			else 
				$_SESSION["status"] = 2;
		}

		header("Location: $url_return");
	}

	function addFriend() {

		require "user_control.php";

		$friendId = $_GET["friendId"];
		$userId = $_SESSION["user"]->getId();

		$arr = Array("id1" => $userId, "id2" => $friendId);
		$context = createMsgContext($arr, "POST");
		$json = file_get_contents("http://localhost/sd/emprestei/system/WebService/makeFriends", false, $context);
		$status = json_decode($json);

		$url_return = "index.php";
		header("Location: $url_return");
	}

	function rmFriend() {

		require "user_control.php";

		$friendId = $_GET["friendId"];
		$userId = $_SESSION["user"]->getId();

		$arr = Array("id1" => $userId, "id2" => $friendId);
		$context = createMsgContext($arr, "DELETE");
		$json = file_get_contents("http://localhost/sd/emprestei/system/WebService/removeFriends", false, $context);
		$status = json_decode($json);

		$url_return = "index.php";
		header("Location: $url_return");
	}

	function addObj() {

		require "user_control.php";

		$arr = Array("Filmes" => 'c', "Jogos" => 'b', "Livros" => 'a');
		$objId = $_GET["idObj"];
		$tipo = $arr[$_GET['tipo']];
		$userId = $_SESSION["user"]->getId();

		$arr = Array("userId" => $userId, "objId" => $objId, "objType" => $tipo);
		$context = createMsgContext($arr, "POST");
		$json = file_get_contents("http://localhost/sd/emprestei/system/WebService/addUserObj", false, $context);
		$status = json_decode($json);

		$url_return = "index.php";
		header("Location: $url_return");
	}

	function rmObj() {

		require "user_control.php";

		$arr = Array("Filmes" => 'c', "Jogos" => 'b', "Livros" => 'a');
		$objId = $_GET["idObj"];
		$tipo = $arr[$_GET['tipo']];
		$userId = $_SESSION["user"]->getId();

		$arr = Array("userId" => $userId, "objId" => $objId, "objType" => $tipo);
		$context = createMsgContext($arr, "DELETE");
		$json = file_get_contents("http://localhost/sd/emprestei/system/WebService/removeUserObj", false, $context);
		$status = json_decode($json);

		$url_return = "index.php";
		header("Location: $url_return");
	}
?>
