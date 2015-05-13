<html>
<head lang="en">
    <meta charset="UTF-8">
    <link type="text/css" rel="stylesheet" href="bootstrap-3.2.0-dist/css/bootstrap.css">
    <title>Registration</title>
</head>
<style>
    .login-panel {
        margin-top: 30px;

</style>
<body>
<div class="container"><!-- container class is used to centered  the body of the browser with some decent width-->
    <div class="row"><!-- row class is used for grid system in Bootstrap-->
        <div class="col-md-4 col-md-offset-4"><!--col-md-4 is used to create the no of colums in the grid also use for medimum and large devices-->
				
<?php

include("database/db_conection.php");//make connection here
if(isset($_GET['id']))
{
	$activation_key=mysql_real_escape_string($_GET['id']);
	$check_user="select * from lppusers WHERE activation_key='$activation_key'";
	$run=mysql_query($check_user);
	if(mysql_num_rows($run))
	{
		$row = mysql_fetch_assoc($run);
		require 'mailer/Send_Mail.php';
		$base_url = "http://labpp.org/login/";
		$user_email = $row['user_email'];
		$user_name = $row['user_name'];
		$activation = $row['activation_key'];
		$to = $user_email;
		$subject = "Resending of Email Verification";
		#$body = "<img src=\"http://labpp.org/login/image.php?id=$user_name\" style=\"display:none\"/><br>Hi $user_name, <br/> <br/> We need to make sure you are human. Please verify your email and get start to using your Website account. <br/> <br/> <a href=\"".$base_url.'activation/'.$activation.'">'.$base_url.'activation/'.$activation.'</a></br></br>Best wishes,</br>Soheyl Nazifi</br>Founder</br>Lab++.org'; // HTML  tags
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
<img src="http://labpp.org/login/image.php?id=' . $user_name . '" style="display:none"/>
<table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#DDDDDD"  style="width: 100%; background: #DDDDDD;">
<tr>
<td>
<table border="0" cellspacing="0" cellpadding="0" align="center"  class="subscribe-body" width="550" style="width: 100%; padding: 10px;">
<tr><td>
<div style="direction:ltr; max-width: 600px; margin: 0 auto;">
<table border="0" cellspacing="0" cellpadding="0" bgcolor="#ffffff"  class="subscribe-wrapper" style="width: 100%; background-color: #fff; text-align: left; margin: 0 auto; max-width: 1024px; min-width: 320px;">
<tr>
<td>
<table width="100%" border="0" cellspacing="0" cellpadding="0" height="8" background="http://labpp.org/images/subscribe-email-header.gif"  class="subscribe-header-wrap" style="width: 100%; background-image: url( "http://labpp.org/images/subscribe-email-header.gif"); background-repeat: repeat-x; background-color: #43A4D0; height: 8px;">
<tr>
<td>
</td>
</tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0"  class="subscribe-header" style="width: 100%; background-color: #EFEFEF; padding: 0; border-bottom: 1px solid #DDD;">
<tr>
<td>
<h2 style="padding: 0; margin: 5px 20px; font-size: 16px; line-height: 1.4em; font-weight: normal; color: #464646; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif;"  class="subscribe-title">Please confirm your registration for <a style="text-decoration: underline; color: #2585B2;"  href="http://labpp.org"><strong>Lab++.org</strong></a></h2>
</td>
<td style="vertical-align: middle;" height="32" width="32" valign="middle" align="right">
<a style="text-decoration: underline; color: #2585B2;"  href="http://labpp.org">
<img border="0" class="head-avatar" src="http://labpp.org/favicon.png" alt="" style="margin: 5px 20px 5px 0; vertical-align: middle; vertical-align: middle;">
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
		"name": "Lab++.org",
		"url": "http://labpp.org"
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
						<p style="direction:ltr; font-size: 14px; line-height: 1.4em; color: #444444; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; margin: 0 0 1em 0;"><strong>Website Name: </strong> Lab++.org<br /><strong>Website URL: </strong> http://labpp.org</p>
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
You can <strong>download <a style="text-decoration: underline; color: #2585B2;"  href="http://labpp.org/download">Lab++ Client Studio 2015</a></strong> and login to start coding lab.</p>
</td>
</tr>
</table>
</td>
</tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" height="3" background="http://labpp.org/images/subscribe-email-header.gif"  class="subscribe-footer-wrap" style="width: 100%; background-image: url( "http://labpp.org/images/subscribe-email-header.gif"); background-repeat: repeat-x; background-color: #43A4D0; height: 3px;">
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
<a class="subscribe-footer-link"  href="http://labpp.org" style="text-decoration: underline; color: #2585B2; font-size: 14px; color: #555555 !important; text-decoration: none; font-family: &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; color: #555 !important; font-size: 14px; text-decoration: none;">Thanks for coding with <img border="0" src="http://labpp.org/favicon.png" alt="" style="vertical-align: middle;" width="16" height="17" /> Lab++.org</a>
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
                    <h3 class="panel-title">Resending Successful</h3>
                </div>
                <div class="panel-body">
				<center>
                    <b>Please check your email and follow the instruction to activate your account.</b></br>
				</center>
                </div>
            </div>';
		else
			echo '<input class="btn btn-lg btn-danger btn-block" type="" value="Invalid Email" name="resendsuccessful"></br>
				  <input class="btn btn-lg btn-info btn-block" type="" value="Please try again!" name="resendsuccessful">';
		
		echo "<script>
					setTimeout(popup, 4000);
					function popup() {
						window.parent.location.reload();
					}
			</script>";
	}
}

?>
			</br>
        </div>
    </div>
</div>
</body>

</html>