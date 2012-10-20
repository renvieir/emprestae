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
										$_SESSION["page"] = "emprestimo.php";
										require "sidebar.php";
									?>
									<div id="content">
										<div class="content nova">
											<?php
												if (!isset($_GET['emp'])) {
													echo '<p><a href=emprestimo.php?emp=1>Objetos Emprestados Por Mim</a></p>';
													echo '<p><a href=emprestimo.php?emp=2>Objetos Emprestados De Mim</a></p>';
												}
												else {
													$url = "http://localhost/sd/emprestei/system/WebService/";
													$obj = ($_GET['emp'] == 1) ? 'getEmpPorMim' : 'getEmpDeMim' ;
													echo "<ul>";
													$json = file_get_contents($url . $obj . '/' . $_SESSION['user']->getId());
													$res = json_decode($json, true);
													$arr = Array('c' => 'Filmes', 'b' => 'Jogos', 'a' => 'Livros');
													unset($res['status']);
														//foreach ($objs as $key => $obj) {
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
																			echo '<td colspan="50" width="140">';
																		foreach ($elem as $key => $value) {
																			if ($key == 'tipoObjeto')
																				echo '<p>' . $key . ': ' . $arr[$value] . '</p>';
																			else if (strpos($key, 'id') === false) {
																				echo '<p>' . $key . ': ' . $value . '</p>';
																			}
																		}
																			echo '</td>';
																		echo '</tr></li>';
																	}
																}
																echo "</table>";
															}
														//}
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
												}
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

