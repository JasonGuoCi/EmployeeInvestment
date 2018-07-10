<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnnouncementCtrl.ascx.cs" Inherits="KMS_Copy.CONTROLTEMPLATES.KMS_Copy.HomePage.AnnouncementCtrl" %>
<link href="/_layouts/15/KMS_Copy/Styles/Site.css" rel="stylesheet" />

<div id="announcement">
    <div class="content">
        <div class="displayTite">
            <span data-bind="text: TitleDisplay"></span>
        </div>
        <div id="announcement-content">
            <div id="scroller">
                <div data-bind="foreach: DataAttr" style="margin-right: 10px;">
                    <div style="margin-top: 10px;">
                        tg
                        <div><span style="font-size: 16px; font-weight: bold;" data-bind="text: Title"></span></div>
                        <div>
                            <span data-bind="text: Content"></span>
                        </div>
                        <div>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <div class="focuswin-diver"></div>
                                    </td>
                                    <td style="width: 100px; text-align: right;">
                                        <span style="height: 15px; line-height: 15px;" data-bind="text: Modified"></span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div style="text-align: center;">
                    <a href="javascript:void();" data-bind="click: loadmore, visible: MoreVisible">MORE</a>
                </div>
            </div>
        </div>
        <div class="link">
            <img src="/_layouts/15/KMS_Copy/Images/AnnoucementMenu.png" class="bg-img" />
            <div class="circle div1" data-bind="click: Navigate2Page.bind($data, 'Wikipedia.aspx'), event: { mouseover: enablewiki, mouseout: disablewiki }">
                <img src="/_layouts/15/KMS_Copy/Images/Icon_circle.png" style="width: 100%; height: 100%;" data-bind="visible: WikiEnable" />
            </div>
            <div class="textword div1">
                <span data-bind="text: WikiDisplay, click: Navigate2Page.bind($data, 'Wikipedia.aspx')"></span>
            </div>
            <div class="circle div2" data-bind="click: Navigate2Page.bind($data, 'Manual.aspx'), event: { mouseover: enablemanual, mouseout: disablemanual }">
                <img src="/_layouts/15/KMS_Copy/Images/Icon_circle.png" style="width: 100%; height: 100%;" data-bind="visible: ManualEnable" />
            </div>
            <div class="textword div2"><span data-bind="text: ManualDisplay, click: Navigate2Page.bind($data, 'Manual.aspx')"></span></div>
            <div class="circle div3" data-bind="click: Navigate2Page.bind($data, 'ForumContent.aspx'), event: { mouseover: enableforum, mouseout: disableforum }">
                <img src="/_layouts/15/KMS_Copy/Images/Icon_circle.png" style="width: 100%; height: 100%;" data-bind="visible: ForumEnable" />
            </div>
            <div class="textword div3"><span data-bind="text: ForumDisplay, click: Navigate2Page.bind($data, 'ForumContent.aspx')"></span></div>
            <div class="focuswin-clear"></div>
        </div>
    </div>
</div>
<script type="text/javascript" src="/_layouts/15/KMS_Copy/Scripts/backstretch-ini.js"></script>
<script type="text/javascript" src="/_layouts/15/KMS_Copy/Scripts/jquery.nicescroll.min.js"></script>
<script type="text/javascript" src="/_layouts/15/KMS_Copy/Scripts/HomePage/Announcement.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        App.init();
    });
</script>

<!--[if lt IE 9]>
<script src="/_layouts/15/KMS_Copy/Global/scripts/respond.js"></script>
<script src="/_layouts/15/KMS_Copy/Global/scripts/html5shiv.js"></script>
<script src="/_layouts/15/KMS_Copy/Global/scripts/placeholder-IE-fixes.js"></script>
<![endif]-->