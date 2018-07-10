var RevolutionSlider = function () {

    return {
        
        //Revolution Slider - Full Width
        initRSfullWidth: function (width,height) {
		    var revapi;
	        jQuery(document).ready(function() {
	            revapi = jQuery('.tp-banner').revolution(
	            {
	                delay:5000,
	                startwidth: width,
	                startheight: height
	            });
	        });
        },

        //Revolution Slider - Full Screen Offset Container
        initRSfullScreenOffset: function () {
		    var revapi;
	        jQuery(document).ready(function() {
	           revapi = jQuery('.tp-banner').revolution(
	            {
	                delay:5000,
	                startwidth:1170,
	                startheight:400,
	                hideThumbs:10,
	                fullWidth:"on",
	                fullScreen:"on",
	                hideCaptionAtLimit: "",
	                fullScreenOffsetContainer: ".header"
	            });
	        });
        }        

    };
}();        