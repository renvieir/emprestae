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
									$_SESSION["page"] = "contact.php";
									require "sidebar.php";
								?>
									<div id="content">
									          <div class="content nova">
											    <ul>
													<li>
														<p>
														Av. Rodrigo Otavio, n 6.200, Campus Universitario Senador Arthur Virgilio Filho. SETOR NORTE. Manaus - AM
														</p>
														<p>
														Tel: 123-456-7890<br/>
														Fax: 123-456-7890
														</p>
														<p>
														baresystems@gmail.com<br/>
														<a href=http://baresystems.wix.com/emprestae><img src="images/bare2.png"></a>
														</p>
													</li>
												</ul>
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
