<?php
include 'database/db_conection.php';
$msg='';
if(!empty($_GET['code']) && isset($_GET['code']))
{
$code=mysql_real_escape_string($_GET['code']);
$c=mysql_query("SELECT id FROM lppusers WHERE activation_key='$code'");

if(mysql_num_rows($c) > 0)
{
$count=mysql_query("SELECT id FROM lppusers WHERE activation_key='$code' and user_active=0");

if(mysql_num_rows($count) == 1)
{
mysql_query("UPDATE lppusers SET user_active=1 WHERE activation_key='$code'");
$msg="Your account is activated"; 
}
else
{
$msg ="Your account is already active, no need to activate again";
}

}
else
{
$msg ="Wrong activation code.";
}

}
?>

<?php echo $msg; ?>