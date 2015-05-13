<?php
//header("Content-type: image/jpeg");
$db=mysql_connect("soheyln.com.mysql","soheyln_com","soheyln1357");
mysql_select_db("soheyln_com",$db);
$myquery = "select labID, devID from prorobot_status where (devType='camera' and status='aLive')";
$query=mysql_query($myquery);
print('[');
while ($row = mysql_fetch_assoc($query)) {
	print('{');
	print('"labID":"');print($row['labID']);print('","devID":"');print($row['devID']);print('"');
	print('},');
}
print('{"labID":"","devID":""}]');
mysql_close();
?>