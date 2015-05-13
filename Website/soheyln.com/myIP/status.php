<?php
$db=mysql_connect("soheyln.com.mysql","soheyln_com","soheyln1357");
mysql_select_db("soheyln_com",$db);

$q=mysql_query($_REQUEST['myquery']);
while($e=mysql_fetch_assoc($q))
	$output[]=$e;
 
print(json_encode($output));
mysql_close();
?>