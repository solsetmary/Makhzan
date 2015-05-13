<?php
if(session_id() == '' || !isset($_SESSION)) {
    // session isn't started
    session_start();
}
//if(!$_SESSION['email'])
if(!$_SESSION['name'])
{

    header("Location: login.php");//redirect to login page to secure the welcome page without login access.
}

?>

<html>

<head lang="en">
    <meta charset="UTF-8">
    <link type="text/css" rel="stylesheet" href="bootstrap-3.2.0-dist/css/bootstrap.css">
    <title></title>
<style>
    .login-panel {
        margin-top: 10px;
	}
</style>
</head>

<body class="login-panel">
<div class="container">
    <div class="row">
        <div class="col-md-4 col-md-offset-4">

			<center>

				<?php

					include("database/db_conection.php");

					//$user_email=mysql_real_escape_string($_SESSION['email']);
					$user_name=mysql_real_escape_string($_SESSION['name']);

					//$check_user="select * from lppusers WHERE user_email='$user_email'";
					$check_user="select * from lppusers WHERE user_name='$user_name'";

					$run=mysql_query($check_user);

					if(mysql_num_rows($run))
					{
						$row = mysql_fetch_assoc($run);
						echo '<dd>'
							 . '<img src="data:image/jpeg;base64,' . base64_encode($row['user_image']) . '"  style="width:40px;height:60px;border-radius:10px;box-shadow: 3px 3px 2px #888888;">'
							 . '</dd>';
				?>
						<b>
				<?php
						echo '<dt><strong>' . $row['user_name'] .'</strong></dt>';
				?>
				</b>
				<?php
						echo $row['user_email'];
						if ($row['user_active']!=1)
						{
							$ak = $row['activation_key'];
							echo '</br><center>
									<input class="btn btn-lg btn-danger btn-block" type="" value="Not activated!" name="registersuccessful"></br><b>Please check your email and follow the instruction to activate your account.</b><br>
									<b>Did not receive the Email?</b> </b><a href="resend.php?id=' . "$ak\"" . ' id="resendemail">Resend Email</a>
								</center>
								<script>
									document.getElementById("resendemail").addEventListener("click", function(){' . 
										''
									. '});
								</script>';
						}

					}
					else
					{
						echo "<script>alert('Email or password is incorrect!')</script>";
					}

				?>
				</br>
				</br>
				<a href="logout.php" class="btn btn-lg btn-info btn-block" >Logout</a>
			</center>
        </div>
    </div>
</div>
</body>

</html>