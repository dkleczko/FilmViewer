
function createRaty(id, rates)
{
    $('#'+id+".score").raty({
        width: 130,
        score: rates,
        path: '/Content/images/rate/',
        starOff: 'star-off.svg',
        starOn: 'star-on.svg'
    });

    //2. Swiper slider
    //Media slider
    //init employee sliders
    var mySwiper = new Swiper('.swiper-container', {
        slidesPerView: 4,
    });

    $('.swiper-slide-active').css({ 'marginLeft': '-1px' });

    $('.comment-link').click(function (ev) {
        ev.preventDefault();
        $('html, body').stop().animate({ 'scrollTop': $('.comment-wrapper').offset().top - 90 }, 900, 'swing');
    });

    //pop up photo media element
    $('.movie__media-item').magnificPopup({
        type: 'image',
        closeOnContentClick: true,
        mainClass: 'mfp-fade',
        image: {
            verticalFit: true
        },

        gallery: {
            enabled: true,
            navigateByImgClick: true,
            preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
        },

        disableOn: function () {
            return toggle;
        }

    });


    //detect if was move after click
    $('.movie__media .swiper-slide').on('mousedown', function (e) {
        toggle = true;
        $(this).on('mousemove', function testMove() {
            toggle = false;
        });
    });

}


