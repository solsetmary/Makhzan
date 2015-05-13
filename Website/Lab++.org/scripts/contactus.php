<?php
echo <<< contactus
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
contactus;

?>