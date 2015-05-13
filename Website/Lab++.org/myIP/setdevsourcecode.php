<?php
include("../login/database/db_conection.php");
$check_exists_query="select * from lpp_sourcecode WHERE (labID=" . $_REQUEST['labID'] . " and devID=" . $_REQUEST['devID'] . ")";
$run_query=mysql_query($check_exists_query);
if(mysql_num_rows($run_query)>0)
{
	$query = "UPDATE lpp_sourcecode set sourcecode='" . $_REQUEST['scode'] . "' WHERE labID = " . $_REQUEST['labID'] . " and devID = " . $_REQUEST['devID'];
	$q=mysql_query($query);
}
else
{
	$query = "INSERT INTO lpp_sourcecode (labID, devID, sourcecode) VALUES (" . $_REQUEST['labID'] . ", " . $_REQUEST['devID'] . ", '".$_REQUEST['scode']."')";
	$q=mysql_query($query);
}
print($q);
mysql_close();
?>