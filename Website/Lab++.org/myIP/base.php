<?php
include("../login/database/db_conection.php");
$q=mysql_query("DROP TABLE IF EXISTS myIP");
$q=mysql_query("CREATE TABLE myIP (PID INT NOT NULL AUTO_INCREMENT, PRIMARY KEY(PID), PublicIP varchar(20))");
$q=mysql_query("INSERT INTO myIP (PID, PublicIP) VALUES (DEFAULT, '".$_REQUEST['publicip']."')");
print(json_encode("Done"));
mysql_close();
?>