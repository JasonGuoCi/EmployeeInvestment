<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GlobalPageCtrl.ascx.cs" Inherits="KMS_Copy.CONTROLTEMPLATES.KMS_Copy.HomePage.GlobalPageCtrl" %>
<link rel="stylesheet" href="/_layouts/15/KMS_Copy/Global/styles/style.css">
<link href="/_layouts/15/KMS_Copy/Styles/bootstrap.css" rel="stylesheet" />
<!-- CSS Implementing Plugins -->
<link rel="stylesheet" href="/_layouts/15/KMS_Copy/Global/styles/animate.css">
<link rel="stylesheet" href="/_layouts/15/KMS_Copy/Global/styles/settings.css" type="text/css"
    media="screen">
<!--[if lt IE 9]>
    <link rel="stylesheet" href="/_layouts/15/KMS_Copy/Global/styles/settings-ie8.css" type="text/css"
          media="screen"><![endif]-->
<div id="global-silders">
    <div class="tp-banner-container">
        <div class="tp-banner">
            <ul data-bind="foreach: DataAttr">
                <!--SLIDE-->
                <li class="revolution-mch-1" data-transition="fade" data-slotamount="5" data-masterspeed="1000"
                    data-title="Slide 1" data-bind="attr: { 'data-title': 'slider' + Id(), Id: 'slider' + Id() }">
                    <div class="global-page-content">
                        <div style="text-align: center; font-weight: bold; margin: 20px 0px;" class="rs-caption-1" data-bind="text: Title">
                        </div>
                        <div class="re-text-v1" data-bind="text: Description"></div>
                        <div style="text-align: center; margin-top: 10px;">
                            <a href="#" class="btn-u btn-u-lg re-btn-brd panel-trigger margin-right-5" data-bind="click: ClickContact, text: $root.ContactUsDisplay"></a>
                            <a href="#" target="_blank" class="btn-u btn-u-lg" data-bind="attr: { href: Link }, text: $root.LinkDisplay"></a>
                        </div>
                    </div>
                </li>
                <!-- END SLIDE -->
            </ul>
            <div class="tp-bannertimer tp-bottom"></div>
        </div>

    </div>
    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" id="contactModel">
        <div class="modal-dialog modal-lg" style="width: 480px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel" data-bind="text: ContactUsDisplay"></h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                        <div data-bind="foreach: ContactAttr">
                            <div style="margin-bottom:10px;height:144px;">
                                <div style="height: 34px; line-height: 34px;padding-left:10px;color:#fff; font-size: 16px; background-color: #6ac8e4;"><span data-bind="text: Title"></span></div>
                                <div style="position: relative;margin-top:5px;"  data-bind="visible: Contact() || Extend3()">
                                    <img src="/_layouts/15/KMS_Copy/Images/profile.png" style="width: 25px; position: absolute; top: 5px; left: 5px;" />
                                    <div style="margin-left: 35px;">
                                        <div class="form-group" style="margin: 0px 10px; height: 25px; line-height: 25px;" data-bind="visible: Contact">
                                            <div style="float: left;width:65px; overflow: hidden;color:#7e7e7e;">
                                                <span data-bind="text: $root.ContactDisplay"></span>:
                                            </div>
                                            <div style="float: left;color:#7e7e7e;">
                                                <span data-bind="text: Contact"></span>
                                            </div>
                                            <div class="focuswin-clear"></div>
                                        </div>
                                        <div class="form-group" style="margin: 0px 10px; height: 25px; line-height: 25px;" data-bind="visible: Extend3">
                                            <div style="float: left;width:65px;  overflow: hidden;color:#7e7e7e;">
                                                <span data-bind="text: $root.Extend3Text"></span>:
                                            </div>
                                            <div style="float: left;color:#7e7e7e;">
                                                <a href="#" data-bind="attr: { href: 'mailto:' + Extend3 ()}"> <span data-bind="    text: Extend3"></span></a>
                                            </div>
                                            <div class="focuswin-clear"></div>
                                        </div>
                                        <div class="focuswin-clear"></div>
                                    </div>
                                </div>
                                <div style="position: relative;margin-top:5px;" data-bind="visible: Extend1() || Extend2()">
                                    <img src="/_layouts/15/Envision.KMS.Solution/Images/mobile.png" style="width: 18px; position: absolute; top: 6px; left: 8px;" />
                                    <div style="margin-left: 35px;">
                                        <div class="form-group" style="margin: 0px 10px; height: 25px; line-height: 25px;" data-bind="visible: Extend1">
                                            <div style="float: left; width:65px; overflow: hidden;color:#7e7e7e;">
                                                <span data-bind="text: $root.Extend1Text"></span>:
                                            </div>
                                            <div style="float: left;color:#7e7e7e;">
                                                <span data-bind="text: Extend1"></span>
                                            </div>
                                            <div class="focuswin-clear"></div>
                                        </div>
                                        <div class="form-group" style="margin: 0px 10px; height: 25px; line-height: 25px;" data-bind="visible: Extend2">
                                            <div style="float: left;width:65px; overflow: hidden;color:#7e7e7e;">
                                                <span data-bind="text: $root.Extend2Text"></span>:
                                            </div>
                                            <div style="float: left;color:#7e7e7e;">
                                                <span data-bind="text: Extend2"></span>
                                            </div>
                                            <div class="focuswin-clear"></div>
                                        </div>
                                        <div class="focuswin-clear"></div>
                                    </div>
                                </div>
                            </div>
                            <div style="background-color: #fafafa; padding: 5px; border: 1px solid #f1f1f1; border-radius: 5px; margin-bottom: 10px;display:none;">
                                <div style="font-size: 16px; font-weight: bold; height: 30px; line-height: 30px; padding-left: 6px;">
                                    <span data-bind="text: Name"></span>
                                </div>
                                <div class="form-group" style="margin: 0px 10px; height: 25px; line-height: 25px;">
                                    <div style="float: left; width: 90px; overflow: hidden;">
                                        <span class="glyphicon glyphicon-info-sign"></span>
                                        <span data-bind="text: $root.ContactDisplay"></span>:
                                    </div>
                                    <div style="float: left; width: 260px;">
                                        <span data-bind="text: Contact"></span>
                                    </div>
                                    <div class="focuswin-clear"></div>
                                </div>

                                <div class="form-group" style="margin: 0px 10px; height: 25px; line-height: 25px;" data-bind="visible: Extend1">
                                    <div style="float: left; width: 90px; overflow: hidden;">
                                        <span class="glyphicon glyphicon-info-sign"></span>
                                        <span data-bind="text: $root.Extend1Text"></span>:
                                    </div>
                                    <div style="float: left; width: 260px;">
                                        <span data-bind="text: Extend1"></span>
                                    </div>
                                    <div class="focuswin-clear"></div>
                                </div>
                                <div class="form-group" style="margin: 0px 10px; height: 25px; line-height: 25px;" data-bind="visible: Extend2">
                                    <div style="float: left; width: 90px; overflow: hidden;">
                                        <span class="glyphicon glyphicon-info-sign"></span>
                                        <span data-bind="text: $root.Extend2Text"></span>:
                                    </div>
                                    <div style="float: left; width: 260px;">
                                        <span data-bind="text: Extend2"></span>
                                    </div>
                                    <div class="focuswin-clear"></div>
                                </div>
                                <div class="form-group" style="margin: 0px 10px; height: 25px; line-height: 25px;" data-bind="visible: Extend3">
                                    <div style="float: left; width: 90px; overflow: hidden;">
                                        <span class="glyphicon glyphicon-info-sign"></span>
                                        <span data-bind="text: $root.Extend3Text"></span>:
                                    </div>
                                    <div style="float: left; width: 260px;">
                                        <span data-bind="text: Extend3"></span>
                                    </div>
                                    <div class="focuswin-clear"></div>
                                </div>
                                <div class="focuswin-clear"></div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="/_layouts/15/KMS_Copy/Scripts/bootstrap.js"></script>
<script src="/_layouts/15/KMS_Copy/Global/scripts/jquery.themepunch.tools.min.js"></script>
<script src="/_layouts/15/KMS_Copy/Global/scripts/jquery.themepunch.revolution.min.js"></script>
<script src="/_layouts/15/KMS_Copy/scripts/jquery.backstretch.min.js"></script>
<script src="/_layouts/15/KMS_Copy/Global/scripts/style-switcher.js"></script>
<script src="/_layouts/15/KMS_Copy/Global/scripts/revolution-slider.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/HomePage/slidereveal.js"></script>
<script src="/_layouts/15/KMS_Copy/Global/scripts/app.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        App.init();
        var SliderMgr = new sliderMgr();
        SliderMgr.Load();
        ko.applyBindings(SliderMgr, document.getElementById('global-silders'));
    });
</script>
<!--[if lt IE 9]>
<script src="/_layouts/15/Envision.KMS.Solution/Global/scripts/respond.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Global/scripts/html5shiv.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Global/scripts/placeholder-IE-fixes.js"></script>
<![endif]-->
