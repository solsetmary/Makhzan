<?php
require 'Send_Mail.php';
$to = "solsetmary@gmail.com";
$subject = "Test Mail Subject";
$body = "Hi<br/>Test Mail<br/>Amazon SES"; // HTML  tags
if(Send_Mail($to,$subject,$body))
	echo "<h1>Sent.</h1>";
else
	echo "<h1>Not Sent.</h1>";
?>
