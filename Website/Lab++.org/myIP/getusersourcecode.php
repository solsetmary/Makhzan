<?php
include("../login/database/db_conection.php");
$check_exists_query="select * from lppuser_sourcecode WHERE (labID=" . $_REQUEST['labID'] . " and devID=" . $_REQUEST['devID'] . " and user_name='" . $_REQUEST['uname'] . "')";
$run_query=mysql_query($check_exists_query);
$row = mysql_fetch_assoc($run_query);
print($row['sourcecode']);
mysql_close();
?>