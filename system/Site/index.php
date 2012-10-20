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
									$_SESSION["page"] = "index.php";
									require "sidebar.php";
								?>
									<div id="content" >
									
									    <img src="images/estante-asterisco.jpg" width="726" height="546" alt="" title="">
										<div class="featured">
										      <div class="header">
											     <ul>
														<li class="first">
															<p>Ae!</p>
														</li>
														<li class="first">
														<p>	Emprestae! </p>
														</li>
												 </ul>
											  </div>
											  <div class="body">
											    <p>
													A oportunidade passou e voce nao pode ver um filme, comprar um jogo ou mesmo um livro, o Emprestae! estah aqui para ajuda-lo.
												</p>
												<p>
													Faca parte desta rede e descubra como emprestar e encontrar um objeto que sempre quis nunca foi tao facil.
												</p>
											  </div>
									    </div>
									</div>
						</div>
					 </div>
					 <div class="shadow">&nbsp;</div>
			  </div>    
	  </div>    
	
</body>
</html>
