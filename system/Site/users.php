<!DOCTYPE html >
<!--  Website template by freewebsitetemplates.com  -->
<html>

<head>
	<title>Emprestae!</title>
	<meta  charset="iso-8859-1" />
	<link href="css/style.css" rel="stylesheet" type="text/css" />
	<!--[if IE 6]>
		<link href="css/ie6.css" rel="stylesheet" type="text/css" />
	<![endif]-->
	<!--[if IE 7]>
        <link href="css/ie7.css" rel="stylesheet" type="text/css" />  
	<![endif]-->

</head>

<?php
	require "user_control.php";
?>

<body>

	  <div id="background">
			  <div id="page">
			  
					 <div class="header">
						<div class="footer">
							<div class="body">
									<?php
										$tmp = $_GET["friend"];
										if ($tmp == 0)
											$_SESSION["page"] = "myaccount.php";
										else if ($tmp == 1)
											$_SESSION["page"] = "users.php";
										else 
											$_SESSION["page"] = "friends.php";
										require "sidebar.php";
									?>
									<div id="content">
										<div class="content nova">
											<?php
												echo "<ul>";
												if ($_SESSION["page"] == "myaccount.php" || $_GET["email"] != "") {
													if ($_GET["email"]) {
														$email = $_GET["email"];
														echo "<h2>$email Account</h2>";
														$json = file_get_contents("http://localhost/sd/emprestei/system/WebService/getUserInfo/$email");
														$res = json_decode($json, true);
														$status = $res['status'];
													}
													else {
														echo "<h2>My Account</h2>";
														$nomes = Array($_SESSION["user"]->getName());
														$emails = Array($_SESSION["user"]->getEmail());
														$res = Array("users" => null);
														$status = 1;
													}
												}
												else if ($_SESSION["page"] == "users.php") {
													echo "<h2>Users</h2>";
													$email = $_SESSION["user"]->getEmail();
													$json = file_get_contents("http://localhost/sd/emprestei/system/WebService/getAllUsers/$email");
													$res = json_decode($json, true);
													$status = $res['status'];
												}
												else {
													echo "<h2>Friends</h2>";
													$id = $_SESSION["user"]->getId();
													$json = file_get_contents("http://localhost/sd/emprestei/system/WebService/getFriends/$id");
													$res = json_decode($json, true);
													$status = $res['status'];
												}
												if ($status == 1) {
													echo '<table border="1" cellpadding="5">';
													foreach ($res['users'] as $tmp) {
														$user = $tmp['user'];
														echo '<li>';
														echo '<tr>';
														echo '<td colspan="20">';
														if ($_GET["email"] == "") {
															echo '<a href=users.php?friend=1&email=' . $user["email"] . '>';
															echo '<img src=' . $user['imagePath'] . ' width="90" height="90">';
															echo '</a>';
														}
														else {
															echo '<img src=' . $user['imagePath'] . ' width="90" height="90">';
														}
														echo '</td>';
														echo '<td colspan="20">';
														echo	'<p>
																	Nome: ' . $user['nome'] . '<br/>
																	Email: ' . $user['email'] . '<br/>
																</p>';
														echo '</td>';
														echo '</tr>';
														echo '</li>';
													}
													if (!$res["users"]) {
														$user = $_SESSION["user"];
														echo '<li>';
														echo "<tr>";
														echo '<td colspan="20">';
														echo '<img src=' . $user->getImagePath() . ' width="90" height="90">';
														echo '</td>';
														echo '<td colspan="20">';
														echo	'<p>
																	Nome: ' . $user->getName() . '<br/>
																	Email: ' . $user->getEmail() . '<br/>
																</p>';
														echo '</td>';
														echo "</tr></li>";
													}

												if ($_GET["email"]) {
													echo '<li><tr><td colspan="20">
															<form name="input" action="validar.php?friendId=' . $user['idusuario'] . '" method="post">
																<input type=submit name=action value="Adicionar Amizade">
															</form></td><td colspan="20">
															<form name="input" action="validar.php?friendId=' . $user['idusuario'] . '" method="post">
																<input type=submit name=action value="Cancelar Amizade">
															</form></td>
														</tr></li>';
												}
													echo "</table>";
												}
												if ($_SESSION["page"] == "myaccount.php" && $_GET["email"] == "") {
													echo "<li>
															<form name=\"input\" action=\"validar.php\" method=\"post\">
																<input type=submit name=action value=\"Edit\">
															</form>
														</li>";
												}
												if ($_SESSION["page"] == "myaccount.php" || $_GET["email"]) {
													$id = ($_GET['email']) ? $user['idusuario'] : $_SESSION["user"]->getId();
													$json = file_get_contents("http://localhost/sd/emprestei/system/WebService/getUserObjs/$id");
													$res = json_decode($json, true);
													unset($res['status']);
													echo "<br/><br/>";
													foreach ($res as $type => $objs) {
														echo "<h3>$type</h3>";
														echo '<table border="0" cellpadding="5">';
														foreach ($objs as $obj) {
															foreach ($obj as $elem) {
																echo '<li><tr>';
																echo '<td colspan="50">';
																echo '<img src=' . $elem['imagePath'] . ' width="100" height="100">';
																echo '</td>';
																	echo '<td colspan="50" width="100">';
																foreach ($elem as $key => $value) {
																	if ($key != "imagePath" && strpos($key, 'id') === false) {
																		echo '<p>' . $key . ': ' . $value . '</p>';
																	}
																}
																	echo '</td>';
																echo '</tr></li>';
															}
														}
														echo "</table>";
													}
												}
												echo "</ul>";
											?>
										</div>
									</div>
							</div>
						</div>
					 </div>
					 <div class="shadow">
					 </div>
			  </div>    
	  </div>    
	
</body>
</html>

