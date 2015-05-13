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