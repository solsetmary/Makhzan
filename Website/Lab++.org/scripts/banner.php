<?php
echo <<< BANNER
    <section id="home" class="banner">
        <div class="full-screen-bnr">
            <div class="full-screen-bnr-content color-overlay">
                <div class="container">
					<div class="row text-center margin-bottom-20">
						</br></br></br></br>
						<div class="col-sm-4 col-xs-12 service-item theme-bg wow flipInY animated" data-toggle="modal" data-target="#myModal" data-wow-delay="3.5s" style="visibility: visible; -webkit-animation-delay: 3.5s;cursor:pointer;background-color:#00D7C6;" id="livelab">
							<div class="bg-black-1 service-item-content" id="livelabcontent" style="padding:15px 5px 5px 5px;">
								<a id="playliveimage" href="#" style="position:absolute;top:0;left:0;color:yellow;z-index=1000;">Pause</a><h4 id="textlivelab" style="text-decoration: underline;color:white;"><img src="images/icon_blink.gif" style="width:16px;height:16px" id="imgblinklivelab"/>  Live Arduino Lab </h4><h6 style="position:absolute;top:0;color:yellow;" id="countdownliveimage"></h6>
								<img src="" class="sub-head margin-bottom-40 wow fadeIn" id="labs_preview" style="width:320;height:240;max-width:320px;max-height:240px;min-width: 160px; min-height: 120px;cursor:pointer;"/>
							</div>
							<div class="skill-progress" id="countdownlivebardiv">
                                <div class="bar" id="countdownlivebar" style="width: 0%;"></div>
                            </div>
						</div>
						</br></br></br></br>
						<h1 key="mainline" class="tr heading text-uppercase wow flipInX" data-wow-delay="1s" style="color:#B6242F;">Remote Coding in lab</h1>
						<p  key="guideline" class="tr sub-head margin-bottom-40 wow fadeIn" data-wow-delay="1.5s" style="color:#F9F507;">Learn how to program a robot, it is easy and fun ;)</br>Also, you can help us to continue our service for you:</p>
					</div>
                    <div class="row text-left wow fadeIn">
                        <div class="col-md-4 col-sm-6 col-xs-12 feature-item wow flipInX" data-wow-delay="2.5s">
                            <i class="feature-icon fa "></i>
                            <div class="feature-content">
                                <p  key="langDonate" style="font-weight: bold;color: #9E2A00;" class="tr sub-head margin-bottom-40 wow fadeIn">By donating, we put your name and/or your picture on website, our softwares, lab's camera and lab's info, more Infos on next step.</p>
                                <a href="#" style="color: #9E2A00;border-color: #9E2A00;" class="btn btn-outlined btn-white btn-lg wow fadeIn" data-toggle="modal" data-target="#myModalDonate">d = new Donate();</a>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-6 col-xs-12 feature-item wow flipInX" data-wow-delay="2s">
                            <i class="feature-icon fa "></i>
                            <div class="feature-content">
                                <p  key="langMakers" style="font-weight: bold;color: #9E005A;" class="tr sub-head margin-bottom-40 wow fadeIn">You can join to the community of Lab Makers that known as Labers and learn how to make your lab and share it with the world.</p></br>
								<a href="#" style="color: #9E005A;border-color: #9E005A;" class="btn btn-outlined btn-white btn-lg wow fadeIn">LAB::MAKERS.How();</a>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-6 col-xs-12 feature-item wow fadeIn" data-wow-delay="1.5s">
                            <i class="feature-icon fa "></i>
                            <div class="feature-content">
                                <p  key="langCoding" style="font-weight: bold;color: #005E56;" class="tr sub-head margin-bottom-40">Start to code your Robot, there are some tutorials to learn how to program and also you need to download the Lab++ Client studio 2015</br></p>
                                <a href="#" style="color: #005E56;border-color: #005E56;" class="btn btn-outlined btn-white btn-lg wow fadeIn">Coding.Start()</a>
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
BANNER;

?>