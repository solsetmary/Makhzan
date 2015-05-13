<?php
include("../login/database/db_conection.php");
$q="SELECT count(*) FROM prorobot_status WHERE (labID='".$_REQUEST['labID']."' and devID='".$_REQUEST['devID']."' and devIndex='".$_REQUEST['devIndex']."' and devType='".$_REQUEST['devType']."')";
$result = mysql_query($q);
if (!$result) echo mysql_error();
//print(json_encode($q."<br>"));

if (mysql_result($result,0)==0){
	$q = "INSERT INTO prorobot_status (labID, devID, devIndex, devType, status, portNr) VALUES ('".$_REQUEST['labID']."', '" .$_REQUEST['devID']."', '".$_REQUEST['devIndex']."', '".$_REQUEST['devType']."', '".$_REQUEST['status']."', '".$_REQUEST['portNr']."')";
	$result = mysql_query($q);
	if (!$result) echo mysql_error();
}else{
	$q = "Update prorobot_status SET status='" .$_REQUEST['status']."', portNr='" .$_REQUEST['portNr']."' WHERE (labID='".$_REQUEST['labID']."' and devID='".$_REQUEST['devID']."' and devIndex='".$_REQUEST['devIndex']."')";
	$result = mysql_query($q);
	if (!$result) echo mysql_error();
}

//print(json_encode("<br>".$q));
mysql_close();
?>