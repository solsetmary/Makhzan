<?php
//header("Content-type: image/jpeg");
$db=mysql_connect("soheyln.com.mysql","soheyln_com","soheyln1357");
mysql_select_db("soheyln_com",$db);
$labID = $_REQUEST['mylab'];
$devID = $_REQUEST['mycam'];
$myquery = "select labID, devID, image_preview as labImage from prorobot_imagepreview where (labID=$labID and devID=$devID)";
$query=mysql_query($myquery);
$labImages = array();
print('[');
while ($row = mysql_fetch_assoc($query)) {
  //$labImages[] = base64_encode($row['labImage']);
  //$labImages[] = base64_encode($row['devImage']);
	print('{');
	print('"labID":"');print($row['labID']);print('","devID":"');print($row['devID']);print('",');
	print('"labImage":"');print($row['labImage']);print('"');
	print('},');
}
print('{"labID":"","devID":"","labImage":""}]');
mysql_close();
?>