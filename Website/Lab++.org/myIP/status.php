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
	$rtn = str_replace('\0', '', $rtn); // Replaces all spaces.
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


include("../login/database/db_conection.php");

if ($from_source == "1"){
	$q=mysql_query('select rs.*, rm.Comments as camComments, rl.Name as labName, rl.Comments as labComments, rl.hostBy as hosted, rl.labIP as labip, rl.note as labNote, rm.note as mediaNote from prorobot_status rs join prorobot_media rm on (rs.devID=rm.devID and rs.labID=rm.labID  and rs.devType=rm.devType) join prorobot_lab rl on (rs.labID=rl.labID) where (status="alive")');
}else{
	$dtext = decryptRJ256($ky, $iv, $from_source);
	$q=mysql_query($dtext);
}

while($e=mysql_fetch_assoc($q))
	$output[]=$e;

$etext = encryptRJ256($ky, $iv, json_encode($output));
print($etext);
mysql_close();
?>