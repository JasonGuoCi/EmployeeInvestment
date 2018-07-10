$(document).ready(function() {
    $.getScript("/_layouts/15/Envision.KMS.Solution/Scripts/jquery.backstretch.min.js", function () {
        var bgUrl = SP2013.Utility.concatUrl(SP2013.O365.currentWebAbsoluteUrl(), "PublishingImages/KMS/bg.jpg");
        $(".project-frame-main").backstretch(bgUrl);
  });
});