<?php
$db=mysql_connect("soheyln.com.mysql","soheyln_com","soheyln1357");
mysql_select_db("soheyln_com",$db);
$q=mysql_query("DROP TABLE IF EXISTS myIP");
$q=mysql_query("CREATE TABLE myIP (PID INT NOT NULL AUTO_INCREMENT, PRIMARY KEY(PID), PublicIP varchar(20))");
$q=mysql_query("INSERT INTO myIP (PID, PublicIP) VALUES (DEFAULT, '".$_REQUEST['publicip']."')");
print(json_encode("Done"));
mysql_close();
?>