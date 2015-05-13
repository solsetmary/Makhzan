<?php
//header("Content-type: image/jpeg");
include("../login/database/db_conection.php");
$sid = $_REQUEST['sid'];
$phpsid = $_COOKIE[PHPSESSID];
if($sid!='reload'){
if($sid!=$phpsid){
	exit('Invalid Session. Please Login.');
}
}
$labID = $_REQUEST['first'];
$devID = $_REQUEST['second'];
$myquery = "select labID, devID, image_preview as labImage from prorobot_imagepreview where (labID=$labID and devID=$devID)";
$query=mysql_query($myquery);
$labImages = array();
print('[');
while ($row = mysql_fetch_assoc($query)) {
  //$labImages[] = base64_encode($row['labImage']);
  //$labImages[] = base64_encode($row['devImage']);
	print('{');
	print('"first":"');print($row['labID']);print('","second":"');print($row['devID']);print('",');
	print('"third":"');print($row['labImage']);print('"');
	print('},');
}
print('{"first":"","second":"","third":""}]');
mysql_close();
?>