<?php
//if(session_id() == '' || !isset($_SESSION) || !isset($_COOKIE['user_email']) || !isset($_COOKIE['user_pass'])) {
if(session_id() == '' || !isset($_SESSION) || !isset($_COOKIE['user_name']) || !isset($_COOKIE['user_pass'])) {
    // session isn't started
    session_start();
}
echo '
	<script type="text/javascript" src="js/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="js/jquery-migrate-1.2.1.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/jquery.nav.js"></script>
    <script type="text/javascript" src="js/wow.min.js"></script>
    <script type="text/javascript" src="js/lightbox.min.js"></script>
    <script type="text/javascript" src="js/main.js"></script>';

if(!$_SESSION['name'])
//if(!$_SESSION['email'])
//if(!isset($_COOKIE['user_email']))
{
	echo '
	<script>
		$(document).ready(function () {
			$("#service-three-contactus").html("<iframe src=\"./login/welcome.php\"  width=\"100%\" height=\"400px\" frameborder=\"0\" style=\"border: 0\"></iframe>");
            //setTimeout(popup, 3000);
            function popup() {
                $("#logindiv").css("display", "block");
                //$("#contactdiv").css("display", "block");
            }
		});
	</script>';
    //header("Location: login.php");//redirect to login page to secure the welcome page without login access.
}
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
			$("#onclick").html("{' . $username . '}");
			$("#service-three-contactus").html("<iframe src=\"./login/welcome.php\"  width=\"100%\" height=\"300px\" frameborder=\"0\" style=\"border: 0\"></iframe>");
		});
	</script>';
}

?>
<html lang="en">
<head>
    <script type="text/javascript">var _gaq = _gaq || []; _gaq.push(['_setSiteSpeedSampleRate', 100]);</script>
    <!-- =================================
        Basic Information
        ================================== -->
    <meta charset="utf-8">
    <title>Lab++.org | Remote Coding in Lab</title>
    <meta name="keywords" content="Lab, Lab++, Lab++.org, Remote, Coding, Robot, Learning, Arduino, Labpp, Labplusplus, Lpp, microcontroller, client, server">
    <meta name="description" content="Lab++ - Remote Coding in Lab">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <!-- =================================
        Stylesheet
        ================================== -->
    <!-- Google Fonts -->
    <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Roboto:400,300,300italic,400italic,700,700italic,900" type="text/css">
    <!-- Animation by Animate.css -->
    <link rel="stylesheet" href="http://soheyln.com/labpp/css/animate2.css" />
    <link rel="stylesheet" href="http://soheyln.com/labpp/css/labpp.css" />
    <!-- Font Awesome -->
    <!-- Lightbox -->
    <!-- Bootstrap Css Plugin -->
    <!-- Styles -->
    <!-- Responsive Style -->
    <!-- Theme Color -->
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
              <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
              <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->
</head>
<body>

<!-- PRE LOADER -->
<div class="preloader">
    <div class="status">&nbsp;</div>
</div>
<!-- /.preloader -->
<header>
    <!-- STICKY NAVIGATION -->
    <nav class="navbar navbar-custom navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="navbar-header page-scroll">
                        <!-- MENU BOR MOVILE DEVICES -->
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#hrs-navigation">
                            <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
                        </button>
                        <!-- /.navbar-toggle -->
                        <!-- LOGO ON STICKY NAV BAR -->
                        <a class="logo" href="#home">
                            <img class="img-responsive" src="images/logo-lppui.png" alt="Lab++.org" pagespeed_url_hash="1084840945">
                        </a>
                    </div>
                    <!-- /.navbar-header -->
                    <!-- NAVIGATION LINKS -->
                    <div class="navbar-collapse collapse" id="hrs-navigation">
                        <ul class="nav navbar-nav navbar-right main-navigation">
                            <li class="page-scroll"><a href="#home">Home</a> </li>
                            <li class="page-scroll"><a href="#features">Download</a> </li>
                            <li class="page-scroll"><a href="#services">Book Lab</a> </li>
                            <li class="page-scroll"><a href="#portfolio">Tutorial</a> </li>
                            <li class="page-scroll"><a href="#about-us">About Us</a> </li>
                            <li class="page-scroll"><a href="#contact-us">Blog</a> </li>
                            <li class="page-scroll"><a href="#" id="onclick">login</a> </li>
                            <li class="page-scroll"><a href="https://wrapbootstrap.com/?ref=hasibrsohel" class="external">Supporters</a> </li>
                        </ul>
                        <!-- /.navbar-nav -->
                    </div>
                    <!-- /.navbar-collapse -->
                </div>
                <!-- / .navbar-header -->
            </div>
        </div>
    </nav>
