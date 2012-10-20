<!DOCTYPE html >
<!--  Website template by freewebsitetemplates.com  -->
<html>

<head>
	<title>Emprestae!</title>	<meta  charset="iso-8859-1" />
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
							require "sidebar.php";
						?>
							<div id="content">
								<div class="nova">
									<form name="input" action="validar.php" method="post">
										<table width="100%" border="0" cellpadding="3">
											<tr>
												<td colspan="3">
													<?php
														if (!isset($_SESSION["update"]))
															echo "<strong>Cadastrar</strong>";
														else
															echo "<strong>Atualizar Dados</strong>";
														if ( $_SESSION["status"] == 1 ) {
															echo '<p style="color:green;"><b>Cadastro Realizado</b></p>';
															unset($_SESSION["status"]);
														}
														else if ( $_SESSION["status"] == 2 ) {
															echo '<p style="color:red;"><b>Erro ao cadastrar!</b></p>';
															unset($_SESSION["status"]);
														}
													?>
												</td>
											</tr>
											<tr>
												<td>Nome:</td>
												<td><input name="name" type="text" id="name"></td>
											</tr>
											<tr>
												<td width="78">User:</td>
												<td width="100%"><input name="user" type="text" id="user"></td>
											</tr>
											<tr>
												<td>Password:</td>
												<td><input name="pwd" type="password" id="pwd"></td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td>
													<input type="submit" name="action" value="Submit">
												</td>
											</tr>
										</table>
									</form>
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
