<?php
	include("scripts/startup.php");
?>

<html lang="<?php
if(isset($_COOKIE['userlanguage']))
	echo $_COOKIE['userlanguage'];
else
	echo 'en';
?>">

<?php
	if(isset($_COOKIE['userlanguage'])){
		if($_COOKIE['userlanguage'] != 'en'){
			echo '<script type="text/javascript">
			// translate all translatable elements
			$(document).ready(function ()
			{
				var $dropTrigger = $(".dropdown dt a");
				var $languageList = $(".dropdown dd ul");
				var clickedValue = \'' . $_COOKIE['userlanguage'] . '\';
				var clickedTitle = \'\';
				$("#target dt").removeClass().addClass(clickedValue);
				$("#target dt em").html(clickedTitle);
				$languageList.hide();
				$dropTrigger.removeAttr("class");
				$(\'.tr\').each(function(i){
					$(this).text(aLangKeys[\'' . $_COOKIE['userlanguage'] . '\'][ $(this).attr(\'key\') ]);
				});
			});
			</script>';
		}
	}
?>

<head>
	<?php
		include("scripts/header.php");
	?>
</head>

<body>

<!-- PRE LOADER -->
<div class="preloader">
    <div class="status">&nbsp;</div>
</div>
<!-- /.preloader -->

<header>
    <!-- STICKY NAVIGATION -->
	<?php
		include("scripts/navbar.php");
	?>
</header>
    <!-- STICKY StaticSocialButtons -->
	<?php
		include("scripts/SocialButtonsScroll.php");
	?>

    <!-- Modal Forms -->
	<?php
		include("scripts/modalforms.php");
	?>

    <!-- =========================
        Banner
    ============================== -->
    <section id="home" class="banner">
        <div class="full-screen-bnr">
            <div class="full-screen-bnr-content color-overlay">
                <div class="container">
					<div class="row text-center margin-bottom-20">
						</br></br></br></br>
						<div class="col-sm-4 col-xs-12 service-item theme-bg wow flipInY animated" data-toggle="modal" data-target="#myModal" data-wow-delay="3.5s" style="visibility: visible; -webkit-animation-delay: 3.5s;cursor:pointer;" id="livelab">
							<div class="bg-black-1 service-item-content" id="livelabcontent">
								<h4 id="textlivelab"><img src="images/icon_blink.gif" style="width:16;height:16" id="imgblinklivelab"/>  Live Arduino Lab </h4><h6 style="position:absolute;top:0;color:yellow;" id="countdownliveimage"></h6>
								<img src="" class="sub-head margin-bottom-40 wow fadeIn" id="labs_preview" style="width:320;height:240;max-width:320;max-height:240;cursor:pointer;"/>
							</div>
						</div>
						</br></br></br></br>
						<h1 key="mainline" class="tr heading text-uppercase wow flipInX" data-wow-delay="1s">Remote Coding in lab</h1>
						<p  key="guideline" class="tr sub-head margin-bottom-40 wow fadeIn" data-wow-delay="1.5s">Learn how to program a robot, it is easy and fun ;)</br>Also, you can help us to continue our service for you:</p>
					</div>
                    <div class="row text-left wow fadeIn">
                        <div class="col-md-4 col-sm-6 col-xs-12 feature-item wow flipInX" data-wow-delay="2.5s">
                            <i class="feature-icon fa "></i>
                            <div class="feature-content">
                                <p  key="langDonate" class="tr sub-head margin-bottom-40 wow fadeIn">By donating, we put your name and/or your picture on website, our softwares, lab's camera and lab's info, more Infos on next step.</p>
                                <a href="#" class="btn btn-outlined btn-white btn-lg wow fadeIn" data-toggle="modal" data-target="#myModalDonate">d = new Donate();</a>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-6 col-xs-12 feature-item wow flipInX" data-wow-delay="2s">
                            <i class="feature-icon fa "></i>
                            <div class="feature-content">
                                <p  key="langMakers" class="tr sub-head margin-bottom-40 wow fadeIn">You can join to the community of Lab Makers that known as Labers and learn how to make your lab and share it with the world.</p></br>
								<a href="#" class="btn btn-outlined btn-white btn-lg wow fadeIn">LAB::MAKERS.How();</a>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-6 col-xs-12 feature-item wow fadeIn" data-wow-delay="1.5s">
                            <i class="feature-icon fa "></i>
                            <div class="feature-content">
                                <p  key="langCoding" class="tr sub-head margin-bottom-40">Start to code your Robot, there are some tutorials to learn how to program and also you need to download the Lab++ Client studio 2015</br></p>
                                <a href="#" class="btn btn-outlined btn-white btn-lg wow fadeIn">Coding.Start()</a>
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
                        <iframe class="embed-responsive-item" src="https://www.youtube.com/embed/NISj2IAK1wc?list=PLIOMauXCOoF_xMS5ZPrj3KUVpggzyU3Fe" frameborder="0" allowfullscreen></iframe>
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
                    <h3 class="text-uppercase font-700 margin-bottom-30 wow fadeIn">Our Goal</h3>
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
                                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d2352.771912235307!2d9.915446!3d53.8647037!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47b23219c166e0b9%3A0xb04af9b1af75ac2b!2sAlte+Landstra%C3%9Fe%2C+N%C3%BCtzen%2C+Germany!5e0!3m2!1sen!2s!4v1425604805729" width="100%" height="200" frameborder="0" style="border:0"></iframe>
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
                            <h4 class="team-name">Soheyl</h4>
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
                            <h4 class="team-name">Setareh</h4>
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
                            <h4 class="team-name">Esther</h4>
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
	<div id="tomove">
    </div>
	
	<pre  cols="60" name="code" rows="10" style="width:100%;height:100%;display:none;"></pre>
    
	<!-- =========================
        Footer
    ============================== -->
    <footer>
        <div class="container">
            <div class="row wow fadeInUp">
                <div class="col-lg-6">
                    <p class="pull-left">&copy; 2015 all right reserved. Designed &amp; Develop by <a href="http://soheyln.com/">Soheyl Nazifi</a></p>
                </div>
                <div class="col-lg-6">
                    <ul class="footer-menu pull-right">
                        <li><a href="#home">Home</a> </li>
                        <li><a href="#features">Features</a> </li>
                        <li><a href="#services">Services</a> </li>
                        <li><a href="#portfolio">Portfolio</a> </li>
                        <li><a href="#about-us">About Us</a> </li>
                        <li><a href="#contact-us">Contact Us</a> </li>
                        <li><a href="https://soheyln.com" class="external">Purchase Now</a> </li>
                    </ul>
                    <!--/.footer-menu-->
                </div>
            </div>
        </div>
    </footer>
	<script type="text/javascript">
		window.onload = function () { 
			dp.SyntaxHighlighter.ClipboardSwf = '/scripts/clipboard.swf';
			dp.SyntaxHighlighter.HighlightAll('code',true,false,false,1,false);
		}
	</script>
</body>
</html>
