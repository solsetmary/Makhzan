<?php 
if(session_id() == '') 
{
	ini_set('session.cookie_httponly', 1);
	session_start();
}
if ($_COOKIE[PHPSESSID] != session_id())
{
	$_COOKIE[PHPSESSID] = session_id();
	echo "reload";
}else
{
	$_COOKIE[PHPSESSID] = session_id();
	echo $_COOKIE[PHPSESSID];//
}
?>