<?php
$db=mysql_connect("soheyln.com.mysql","soheyln_com","soheyln1357");
mysql_select_db("soheyln_com",$db);
$check_exists_query="select * from prorobot_imagepreview WHERE (labID=" . $_REQUEST['labID'] . " and devID=" . $_REQUEST['devID'] . ")";
$run_query=mysql_query($check_exists_query);
if(mysql_num_rows($run_query)>0)
{
	$query = "UPDATE prorobot_imagepreview set image_preview='" . $_REQUEST['image'] . "' WHERE labID = " . $_REQUEST['labID'] . " and devID = " . $_REQUEST['devID'];
	$q=mysql_query($query);
}
else
{
	$query = "INSERT INTO prorobot_imagepreview (labID, devID, image_preview) VALUES (" . $_REQUEST['labID'] . ", " . $_REQUEST['devID'] . ", '".$_REQUEST['image']."')";
	$q=mysql_query($query);
}
print($q);
mysql_close();
?>