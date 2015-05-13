<?php

//header("Content-type: image/jpeg");
$db=mysql_connect("soheyln.com.mysql","soheyln_com","soheyln1357");
mysql_select_db("soheyln_com",$db);

$query=mysql_query($_REQUEST['myquery']);
$labImages = array();
print('[');
while ($row = mysql_fetch_assoc($query)) {
  //$labImages[] = base64_encode($row['labImage']);
  //$labImages[] = base64_encode($row['devImage']);
	print('{');
	print('"labID":"');print($row['labID']);print('","devID":"');print($row['devID']);print('",');
	print('"labImage":"');print(base64_encode($row['labImage']));print('","devImage":"');print(base64_encode($row['devImage']));print('"');
	print('},');
}
print('{"labImage":"","devImage":""}]');
//print(json_encode($labImages));


mysql_close();
?>