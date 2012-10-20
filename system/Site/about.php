<!DOCTYPE html >
<!--  Website template by freewebsitetemplates.com  -->
<html>
<head>
<title>Emprestae!</title>
<meta charset="iso-8859-1" />
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
				$_SESSION["page"] = "about.php";
				require "sidebar.php";
			?>
          <div id="content">
            <div class="content nova">
              <ul>
                <li>
                  <h2>Emprestae!</h2>
				  � uma rede social de empr�stimo de objetos, o objetivo do Emprestae � incentivar o compartilhamento de objetos e estimular o consumismo sustent�vel, apoiando a reutiliza��o dos objetos que n�o est�o sendo utilizados pelo seu dono e pode ser de grande utilidade para outras pessoas. O emprestae oferece recursos para manter controle dos objetos emprestados e lembrar as pessoas as datas e lugares de devolu��o
				  </li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="shadow"> </div>
  </div>
</div>
</body>
</html>
