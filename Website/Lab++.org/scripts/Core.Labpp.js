var labsaLive;
var counterlabs = 0;
var currentlab = 0;
var durationlabmax = 30;
var durationlab = durationlabmax;
var isLivePlaying = true;
var isAnnounce = true;
var myVarTimeout;

function myModalAnnounceshow() {
	//Show("myModalAnnounce");
	if(myAnnounces != ''){
		$('#myModalAnnounce').modal('show');
	}
}

//Fix modal mobile Boostrap 3
function Show(id){
	//Fix CSS
	$(".modal-footer").css({"padding":"19px 20px 20px","margin-top":"15px","text-align":"right","border-top":"1px solid #e5e5e5"});
	$(".modal-body").css("overflow-y","auto");
	//Fix .modal-body height
	$('#'+id).on('shown.bs.modal',function(){
		$("#"+id+">.modal-dialog>.modal-content>.modal-body").css("height","auto");
		h1=$("#"+id+">.modal-dialog").height();
		h2=$(window).height();
		h3=$("#"+id+">.modal-dialog>.modal-content>.modal-body").height();
		h4=h2-(h1-h3);		
		if($(window).width()>=768){
			if(h1>h2){
				$("#"+id+">.modal-dialog>.modal-content>.modal-body").height(h4);
			}
			$("#"+id+">.modal-dialog").css("margin","30px auto");
			$("#"+id+">.modal-dialog>.modal-content").css("border","1px solid rgba(0,0,0,0.2)");
			$("#"+id+">.modal-dialog>.modal-content").css("border-radius",6);				
			if($("#"+id+">.modal-dialog").height()+30>h2){
				$("#"+id+">.modal-dialog").css("margin-top","0px");
				$("#"+id+">.modal-dialog").css("margin-bottom","0px");
			}
		}
		else{
			//Fix full-screen in mobiles
			$("#"+id+">.modal-dialog>.modal-content>.modal-body").height(h4);
			$("#"+id+">.modal-dialog").css("margin",0);
			$("#"+id+">.modal-dialog>.modal-content").css("border",0);
			$("#"+id+">.modal-dialog>.modal-content").css("border-radius",0);	
		}
		//Aply changes on screen resize (example: mobile orientation)
		window.onresize=function(){
			$("#"+id+">.modal-dialog>.modal-content>.modal-body").css("height","auto");
			h1=$("#"+id+">.modal-dialog").height();
			h2=$(window).height();
			h3=$("#"+id+">.modal-dialog>.modal-content>.modal-body").height();
			h4=h2-(h1-h3);
			if($(window).width()>=768){
				if(h1>h2){
					$("#"+id+">.modal-dialog>.modal-content>.modal-body").height(h4);
				}
				$("#"+id+">.modal-dialog").css("margin","30px auto");
				$("#"+id+">.modal-dialog>.modal-content").css("border","1px solid rgba(0,0,0,0.2)");
				$("#"+id+">.modal-dialog>.modal-content").css("border-radius",6);				
				if($("#"+id+">.modal-dialog").height()+30>h2){
					$("#"+id+">.modal-dialog").css("margin-top","0px");
					$("#"+id+">.modal-dialog").css("margin-bottom","0px");
				}
			}
			else{
				//Fix full-screen in mobiles
				$("#"+id+">.modal-dialog>.modal-content>.modal-body").height(h4);
				$("#"+id+">.modal-dialog").css("margin",0);
				$("#"+id+">.modal-dialog>.modal-content").css("border",0);
				$("#"+id+">.modal-dialog>.modal-content").css("border-radius",0);	
			}
		};
	});  
	//Free event listener
	$('#'+id).on('hide.bs.modal',function(){
		window.onresize=function(){};
	});  
	//Mobile haven't scrollbar, so this is touch event scrollbar implementation
	var y1=0;
	var y2=0;
	var div=$("#"+id+">.modal-dialog>.modal-content>.modal-body")[0];
	div.addEventListener("touchstart",function(event){
		y1=event.touches[0].clientY;
	});
	div.addEventListener("touchmove",function(event){
		event.preventDefault();
		y2=event.touches[0].clientY;
		var limite=div.scrollHeight-div.clientHeight;
		var diff=div.scrollTop+y1-y2;
		if(diff<0)diff=0;
		if(diff>limite)diff=limite;
		div.scrollTop=diff;
		y1=y2;
	});
	//Fix position modal, scroll to top.	
	$('html, body').scrollTop(0);
	//Show
	$("#"+id).modal('show');
}

