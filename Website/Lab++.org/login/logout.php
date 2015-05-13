<?php
if(session_id() == '' || !isset($_SESSION)) {    // session isn't started    
session_start();
}
session_destroy();
/*
ob_start(); //set output buffering before setting cookies..
setcookie('username', '', time() - 96 * 3600, '/');
setcookie('userpass', '', time() - 96 * 3600, '/');
//setcookie("cookie[useremail]", '');
//setcookie("cookie[userpass]", '');
unset($_COOKIE['username']);
unset($_COOKIE['userpass']);
ob_end_flush(); // flush data
*/
$_POST['logout']='logout';
//header("Location: login.php");//use for the redirection to some page
echo "<script>window.open('http://labpp.org/?logout=ok','_top');</script>";
//echo "<script type='text/javascript'>window.parent.location.reload();</script>";
?>