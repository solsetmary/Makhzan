<?php
// error handler function
function myErrorHandler($errno, $errstr, $errfile, $errline)
{
    if (!(error_reporting() & $errno)) {
        // This error code is not included in error_reporting
        return;
    }

    switch ($errno) {
    case E_USER_ERROR:
        echo "<b>My ERROR</b><br />\n";
        echo "  Fatal error ";
        echo ", PHP " . PHP_VERSION . " (" . PHP_OS . ")<br />\n";
        echo "Aborting...<br />\n";
        exit(1);
        break;

    case E_USER_WARNING:
        echo "<b>My WARNING</b><br />\n";exit;
        break;

    case E_USER_NOTICE:
        echo "<b>My NOTICE</b><br />\n";exit;
        break;

    default:
        echo "Unknown error type<br />\n";exit;
        break;
    }

    /* Don't execute PHP internal error handler */
    return true;
}

function decryptRJ256($key,$iv,$string_to_decrypt)
{
    $string_to_decrypt = base64_decode($string_to_decrypt);
    $rtn = mcrypt_decrypt(MCRYPT_RIJNDAEL_256, $key, $string_to_decrypt, MCRYPT_MODE_CBC, $iv);
    $rtn = rtrim($rtn, "\0\4");
    return($rtn);
}

function encryptRJ256($key,$iv,$string_to_encrypt)
{
    $rtn = mcrypt_encrypt(MCRYPT_RIJNDAEL_256, $key, $string_to_encrypt, MCRYPT_MODE_CBC, $iv);
    $rtn = base64_encode($rtn);
    return($rtn);
}    

// set to the user defined error handler
$old_error_handler = set_error_handler("myErrorHandler");
$ky = 'lkirwf897+22#bbtrm8814z5qq=498j5'; // 32 * 8 = 256 bit key
$iv = '741952hheeyy66#cs!9hjv887mxx7@8y'; // 32 * 8 = 256 bit iv
$text = "";
$from_source = $_REQUEST['myquery'];

//header("Content-type: image/jpeg");
include("../login/database/db_conection.php");

if ($from_source == "1"){
	$q=mysql_query('select rl.labID, rm.devID, rl.Image as labImage, rm.Image as devImage from prorobot_status rs join prorobot_media rm on (rs.devID=rm.devID and rs.labID=rm.labID  and rs.devType=rm.devType) join prorobot_lab rl on (rs.labID=rl.labID) where (status="alive")');
}else{
	$dtext = decryptRJ256($ky, $iv, $from_source);
	$q=mysql_query($dtext);
}
$labImages = array();
//print('[');
$output =  '[';
while ($row = mysql_fetch_assoc($q)) {
	//print('{');
	$output .= '{';
	//print('"labID":"');print($row['labID']);print('","devID":"');print($row['devID']);print('",');
	$output .= '"labID":"';$output .= $row['labID'];$output .= '","devID":"';$output .= $row['devID'];$output .= '",';
	//print('"labImage":"');print(base64_encode($row['labImage']));print('","devImage":"');print(base64_encode($row['devImage']));print('"');
	$output .= '"labImage":"';$output .= base64_encode($row['labImage']);$output .= '","devImage":"';$output .= base64_encode($row['devImage']);$output .= '"';
	//print('},');
	$output .= '},';
}
//print('{"labImage":"","devImage":""}]');
$output .= '{"labImage":"","devImage":""}]';

$etext = encryptRJ256($ky, $iv, $output);
print($etext);

mysql_close();
?>