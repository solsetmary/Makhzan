<?php
echo <<< NAVBAR
<header>
    <!-- STICKY NAVIGATION -->
    <nav class="navbar navbar-custom navbar-fixed-top" role="navigation">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="navbar-header page-scroll">
                        <!-- MENU BOR MOBILE DEVICES -->
                        <button type="button" style="z-index:1300" class="navbar-toggle" data-toggle="collapse" data-target="#hrs-navigation">
                            <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
                        </button>
                        <!-- /.navbar-toggle -->
                        <!-- LOGO ON STICKY NAV BAR -->
                        <a class="logo" href="#home">
                            <img class="img-responsive" style="display:inline-block;vertical-align: middle" src="images/logo-lppui.png" alt="Lab++.org" pagespeed_url_hash="1084840945"/>
							<!--<ul class="nav pull-right login-right-fix">
								<li>
									<span style="vertical-align: center" data-toggle="modal" data-target="#myModalLogin" id="onclick">Log In</span>
								</li>
							</ul> -->
                        </a>
                    </div>
                    <!-- /.navbar-header -->
                    <!-- NAVIGATION LINKS -->
                    <div class="navbar-collapse collapse" id="hrs-navigation">
                        <ul class="nav navbar-nav navbar-right main-navigation">
                            <li class="page-scroll"><a href="#" id="onclick" data-toggle="modal" data-target="#myModalLogin" style="color:#9E2A00;">Log.In()</a> </li>
                            <li class="page-scroll"><a href="#" onclick="window.open('http://labpp.org','_top')" style="color:#ECD1D1;">Home = new LABpp()</a> </li>
                            <li class="page-scroll"><a href="download/lpp.zip" id="DownloadLABpp" style="color:#2E009E;">Download.getFile</a> </li>
                            <li class="page-scroll"><a href="#" id="golab" style="color:#9E005A;">LAB.timing()</a> </li>
                            <li class="page-scroll"><a href="#tutorial" id="tutorialstart" style="color:#005E56;">Tutorial.start</a> </li>
                            <li class="page-scroll"><a href="#blog" style="color:#00FF17;">Blog.write('Hi')</a> </li>
                            <li class="page-scroll"><a href="#about-us" style="color:#D2FF00;">About('US')</a> </li>
                            <!--<li class="page-scroll"><a href="http://labpp.org/" class="external">LAB.MAKERS.How()</a> </li>-->
                        </ul>
                        <!-- /.navbar-nav -->
                    </div>
                    <!-- /.navbar-collapse -->
                </div>
                <!-- / .navbar-header -->
            </div>
        </div>
    </nav>
	<div id="country-select">
	<form>
		<select id="country-options" name="country-options">
			<option selected="selected" title="" value="en" id="us"></option>
			<option title="" value="de" id="de"></option>
			<option title="" value="uk" id="uk"></option>
			<option title="" value="fr" id="ru"></option>
			<option title="" value="nl" id="nl"></option>
		</select>
	</form>
	</div>
</header>
    <!-- STICKY StaticSocialButtons -->
NAVBAR;

?>