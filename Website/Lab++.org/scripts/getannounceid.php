<?php
//header("Content-type: image/jpeg");
ob_start();
include("login/database/db_conection.php");
if(session_id() == '' || !isset($_SESSION)) {
    session_start();
}
$myquery = "select ID, sourcecode, date, time from lpp_announce WHERE ID=(SELECT MAX(ID) FROM lpp_announce)";
$query=mysql_query($myquery);
$res = '';
while ($row = mysql_fetch_assoc($query)) {
	$res .= $row['ID'];
}
mysql_close();
print $res;
ob_end_flush();
?>