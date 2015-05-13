<?php
$iCookieTime = time() + 24*60*60*30; // = 1 month
ob_start(); //set output buffering before setting cookies..
setcookie("userlanguage", $_POST['lang'], $iCookieTime, '/');
ob_end_flush(); // flush data
?>