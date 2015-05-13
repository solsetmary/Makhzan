<?php
function Send_Mail($to,$subject,$body)
{
require 'class.phpmailer.php';
$from = "admin@labpp.org";
$mail = new PHPMailer();
$mail->IsSMTP(true); // SMTP
$mail->SMTPAuth   = false;  // SMTP authentication
$mail->Mailer = "smtp";
$mail->Host       = "mailout.one.com"; // Amazon SES server, note "tls://" protocol
$mail->Port       = 25;                    // set the SMTP port 465 if authentication is true
$mail->Username   = "admin@labpp.org";  // SES SMTP  username
$mail->Password   = "setareh.1388";  // SES SMTP password
$mail->SetFrom($from, 'Lab++.org Administration');
$mail->AddReplyTo($from,'Lab++.org Reply');
$mail->Subject = $subject;
$mail->MsgHTML($body);
$address = $to;
$mail->AddAddress($address, $to);

if(!$mail->Send())
	return false;
else
	return true;

}
?>