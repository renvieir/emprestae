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
										$_SESSION["page"] = "objeto.php";
										require "sidebar.php";
									?>
									<div id="content">
										<div class="content nova">
											<?php
												$url = "http://localhost/sd/emprestei/system/WebService/";
												echo "<ul>";
												if (!isset($_GET["idObj"]) && !isset($_GET["tipoObj"]))
													$objs = Array("Filmes" => "getAllFilms", "Livros" => "getAllBooks", "Jogos" => "getAllGames");
												else {
													$id = $_GET['idObj'];
													$objs = Array("Filmes" => "getFilmInfo/$id", "Livros" => "getBookInfo/$id", "Jogos" => "getGameInfo/$id");
													$objs = Array($_GET["tipoObj"] => $objs[$_GET["tipoObj"]]);
												}
													foreach ($objs as $key => $obj) {
														$json = file_get_contents($url . $obj);
														$res = json_decode($json, true);
														unset($res['status']);
														foreach ($res as $type => $objs) {
															$type[0] = strtoupper($type[0]);
															echo "<h3>$type</h3>";
															echo '<table border="0" cellpadding="5">';
															foreach ($objs as $obj) {
																foreach ($obj as $elem) {
																	$opa = array_keys($obj)[0];
																	$opa[0] = strtoupper($opa[0]);
																	echo '<li><tr>';
																	echo '<td colspan="50">';
																	if (!isset($_GET['idObj']))
																		echo '<a href=objeto.php?idObj=' . $elem["id$opa"] . '&tipoObj=' . $type . '>';
																	echo '<img src=' . $elem['imagePath'] . ' width="100" height="100">';
																	if (!isset($_GET['idObj']))
																		echo '</a>';
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
												if (isset($_GET["idObj"])) {
													echo '<table border="0" cellpadding="5">';
													echo '<li><tr><td colspan="20">
															<form name="input" action="validar.php?idObj=' . $_GET['idObj'] . '&tipo=' . $_GET['tipoObj'] . '" method="post">
																<input type=submit name=action value="Adicionar Objeto">
															</form></td><td colspan="20">
															<form name="input" action="validar.php?idObj=' . $_GET['idObj'] . '&tipo=' . $_GET['tipoObj'] . '" method="post">
																<input type=submit name=action value="Remover Objeto">
															</form></td>
														</tr></li></table>';
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

