/* global
$, WOW, Swiper
*/
$(function () {
    //new WOW().init();
    
    var view_width = window.innerWidth;
	const navEffHeight = 20;
    const $mainContainer = $(".main");
    const $navbar = $(".navbar");
	const $searchBox = $("#smart-sreach-box");
	
    var showSerachBtnHeight = getContentSearchBoxBottom();
	 
    function resetMainContainer() {
        $mainContainer.attr("class", "main");
    }

    function lockMainOverflow(io) {
        //$("body").css("overflow", io ? "hidden" : "visible");
    }
	function getContentSearchBoxBottom() {
        return $searchBox.length ? $searchBox.outerHeight() + $searchBox.position().top - $navbar.outerHeight() : -99;
    }  
	 $('body').click(function (e) {
        if ($('.navbar-cards-member').is(':visible')
            && $(e.target).parents('.navbar-cards-member').length == 0
            && $(e.target).parents('.mcBtn').length == 0
            && $(e.target).parents('.nav-link').length == 0) {
            resetMainContainer();
        }
    });
	$(".navbar a[data-key*='open-bn'],.member-nav-links a[data-key='open-searchbox']").on("click", function () {

		if ($mainContainer.hasClass($(this).data("key"))) {
			resetMainContainer();
			lockMainOverflow(false);
		} 
		else {
			$mainContainer.attr("class", "main " + $(this).data("key"));
			if ($(window).width() < 992) {
				lockMainOverflow(true);
			}
		}

	});
	$(".navbar-cards").on("click", function (e) {
		if (e.target === this && $(window).width() < 992) {
			resetMainContainer();
			lockMainOverflow(false);
		}
	});
	$(".navbar-cards .close-btn").on("click", function () {
		resetMainContainer();
		lockMainOverflow(false);
	});
   
	 
	
});