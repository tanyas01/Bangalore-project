$(function() {

	$('.hs-menu-item.has-children > a').append('<span class="child-trigger"></span>');

	$('.mobile-trigger').click(function() {
		$('body').toggleClass('mobile-open');
		$('.child-trigger').removeClass('child-open');
		$('.hs-menu-children-wrapper').slideUp(0);
		return false;
	});

	$(".child-trigger").click(function() {
		$(this).parent().parent().siblings(".hs-menu-item.has-children").find(".child-trigger").removeClass("child-open");
		$(this).parent().parent().siblings(".hs-menu-item.has-children").find(".hs-menu-children-wrapper").slideUp(250);
		$(this).parent().next(".hs-menu-children-wrapper").slideToggle(250);
		$(this).parent().next(".hs-menu-children-wrapper").children(".hs-menu-item.has-children").find(".hs-menu-children-wrapper").slideUp(250);
		$(this).parent().next(".hs-menu-children-wrapper").children(".hs-menu-item.has-children").find(".child-trigger").removeClass("child-open");
		$(this).toggleClass("child-open");
		return false;
	});

	$( ".custom-menu-area" ).click(function(event) {
		event.stopPropagation();
	});

	$('.custom-header-wrapper').css('padding-bottom',$('.custom-header-container').height());
	$('.custom-menu-primary').before($('.custom-logo').clone());
	$('.custom-menu-area .custom-logo a').after('<div class="trigger-toggle"><i></i></div>');

	$('body, .custom-menu-area .trigger-toggle').click(function() {
		$('body').removeClass('mobile-open');
	});


	$(window).scroll(function() {
		if ($(this).scrollTop() > 28) {
			$("body").addClass("scroll-header");
		} else {
			$("body").removeClass("scroll-header");
		}

		ScrollOut({
			targets: '.cbp-so-section'
		})

	});


	


});