$(document).ready(function ()
{

	$.ajaxSetup({
		cache: false
	});

	$("#labs_preview").css('width', '100%');
	$("#labs_preview").css('height', '100%');
	$("#labs_preview").css('max-width', '320px');
	$("#labs_preview").css('max-height', '240px');
	$("#labs_preview").css('right', '0px');
	$("#labs_preview").css('top', '0%');
	$("#labs_preview").css('border-radius', '0px');
	
	function deselect(e) {
	  $('.pop').slideFadeToggle(function() {
		e.removeClass('selected');
	  });    
	}

	$(function() {
	  $('#showchat').on('click', function() {
		if($(this).hasClass('selected')) {
		  deselect($(this));               
		} else {
		  $(this).addClass('selected');
		  $('.pop').slideFadeToggle();
		}
		return false;
	  });
	// close chat when anywhere else on the screen is clicked
	$(document).bind('click', function(e) {
		var $clicked = $(e.target);
		if ($('#showchat').hasClass("selected"))
			deselect($('#showchat'));
	});

	$('.chatclose').on('click', function() {
		deselect($('#showchat'));
		return false;
	  });
	});

	$.fn.slideFadeToggle = function(easing, callback) {
	  return this.animate({ opacity: 'toggle', height: 'toggle' }, 'fast', easing, callback);
	};

	$("#onclick_inactive").click(function (e) {
		//$("#contactdiv").css("display", "block");
		$("#logindiv").css("display", "block");
		//$("#loginsignupdiv").css("display", "block");
	});

	$("#showagain").change(function () {
		//$(this).parent().parent().hide();
		var mydata = {};
		if($("#showagain").is(':checked')){
			mydata = {
				"announce": myShowAnnounce,
				"status": "showme",
				"Action": "soheyln.com"
			};
		}else{
			mydata = {
				"announce": myShowAnnounce,
				"status": "notshowme",
				"Action": "soheyln.com"
			};
		}
		$.ajax(
		{
			url: "scripts/setannounceshowagain.php",
			data: mydata,
			type: "POST",
			contentType: "application/x-www-form-urlencoded;charset=UTF-8",
			success: function (data) {
			},
			error: function (data) {}
		});
	});
	
	$('#myModal').on('shown.bs.modal', function () {
		$("#labs_preview").css('width', '100%');
		$("#labs_preview").css('height', '100%');
		$("#labs_preview").css('max-width', '1000px');
		$("#labs_preview").css('max-height', '900px');
		$("#labs_preview").css('right', '0px');
		$("#labs_preview").css('top', '0%');
		$("#labs_preview").css('border-radius', '10px');
		$("#countdownliveimage").css('color', 'red');
		$("#countdownlivebardiv").detach().appendTo('#mymodalcontent');
		$("#countdownliveimage").detach().appendTo('#myModalLabel');
		$("#playliveimage").detach().appendTo('#myModalLabel');
		$("#labs_preview").detach().appendTo('#boxfront');
	});
	
	$('#myModal').on('hidden.bs.modal', function () {
		$("#labs_preview").css('width', '100%');
		$("#labs_preview").css('height', '100%');
		$("#labs_preview").css('max-width', '320px');
		$("#labs_preview").css('max-height', '240px');
		$("#labs_preview").css('min-width', '160px');
		$("#labs_preview").css('min-height', '120px');
		$("#labs_preview").css('right', '0px');
		$("#labs_preview").css('top', '0%');
		$("#labs_preview").css('border-radius', '0px');
		$("#countdownliveimage").css('color', 'yellow');
		$("#countdownliveimage").detach().appendTo('#livelabcontent');
		$("#playliveimage").detach().appendTo('#livelabcontent');
		$("#labs_preview").detach().appendTo('#livelabcontent');
		$("#countdownlivebardiv").detach().appendTo('#livelab');
	});
	
	$('#myModalAnnounce').on('shown.bs.modal', function () {
		$('#myModalBodyAnnounce').html(myAnnounces);
	});

	$('#myModalAnnounce').on('hidden.bs.modal', function () {
		$('#myModalBodyAnnounce').html('');
	});

	$('#playliveimage').on('click', function(event) {
		if(isLivePlaying){
			$('#playliveimage').html("Resume");
			isLivePlaying = false;
			clearInterval(myVarTimeout);
			myVarTimeout = 0;
		}else{
			$('#playliveimage').html("Pause");
			isLivePlaying = true;
			if(myVarTimeout==0)
				myVarTimeout = setInterval(imagerefresh, 1000);
		}
		return false;
	});
	
	$('.scroll-top').on('click', function(event) {
		event.preventDefault();
		$('html, body').animate({scrollTop:0}, 1000);      
	});
	
	// --- language dropdown --- //

	// turn select into dl
	createDropDown();
	
	var $dropTrigger = $(".dropdown dt a");
	var $languageList = $(".dropdown dd ul");
	
	// open and close list when button is clicked
	$dropTrigger.toggle(function() {
		$languageList.slideDown(200);
		$dropTrigger.addClass("active");
	}, function() {
		$languageList.slideUp(200);
		$(this).removeAttr("class");
	});

	// close list when anywhere else on the screen is clicked
	$(document).bind('click', function(e) {
		var $clicked = $(e.target);
		if (! $clicked.parents().hasClass("dropdown"))
			$languageList.slideUp(200);
			$dropTrigger.removeAttr("class");
	});

	// when a language is clicked, make the selection and then hide the list
	$(".dropdown dd ul li a").click(function() {
        var lang = $(this).attr('id'); // obtain language id
		/*var xmlhttp;
		if (window.XMLHttpRequest)
		  {// code for IE7+, Firefox, Chrome, Opera, Safari
			xmlhttp=new XMLHttpRequest();
		  }
		else
		  {// code for IE6, IE5
		  xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
		  }
		xmlhttp.onreadystatechange=function()
		  {
		  if (xmlhttp.readyState==4 && xmlhttp.status==200)
			{
				console.log("success:" + xmlhttp.responseText);
			}else{

				console.log("error:" + xmlhttp.responseText);
			}
		  }
		xmlhttp.open("POST","scripts/setlang.php",true);
		xmlhttp.setRequestHeader("Content-type","application/x-www-form-urlencoded");
		xmlhttp.send("lang="+lang);*/

		$.ajax(
		{
			url: "scripts/setlang.php",
			data: {
				"lang": lang,
				"Action": "soheyln.com",
			},
			type: "POST",
			contentType: "application/x-www-form-urlencoded;charset=UTF-8",
			success: function (data) {
			},
			error: function (data) {}
		});

        // translate all translatable elements
        $('.tr').each(function(i){
          $(this).text(aLangKeys[lang][ $(this).attr('key') ]);
        });
		
		var clickedValue = $(this).parent().attr("class");
		var clickedTitle = $(this).find("em").html();
		$("#target dt").removeClass().addClass(clickedValue);
		$("#target dt em").html(clickedTitle);
		$languageList.hide();
		$dropTrigger.removeAttr("class");
	});

	//full list of reserved words: http://dev.mysql.com/doc/refman/5.0/en/reserved-words.html
	var k = ["AND", "String", "Serial", "int", "CASE", "ELSE", "ELSEIF", "FALSE", "FOR", "IF", "NULL", "OR", "TRUE", "char", "void", "OUTPUT", "INPUT"];
	//adding lowercase keyword support
	var len = k.length;
	for(var i = 0; i < len; i++)
	{
		k.push(k[i].toLowerCase());
	}
	
	var re;
	var c = $("#highlight").val(); //raw code
	
	// find \n between '' or ""
	c = c.replace(/(['`].?\n['`])/g, "'\\n'");
	c = c.replace(/(.?\n["])/g, "$1\\n\"");
	
	//regex time
	//highlighting special characters. /, *, + are escaped using a backslash
	//'g' = global modifier = to replace all occurances of the match
	//$1 = backreference to the part of the match inside the brackets (....)
	c = c.replace(/(=|%|\/|\*|-|,|;|\+|<|>)/g, "<span class=\"sc\">$1</span>");
	
	//c = c.replace(/(["].*?["])/g, "<span class=\"string\">$1</span>");
	
	//strings - text inside single quotes and backticks
	c = c.replace(/(['`].*?['`])/g, "<span class=\"string\">$1</span>");
	
	//numbers - same color as strings
	c = c.replace(/(\d+)/g, "<span class=\"string\">$1</span>");
	
	//functions - any string followed by a '('
	c = c.replace(/(\w*?)\(/g, "<span class=\"function\">$1</span>(");
	
	//brackets - same as special chars
	c = c.replace(/([\(\)])/g, "<span class=\"sc\">$1</span>");
	
	//reserved mysql keywords
	for(var i = 0; i < k.length; i++)
	{
		//regex pattern will be formulated based on the array values surrounded by word boundaries. since the replace function does not accept a string as a regex pattern, we will use a regex object this time
		re = new RegExp("\\b"+k[i]+"\\b", "g");
		c = c.replace(re, "<span class=\"keyword\">"+k[i]+"</span>");
	}
	
	//comments - tricky...
	//comments starting with a '#'
	c = c.replace(/(#.*?\n)/g, clear_spans);
	
	//comments starting with '-- '
	//first we need to remove the spans applied to the '--' as a special char
	c = c.replace(/<span class=\"sc\">-<\/span><span class=\"sc\">-<\/span>/g, "--");
	c = c.replace(/(-- .*?\n)/g, clear_spans);

	//comments starting with '// '
	//first we need to remove the spans applied to the '//' as a special char
	c = c.replace(/<span class=\"sc\">\/<\/span><span class=\"sc\">\/<\/span>/g, "//");
	c = c.replace(/(\/\/.*?\n)/g, clear_spans);
	
	//comments inside /*...*/
	//filtering out spans attached to /* and */ as special chars
	c = c.replace(/<span class=\"sc\">\/<\/span><span class=\"sc\">\*<\/span>/g, "/*");
	c = c.replace(/<span class=\"sc\">\*<\/span><span class=\"sc\">\/<\/span>/g, "*/");
	//In JS the dot operator cannot match newlines. So we will use [\s\S] as a hack to select everything(space or non space characters)
	c = c.replace(/(\/\*[\s\S]*?\*\/)/g, clear_spans);
	
	//Correction
	var re = new RegExp("<span class=\"<span class=\"keyword\">string</span>", 'g');
	c = c.replace(re, "<span class=\"string");
	
	$("#layer").html(c); //injecting the code into the pre tag
	
	//as you can see keywords are still purple inside comments.
	//we will create a filter function to remove those spans. This function will noe be used in .replace() instead of a replacement string
	function clear_spans(match)
	{
		match = match.replace(/<span.*?>/g, "");
		match = match.replace(/<\/span>/g, "");
		return "<span class=\"comment\">"+match+"</span>";
	}
	
	setTimeout(mylivelabs, 2000);

	function mylivelabs() {
		$.ajax(
		{
			url: "get/live/labs/",
			data: {
				"sid": validsessionid,
				"Action": "soheyln.com",
			},
			type: "POST",
			contentType: "application/x-www-form-urlencoded;charset=UTF-8",
			cache: false,
			success: function (data) {
				var myData = data.toString();
				if (myData.indexOf("Invalid Session") > -1) {
					//location.reload(); //true - Reloads the current page from the server, false - Default. Reloads the current page from the cache.
					labsaLive = 1;
				}else{
					labsaLive = JSON.parse(myData);
				}
				counterlabs = labsaLive.length - 1;
				if(counterlabs >= 0)
				{
					// setTimeout(imagerefresh, 500);
					myVarTimeout = setInterval(imagerefresh, 1000);
				}
				
				if(isAnnounce)
					setTimeout(myModalAnnounceshow, 2000);
			},
			error: function (data) {}
		});
	}
	
	function imagerefresh() {
		if(!isLivePlaying){
			// setTimeout(imagerefresh, 50);
			return;
		}
		if(counterlabs <= currentlab)
		{
			durationlab = durationlabmax;
			currentlab = 0;
			$.ajax(
			{
				url: "get/live/labs/",
				data: {
					"sid": validsessionid,
					"Action": "soheyln.com",
				},
				type: "POST",
				contentType: "application/x-www-form-urlencoded;charset=UTF-8",
				cache: false,
				success: function (data) {
					var myData = data.toString();
					if (myData.indexOf("Invalid Session") > -1) {
						//location.reload(); //true - Reloads the current page from the server, false - Default. Reloads the current page from the cache.
						labsaLive = 1;
					}else{
						labsaLive = JSON.parse(myData);
					}
					counterlabs = labsaLive.length - 1;
				},
				error: function (data) {}
			});
			if(counterlabs <= 0)
			{
				$("#labs_preview").attr('src', 'images/background.png');
				/*$("#labs_preview").css('width', '320px');
				$("#labs_preview").css('height', '240px');
				$("#labs_preview").css('max-width', '320px');
				$("#labs_preview").css('max-height', '240px');*/
				$("#labs_previewsmall").attr('src', 'images/background.png');
				$("#textlivelab").html('No Active Lab');
				$("#countdownliveimage").html('');
				$("#textlivelabmodal").html('No Active Lab');
				// setTimeout(imagerefresh, 50);
				return;
			}
			else
			{
				$("#textlivelab").html('<img src="images/icon_blink.gif" style="width:16px;height:16px" id="imgblinklivelab"/>  Live Arduino Lab');
				$("#textlivelabmodal").html('<img src="images/icon_blink.gif" style="width:16px;height:16px" id="imgblinklivelab"/>  Live Arduino Lab');
			}
		}
		if(durationlab > 0)
		{
			var myfirst = '"' + labsaLive[currentlab].first + '"';
			var mysecond = '"' + labsaLive[currentlab].second + '"';
			$.ajax(
			{
				url: "get/new/images/",
				data: {
					"sid": validsessionid,
					"Action": "soheyln.com",
					"first": myfirst,
					"second": mysecond,
				},
				type: "POST",
				contentType: "application/x-www-form-urlencoded;charset=UTF-8",
				cache: false,
				success: function (data) {
					var myData = data.toString();
					var obj = JSON.parse(myData);
					var imagesource = 'data:image/jpeg;base64,' + obj[0].third;
					var widthbar = Math.round(durationlab * (100 / durationlabmax));
					widthbar = widthbar + "%";
					$("#labs_preview").attr('src', imagesource);
					$("#labs_previewsmall").attr('src', imagesource);
					$("#countdownlivebar").css('width', widthbar);
					$("#countdownlivebar").css('background-color', 'orange');
					$("#countdownliveimage").html(durationlab);
					$("#countdownliveimage").css('display', 'none');
					$("#textlivelab").html('<img src="images/icon_blink.gif" style="width:16px;height:16px" id="imgblinklivelab"/> Live Arduino Lab');
					$("#textlivelabmodal").html('<img src="images/icon_blink.gif" style="width:16px;height:16px" id="imgblinklivelab"/> Live Arduino Lab');
					durationlab--;// setTimeout(imagerefresh, 50);
				},
				error: function (data) {}
			});
		}
		else
		{
			currentlab++;
			durationlab = durationlabmax;// setTimeout(imagerefresh, 50);
		}
	}
});

// actual function to transform select to definition list
function createDropDown(){
	var $form = $("div#country-select form");
	$form.hide();
	var source = $("#country-options");
	source.removeAttr("autocomplete");
	var selected = source.find("option:selected");
	var options = $("option", source);
	$("#country-select").append('<dl id="target" class="dropdown"></dl>')
	$("#target").append('<dt class="' + selected.val() + '"><a href="#"><span class="flag"></span><em>' + selected.text() + '</em></a></dt>')
	$("#target").append('<dd><ul></ul></dd>')
	options.each(function(){
		//$("#target dd ul").append('<li class="' + $(this).val() + '"><a href="' + $(this).attr("title") + '"><span class="flag"></span><em>' + $(this).text() + '</em></a></li>');
		$("#target dd ul").append('<li class="' + $(this).val() + '"><a href="#" id="' + $(this).attr("value") + '"><span class="flag"></span><em>' + $(this).text() + '</em></a></li>');
		});
}
