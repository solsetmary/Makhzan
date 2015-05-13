<?php
//header("Content-type: image/jpeg");
include("../login/database/db_conection.php");
$sid = $_REQUEST['sid'];
$phpsid = $_COOKIE[PHPSESSID];
if($sid!='reload'){
if($sid!=$phpsid){
	exit('Invalid Session.'.$sid.' Please Login.'.$phpsid);
}
}
$myquery = "select labID, devID from prorobot_status where (devType='camera' and status='aLive')";
$query=mysql_query($myquery);
print('[');
while ($row = mysql_fetch_assoc($query)) {
	print('{');
	print('"first":"');print($row['labID']);print('","second":"');print($row['devID']);print('"');
	print('},');
}
print('{"first":"","second":""}]');
mysql_close();
?>