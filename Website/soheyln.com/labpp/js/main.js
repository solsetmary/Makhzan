/*=============================
 Loader                     
===============================*/
// makes sure the whole site is loaded
$(window).load(function(){
	jQuery(".status").fadeOut();
	jQuery(".preloader").delay(1000).fadeOut("slow");
})


/*=============================
 Contact Form                   
===============================*/
$(document).ready(function(){
	var form = $('#form');
	var submit = $('#submit');	
	var alert = $('.alert');

	form.on('submit', function(e){
		e.preventDefault();

		$.ajax({
			url: '',
			type: 'POST',
			dataType: 'html',
			data: form.serialize(),
			beforeSend: function(){
				alert.fadeOut();
				submit.html('Sending....');
			},
			success: function(e) {
				alert.html(e).fadeIn();
				form.trigger('reset'); // reset form
				submit.html('Submit');
			},
			error: function(e) {
				console.log(e)
			}
		});
	});
});



/*=============================
 Navigation Bar                    
===============================*/

//jQuery to collapse the navbar on scroll

$(window).scroll(function() {
	var top = (document.documentElement && document.documentElement.scrollTop) || document.body.scrollTop;
    if (top > 40) $(".navbar-fixed-top").addClass("top-nav-collapse");
	else $(".navbar-fixed-top").removeClass("top-nav-collapse");
})

/*=============================
 Slider                 
===============================*/
$('.carousel').carousel();


/*=============================
 WOW for Animate.css                     
===============================*/
new WOW().init(
{
	mobile: false
});