<?php

define('INCLUDE_CHECK',1);
require "functions.php";
require "connect.php";


if(ini_get('magic_quotes_gpc'))
$_POST['inputField']=stripslashes($_POST['inputField']);



$_POST['inputField'] = mysql_real_escape_string(strip_tags($_POST['inputField']),$link);

if(mb_strlen($_POST['inputField']) < 1 || mb_strlen($_POST['inputField'])>140)
die("0");

mysql_query("INSERT INTO demo_twitter_timeline SET tweet='".$_POST['inputField']."',dt=NOW()");

if(mysql_affected_rows($link)!=1)
die("0");

echo formatTweet($_POST['inputField'],time());

?>
