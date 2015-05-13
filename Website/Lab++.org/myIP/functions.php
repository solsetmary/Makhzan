<?php// error handler functionfunction myErrorHandler($errno, $errstr, $errfile, $errline){    if (!(error_reporting() & $errno)) {        // This error code is not included in error_reporting        return;    }    switch ($errno) {    case E_USER_ERROR:        echo "<b>My ERROR</b><br />\n";        echo "  Fatal error ";        echo ", PHP " . PHP_VERSION . " (" . PHP_OS . ")<br />\n";        echo "Aborting...<br />\n";        exit(1);        break;    case E_USER_WARNING:        echo "<b>My WARNING</b><br />\n";exit;        break;    case E_USER_NOTICE:        echo "<b>My NOTICE</b><br />\n";exit;        break;    default:        echo "Unknown error type<br />\n";exit;        break;    }    /* Don't execute PHP internal error handler */    return true;}function decryptRJ256($key,$iv,$string_to_decrypt){    $string_to_decrypt = base64_decode($string_to_decrypt);    $rtn = mcrypt_decrypt(MCRYPT_RIJNDAEL_256, $key, $string_to_decrypt, MCRYPT_MODE_CBC, $iv);    $rtn = rtrim($rtn, "\0\4");    return($rtn);}function encryptRJ256($key,$iv,$string_to_encrypt){    $rtn = mcrypt_encrypt(MCRYPT_RIJNDAEL_256, $key, $string_to_encrypt, MCRYPT_MODE_CBC, $iv);    $rtn = base64_encode($rtn);    return($rtn);}    ?>