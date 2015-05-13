<?php
if(isset($_POST['announce']))
{
	$iCookieTime = time() + 24*60*60*30; // = 1 month
	if($_POST['status']=="notshowme"){
		setcookie("announce", $_POST['announce'], $iCookieTime, '/');
	}else{
		setcookie("announce", 'showme', $iCookieTime, '/');
	}
}
?>