<?php

require "post_context.php";

session_start();
$inactive = 2000; // if a session is inactive for 120s, it'll be destroyed

if (isset($_SESSION["timeout"])) {
	$sessionTTL = time() - $_SESSION["timeout"];
	if ($sessionTTL > $inactive) {
		session_unset();
		session_destroy();
		header("Location: index.php");
	}
}

$_SESSION["timeout"] = time();

class User {

	var $_name;
	var $email;
	var $id;
	var $imagePath;

	function User($_name, $email, $id, $imagePath) {
		$this->_name = $_name;
		$this->email = $email;
		$this->id = $id;
		$this->imagePath = $imagePath;
	}

	function setEmail($email) {
		$this->email = $email;
	}

	function setId($id) {
		$this->id = $id;
	}

	function setName($_name) {
		$this->_name = $_name;
	}

	function setImage($imagePath) {
		$this->imagePath = $imagePath;
	}

	function getEmail() {
		return $this->email;
	}

	function getId() {
		return $this->id;
	}

	function getName() {
		return $this->_name;
	}

	function getImagePath() {
		return $this->imagePath;
	}

}

?>
