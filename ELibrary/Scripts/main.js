(function ($) {
    "use strict";

    /*---------------------------------------------------
        Site Preloader
    ----------------------------------------------------*/
    $(window).on('load', function () {
        $('.site-preloader').fadeOut(1000);
        $('#popup-box').modal('show');
    });

    /*---------------------------------------------------
        POPUP Dialog Box
    ----------------------------------------------------*/
    $('#popup-box').on('show.bs.modal', function (e) {
        if (window.innerWidth < 767) {
            return e.preventDefault();
        }
    });
    $('#popup-box').on('show.bs.modal', function (e) {
        $('body').addClass('remove-padding');
    });

    /*---------------------------------------------------
        Counter
    ----------------------------------------------------*/
    $('.counter-item h1').counterUp({
        delay: 10, // the delay time in ms
        time: 1000 // the speed time in ms
    });

    /*---------------------------------------------------
        Slider Carousel
    ----------------------------------------------------*/
    $('.directory-ctegory-items,.latest-item-carousel .latest-items').slick({
        dots: false,
        slidesToShow: 4,
        slidesToScroll: 1,
        touchMove: false,
        arrows: true,
        autoplay: true,
        autoplaySpeed: 2000,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 991,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 767,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 320,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
                            }

                    ]
    });

    /*---------------------------------------------------
        Single Listing Page Carousel
    ----------------------------------------------------*/
    $('.listing-single-carousel .list-singl-carosl-items').slick({
        dots: false,
        slidesToShow: 3,
        slidesToScroll: 1,
        touchMove: false,
        arrows: true,
        autoplay: true,
        autoplaySpeed: 2000,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 991,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 767,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 320,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
                            }

                    ]
    });
    /*---------------------------------------------------
       Partner Carousel
    ----------------------------------------------------*/
    $('.partner-item .partner-item-list').slick({
        dots: false,
        slidesToShow: 5,
        slidesToScroll: 1,
        touchMove: false,
        arrows: true,
        autoplay: true,
        autoplaySpeed: 2000,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 991,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 767,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
                            },
            {
                breakpoint: 320,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
                            }

                    ]
    });
    /*---------------------------------------------------
        Scroll To Top
    ----------------------------------------------------*/
    $('a[href=#top]').on('click', function () {
        $('body,html').animate({
            scrollTop: 0
        }, 600);
        return false;
    });
    $(window).on('scroll', function () {
        if ($(this).scrollTop() > 250) {
            $('.totop').fadeIn();
        } else {
            $('.totop').fadeOut();
        }
    });

    /*---------------------------------------------------
          Tooltip
      ----------------------------------------------------*/
    $('[data-toggle="tooltip"]').tooltip();
    /*---------------------------------------------------
        Search Form Select
    ----------------------------------------------------*/
    $(".directory-search-form-one .select-city").select2({
        placeholder: "Select Cities"
    });

    /*---------------------------------------------------
        Slider Fade Effects
    ----------------------------------------------------*/
    $(".carousel-fade").carousel({
        interval: 2000,
        pause: false
    });

    /*---------------------------------------------------
        Bootstrap Select Option
    ----------------------------------------------------*/
    $('.selectpicker').selectpicker({
        style: 'btn-info',
        size: 4
    });

    /*---------------------------------------------------
        Range Slider
    ----------------------------------------------------*/
    $("#price-range").slider();
    // With JQuery
    $("#distance-range").slider({
        tooltip: 'always'
    });
    $("#ex13").slider();
    /*---------------------------------------------------
        Style Switcher Panel
    ----------------------------------------------------*/
    $(".switcher-panel-box .switcher-panel-spinner").on('click', function (event) {
        event.preventDefault();
        if ($(this).hasClass("switcher-panel-icon")) {
            $(".style-switcher-panel").stop().animate({
                left: "-200px"
            }, 500);
        } else {
            $(".style-switcher-panel").stop().animate({
                left: "0px"
            }, 500);
        }
        $(this).toggleClass("switcher-panel-icon");
        return false;
    });

    /*---------------------------------------------------
        Switcher Colors
    ----------------------------------------------------*/
    function colorBase() {
        $('body').attr('class', 'color-base');
    }

    function colorOne() {
        $('body').attr('class', 'color-one');
    }

    function colorTwo() {
        $('body').attr('class', 'color-two');
    }

    function colorThree() {
        $('body').attr('class', 'color-three');
    }

    function colorFour() {
        $('body').attr('class', 'color-four');
    }

    function colorFive() {
        $('body').attr('class', 'color-five');
    }

    function colorSix() {
        $('body').attr('class', 'color-six');
    }

    function colorSeven() {
        $('body').attr('class', 'color-seven');
    }

    function colorEight() {
        $('body').attr('class', 'color-eight');
    }

    function colorNine() {
        $('body').attr('class', 'color-nine');
    }

    function colorTen() {
        $('body').attr('class', 'color-ten');
    }

    function colorEleven() {
        $('body').attr('class', 'color-eleven');
    }

    function layoutBoxed() {
        $('body').attr('class', 'layout-boxed');
    }

    function layoutWide() {
        $('body').attr('class', 'layout-wide');
    }

    $('.switcher-colors span.color-base').on('click', colorBase);
    $('.switcher-colors span.color-one').on('click', colorOne);
    $('.switcher-colors span.color-two').on('click', colorTwo);
    $('.switcher-colors span.color-three').on('click', colorThree);
    $('.switcher-colors span.color-four').on('click', colorFour);
    $('.switcher-colors span.color-five').on('click', colorFive);
    $('.switcher-colors span.color-six').on('click', colorSix);
    $('.switcher-colors span.color-seven').on('click', colorSeven);
    $('.switcher-colors span.color-eight').on('click', colorEight);
    $('.switcher-colors span.color-nine').on('click', colorNine);
    $('.switcher-colors span.color-ten').on('click', colorTen);
    $('.switcher-colors span.color-eleven').on('click', colorEleven);
    $('.switcher-layout span.boxed').on('click', layoutBoxed);
    $('.switcher-layout span.wide').on('click', layoutWide);

    $('.switcher-layout span.boxed').click(function () {
        $('body div.container').addClass('container-fluid').removeClass('container');
    });
    $('.switcher-layout span.wide').click(function () {
        $('body div.container-fluid').addClass('container').removeClass('container-fluid');
    });


    /*---------------------------------------------------
        Header Style Switcher
    ----------------------------------------------------*/
    $(".switcher-header .hdr-main-one").click(function () {
        $(".header.header-main").addClass("switcher");
        $(".header.header-main").removeClass("header-two");
        $(".header.header-main").removeClass("header-three");
        $(".header.header-main").removeClass("header-static-one");
        $(".header.header-main").removeClass("header-static-two");
        $(".header.header-main").removeClass("header-static-three");
    });
    $(".switcher-header .hdr-main-two").click(function () {
        $(".header.header-main").addClass("header-two");
        $(".header.header-main").removeClass("header-three");
        $(".header.header-main").removeClass("switcher");
        $(".header.header-main").removeClass("header-static-one");
        $(".header.header-main").removeClass("header-static-two");
        $(".header.header-main").removeClass("header-static-three");
    });
    $(".switcher-header .hdr-main-three").click(function () {
        $(".header.header-main").addClass("header-three");
        $(".header.header-main").removeClass("header-two");
        $(".header.header-main").removeClass("switcher");
        $(".header.header-main").removeClass("header-static-one");
        $(".header.header-main").removeClass("header-static-two");
        $(".header.header-main").removeClass("header-static-three");
    });
    /*---------------------------------------------------
        Header Static Switcher
    ----------------------------------------------------*/
    $(".switcher-header .hdr-static-one").click(function () {
        $(".header.header-main").addClass("header-static-one");
        $(".header.header-main").removeClass("header-two");
        $(".header.header-main").removeClass("header-three");
        $(".header.header-main").removeClass("switcher");
        $(".header.header-main").removeClass("header-static-two");
        $(".header.header-main").removeClass("header-static-three");
    });
    $(".switcher-header .hdr-static-two").click(function () {
        $(".header.header-main").addClass("header-static-two");
        $(".header.header-main").removeClass("header-two");
        $(".header.header-main").removeClass("header-three");
        $(".header.header-main").removeClass("switcher");
        $(".header.header-main").removeClass("header-static-one");
        $(".header.header-main").removeClass("header-static-three");
    });
    $(".switcher-header .hdr-static-three").click(function () {
        $(".header.header-main").addClass("header-static-three");
        $(".header.header-main").removeClass("header-two");
        $(".header.header-main").removeClass("header-three");
        $(".header.header-main").removeClass("switcher");
        $(".header.header-main").removeClass("header-static-one");
        $(".header.header-main").removeClass("header-static-two");
    });
    /*---------------------------------------------------
           Footer Style Switcher
    ----------------------------------------------------*/
    $(".switcher-header .fotr-one").click(function () {
        $(".footer").addClass("switcher-footer");
        $(".footer").removeClass("switcher-footer-two");
        $(".footer").removeClass("switcher-footer-three");
    });
    $(".switcher-header .fotr-two").click(function () {
        $(".footer").addClass("switcher-footer-two");
        $(".footer").removeClass("switcher-footer-three");
        $(".footer").removeClass("switcher-footer");
    });
    $(".switcher-header .fotr-three").click(function () {
        $(".footer").addClass("switcher-footer-three");
        $(".footer").removeClass("switcher-footer-two");
        $(".footer").removeClass("switcher-footer");
    });


    /*---------------------------------------------------
        Bootstrap Date Picker
    ----------------------------------------------------*/
    $('#datetimepicker1,#datetimepicker2').datetimepicker();

})(jQuery);