</header>


    <!-- =========================
        Banner
    ============================== -->
    <section id="home" class="banner">
        <div class="full-screen-bnr">
            <div class="full-screen-bnr-content color-overlay">
                <div class="container">
                    <h1 class="heading text-uppercase wow flipInX" data-wow-delay="1s">Code your robot in real lab</h1>
                    <p class="sub-head margin-bottom-40 wow fadeIn" data-wow-delay="1.5s">Now, are you ready to learn how to program a robot step by step, it is easy and fun ;)</br>Also, you can help us to go on:</p>

                    <div class="row text-left wow fadeIn">
                        <div class="col-md-4 col-sm-6 col-xs-12 feature-item">
                            <i class="feature-icon fa "></i>
                            <div class="feature-content">
                                <p class="sub-head margin-bottom-40 wow fadeIn" data-wow-delay="1.5s">By donating, we put your name and/or your picture on website, our softwares, lab's camera and lab's info, more Infos on next step.</p>
                                <a href="#" class="btn btn-outlined btn-white btn-lg wow fadeIn" data-wow-delay="1.7s">I want to donate</a>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-6 col-xs-12 feature-item">
                            <i class="feature-icon fa "></i>
                            <div class="feature-content">
                                <p class="sub-head margin-bottom-40 wow fadeIn" data-wow-delay="1.5s">You can join to the supporter's community of making labs that known as maker's lab and share it with the world.</br></p>
                                <a href="#" class="btn btn-outlined btn-white btn-lg wow fadeIn" data-wow-delay="1.7s">Let's Make a lab</a>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-6 col-xs-12 feature-item">
                            <i class="feature-icon fa "></i>
                            <div class="feature-content">
                                <p class="sub-head margin-bottom-40 wow fadeIn" data-wow-delay="1.5s">Start to code your Robot, there are some tutorials to learn how to program and also you need to download the Lab++ Client studio 2015</br></p>
                                <a href="#" class="btn btn-outlined btn-white btn-lg wow fadeIn" data-wow-delay="1.7s">I want to code</a>
                            </div>
                        </div>
                    </div>
                    <div class="row text-center margin-top-50" style="display: none">
                        <a href="#features" class="btn btn-circle page-scroll">
                            <i class="fa fa-angle-double-down animated" data-wow-delay="2s"></i>
                        </a>
                    </div>
                </div>
            </div>
            <!-- / .full-screen-bnr-content -->
        </div>
        <!-- / .full-screen-bnr -->
    </section>
    <!-- / .banner -->
    <!-- =========================
        Features
    ============================== -->
    <section id="features">
        <div class="container">
            <div class="row text-center margin-bottom-50">
                <div class="col-xs-12">
                    <h3 class="font-300 margin-bottom-20 wow bounceIn">It's not just a template. It's a digital sales person presenting your product.</h3>
                    <p class="wow fadeIn" data-wow-delay="0.2s">Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. </p>
                </div>
            </div>
            <div class="row text-left wow fadeIn">
                <div class="col-md-4 col-sm-6 col-xs-12 feature-item">
                    <i class="feature-icon fa fa-file-archive-o"></i>
                    <div class="feature-content">
                        <h4>Unique Template</h4>
                        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.</p>
                    </div>
                    <!-- / .feature-content -->
                </div>
                <!-- / .feature-item -->
                <div class="col-md-4 col-sm-6 col-xs-12 feature-item">
                    <i class="feature-icon fa fa-bolt"></i>
                    <div class="feature-content">
                        <h4>Amazing Features</h4>
                        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.</p>
                    </div>
                    <!-- / .feature-content -->
                </div>
                <!-- / .feature-item -->
                <div class="col-md-4 col-sm-6 col-xs-12 feature-item">
                    <i class="feature-icon fa fa-gears"></i>
                    <div class="feature-content">
                        <h4>Easy To Customize</h4>
                        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.</p>
                    </div>
                    <!-- / .feature-content -->
                </div>
                <!-- / .feature-item -->
                <div class="col-md-4 col-sm-6 col-xs-12 feature-item">
                    <i class="feature-icon fa fa-compass"></i>
                    <div class="feature-content">
                        <h4>Clean &amp; Modern</h4>
                        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.</p>
                    </div>
                    <!-- / .feature-content -->
                </div>
                <!-- / .feature-item -->
                <div class="col-md-4 col-sm-6 col-xs-12 feature-item">
                    <i class="feature-icon fa fa-arrows-alt"></i>
                    <div class="feature-content">
                        <h4>Flexible Layout</h4>
                        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.</p>
                    </div>
                    <!-- / .feature-content -->
                </div>
                <!-- / .feature-item -->
                <div class="col-md-4 col-sm-6 col-xs-12 feature-item">
                    <i class="feature-icon fa fa-trophy"></i>
                    <div class="feature-content">
                        <h4>Award Winning Design</h4>
                        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa.</p>
                    </div>
                    <!-- / .feature-content -->
                </div>
                <!-- / .feature-item -->
            </div>
        </div>
    </section>
    <!-- =========================
        Promo
    ============================== -->
    <section class="secondary-bg">
        <div class="container">
            <div class="row text-center">
                <div class="col-md-9 col-sm-12">
                    <h3 class="text-uppercase font-700 wow fadeInDown">We are growing faster with over 25,000+ satisfied customers</h3>
                </div>
                <div class="visible-xs padding-5"></div>
                <div class="col-md-3 col-sm-12">
                    <a href="#" class="btn btn-outlined btn-black btn-lg wow rotateIn">Purchase Now</a>
                </div>
            </div>
        </div>
    </section>
    <!-- =========================
        Services
    ============================== -->
    <section id="services">
        <div class="container">
            <div class="row text-center margin-bottom-20">
                <div class="col-xs-12">
                    <h1 class="text-uppercase font-300 margin-bottom-30 wow fadeIn">Services</h1>
                    <p class="lead wow fadeIn" data-wow-delay="0.2s">Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.</p>
                </div>
            </div>
            <div class="visible-lg padding-20"></div>
            <div class="row text-center margin-bottom-20">
                <div class="col-sm-4 col-xs-12 service-item theme-bg wow fadeInLeft">
                    <div class="bg-black-0 service-item-content">
                        <i class="icon-white fa fa-university"></i>
                        <h4>Brand Development</h4>
                        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes</p>
                        <div class="margin-40"></div>
                        <a href="#" class="btn btn-outlined btn-white">Check Details</a>
                    </div>
                    <!--/.service-item-content -->
                </div>
                <!--/.service-item -->
                <div class="col-sm-4 col-xs-12 service-item theme-bg wow fadeIn" data-wow-delay="0.2s">
                    <div class="bg-black-1 service-item-content">
                        <i class="icon-white fa fa-code"></i>
                        <h4>Web Development</h4>
                        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes</p>
                        <div class="margin-40"></div>
                        <a href="#" class="btn btn-outlined btn-white">Check Details</a>
                    </div>
                    <!--/.service-item-content -->
                </div>
                <!--/.service-item -->
                <div class="col-sm-4 col-xs-12 service-item theme-bg wow fadeInRight">
                    <div class="bg-black-2 service-item-content">
                        <i class="icon-white fa fa-briefcase"></i>
                        <h4>Online Marketing</h4>
                        <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes</p>
                        <div class="margin-40"></div>
                        <a href="#" class="btn btn-outlined btn-white">Check Details</a>
                    </div>
                    <!--/.service-item-content -->
                </div>
                <!--/.service-item -->
            </div>
            <div class="visible-lg padding-20"></div>
            <div class="visible-xs padding-10"></div>
            <!-- =========================
                How We Go / Process
            ============================== -->
            <div class="row text-center margin-bottom-40">
                <div class="col-xs-12">
                    <h3 class="text-uppercase font-700 margin-bottom-30 wow fadeIn">How We Go</h3>
                    <p class="lead wow fadeIn" data-wow-delay="0.2s">Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.</p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 col-sm-2 col-sm-offset-1 process-item wow fadeInLeft" data-wow-delay="0.1s">
                    <i class="process-icon fa fa-comments-o"></i>
                    <p class="text-center">Discovery Idea</p>
                    <i class="arrow-right fa fa-chevron-right visible-md visible-lg"></i>
                </div>
                <!--/.process-item -->
                <div class="col-md-2 col-sm-2 process-item wow fadeInLeft" data-wow-delay="0.3s">
                    <i class="process-icon fa fa-pencil-square-o"></i>
                    <p class="text-center">Creative Design</p>
                    <i class="arrow-right fa fa-chevron-right visible-md visible-lg"></i>
                </div>
                <!--/.process-item -->
                <div class="col-md-2 col-sm-2 process-item wow fadeInLeft" data-wow-delay="0.5s">
                    <i class="process-icon fa fa-code"></i>
                    <p class="text-center">Development</p>
                    <i class="arrow-right fa fa-chevron-right visible-md visible-lg"></i>
                </div>
                <!--/.process-item -->
                <div class="col-md-2 col-sm-2 process-item wow fadeInLeft" data-wow-delay="0.7s">
                    <i class="process-icon fa fa-check-square-o"></i>
                    <p class="text-center">Quality Assurance</p>
                    <i class="arrow-right fa fa-chevron-right visible-md visible-lg"></i>
                </div>
                <!--/.process-item -->
                <div class="col-md-2 col-sm-2 process-item wow fadeInLeft" data-wow-delay="0.9s">
                    <i class="process-icon fa fa-send-o"></i>
                    <p class="text-center">Go Live</p>
                </div>
                <!--/.process-item -->
            </div>
        </div>
    </section>
    <!-- =========================
        Portfolio
    ============================== -->
    <section id="portfolio" class="theme-bg">
        <div class="container">
            <div class="row text-center margin-bottom-30">
                <div class="col-xs-12">
                    <h1 class="text-uppercase text-color-white font-300 margin-bottom-30 wow fadeIn">Portfolio</h1>
                    <p class="lead text-color-white font-300 wow fadeIn" data-wow-delay="0.2s">Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.</p>
                </div>
            </div>
            <div class="portfolio-gallery row text-center">
                <div class="col-sm-4 col-xs-6 portfolio-item wow fadeIn">
                    <div class="portfolio-item-info">
                        <h4>Portfolio Item Title</h4>
                        <p>Item short description</p>
                        <a href="images/image-1-big.jpg" class="btn btn-outlined btn-white btn-sm" data-lightbox="image-1" data-title="Image title here">View the item</a>
                    </div>
                    <img class="img-responsive" src="images/image-1.jpg" alt="Lightbox" pagespeed_url_hash="2248183927">
                </div>
                <!--/.portfolio-item -->
                <div class="col-sm-4 col-xs-6 portfolio-item wow fadeIn">
                    <div class="portfolio-item-info">
                        <h4>Portfolio Item Title</h4>
                        <p>Item short description</p>
                        <a href="images/image-2-big.jpg" class="btn btn-outlined btn-white btn-sm" data-lightbox="image-1" data-title="Image title here">View the item</a>
                    </div>
                    <img class="img-responsive" src="images/image-2.jpg" alt="Lightbox" pagespeed_url_hash="2542683848">
                </div>
                <!--/.portfolio-item -->
                <div class="col-sm-4 col-xs-6 portfolio-item wow fadeIn">
                    <div class="portfolio-item-info">
                        <h4>Portfolio Item Title</h4>
                        <p>Item short description</p>
                        <a href="images/image-3-big.jpg" class="btn btn-outlined btn-white btn-sm" data-lightbox="image-1" data-title="Image title here">View the item</a>
                    </div>
                    <img class="img-responsive" src="images/image-3.jpg" alt="Lightbox" pagespeed_url_hash="2837183769">
                </div>
                <!--/.portfolio-item -->
                <div class="col-sm-4 col-xs-6 portfolio-item wow fadeIn" data-wow-delay="0.3s">
                    <div class="portfolio-item-info">
                        <h4>Portfolio Item Title</h4>
                        <p>Item short description</p>
                        <a href="images/image-4-big.jpg" class="btn btn-outlined btn-white btn-sm" data-lightbox="image-1" data-title="Image title here">View the item</a>
                    </div>
                    <img class="img-responsive" src="images/image-4.jpg" alt="Lightbox" pagespeed_url_hash="3131683690">
                </div>
                <!--/.portfolio-item -->
                <div class="col-sm-4 col-xs-6 portfolio-item wow fadeIn" data-wow-delay="0.2s">
                    <div class="portfolio-item-info">
                        <h4>Portfolio Item Title</h4>
                        <p>Item short description</p>
                        <a href="images/image-5-big.jpg" class="btn btn-outlined btn-white btn-sm" data-lightbox="image-1" data-title="Image title here">View the item</a>
                    </div>
                    <img class="img-responsive" src="images/image-5.jpg" alt="Lightbox" pagespeed_url_hash="3426183611">
                </div>
                <!--/.portfolio-item -->
                <div class="col-sm-4 col-xs-6 portfolio-item wow fadeIn" data-wow-delay="0.2s">
                    <div class="portfolio-item-info">
                        <h4>Portfolio Item Title</h4>
                        <p>Item short description</p>
                        <a href="images/image-6-big.jpg" class="btn btn-outlined btn-white btn-sm" data-lightbox="image-1" data-title="Image title here">View the item</a>
                    </div>
                    <img class="img-responsive" src="images/image-6.jpg" alt="Lightbox" pagespeed_url_hash="3720683532">
                </div>
                <!--/.portfolio-item -->
            </div>
            <!--/.portfolio-gallery -->
        </div>
    </section>
    <!-- =========================
        About Us
    ============================== -->
    <section id="about-us">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="text-uppercase text-center font-300 margin-bottom-30 wow fadeIn">ABOUT US</h1>
                </div>
            </div>
            <div class="visible-lg visible-md padding-20"></div>
            <div class="row">
                <div class="col-sm-6 col-xs-12 wow fadeInLeft">
                    <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus.</p>
                    <p>Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum.</p>
                </div>
                <div class="col-sm-6 col-xs-12 wow fadeInRight">
                    <div class="embed-responsive embed-responsive-16by9">
                        <iframe class="embed-responsive-item" src="https://www.youtube.com/embed/AJoUEBYIniI?rel=0" allowfullscreen></iframe>
                    </div>
                </div>
            </div>
            <div class="visible-lg padding-30"></div>
            <div class="visible-xs padding-20"></div>
            <!-- =========================
                Our Mission
            ============================== -->
            <div class="row text-center margin-bottom-20">
                <div class="col-xs-12">
                    <h3 class="text-uppercase font-700 margin-bottom-30 wow fadeIn">Our Mission</h3>
                    <p class="lead wow fadeIn">Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim.</p>
                </div>
            </div>
            <div class="visible-lg padding-20"></div>
            <div class="row margin-bottom-40">
                <div class="col-sm-6 wow fadeIn" data-wow-delay="0.2s">
                    <h3 class="text-uppercase font-700 margin-bottom-30">The History</h3>
                    <div class="timeline-centered">
                        <article class="timeline-entry">
                            <div class="timeline-entry-inner">
                                <div class="timeline-icon theme-bg">2014</div>
                                <div class="timeline-label">
                                    <h5 class="font-400"><a href="#">BASIS</a> Award</h5>
                                    <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. </p>
                                </div>
                                <!--/.timeline-label -->
                            </div>
                            <!--/.timeline-entry-inner -->
                        </article>
                        <!--/.timeline-entry -->
                        <article class="timeline-entry">
                            <div class="timeline-entry-inner">
                                <div class="timeline-icon theme-bg">2012</div>
                                <div class="timeline-label">
                                    <h5 class="font-400">Moving the Office to Port Charlotte</h5>
                                    <p>Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet.</p>
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d3553.4733611311763!2d-82.2546583!3d27.046802500000002!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x88c348d201bb7f07%3A0x48a1e08741af2482!2sTamiami+Trail%2C+Florida%2C+USA!5e0!3m2!1sen!2sbd!4v1413137185422" width="100%" height="150" frameborder="0" style="border: 0"></iframe>
                                </div>
                                <!--/.timeline-label -->
                            </div>
                            <!--/.timeline-entry-inner -->
                        </article>
                        <!--/.timeline-entry -->
                        <article class="timeline-entry">
                            <div class="timeline-entry-inner">
                                <div class="timeline-icon theme-bg">2010</div>
                                <div class="timeline-label">
                                    <h5 class="font-400">Start Moving</h5>
                                    <p>Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. </p>
                                </div>
                                <!--/.timeline-label -->
                            </div>
                            <!--/.timeline-entry-inner -->
                        </article>
                        <!--/.timeline-entry -->
                        <article class="timeline-entry begin">
                            <div class="timeline-entry-inner">
                                <div class="timeline-icon theme-bg" style="-webkit-transform: rotate(-90deg); -moz-transform: rotate(-90deg);"><i class="entypo-flight"></i>+ </div>
                            </div>
                            <!--/.timeline-entry-inner -->
                        </article>
                        <!--/.timeline-entry -->
                    </div>
                </div>
                <div class="visible-xs padding-10"></div>
                <!-- =========================
                    The Skills
                ============================== -->
                <div class="col-sm-6 wow fadeIn" data-wow-delay="0.6s">
                    <h3 class="text-uppercase font-700 margin-bottom-30">The Skills</h3>
                    <p class="margin-bottom-30">Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem.</p>
                    <h5 class="text-uppercase font-700">Design Skills</h5>
                    <div class="row">
                        <div class="skills-wrap col-md-12">
                            <h6>Adobe Photoshop</h6>
                            <div class="skill-progress">
                                <div class="bar" style="width: 90%;"></div>
                            </div>
                            <!--/.skill-progress -->
                        </div>
                        <!-- /.skills-wrap -->
                    </div>
                    <div class="row">
                        <div class="skills-wrap col-md-12">
                            <h6>Adobe Illustrator</h6>
                            <div class="skill-progress">
                                <div class="bar" style="width: 70%;"></div>
                            </div>
                            <!--/.skill-progress -->
                        </div>
                        <!-- /.skills-wrap -->
                    </div>
                    <div class="row">
                        <div class="skills-wrap col-md-12">
                            <h6>HTML</h6>
                            <div class="skill-progress">
                                <div class="bar" style="width: 90%;"></div>
                            </div>
                            <!--/.skill-progress -->
                        </div>
                        <!-- /.skills-wrap -->
                    </div>
                    <div class="row">
                        <div class="skills-wrap col-md-12">
                            <h6>CSS</h6>
                            <div class="skill-progress">
                                <div class="bar" style="width: 90%;"></div>
                            </div>
                            <!--/.skill-progress -->
                        </div>
                        <!-- /.skills-wrap -->
                    </div>
                    <div class="margin-bottom-40"></div>
                    <h5 class="text-uppercase font-700">Development Skills</h5>
                    <div class="row">
                        <div class="skills-wrap col-md-12">
                            <h6>jQuery</h6>
                            <div class="skill-progress">
                                <div class="bar" style="width: 50%;"></div>
                            </div>
                            <!--/.skill-progress -->
                        </div>
                        <!-- /.skills-wrap -->
                    </div>
                    <div class="row">
                        <div class="skills-wrap col-md-12">
                            <h6>PHP</h6>
                            <div class="skill-progress">
                                <div class="bar" style="width: 35%;"></div>
                            </div>
                            <!--/.skill-progress -->
                        </div>
                        <!-- /.skills-wrap -->
                    </div>
                    <div class="row">
                        <div class="skills-wrap col-md-12">
                            <h6>WordPress</h6>
                            <div class="skill-progress">
                                <div class="bar" style="width: 90%;"></div>
                            </div>
                            <!--/.skill-progress -->
                        </div>
                        <!-- /.skills-wrap -->
                    </div>
                    <div class="row">
                        <div class="skills-wrap col-md-12">
                            <h6>Pligg</h6>
                            <div class="skill-progress">
                                <div class="bar" style="width: 40%;"></div>
                            </div>
                            <!--/.skill-progress -->
                        </div>
                        <!-- /.skills-wrap -->
                    </div>
                </div>
            </div>
            <div class="visible-lg visible-md padding-20"></div>
            <!-- =========================
                The Team
            ============================== -->
            <div class="row text-center margin-bottom-40">
                <div class="col-xs-12 wow fadeInUp">
                    <h3 class="text-uppercase font-700 margin-bottom-30">The Team</h3>
                    <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim.</p>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4 col-xs-12 team-item theme-bg wow fadeInLeft" data-wow-delay="0.2s">
                    <div class="row bg-black-0 team-item-content">
                        <div class="team-img col-xs-5">
                            <img class="img-circle" src="images/team-1.jpg" alt="Team 1" pagespeed_url_hash="2437588079">
                        </div>
                        <!-- /.team-img -->
                        <div class="team-info col-xs-7">
                            <h4 class="team-name">John Bown</h4>
                            <p class="team-designation">Founder</p>
                            <p>Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum.</p>
                            <ul class="team-social-icons">
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-facebook"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-twitter"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-pinterest"></i></a></li>
                            </ul>
                            <!-- /.team-social-icons -->
                        </div>
                        <!-- /.team-info -->
                    </div>
                    <!-- /.team-item-content -->
                </div>
                <!-- /.team-item -->
                <div class="col-sm-4 col-xs-12 team-item theme-bg wow fadeIn" data-wow-delay="0.2s">
                    <div class="row bg-black-1 team-item-content">
                        <div class="team-img col-xs-5">
                            <img class="img-circle" src="images/team-2.jpg" alt="Team 1" pagespeed_url_hash="2732088000">
                        </div>
                        <!-- /.team-img -->
                        <div class="team-info col-xs-7">
                            <h4 class="team-name">John Bown</h4>
                            <p class="team-designation">Founder</p>
                            <p>Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum.</p>
                            <ul class="team-social-icons">
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-facebook"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-twitter"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-pinterest"></i></a></li>
                            </ul>
                            <!-- /.team-social-icons -->
                        </div>
                        <!-- /.team-info -->
                    </div>
                    <!-- /.team-item-content -->
                </div>
                <!-- /.team-item -->
                <div class="col-sm-4 col-xs-12 team-item theme-bg wow fadeInRight" data-wow-delay="0.2s">
                    <div class="row bg-black-2 team-item-content">
                        <div class="team-img col-xs-5">
                            <img class="img-circle" src="images/team-3.jpg" alt="Team 1" pagespeed_url_hash="3026587921">
                        </div>
                        <!-- /.team-img -->
                        <div class="team-info col-xs-7">
                            <h4 class="team-name">John Bown</h4>
                            <p class="team-designation">Founder</p>
                            <p>Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum.</p>
                            <ul class="team-social-icons">
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-facebook"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-twitter"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-pinterest"></i></a></li>
                            </ul>
                            <!-- /.team-social-icons -->
                        </div>
                        <!-- /.team-info -->
                    </div>
                    <!-- /.team-item-content -->
                </div>
                <!-- /.team-item -->
                <div class="col-sm-4 col-xs-12 team-item theme-bg wow fadeInLeft" data-wow-delay="0.4s">
                    <div class="row bg-black-3 team-item-content">
                        <div class="team-img col-xs-5">
                            <img class="img-circle" src="images/team-4.jpg" alt="Team 1" pagespeed_url_hash="3321087842">
                        </div>
                        <!-- /.team-img -->
                        <div class="team-info col-xs-7">
                            <h4 class="team-name">John Bown</h4>
                            <p class="team-designation">Founder</p>
                            <p>Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum.</p>
                            <ul class="team-social-icons">
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-facebook"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-twitter"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-pinterest"></i></a></li>
                            </ul>
                            <!-- /.team-social-icons -->
                        </div>
                        <!-- /.team-info -->
                    </div>
                    <!-- /.team-item-content -->
                </div>
                <!-- /.team-item -->
                <div class="col-sm-4 col-xs-12 team-item theme-bg wow fadeIn" data-wow-delay="0.4s">
                    <div class="row bg-black-4 team-item-content">
                        <div class="team-img col-xs-5">
                            <img class="img-circle" src="images/team-5.jpg" alt="Team 1" pagespeed_url_hash="3615587763">
                        </div>
                        <!-- /.team-img -->
                        <div class="team-info col-xs-7">
                            <h4 class="team-name">John Bown</h4>
                            <p class="team-designation">Founder</p>
                            <p>Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum.</p>
                            <ul class="team-social-icons">
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-facebook"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-twitter"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-pinterest"></i></a></li>
                            </ul>
                            <!-- /.team-social-icons -->
                        </div>
                        <!-- /.team-info -->
                    </div>
                    <!-- /.team-item-content -->
                </div>
                <!-- /.team-item -->
                <div class="col-sm-4 col-xs-12 team-item theme-bg wow fadeInRight" data-wow-delay="0.4s">
                    <div class="row bg-black-5 team-item-content">
                        <div class="team-img col-xs-5">
                            <img class="img-circle" src="images/team-6.jpg" alt="Team 1" pagespeed_url_hash="3910087684">
                        </div>
                        <!-- /.team-img -->
                        <div class="team-info col-xs-7">
                            <h4 class="team-name">John Bown</h4>
                            <p class="team-designation">Founder</p>
                            <p>Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum.</p>
                            <ul class="team-social-icons">
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-facebook"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-twitter"></i></a></li>
                                <li><a href="#"><i class="icon-circle-bg-white fa fa-pinterest"></i></a></li>
                            </ul>
                            <!-- /.team-social-icons -->
                        </div>
                        <!-- /.team-info -->
                    </div>
                    <!-- /.team-item-content -->
                </div>
                <!-- /.team-item -->
            </div>
        </div>
    </section>
    <!-- =========================
        Contact Us
    ============================== -->
    <section id="contact-us" class="secondary-bg">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <h1 class="text-uppercase text-center font-300 margin-bottom-30 wow fadeIn" data-wow-duration="1.5s">CONTACT US</h1>
                </div>
            </div>
            <div class="visible-lg visible-md padding-20"></div>
            <div class="visible-xs padding-10"></div>
            <div class="row margin-bottom-40">
                <div class="col-md-4 col-sm-6 col-xs-12 wow fadeInLeft" data-wow-duration="1.5s">
                    <h4 class="text-uppercase font-700 margin-bottom-30">Contact Info</h4>
                    <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.</p>
                    <p>Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim.</p>
                    <ul class="contact-icons">
                        <li><i class="fa fa-map-marker"></i>123 Peckham St SE, Port Charlotte, FL 33952</li>
                        <li><i class="fa fa-phone"></i>+0123 456 789</li>
                        <li><i class="fa fa-envelope-o"></i><a href="mailto:info@yourdomainname.com">info@yourdomainname.com</a></li>
                    </ul>
                    <!-- /.contact-icons -->
                </div>
                <!-- =========================
                    Contact form
                ============================== -->
                <div class="contact-form col-md-8 col-sm-6 col-xs-12 wow fadeInUp" data-wow-duration="1.5s">
                    <div class="visible-xs padding-20"></div>
                    <h4 class="text-uppercase font-700 margin-bottom-30">Contact Form</h4>
                    <p class="margin-bottom-30">Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes.</p>
                    <form id="form" action="" method="post">
                        <div class="well well-sm"><i class="fa fa-check"></i>Required Field</div>
                        <div class="form-group">
                            <label for="name">Your Name</label>
                            <div class="input-group">
                                <input type="text" class="form-control" name="name" id="name" placeholder="Enter Your Name" required>
                                <span class="input-group-addon"><i class="fa fa-check"></i></span>
                            </div>
                            <!-- /.input-group -->
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label for="email">Your Email</label>
                            <div class="input-group">
                                <input type="email" class="form-control" id="email" name="email" placeholder="Enter Your Email" required>
                                <span class="input-group-addon"><i class="fa fa-check"></i></span>
                            </div>
                            <!-- /.input-group -->
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label for="subject">Your Subject</label>
                            <div class="input-group">
                                <input type="text" class="form-control" name="subject" id="subject" placeholder="Enter Your Subject" required>
                                <span class="input-group-addon"><i class="fa fa-check"></i></span>
                            </div>
                            <!-- /.input-group -->
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label for="message">Message</label>
                            <div class="input-group">
                                <textarea name="message" id="message" class="form-control" rows="5" required></textarea>
                                <span class="input-group-addon"><i class="fa fa-check"></i></span>
                            </div>
                            <!-- /.input-group -->
                        </div>
                        <!-- /.form-group -->
                        <div class="alert notification padding-0"></div>
                        <!-- Contact form notification message will be display here -->
                        <button type="submit" name="submit" id="submit" class="btn btn-outlined btn-black pull-left">Submit</button>
                    </form>
                </div>
                <!-- /.contact-form -->
            </div>
        </div>
    </section>

    <!-- =========================
        Login
    ============================== -->
    <!-- Contact Form -->
    <div id="contactdiv">
        <div class="login">
        <div class="login-screen">
          <div class="login-icon">
            <img src="images/lightbox/next.png" alt="Welcome to Mail App">
            <h4>Welcome to <small>Mail App</small></h4>
          </div>

          <div class="login-form">
            <div class="form-group">
              <input type="text" class="form-control login-field" value="" placeholder="Enter your name" id="login-name">
              <label class="login-field-icon fui-user" for="login-name"></label>
            </div>

            <div class="form-group">
              <input type="password" class="form-control login-field" value="" placeholder="Password" id="login-pass">
              <label class="login-field-icon fui-lock" for="login-pass"></label>
            </div>

            <a class="btn btn-primary btn-lg btn-block" href="#">Log in</a>
            <a class="login-link" href="#">Lost your password?</a>
          </div>
        </div>
      </div>
    </div>
    <!--Login Form -->
    <div id="logindiv">
        <form class="form" action="#" id="login">
            <img src="button_cancel.png" class="img" id="loginimgCancel" />

            <div class="col-lg-12" id="service-three-contactus">


            </div>


        </form>
    </div>

    <!-- =========================
        Subscribe Form for MailChimp
    ============================== -->
    <section>
        <div class="container wow bounceIn" data-wow-duration="2s">
            <div class="row text-center margin-bottom-20">
                <div class="col-xs-12">
                    <h1 class="text-uppercase text-center font-300 margin-bottom-30">Subscribe Now!</h1>
                    <p>Subscribe today and stay up to date with the latest news and offers from velocity!</p>
                </div>
            </div>
            <div class="row margin-bottom-30">
                <div class="col-sm-8 col-sm-offset-2">
                    <div id="notification_container" class="text-center notification"></div>
                    <!-- Subscribe form notification message will be display here -->
                    <form action="http://shoilysolutions.us5.list-manage.com/subscribe/post-json?u=995376304f7ffa57a6db40f0d&amp;id=fda81f5dda&amp;c=?" method="post" id="mc-embedded-subscribe-form" name="mc-embedded-subscribe-form" class="validate" target="_blank" novalidate>
                        <div class="col-lg-8 col-sm-7 col-xs-12">
                            <input class="subscribe-input email" type="email" value="" name="EMAIL" id="mce-EMAIL" placeholder="Enter your email address to get updates" required>
                        </div>
                        <div class="visible-xs padding-40"></div>
                        <!-- This empty div will be create space between input and button in mobile layout -->
                        <div class="col-lg-4 col-sm-5 col-xs-12">
                            <div style="position: absolute; left: -5000px;">
                                <input type="text" name="b_995376304f7ffa57a6db40f0d_fda81f5dda" value="">
                            </div>
                            <button class="btn btn-outlined btn-black btn-block btn-lg" id="mc-embedded-subscribe" type="submit">Notify Me <i class="fa fa-chevron-right"></i></button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </section>

    <!-- =========================
        Footer
    ============================== -->
    <footer>
        <div class="container">
            <div class="row wow fadeInUp">
                <div class="col-lg-6">
                    <p class="pull-left">&copy; 2014 all right reserved. Designed &amp; Develop by <a href="http://hasibrsohel.com/">hasibrsohel</a></p>
                </div>
                <div class="col-lg-6">
                    <ul class="footer-menu pull-right">
                        <li><a href="#home">Home</a> </li>
                        <li><a href="#features">Features</a> </li>
                        <li><a href="#services">Services</a> </li>
                        <li><a href="#portfolio">Portfolio</a> </li>
                        <li><a href="#about-us">About Us</a> </li>
                        <li><a href="#contact-us">Contact Us</a> </li>
                        <li><a href="https://wrapbootstrap.com/?ref=hasibrsohel" class="external">Purchase Now</a> </li>
                    </ul>
                    <!--/.footer-menu-->
                </div>
            </div>
        </div>
    </footer>
    


    <script type="text/javascript">

        $(document).ready(function () {
            //setTimeout(popup, 3000);
            function popup() {
                //$("#logindiv").css("display", "block");
                $("#contactdiv").css("display", "block");
            }
			

            $("#loginCancel").click(function () {
                $(this).parent().parent().parent().parent().parent().hide();
            });
            $("#loginimgCancel").click(function () {
                $(this).parent().parent().hide();
            });
            $("#Cancel").click(function () {
                $(this).parent().parent().parent().hide();
            });
            $("#contactCancel").click(function () {
                $(this).parent().parent().parent().parent().parent().hide();
            });
            $("#onclick").click(function (e) {
                //$("#contactdiv").css("display", "block");
                $("#logindiv").css("display", "block");
                //$("#loginsignupdiv").css("display", "block");

            });
            // Contact form popup send-button click event.
            $("#send").click(function () {
                var name = $("#name").val();
                var email = $("#email").val();
                var contact = $("#contactno").val();
                var message = $("#message").val();
                if (name == "" || email == "" || contactno == "" || message == "") {
                    alert("Please Fill All Fields");
                } else {
                    if (validateEmail(email)) {
                        $("#contactdiv").css("display", "none");
                    } else {
                        alert('Invalid Email Address');
                    }
                    function validateEmail(email) {
                        var filter = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/;
                        if (filter.test(email)) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                }
            });
            // Login form popup login-button click event.
            $("#loginbtn").click(function () {
                var name = $("#username").val();
                var password = $("#password").val();
                if (username == "" || password == "") {
                    alert("Username or Password was Wrong");
                } else {
                    $("#logindiv").css("display", "none");
                }
            });
        });


    </script>
</body>
</html>
