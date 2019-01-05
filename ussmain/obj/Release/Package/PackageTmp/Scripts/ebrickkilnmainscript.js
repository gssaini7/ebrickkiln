; (function () {

    'use strict';



    // iPad and iPod detection	
    var isiPad = function () {
        return (navigator.platform.indexOf("iPad") != -1);
    };

    var isiPhone = function () {
        return (
			(navigator.platform.indexOf("iPhone") != -1) ||
			(navigator.platform.indexOf("iPod") != -1)
	    );
    };

    // Parallax
    var parallax = function () {
        $(window).stellar();
    };



    // Burger Menu
    var burgerMenu = function () {

        $('body').on('click', '.js-ussebk-nav-toggle', function (event) {

            event.preventDefault();

            if ($('#navbar').is(':visible')) {
                $(this).removeClass('active');
            } else {
                $(this).addClass('active');
            }



        });

    };

   

    var testimonialCarousel = function () {
        var owl = $('.owl-carousel-fullwidth');
        owl.owlCarousel({
            items: 1,
            loop: true,
            margin: 0,
            responsiveClass: true,
            nav: false,
            dots: true,
            smartSpeed: 500,
            autoHeight: true
        });
    };

    // Page Nav
    var clickMenu = function () {

        $('#navbar a:not([class="external"])').click(function (event) {
            var section = $(this).data('nav-section'),
				navbar = $('#navbar');

            if ($('[data-section="' + section + '"]').length) {
                $('html, body').animate({
                    scrollTop: $('[data-section="' + section + '"]').offset().top - 55
                }, 500);
            }

            if (navbar.is(':visible')) {
                navbar.removeClass('in');
                navbar.attr('aria-expanded', 'false');
                $('.js-ussebk-nav-toggle').removeClass('active');
            }

            event.preventDefault();
            return false;
        });


    };

    

    // Reflect scrolling in navigation
    var navActive = function (section) {

        var $el = $('#navbar > ul');
        $el.find('li').removeClass('active');
        $el.each(function () {
            $(this).find('a[data-nav-section="' + section + '"]').closest('li').addClass('active');
        });

    };

    var navigationSection = function () {

        var $section = $('section[data-section]');

        $section.waypoint(function (direction) {

            if (direction === 'down') {
                navActive($(this.element).data('section'));
            }
        }, {
            offset: '150px'
        });

        $section.waypoint(function (direction) {
            if (direction === 'up') {
                navActive($(this.element).data('section'));
            }
        }, {
            offset: function () { return -$(this.element).height() + 155; }
        });

    };

    // Window Scroll
    var windowScroll = function () {
        var lastScrollTop = 0;

        $(window).scroll(function (event) {

            var header = $('#ussebk-header'),
				scrlTop = $(this).scrollTop();

            if (scrlTop > 500 && scrlTop <= 2000) {
                header.addClass('navbar-fixed-top ussebk-animated slideInDown');
            } else if (scrlTop <= 500) {
                if (header.hasClass('navbar-fixed-top')) {
                    header.addClass('navbar-fixed-top ussebk-animated slideOutUp');
                    setTimeout(function () {
                        header.removeClass('navbar-fixed-top ussebk-animated slideInDown slideOutUp');
                    }, 100);
                }
            }

        });
    };


    // Animations
    // Home
    var homeAnimate = function () {
        if ($('#ussebk-home').length > 0) {

            $('#ussebk-home').waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {


                    setTimeout(function () {
                        $('#ussebk-home .to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInUp animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };

    var exploreAnimate = function () {

        var explore = $('#ussebk-explore');
        if (explore.length > 0) {

            explore.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {


                    setTimeout(function () {
                        explore.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInUp animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);

                    setTimeout(function () {
                        explore.find('.to-animate-2').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInLeft animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 700);

                    setTimeout(function () {
                        explore.find('.to-animate-3').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInRight animated');
                            }, k * 200, 'easeInOutExpo');

                        });
                    }, 1000);


                    $(this.element).addClass('animated');

                }
            }, { offset: '80%' });

        }
    };

    var testimonyAnimate = function () {
        var testimony = $('#ussebk-testimony');
        if (testimony.length > 0) {

            testimony.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {


                    setTimeout(function () {
                        testimony.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInUp animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };

    var gettingStartedAnimate = function () {
        var started = $('.getting-started-1');
        if (started.length > 0) {

            started.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {

                 
                    setTimeout(function () {
                        started.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInUp animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);

                    setTimeout(function () {
                        started.find('.to-animate-2').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInRight animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);

                   


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };

    var gettingStarted2Animate = function () {
        var started = $('.getting-started-2');
        if (started.length > 0) {

            started.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {


                    setTimeout(function () {
                        started.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInUp animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);

                    setTimeout(function () {
                        started.find('.to-animate-2').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInRight animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };

    var pricingAnimate = function () {
        var pricing = $('#ussebk-pricing');
        if (pricing.length > 0) {

            pricing.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {


                    setTimeout(function () {
                        pricing.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeIn animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);

                    setTimeout(function () {
                        pricing.find('.to-animate-2').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInUp animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };


    var servicesAnimate = function () {
        var services = $('#ussebk-advantage');
        if (services.length > 0) {

            services.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {


                    //var sec = services.find('.to-animate').length,
                    //	sec = parseInt((sec * 200) + 400);
                    var sec = services.find('.to-animate').length,
                       sec = parseInt((sec * 100) + 150);

                    setTimeout(function () {
                        services.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInUp animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);

                    setTimeout(function () {
                        services.find('.to-animate-2').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('bounceIn animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, sec);


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };


    var teamAnimate = function () {
        var team = $('#ussebk-team');
        if (team.length > 0) {

            team.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {

                    var sec = team.find('.to-animate').length,
						sec = parseInt((sec * 150) + 400);

                    setTimeout(function () {
                        team.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeIn animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);

                    setTimeout(function () {
                        team.find('.to-animate-2').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInUp animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, sec);


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };


    var faqAnimate = function () {
        var faq = $('#ussebk-faq');
        if (faq.length > 0) {

            faq.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {

                    var sec = faq.find('.to-animate').length,
						sec = parseInt((sec * 150) + 400);

                    setTimeout(function () {
                        faq.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeIn animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);

                    setTimeout(function () {
                        faq.find('.to-animate-2').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeInUp animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, sec);


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };

    var trustedAnimate = function () {
        var trusted = $('#ussebk-trusted');
        if (trusted.length > 0) {

            trusted.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {

                    var sec = trusted.find('.to-animate').length,
						sec = parseInt((sec * 150) + 300);

                    setTimeout(function () {
                        trusted.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeIn animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);

                    setTimeout(function () {
                        trusted.find('.to-animate-2').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('bounceIn animated');
                            }, k * 100, 'easeInOutExpo');

                        });
                    }, sec);


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };


    var footerAnimate = function () {
        var footer = $('#ussebk-footer');
        if (footer.length > 0) {

            footer.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {

                    setTimeout(function () {
                        footer.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeIn animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };

    var footerAnimate = function () {
        var footer = $('#ussebk-footer');
        if (footer.length > 0) {

            footer.waypoint(function (direction) {

                if (direction === 'down' && !$(this.element).hasClass('animated')) {

                    setTimeout(function () {
                        footer.find('.to-animate').each(function (k) {
                            var el = $(this);

                            setTimeout(function () {
                                el.addClass('fadeIn animated');
                            }, k * 150, 'easeInOutExpo');

                        });
                    }, 150);


                    $(this.element).addClass('animated');

                }
            }, { offset: '90%' });

        }
    };

    var backtotop = function () {
        //back to top start
        var offset = 250;
        var duration = 500;
        jQuery(window).scroll(function () {
            if (jQuery(this).scrollTop() > offset) {
                jQuery('#back-top').fadeIn(duration);
            } else {
                jQuery('#back-top').fadeOut(duration);
            }
        });

        jQuery('#back-top').click(function (event) {
            event.preventDefault();
            jQuery('html, body').animate({ scrollTop: 0 }, duration);
            return false;
        });
        //back to top end
    }

    // Document on load.
    $(function () {

        parallax();
        burgerMenu();
        clickMenu();
        windowScroll();
        navigationSection();
        //testimonialCarousel();

        // Animations
        homeAnimate();
        exploreAnimate();
        testimonyAnimate();
        gettingStartedAnimate();
        gettingStarted2Animate();
        pricingAnimate();
        servicesAnimate();
        teamAnimate();
        faqAnimate();
        trustedAnimate();
        footerAnimate();
        backtotop();

    });


}());