<?php
$db=mysql_connect("soheyln.com.mysql","soheyln_com","soheyln1357");
mysql_select_db("soheyln_com",$db);
$check_exists_query="select * from lpp_sourcecode WHERE (labID=" . $_REQUEST['labID'] . " and devID=" . $_REQUEST['devID'] . ")";
$run_query=mysql_query($check_exists_query);
$row = mysql_fetch_assoc($run_query);
print($row['sourcecode']);
mysql_close();
?>