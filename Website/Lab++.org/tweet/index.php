<?php

define('INCLUDE_CHECK',1);
require "functions.php";
require "connect.php";


// remove tweets older than 1 hour to prevent spam
mysql_query("DELETE FROM demo_twitter_timeline WHERE id>1 AND dt<SUBTIME(NOW(),'0 1:0:0')");
	
//fetch the timeline
$q = mysql_query("SELECT * FROM demo_twitter_timeline ORDER BY ID DESC");

$timeline='';
while($row=mysql_fetch_assoc($q))
{
	$timeline.=formatTweet($row['tweet'],$row['dt']);
}

// fetch the latest tweet
$lastTweet = '';
list($lastTweet) = mysql_fetch_array(mysql_query("SELECT tweet FROM demo_twitter_timeline ORDER BY id DESC LIMIT 1"));

if(!$lastTweet) $lastTweet = "You don't have any tweets yet!";

?>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Making Our Own Twitter Timeline</title>

<link rel="stylesheet" type="text/css" href="demo.css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
<script type="text/javascript" src="script.js"></script>


</head>

<body>

<div id="twitter-container">
<form id="tweetForm" action="submit.php" method="post">

<span class="counter">140</span>
<label for="inputField">What are you doing?</label>

<textarea name="inputField" id="inputField" tabindex="1" rows="2" cols="40"></textarea>
<input class="submitButton inact" name="submit" type="submit" value="update" disabled="disabled" />

<span class="latest"><strong>Latest: </strong><span id="lastTweet"><?=$lastTweet?></span></span>

<div class="clear"></div>

</form>

<h3 class="timeline">Timeline</h3>

<ul class="statuses"><?=$timeline?></ul>


</div>

</body>
</html>
