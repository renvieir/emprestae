<div id="sidebar">
	<a href="index.php"><img id="logo" src="images/oo.png" width="157" height="30" alt="" title=""/></a>
	<ul class="navigation">
		<?php

			$active = "class=\"active\"";

			/* HOME */
			$tmp = "";
			if ($_SESSION["page"] == "index.php")
				$tmp = $active;
			echo "<li $tmp><a href=\"index.php\">HOME</a></li>";

			/* ABOUT */
			$tmp = "";
			if ($_SESSION["page"] == "about.php")
				$tmp = $active;
			echo "<li $tmp><a href=\"about.php\">ABOUT</a></li>";
		
			/* LOGIN */
			$tmp = "";
			if ($_SESSION["page"] == "login.php")
				$tmp = $active;
			if (!isset($_SESSION["user"]))
				echo "<li $tmp><a href=\"login.php\">LOGIN</a></li>";
			
			/* Show User Account, Users and Friends */
			if (isset($_SESSION["user"])) {

				$tmp = "";
				if ($_SESSION["page"] == "myaccount.php")
					$tmp = $active;
				echo "<li $tmp><a href=\"users.php?friend=0\">MY ACCOUNT</a></li>";

				$tmp = "";
				if ($_SESSION["page"] == "users.php")
					$tmp = $active;
				echo "<li $tmp><a href=\"users.php?friend=1\">USERS</a></li>";

				$tmp = "";
				if ($_SESSION["page"] == "friends.php")
					$tmp = $active;
				echo "<li $tmp><a href=\"users.php?friend=2\">FRIENDS</a></li>";

				$tmp = "";
				if ($_SESSION["page"] == "objeto.php")
					$tmp = $active;
				echo "<li $tmp><a href=\"objeto.php\">OBJECTS</a></li>";

				$tmp = "";
				if ($_SESSION["page"] == "emprestimo.php")
					$tmp = $active;
				echo "<li $tmp><a href=\"emprestimo.php\">EMPRESTIMOS</a></li>";
			}

			/* CONTACT */
			$tmp = "class=\"last\"";
			if ($_SESSION["page"] == "contact.php")
				$tmp = "class=\"active last\"";
			echo "<li $tmp><a href=\"contact.php\">CONTACT</a></li>";

			/* LOGOUT */
			$tmp = "";
			if ($_SESSION["page"] == "login.php")
				$tmp = $active;
			if (isset($_SESSION["user"]))
				echo "<li $tmp><a href=\"logout.php\">LOGOUT</a></li>";
		?>
	</ul>

	<div class="connect">
		<a href="http://facebook.com/freewebsitetemplates" class="facebook">&nbsp;</a>
		<a href="http://twitter.com/fwtemplates" class="twitter">&nbsp;</a>
		<a href="http://www.youtube.com/fwtemplates" class="vimeo">&nbsp;</a>
	</div>

	<div class="footenote">
		<span>&copy; Copyright &copy; 2011.</span>
		<span><a href="index.html">Company name</a> all rights reserved</span>
	</div>
</div>
