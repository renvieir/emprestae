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
										$_SESSION["page"] = "login.php";
										require "sidebar.php";
									?>
									<div id="content">
										<div class="content nova">
											<form name="input" action="validar.php" method="post">
												<table border="0" cellpadding="3">
													<tr>
														<td colspan="3">
															<strong>Login</strong>
															<?php
																if ( isset($_SESSION["chances"]) ) {
																	echo '<p style="color:red;"><b>Invalid login/password</b></p>';
																	unset($_SESSION["chances"]);
																}
															?>
														</td>
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
															<input type="submit" name="action" value="Login">
															<input type="submit" name="action" value="Cadastrar">
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
