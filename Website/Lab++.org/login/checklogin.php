<?php

ob_start();
include("database/db_conection.php");

if(session_id() == '' || !isset($_SESSION)) {
    // session isn't started
    session_start();
	//echo session_id();
}

if(isset($_POST['login']))
{

    $user_login=mysql_real_escape_string($_POST['login']);
    $user_name=mysql_real_escape_string($_POST['name']);
    //$user_email=mysql_real_escape_string($_POST['email']);
    $user_pass=mysql_real_escape_string($_POST['pass']);
	$user_pass = md5($user_pass);
    $rememberme=$_POST['remember'];

    //$check_user="select * from lppusers WHERE user_email='$user_email' AND user_pass='$user_pass'";
    $check_user="select * from lppusers WHERE user_name='$user_name' AND user_pass='$user_pass'";

    $run=mysql_query($check_user);

    if(mysql_num_rows($run))
    {
        //$_SESSION['email']=$user_email;//here session is used and value of $user_email store in $_SESSION.
        $_SESSION['name']=$user_name;//here session is used and value of $user_email store in $_SESSION.
        //$_POST['email']=$user_email;//here session is used and value of $user_email store in $_SESSION.
		
		if ($rememberme == "yes") //if the Remember me is checked, it will create a cookie.
		{
			//setcookie("user_email", $user_email, time() + (86400 * 30), "/", ""); //86400 = 1 day
			//setcookie("user_pass", md5($user_pass), time() + (86400 * 30), "/", ""); //86400 = 1 day
			$iCookieTime = time() + 24*60*60*30; // = 1 month
			//ob_start(); //set output buffering before setting cookies..
			setcookie("username", $user_name, $iCookieTime, '/');
			setcookie("userpass", $user_pass, $iCookieTime, '/');
			setcookie("tourdone", $user_name, $iCookieTime, '/');
			//ob_end_flush(); // flush data
			//setcookie("cookie[useremail]", $user_email);
			//setcookie("cookie[userpass]", $user_pass);
			//$_COOKIE['useremail'] = $user_email;
			//$_COOKIE['userpass'] = $user_pass;
		}else{
			//setcookie("user_email", $user_email, false, "/", ".labpp.org"); //86400 = 1 day
			//setcookie("user_pass", md5($user_pass), false, "/", ".labpp.org"); //86400 = 1 day
			//ob_start(); //set output buffering before setting cookies..
			setcookie('username', '', time() - 96 * 3600, '/');
			setcookie('userpass', '', time() - 96 * 3600, '/');
			//setcookie("cookie[useremail]", '');
			//setcookie("cookie[userpass]", '');
			unset($_COOKIE['username']);
			unset($_COOKIE['userpass']);
			//ob_end_flush(); // flush data
		}
        //echo "<script>window.open('http://labpp.org/index.php?email=" . $user_email . "','_blank')</script>";
        //echo "<script>window.open('http://labpp.org/index.php','_blank')</script>";
		
		$row = mysql_fetch_assoc($run);
		if ($row['user_active']==1)
			echo '<script type="text/javascript">window.open("http://labpp.org","_top");
				//window.parent.location.reload();
			</script>';
		else
		{
			echo '<center></br></br>
                    <input class="btn btn-lg btn-danger btn-block" type="" value="Not activated!" name="registersuccessful"></br><b>Please check your email and follow the instruction to activate your account.</b></br>
					<h3 id="counterdown" style="color:red">10</h3>
				</center>' . 
				'<script>
				document.addEventListener(\'DOMContentLoaded\', function() {
					document.getElementById("maindiv").style.display = "none";
					var count = 10;
					var countdown = setInterval(function(){
						var element = document.getElementById("counterdown");
						while (element.firstChild) {
							element.removeChild(element.firstChild);
						}
						element.appendChild(document.createTextNode(count));
						
						if (count == 0) {
							clearInterval(countdown);
							//window.parent.location.reload();
							window.open("http://labpp.org","_top");
						}
						count--;
					}, 1000);
					/*setTimeout(popup, 5000);
					function popup() {
						window.parent.location.reload();
					}*/
				});
				</script>';
		}
    }
    else
    {
        echo '<script>alert("Email or password is incorrect!")</script>';
    }
}
ob_end_flush();
?>