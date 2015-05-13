<?php
ob_start();

if(session_id() == '' || !isset($_SESSION)) {
    // session isn't started
	//echo session_id();
    session_start();
}
$tourdone_name= $_COOKIE['tourdone'];
echo '
	<script type="text/javascript" src="js/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="js/jquery-migrate-1.2.1.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
	<script type="text/javascript" src="js/bootstrap-tour.min.js"></script>
    <script type="text/javascript" src="js/jquery.nav.js"></script>
    <script type="text/javascript" src="js/wow.min.js"></script>
    <script type="text/javascript" src="js/lightbox.min.js"></script>
    <script type="text/javascript" src="js/main.js"></script>
	<!-- prefixfree 
	<script type="text/javascript" src="js/prefixfree-1.0.7.js"></script>-->
	<script type="text/javascript" src="scripts/Core.Labpp.js"></script>';

if(isset($_COOKIE['username']) && !isset($_GET['logout']))
{
	//echo $_COOKIE['username'] . $_COOKIE['userpass'];
	include("login/database/db_conection.php");
	$user_name=mysql_real_escape_string($_COOKIE['username']);
    $user_pass=mysql_real_escape_string($_COOKIE['userpass']);
	$check_user="select * from lppusers WHERE user_name='$user_name' AND user_pass='$user_pass'";
	$run=mysql_query($check_user);
	if(mysql_num_rows($run))
	{
		//$row = mysql_fetch_assoc($run);
		//$username = $row['user_name'];
		$_SESSION['name']=$user_name;//here session is used and value of $user_email store in $_SESSION.
		echo '
		<script>
			$(document).ready(function () {
				$("#onclick").html("Log.Out()");
				//$("#onclick").html("{' . $user_name . '}");
				$("#service-three-contactus").html("<iframe src=\"./login/welcome.php\" width=\"100%\" height=\"300px\" frameborder=\"0\" style=\"border: 0;\"></iframe>");
			});
		</script>';
	}
	ob_end_flush();
	return;
}

ob_end_flush();

if(!$_SESSION['name']){
//if(!$_SESSION['email'])
echo <<< STARTINGwelcome
	<script>
		$(document).ready(function () {
			$("#service-three-contactus").html("<iframe src=\"./login/welcome.php\" width=\"100%\" height=\"400px\" frameborder=\"0\" style=\"border: 0\"></iframe>");
		});
	</script>
STARTINGwelcome;
if(!isset($_COOKIE['tourdone']))
{

	echo <<< STARTING
	<script>
		$(document).ready(function () {
			$("#service-three-contactus").html("<iframe src=\"./login/welcome.php\" width=\"100%\" height=\"400px\" frameborder=\"0\" style=\"border: 0\"></iframe>");
            setTimeout(popup, 5000);
			var tour;
            function popup() {
                //$("#logindiv").css("display", "block");
				// Instance the tour
				var t = '<div class="popover tour-tour tour-tour-0 fade bottom in" role="tooltip" id="step-0" style="top: 299px; left: 574.453125px; display: block;"> <div class="arrow"></div> <h3 class="popover-title">Welcome to Bootstrap Tour!</h3> <div class="popover-content">Introduce new users to your product by walking them through it step by step.</div> <div class="popover-navigation"> <div class="btn-group"> <button class="btn btn-sm btn-default disabled" data-role="prev">« Prev</button> <button class="btn btn-sm btn-default" data-role="next">Next »</button>  </div> <button class="btn btn-sm btn-default" data-role="end">End</button> </div> </div>';
						
				tour = new Tour({
					name: "tour",
					steps: [
						{
							title: "Welcome Newbie",
							orphan: true,
							backdrop : true,
							content: "We are happy that you are visiting us, you can continue this short tour to know some tips on the website. Or do it later in <strong>About('US')</strong> section."
						},
						{
							element: "#country-select",
							title: "Language",
							backdrop : true,
							content: "You can choose the proper language: English, Deutsch, French and Nederlands."
						},
						{
							element: "#scroll-top-button",
							title: "Scroll Top",
							backdrop : true,
							content: "In every section of this page, you can back on top by clicking this button"
						},
						{
							element: "#StaticSocialButtons",
							title: "Social Buttons",
							content: "Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus."
						},
						{
							element: "#onclick",
							title: "Membership",
							content: "Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum."
						},
						{
							element: "#golab",
							title: "Scheduale Lab",
							content: "Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum."
						},
						{
							element: "#DownloadLABpp",
							title: "Download Lab++ Studio",
							content: "Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum."
						},
						{
							element: "#livelab",
							title: "Live Lab Preview",
							content: "Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum."
						}
					],
					container: "body",
					keyboard: true,
					storage: window.localStorage,
					debug: false,
					backdrop : false,
					backdropPadding: 0,
					redirect: true,
					orphan: false,
					duration: false,
					delay: false,
					basePath: "",
					template: t,
					afterGetState: function (key, value) {},
					afterSetState: function (key, value) {},
					afterRemoveState: function (key, value) {},
					onStart: function (tour) {},
					onEnd: function (tour) {},
					onShow: function (tour) {},
					onShown: function (tour) {},
					onHide: function (tour) {},
					onHidden: function (tour) {},
					onNext: function (tour) {},
					onPrev: function (tour) {},
					onPause: function (tour, duration) {},
					onResume: function (tour, duration) {}
				});
				// Initialize the tour
				tour.init();

				// Start the tour
				tour.restart();
            }
		});
	</script>
STARTING;
    //header("Location: login.php");//redirect to login page to secure the welcome page without login access.
}}
else
{
	include("login/database/db_conection.php");
	//$user_email=mysql_real_escape_string($_SESSION['email']);
	$user_name=mysql_real_escape_string($_SESSION['name']);
	//$check_user="select * from lppusers WHERE user_email='$user_email'";
	$check_user="select * from lppusers WHERE user_name='$user_name'";
	$run=mysql_query($check_user);
	//$username = $_SESSION['email'];
	$username = $_SESSION['name'];
	if(mysql_num_rows($run))
	{
		$row = mysql_fetch_assoc($run);
		$username = $row['user_name'];
	}
	echo '
	<script>
		$(document).ready(function () {
			$("#onclick").html("Log.Out()");
			//$("#onclick").html("{' . $username . '}");
			$("#service-three-contactus").html("<iframe src=\"./login/welcome.php\" width=\"100%\" height=\"300px\" frameborder=\"0\" style=\"border: 0;\"></iframe>");
		});
	</script>';
}

?>