<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<?php
if(isset($_COOKIE['userlanguage']))
	echo $_COOKIE['userlanguage'];
else
	echo 'en';
?>">

<?php
	include("scripts/startup.php");
	include("scripts/applylang.php");
	include("scripts/header.php");
?>
<body>
<script type="text/javascript">
var myShowAnnounce = '<?php include("scripts/getannounceid.php"); ?>';
var myAnnounces = '<?php include("scripts/getannounce.php"); ?>';
var validsessionid = '<?php include("scripts/getsessionid.php"); ?>';
</script>

<?php include_once("scripts/analyticstracking.php") ?>

<!-- PRE LOADER -->
<div class="preloader">
    <div class="status">&nbsp;</div>
</div>
<!-- /.preloader -->

<?php
	include("scripts/navbar.php");
	include("scripts/SocialButtonsScroll.php");
	include("scripts/modalforms.php");
	include("scripts/banner.php");
?>

	<!--Login Form -->
    <div id="logindiv">
        <form class="form" action="#" id="login">
            <img src="button_cancel.png" class="img" id="loginimgCancel" />
        </form>
    </div>

	<!-- =========================
        About Us
    ============================== -->
    <section id="about-us">
        <div class="container">
<?php
	include("scripts/aboutus.php");
	include("scripts/ourgoal.php");
	include("scripts/theteam.php");
?>
        </div>
    </section>
	
    <!-- =========================
        Contact Us
    ============================== -->
<?php
	include("scripts/contactus.php");
?>
	
	<div id="tomove"></div>
	
	<pre  cols="60" name="code" rows="10" style="width:100%;height:100%;display:none;"></pre>
    
	<!-- =========================
        Footer
    ============================== -->
    <footer>
<?php
	include("scripts/footer.php");
?>
    </footer>
	<!--<script type="text/javascript">
		window.onload = function () { 
			dp.SyntaxHighlighter.ClipboardSwf = '/scripts/clipboard.swf';
			dp.SyntaxHighlighter.HighlightAll('code',true,false,false,1,false);
		}
	</script>-->
</body>
</html>
