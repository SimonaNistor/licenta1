/*
Author       : Theme Crazy
Template Name: xCode - App Landing Page
Version      : 1.0
*/

(function($)
{
	"use strict";
	
	// Preloader
	jQuery(window).on('load', function() {
		preloader();
	})
	
	// JQuery for page scrolling feature - requires JQuery Easing plugin
	jQuery(document).on('ready', function () {
		jQuery('.navbar-nav li a').on('click', function(event) {
			jQuery('.nav li').removeClass('active');
			jQuery(this).parent().addClass('active');
			var $anchor = jQuery(this);
			jQuery('html, body').stop().animate({
				scrollTop: jQuery($anchor.attr('href')).offset().top-66
			}, 1500, 'easeInOutExpo');
			event.preventDefault();
		});
		
		jQuery(".navbar-nav li a").on("click",function(event){
			jQuery(".navbar-collapse").removeClass('in');
		});
		 
		jQuery('body').scrollspy({ 
			target: '#navbar',
			offset: 68
		});
				
		// Parallax Bg
		jQuery('#home').parallax("50%", 0.2);
		jQuery('#fun-facts').parallax("50%", 0.2);
		jQuery('#downloads').parallax("50%", 0.2);
		//jQuery('#video').parallax("50%", 0.2);
		jQuery('#subscription').parallax("50%", 0.2);
		
		// Video Bg
		jQuery('.player').mb_YTPlayer();
		
		// App Video
		function autoPlayYouTubeModal() {
			var trigger = $("body").find('[data-toggle="modal"]');
			trigger.on("click",function() {
                var theModal = $(this).data("target"),
                    videoSRC = $(theModal + ' iframe').attr('src'),
				videoSRCauto = videoSRC + "?autoplay=1";
			  $(theModal + ' iframe').attr('src', videoSRCauto);
			  $(theModal + ' button.close').on("click",function() {
				$(theModal + ' iframe').attr('src', videoSRC);
			  });
			  $('.modal').on("click",function() {
				$(theModal + ' iframe').attr('src', videoSRC);
			  });
			});
		  }
		  autoPlayYouTubeModal();
		
		 //Member Carousel
		jQuery("#member-carousel").owlCarousel({
			//loop:true,
			margin: 30,
			nav: false,
			dots: true,
			smartSpeed:600,
			autoplay: true,
			responsiveClass: true
			//responsive:{
			//	0:{
			//		items:1,
			//	},
			//	640:{
			//		items:2,
			//	},
			//	992:{
			//		items:3,
			//	}
			//}
		});
		
		// Animation section
		 if(jQuery('.wow').length){
			var wow = new WOW(
			  {
				boxClass:     'wow',      // animated element css class (default is wow)
				animateClass: 'animated', // animation css class (default is animated)
				offset:       0,          // distance to the element when triggering the animation (default is 0)
				mobile:       true,       // trigger animations on mobile devices (default is true)
				live:         true       // act on asynchronously loaded content (default is true)
			  }
			);
			wow.init();
		}
		
		// CounterUp 
		jQuery('.counter').counterUp({
			delay: 10,
			time: 2000
		});
		
		// Back to top 
		jQuery(".back-top").hide();
		
		jQuery('.back-top a').on('click', function(event) {
			jQuery('body,html').animate({scrollTop:0},800);
			return false;
		});
		
		// Google Map
	});
	
	jQuery(window).on('scroll', function() {
		//Header
		if (jQuery(".navbar").offset().top > 50) {
			jQuery(".navbar-fixed-top").addClass("top-nav-collapse");
		} else {
			jQuery(".navbar-fixed-top").removeClass("top-nav-collapse");
		}
	
		// Back to top 
		if(jQuery(this).scrollTop()>150){
			jQuery('.back-top').fadeIn();
		}
		else{
			jQuery('.back-top').fadeOut();
		}
	});
	
	// Preload
	function preloader(){
		jQuery(".preloaderimg").fadeOut();
		jQuery(".preloader").delay(200).fadeOut("slow").delay(200, function(){
			jQuery(this).remove();
		});
	}
	
	// Vertical Center Modal
	function centerModals($element) {
		  var $modals;
		  if ($element.length) {
			$modals = $element;
		  } else {
			$modals = jQuery('.modal-vcenter:visible');
		  }
		  $modals.each( function(i) {
			var $clone = jQuery(this).clone().css('display', 'block').appendTo('body');
			var top = Math.round(($clone.height() - $clone.find('.modal-content').height()) / 2);
			top = top > 0 ? top : 0;
			$clone.remove();
			jQuery(this).find('.modal-content').css("margin-top", top);
		  });
	}
		
	jQuery('.modal-vcenter').on('show.bs.modal', function(e) {
	  centerModals($(this));
	});
	jQuery(window).on('resize', centerModals);
	
})(jQuery);	
	
