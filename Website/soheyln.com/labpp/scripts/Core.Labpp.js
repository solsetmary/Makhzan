var labsaLive;
var counterlabs = 0;
var currentlab = 0;
var durationlab = 33;

$(document).ready(function ()
{
	$("#onclick_inactive").click(function (e) {
		//$("#contactdiv").css("display", "block");
		$("#logindiv").css("display", "block");
		//$("#loginsignupdiv").css("display", "block");
	});

	$("#loginimgCancel").click(function () {
		$(this).parent().parent().hide();
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
		$("#countdownliveimage").detach().appendTo('#myModalLabel');
		$("#labs_preview").detach().appendTo('#boxfront');
	});
	
	$('#myModal').on('hidden.bs.modal', function () {
		$("#labs_preview").css('width', '320px');
		$("#labs_preview").css('height', '240px');
		$("#labs_preview").css('max-width', '320px');
		$("#labs_preview").css('max-height', '240px');
		$("#labs_preview").css('right', '0px');
		$("#labs_preview").css('top', '0%');
		$("#labs_preview").css('border-radius', '0px');
		$("#countdownliveimage").css('color', 'yellow');
		$("#countdownliveimage").detach().appendTo('#livelabcontent');
		$("#labs_preview").detach().appendTo('#livelabcontent');
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
		$.ajax(
		{
			url: "scripts/setlang.php",
			data: {
				"lang": lang,
				"Action": "soheyln.com",
			},
			type: "POST",
			contentType: "application/x-www-form-urlencoded;charset=ISO-8859-15",
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
	var k = ["AND", "String", "Serial", "int", "CASE", "ELSE", "ELSEIF", "FALSE", "FOR", "IF", "NULL", "OR", "TRUE", "char", "void"];
	//adding lowercase keyword support
	var len = k.length;
	for(var i = 0; i < len; i++)
	{
		k.push(k[i].toLowerCase());
	}
	
	var re;
	var c = $("#highlight").val(); //raw code
	
	//regex time
	//highlighting special characters. /, *, + are escaped using a backslash
	//'g' = global modifier = to replace all occurances of the match
	//$1 = backreference to the part of the match inside the brackets (....)
	c = c.replace(/(=|%|\/|\*|-|,|;|\+|<|>)/g, "<span class=\"sc\">$1</span>");
	
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
	
	//comments inside /*...*/
	//filtering out spans attached to /* and */ as special chars
	c = c.replace(/<span class=\"sc\">\/<\/span><span class=\"sc\">\*<\/span>/g, "/*");
	c = c.replace(/<span class=\"sc\">\*<\/span><span class=\"sc\">\/<\/span>/g, "*/");
	//In JS the dot operator cannot match newlines. So we will use [\s\S] as a hack to select everything(space or non space characters)
	c = c.replace(/(\/\*[\s\S]*?\*\/)/g, clear_spans);
	
	$("#layer").html(c); //injecting the code into the pre tag
	
	//as you can see keywords are still purple inside comments.
	//we will create a filter function to remove those spans. This function will noe be used in .replace() instead of a replacement string
	function clear_spans(match)
	{
		match = match.replace(/<span.*?>/g, "");
		match = match.replace(/<\/span>/g, "");
		return "<span class=\"comment\">"+match+"</span>";
	}

	$.ajax(
	{
		url: "scripts/getlabslive.php",
		data: {
			"Action": "soheyln.com",
		},
		type: "POST",
		contentType: "application/x-www-form-urlencoded;charset=ISO-8859-15",
		success: function (data) {
			var myData = data.toString();
			labsaLive = JSON.parse(myData);
			counterlabs = labsaLive.length - 1;
			if(counterlabs >= 0)
			{
				setInterval(imagerefresh, 1000);
			}
		},
		error: function (data) {}
	});
	
	function imagerefresh() {
		if(counterlabs <= currentlab)
		{
			durationlab = 30;
			currentlab = 0;
			$.ajax(
			{
				url: "scripts/getlabslive.php",
				data: {
					"Action": "soheyln.com",
				},
				type: "POST",
				contentType: "application/x-www-form-urlencoded;charset=ISO-8859-15",
				success: function (data) {
					var myData = data.toString();
					labsaLive = JSON.parse(myData);
					counterlabs = labsaLive.length - 1;
				},
				error: function (data) {}
			});
			if(counterlabs == 0)
			{
				$("#labs_preview").attr('src', 'images/background.png');
				$("#textlivelab").html('No Active Lab');
				$("#countdownliveimage").html('');
				$("#textlivelabmodal").html('No Active Lab');
				return;
			}
			else
			{
				$("#textlivelab").html('<img src="images/icon_blink.gif" style="width:16;height:16" id="imgblinklivelab"/> Live Arduino Lab');
				$("#textlivelabmodal").html('<img src="images/icon_blink.gif" style="width:16;height:16" id="imgblinklivelab"/> Live Arduino Lab');
			}
		}
		if(durationlab > 0)
		{
			var mylab = '"' + labsaLive[currentlab].labID + '"';
			var mydev = '"' + labsaLive[currentlab].devID + '"';
			$.ajax(
			{
				url: "scripts/getlabimages.php",
				data: {
					"Action": "soheyln.com",
					"mylab": mylab,
					"mycam": mydev,
				},
				type: "POST",
				contentType: "application/x-www-form-urlencoded;charset=ISO-8859-15",
				success: function (data) {
					var myData = data.toString();
					var obj = JSON.parse(myData);
					var imagesource = 'data:image/jpeg;base64,' + obj[0].labImage;
					$("#labs_preview").attr('src', imagesource);
					$("#countdownliveimage").html(durationlab);
					$("#textlivelab").html('<img src="images/icon_blink.gif" style="width:16;height:16" id="imgblinklivelab"/> Live Arduino Lab');
					$("#textlivelabmodal").html('<img src="images/icon_blink.gif" style="width:16;height:16" id="imgblinklivelab"/> Live Arduino Lab');
					durationlab--;
				},
				error: function (data) {}
			});
		}
		else
		{
			currentlab++;
			durationlab = 30;
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
