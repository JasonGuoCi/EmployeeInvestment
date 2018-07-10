var App = function () {

    function handleBootstrap() {
        /*Bootstrap Carousel*/
        jQuery('.carousel').carousel({
            interval: 15000,
            pause: 'hover'
        });

        /*Tooltips*/
        jQuery('.tooltips').tooltip();
        jQuery('.tooltips-show').tooltip('show');
        jQuery('.tooltips-hide').tooltip('hide');
        jQuery('.tooltips-toggle').tooltip('toggle');
        jQuery('.tooltips-destroy').tooltip('destroy');

        /*Popovers*/
        jQuery('.popovers').popover();
        jQuery('.popovers-show').popover('show');
        jQuery('.popovers-hide').popover('hide');
        jQuery('.popovers-toggle').popover('toggle');
        jQuery('.popovers-destroy').popover('destroy');
    }

    var handleFullscreen = function () {
        var WindowHeight = $(window).height();
        HeaderHeight = $("#nav").height();

        $(".project-frame-main").css("height", WindowHeight - HeaderHeight);
        $(".focuswin-content").css("height", WindowHeight - HeaderHeight);

        $(window).resize(function () {
            var WindowHeight = $(window).height();
            $(".project-frame-main").css("height", WindowHeight - HeaderHeight);
            $(".focuswin-content").css("height", WindowHeight - HeaderHeight);

        });
    }

    // handleLangs
    function handleLangs() {
        $(".lang-block").click(function () {
            console.log("click!");
        });
    }

    var handleValignMiddle = function () {
        $(".valign__middle").each(function () {
            $(this).css("padding-top", '80px');
        });
        //$(window).resize(function() {
        //  $(".valign__middle").each(function() {
        //    $(this).css("padding-top", $(this).parent().height() / 2 - $(this).height() / 2);
        //  });
        //});
    }

    return {
        init: function () {
            handleBootstrap();
            handleFullscreen();
        }
    };

}();


