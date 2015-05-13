<?php

//put connection to database here
include("login/database/db_conection.php");

$filename = mysql_real_escape_string($_GET['file']);
$path = $_SERVER['DOCUMENT_ROOT']."/"; //path of this file
$fullPath = $path.$filename; //path to download file

$filetypes = array("rar","zip","pdf","apk");

if (!in_array(substr($filename, -3), $filetypes)) {
	echo "Invalid download type.";
	exit;
}

if ($fd = fopen ($fullPath, "r")) {
	//add download stat
	$result = mysql_query("SELECT COUNT(*) AS countfile FROM lpp_download
	WHERE filename='" . $filename . "'");
	$data = mysql_fetch_array($result);
	$q = "";
	
	if ($data['countfile'] > 0) {
		$q = "UPDATE lpp_download SET stats = stats + 1 WHERE
		filename = '" . $filename . "'";
	} else {
		$q = "INSERT INTO lpp_download (filename, stats) VALUES
		('" . $filename . "', 1)";
	}
	$statresult = mysql_query($q);
	
	//the next part outputs the file
	$fsize = filesize($fullPath);
	$path_parts = pathinfo($fullPath);

	header("Content-type: application/octet-stream");
	header("Content-Disposition: filename=\"".$path_parts["basename"]."\"");
	header("Content-length: $fsize");
	header("Cache-control: private"); //use this to open files directly
	while(!feof($fd)) {
		$buffer = fread($fd, 2048);
		echo $buffer;
	}
}
fclose ($fd);
exit;

?>