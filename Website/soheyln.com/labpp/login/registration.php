
<html>
<head lang="en">
    <meta charset="UTF-8">
    <link type="text/css" rel="stylesheet" href="bootstrap-3.2.0-dist\css\bootstrap.css">
    <title>Registration</title>
</head>
<style>
    .login-panel {
        margin-top: 30px;

</style>
<body>

<div class="container" id="maindiv"><!-- container class is used to centered  the body of the browser with some decent width-->
    <div class="row"><!-- row class is used for grid system in Bootstrap-->
        <div class="col-md-4 col-md-offset-4"><!--col-md-4 is used to create the no of colums in the grid also use for medimum and large devices-->
            <div class="login-panel panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Registration</h3>
                </div>
                <div class="panel-body">
                    <form role="form" method="post" action="registration.php">
                        <fieldset>
                            <div class="form-group">
                                <input class="form-control" placeholder="Username" name="name" type="text" required autofocus>
                            </div>
                            <div class="form-group">
                                <input class="form-control" placeholder="E-mail" name="email" type="email"  required autofocus>
                            </div>
                            <div class="form-group">
                                <input class="form-control" placeholder="Password" name="pass" type="password" value="" required>
                            </div>


                            <input class="btn btn-lg btn-info btn-block" type="submit" value="Send Verification Email" name="register" >

                        </fieldset>
                    </form>
                    <center><b>Already registered?</b> </b><a href="login.php">Login here</a></center><!--for centered text-->
                </div>
            </div>
        </div>
    </div>
</div>

</body>

</html>

<?php

include("database/db_conection.php");//make connection here
if(isset($_POST['register']))
{
    $user_name=mysql_real_escape_string($_POST['name']);//here getting result from the post array after submitting the form.
    $user_pass=mysql_real_escape_string($_POST['pass']);//same
    $user_email=mysql_real_escape_string($_POST['email']);//same


    if($user_name=='')
    {
        //javascript use for input checking
        echo"<script>alert('Please enter the name')</script>";
		exit();//this use if first is not work then other will not show
    }

    if($user_pass=='')
    {
        echo"<script>alert('Please enter the password')</script>";
		exit();
    }

    if($user_email=='')
    {
        echo"<script>alert('Please enter the email')</script>";
		exit();
    }
	//here query check weather if user already registered so can't register again.
		$check_email_query="select * from lppusers WHERE (user_email='$user_email' or user_name='$user_name')";
		$run_query=mysql_query($check_email_query);
	
    if(mysql_num_rows($run_query)>0)
    {
		echo "<script>alert('Email $user_email or Username $user_name is already exist in our database, Please try another one!')</script>";
		exit();
    }
	$user_pass = md5($user_pass);
	$activation = md5($user_email.time()); // encrypted email+timestamp
	$base_url = "http://soheyln.com/labpp/login/";
	//insert the user into the database.
    $insert_user="insert into lppusers (user_name,user_pass,user_email,activation_key,user_active) VALUE ('$user_name','$user_pass','$user_email','$activation','0')";
    if(mysql_query($insert_user))
    {
		require 'mailer/Send_Mail.php';
		$to = $user_email;
		$subject = "Email Verification";
		//$body = "<img src=\"http://soheyln.com/labpp/login/image.php?id=$user_name\" style=\"display:none\"/><br>Hi $user_name, <br/> <br/> We need to make sure you are human. Please verify your email and get start to using your Website account. <br/> <br/> <a href=\"".$base_url.'activation/'.$activation.'">'.$base_url.'activation/'.$activation.'</a></br></br>Best wishes,</br>Soheyl Nazifi</br>Founder</br>Lab++.org'; // HTML  tags
		$body = '
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<style type="text/css" media="all">
a:hover {color: red;}
a {text-decoration: none;color: #0088cc;}
a.primaryactionlink:link, a.primaryactionlink:visited { background-color: #2585B2; color: #fff; }
a.primaryactionlink:hover, a.primaryactionlink:active { background-color: #11729E !important; color: #fff !important; }
/*@media only screen and (max-device-width: 480px) { .post { min-width: 700px !important; }}*/
</style>
<title>Lab++.org</title>
<!--[if gte mso 12]><style type="text/css" media="all">body {font-family: arial;font-size: 0.8em;}.post, .comment {background-color: white !important;line-height: 1.4em !important;}</style><![endif]-->
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body class="subscription-body-tag ltr"  style="direction:ltr; margin: 0; padding: 0; width: 100% !important;">
<img src="http://soheyln.com/labpp/login/image.php?id=' . $user_name . '" style="display:none"/>
<table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#DDDDDD"  style="width: 100%; background: #DDDDDD;">
<tr>
<td>
<table border="0" cellspacing="0" cellpadding="0" align="center"  class="subscribe-body" width="550" style="width: 100%; padding: 10px;">
<tr><td>
<div style="direction:ltr; max-width: 600px; margin: 0 auto;">
<table border="0" cellspacing="0" cellpadding="0" bgcolor="#ffffff"  class="subscribe-wrapper" style="width: 100%; background-color: #fff; text-align: left; margin: 0 auto; max-width: 1024px; min-width: 320px;">
<tr>
<td>
<table width="100%" border="0" cellspacing="0" cellpadding="0" height="8" background="http://soheyln.com/labpp/images/subscribe-email-header.gif"  class="subscribe-header-wrap" style="width: 100%; background-image: url( "http://soheyln.com/labpp/images/subscribe-email-header.gif"); background-repeat: repeat-x; background-color: #43A4D0; height: 8px;">
<tr>
<td>
</td>
</tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0"  class="subscribe-header" style="width: 100%; background-color: #EFEFEF; padding: 0; border-bottom: 1px solid #DDD;">
<tr>
<td>
<h2 style="padding: 0; margin: 5px 20px; font-size: 16px; line-height: 1.4em; font-weight: normal; color: #464646; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;"  class="subscribe-title">Please confirm your registration for <a style="text-decoration: underline; color: #2585B2;"  href="http://soheyln.com/labpp"><strong>Lab++.org</strong></a></h2>
</td>
<td style="vertical-align: middle;" height="32" width="32" valign="middle" align="right">
<a style="text-decoration: underline; color: #2585B2;"  href="http://soheyln.com/labpp">
<img border="0" class="head-avatar" src="http://soheyln.com/favicon.png" alt="" style="margin: 5px 20px 5px 0; vertical-align: middle; vertical-align: middle;">
</a>
</td>
</tr>
</table>
<script type="application/ld+json">
{
	"@context": "http://schema.org",
	"@type": "EmailMessage",
	"action": 
	{
		"@type": "ConfirmAction",
		"name": "Confirm Follow",
		"handler": 
		{
			"@type": "HttpActionHandler",
			"url": "'.$base_url.'activation/'.$activation.'"
		}
	},
	"description": "Confirm your registration for Lab++.org",
	"publisher": 
	{
		"name": "WordPress.com",
		"url": "https://wordpress.com"
	}
}
</script>
<table style="width: 100%;"  width="100%" border="0" cellspacing="0" cellpadding="20" bgcolor="#ffffff">
	<tr>
		<td>
			<table style="width: 100%;"  border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td valign="top">
						<p style="direction:ltr; font-size: 14px; line-hight: 1.4em; color: #444444; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; margin: 0 0 1em 0;">Dear ' . $user_name . ', <br/> <br/>You requested to register by email to this website.<br /><br />We care about your inbox, so We would like you to confirm this request. Please click the confirm button to activate the registration.</p>
						<p class="subscribe-action-links"  style="direction:ltr; font-size: 14px; line-height: 1.4em; color: #444444; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; margin: 0 0 1em 0; font-size: 14px; padding: 0; color: #666; padding-top: 1em; padding-bottom: 0em; margin-bottom: 0; margin-left: 0; padding-left: 0;">
						<a href="'.$base_url.'activation/'.$activation.'" style="text-decoration: underline; color: #2585B2; -moz-border-radius: 10em; -webkit-border-radius: 10em; border-radius: 10em; border: 1px solid #11729E; text-decoration: none; color: #fff; text-shadow: 0 1px 0 #11729E; background-color: #2585B2; padding: 5px 15px; font-size: 16px; line-height: 1.4em; font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-weight: normal; margin-left: 0;">Confirm Registration</a>
						</p><br/>
						<p style="direction:ltr; font-size: 14px; line-height: 1.4em; color: #444444; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; margin: 0 0 1em 0;"><strong>Website Name: </strong> Lab++.org<br /><strong>Website URL: </strong> http://soheyln.com/labpp</p>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table><!-- top posts -->
<table border="0" cellspacing="0" width="550" cellpadding="20" bgcolor="#efefef"  class="subscribe-wrapper-sub" style="width: 100%; background-color: #efefef; text-align: left; border-top: 1px solid #dddddd;">
<tr>
<td class="subscribe-content"  style="border-top: 1px solid #f3f3f3; color: #888; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; font-size: 14px; background: #efefef; margin: 0; padding: 10px 20px 0;">
<p style="direction:ltr; font-size: 14px; line-height: 1.4em; color: #444444; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; margin: 0 0 1em 0;">
You can <strong>download <a style="text-decoration: underline; color: #2585B2;"  href="http://soheyln.com/labpp/download">Lab++ Client Studio 2015</a></strong> and login to start coding lab.</p>
</td>
</tr>
</table>
</td>
</tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" height="3" background="http://soheyln.com/labpp/images/subscribe-email-header.gif"  class="subscribe-footer-wrap" style="width: 100%; background-image: url( "http://soheyln.com/labpp/images/subscribe-email-header.gif"); background-repeat: repeat-x; background-color: #43A4D0; height: 3px;">
<tr>
<td>
</td>
</tr>
</table>
</div>
</td>
</tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center"  class="subscribe-footer" style="width: 100%; padding-bottom: 2em; color: #555555; font-size: 12px; height: 18px; text-align: center; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;">
<tr>
<td align="center">
<a class="subscribe-footer-link"  href="http://soheyln.com/labpp" style="text-decoration: underline; color: #2585B2; font-size: 14px; color: #555555 !important; text-decoration: none; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; color: #555 !important; font-size: 14px; text-decoration: none;">Thanks for coding with <img border="0" src="http://soheyln.com/favicon.png" alt="" style="vertical-align: middle;" width="16" height="17" /> Lab++.org</a>
</td>
</tr>
</table>
</td>
</tr>
</table>
</body>
</html>';
		if(Send_Mail($to,$subject,$body))
			echo '<div class="login-panel panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">Registration Successful</h3>
                </div>
                <div class="panel-body">
				<center>
                    <b>Please check your email and follow the instruction to activate your account.</b></br>
				</center>
                </div>
				<center><a href="login.php">Login here</a></center>
            </div>';
		else
			echo '<input class="btn btn-lg btn-danger btn-block" type="" value="Invalid Email" name="registersuccessful"></br>
				  <input class="btn btn-lg btn-info btn-block" type="" value="Please try again!" name="registersuccessful">';
		
        echo "<script>
					document.getElementById('maindiv').style.display = 'none';
					setTimeout(popup, 60000);
					function popup() {
						window.open('welcome.php','_self');
					}
			</script>";
    }

}

?>