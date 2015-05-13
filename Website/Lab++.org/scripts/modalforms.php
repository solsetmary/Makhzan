<?php
echo <<< NAVBAR
    
<!-- Modal Live Lab-->
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-dialog" style="min-width:500px;width:50%;height:70%;">
    <div class="modal-content"  id="mymodalcontent" style="background-color:#A3B69B;">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <center><h4 class="modal-title" id="myModalLabel"><h4 id="textlivelabmodal"><img src="images/icon_blink.gif" style="width:16;height:16" id="imgblinklivelabmodal"/>  Live Arduino Lab </h4></h4></center>
      </div>
      <div class="modal-body" id="countdownliveimagemodalbody">
		<div class="containerflip">
			<div class="box-front" id="boxfront">
			</div>
			<div class="box-back">
				<!-- pre tag to contain the highlighted code  class="ta5 scrollbarsDemo" cols="60" rows="10" style="width:100%;height:100%"-->
				<pre id="layer" style="width:50%;height:100%;float:left"></pre>
				<pre id="layer2" style="width:50%;height:100%;float:right"><img id="labs_previewsmall" style="width:40px;height:60px;"/></br>user name:</br>email:</pre>
				<textarea id="highlight" style="display:none" name="highlight">
/*
First Arduino Lab
Written By Soheyl Nazifi
*/
int ledPin = 13;
String content = '';
char character;

void setup()
{
    pinMode(ledPin, OUTPUT);
    Serial.begin(9600);
}

void loop()
{
    content = '';
    while(Serial.available()) 
    {
        content = Serial.readStringUntil('\n');
        content.trim();
        //character = Serial.read();
        //content.concat(character);
    }
    if (content != '')
    {
        Serial.println(content);
    }
    if (content == "on")
    {
        Serial.println('LED is going to Fade IN');
        for(int fadeValue = 0 ; fadeValue &lt;= 255; fadeValue +=1)
	 {
            analogWrite(ledPin, fadeValue);
            delay(30);
        }
        Serial.println("Result: Light is on.");
    }
    if (content == "off")
    {
        Serial.println("LED is goinf to Fade OUT");
        for(int fadeValue = 255 ; fadeValue &gt;= 0; fadeValue -=1)
	 {
            analogWrite(ledPin, fadeValue);
            delay(30);
        }
        Serial.println('Result: Light is off.');	
    }
}</textarea>
			</div>
		</div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal" id="buttonCloseliveimagemodal">Close</button>
        <button type="button" class="btn btn-primary" style="display:none;">Save changes</button>
      </div>
    </div>
  </div>
</div>

<!-- Modal Login-->
<div class="modal fade" id="myModalLogin" tabindex="-1" role="dialog" aria-labelledby="myModalLabelLogin" aria-hidden="true" style="top:5%;righ:10%;outline: none;">
  <div class="modal-dialog" style="width:400px;right:2%;outline: none;">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
		<h4 class="modal-title" id="myModalLabelLogin"></h4>
      </div>
      <div class="modal-body" id="service-three-contactus">
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal" id="buttonCloseDonatemodalLogin">Close</button>
      </div>
    </div>
  </div>
</div>

<!-- Modal Tweet
<div class="modal fade" id="myModalTweet" tabindex="-1" role="dialog" aria-labelledby="myModalLabelTweet" aria-hidden="true" style="top:5%;righ:10%;outline: none;">
  <div class="modal-dialog" style="width:750px;right:2%;outline: none;">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
		<h4 class="modal-title" id="myModalLabelTweet"></h4>
      </div>
      <div class="modal-body" id="service-Tweet">
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal" id="buttonCloseDonatemodalTweet">Close</button>
      </div>
    </div>
  </div>
</div>-->
<div class="messagepop pop">
	<div id="service-Tweet">
		<center><h4 style="color:red">Please Login for accessing Chat Service.</h4></center>
	</div>
</div>

<!-- Modal Announce-->
<div class="modal fade" id="myModalAnnounce" tabindex="-1" role="dialog" aria-labelledby="myModalLabelAnnounce" aria-hidden="true" style="top:5%;righ:10%;outline: none;">
  <div class="modal-dialog" style="width:640;height:480;right:2%;outline: none;">
    <div class="modal-content" style="background-color: #ffffff;">
		<div class="modal-body" id="myModalBodyAnnounce">
		</div>
		<center>
			<input class="mycheckbox" type="checkbox" name="showagain" id="showagain" value="yes" checked></input>
			<label class="mycheckbox" for="showagain" style="cursor: pointer;">  Show me again!</label>
		</center>
    </div>
  </div>
</div>

<!-- Modal Donate-->
<div class="modal fade" id="myModalDonate" tabindex="-1" role="dialog" aria-labelledby="myModalLabelDonate" aria-hidden="true">
  <div class="modal-dialog" style="width:400px;height:400px;right:2%;outline: none;">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
		<h4 class="modal-title" id="myModalLabelDonate">Donation</h4>
      </div>
      <div class="modal-body">
		<form action="https://www.paypal.com/cgi-bin/webscr" style="width:100%;height:100%;" method="post" target="_blank">
			<input type="hidden" name="cmd" value="_s-xclick">
			<input type="hidden" name="hosted_button_id" value="REXKGQTB2ZLNY">
			<input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif" style="width:35%;height:15%;" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!">
			<img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1">
		</form>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal" id="buttonCloseDonatemodal">Close</button>
      </div>
    </div>
  </div>
</div>
NAVBAR;

?>