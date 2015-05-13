<?php
include("../login/database/db_conection.php");

$q=mysql_query("SELECT PublicIP FROM myIP WHERE PID=1");
while($e=mysql_fetch_assoc($q))
	$output[]=$e;
 
print(json_encode($output));
mysql_close();
?>
