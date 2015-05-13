<?php
$db=mysql_connect("soheyln.com.mysql","soheyln_com","soheyln1357");
mysql_select_db("soheyln_com",$db);
$check_exists_query="select * from lppuser_sourcecode WHERE (labID=" . $_REQUEST['labID'] . " and devID=" . $_REQUEST['devID'] . " and user_name='" . $_REQUEST['uname'] . "')";
$run_query=mysql_query($check_exists_query);
if(mysql_num_rows($run_query)>0)
{
	$query = "UPDATE lppuser_sourcecode set sourcecode='" . $_REQUEST['scode'] . "' WHERE labID = " . $_REQUEST['labID'] . " and devID = " . $_REQUEST['devID'] . " and user_name='" . $_REQUEST['uname'] . "'";
	$q=mysql_query($query);
}
else
{
	$query = "INSERT INTO lppuser_sourcecode (labID, devID, user_name, sourcecode) VALUES (" . $_REQUEST['labID'] . ", " . $_REQUEST['devID'] . ", '" . $_REQUEST['uname'] . "', '".$_REQUEST['scode']."')";
	$q=mysql_query($query);
}
print($q);
mysql_close();
?>