<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManualContentCtrl.ascx.cs" Inherits="Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Manual.ManualContentCtrl" %>
<link href="/_layouts/15/Envision.KMS.Solution/Styles/Site.css" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/themes/default/style.min.css" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/themes/nav/style.css" rel="stylesheet" />
<div id="manualcontent">
    <table style="width: 100%;">
        <tr>
            <td class="focuswin-leftnav-td">
                <div class="focuswin-leftnav" data-bind="css: IsEnglishTitle() ? 'focuswin-leftnav' : 'focuswin-leftnav chs'">
                    <div class="leftnav-header" data-bind="text: NavTitle, click: Navigate2Root" ></div>
                    <div class="focuswin-diver" style="margin: 10px 20px;"></div>
                    <div id="left-nav"></div>
                </div>
            </td>
            <td>
                <div class="focuswin-content" style="padding-left: 40px; padding-right: 40px;">
                    <div id="toppage">
                        <div>
                            <div class="manual-part-header" data-bind="text: VideoText"></div>
                            <div style="border: 1px solid #0a58aa;"></div>
                            <div style="margin-top: 15px;">
                                <div class="manual-part-div" data-bind="click: ClickTopCategory.bind($data, 'VideoPage')">
                                    <img class="manual-part-bg"  src="/_layouts/15/Envision.KMS.Solution/Images/background_darkblue.png" />
                                    <div class="manual-part-content">
                                        <div style="height: 39px; line-height: 50px; font-size: 14px;" data-bind="text:VideoModuleText"></div>
                                        <div data-bind="text: VideoModuleDesc"></div>
                                        <div style="height: 90px; margin-top: 20px;"><span style="font-size: 63px; height: 80px;" data-bind="text: DataArray().length">15</span><span data-bind="text: CategoriesText"></span></div>
                                    </div>
                                </div>
                                <div class="manual-part-div" data-bind="click: ClickTopCategory.bind($data, 'VideoListPage')">
                                    <img class="manual-part-bg" src="/_layouts/15/Envision.KMS.Solution/Images/background_lightgreen.png" />
                                    <div class="manual-part-content">
                                        <div style="height: 39px; line-height: 50px; font-size: 14px;"><span data-bind="text: VideoCreatedText"></span></div>
                                        <div><span  data-bind="text: VideoCreatedDesc"></span></div>
                                        <div style="height: 90px; margin-top: 20px;"><span style="font-size: 63px; height: 80px;" data-bind="text: ListDataArray().length">15</span><span data-bind="    text: VideoCountText"></span></div>
                                    </div>
                                </div>
                            </div>
                            <div class="focuswin-clear"></div>
                        </div>
                        <div>
                            <div class="manual-part-header" ><span data-bind="text: ManualText"></span></div>
                            <div style="border: 1px solid #0a58aa;"></div>
                            <div style="margin-top: 15px;">
                                <div class="manual-part-div" data-bind="click: ClickTopCategory.bind($data, 'ManualPage')">
                                    <img class="manual-part-bg" src="/_layouts/15/Envision.KMS.Solution/Images/background_lightblue.png" />
                                    <div class="manual-part-content">
                                        <div style="height: 39px; line-height: 50px; font-size: 14px;"><span data-bind="text: UserModuleText"></span></div>
                                        <div><span data-bind="text: UserModuleDesc"></span></div>
                                        <div style="height: 90px; margin-top: 20px;"><span style="font-size: 63px; height: 80px;" data-bind="text: ManualTotal">8</span><span data-bind="    text: ManualCountText"></span></div>
                                    </div>
                                </div>
                            </div>
                            <div class="focuswin-clear"></div>
                        </div>
                    </div>
                    <div id="VideoPage" style="display: none;">
                         <div style="height: 30px; cursor: pointer;margin-top:10px;margin-bottom:10px;" data-bind="click: ReturnTopCategory.bind($data, 'VideoPage')">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <div style="width: 100%;" data-bind="foreach: DataArray">
                            <div class="manual-videopage-content" data-bind="click: click2Detail">
                                <img src="/_layouts/15/images/256_folder.png" style="width: 150px; height: 150px;" />
                                <div class="manual-videopage-cover">
                                    <span data-bind="text: Title, attr: { title: Title }"></span>
                                </div>
                            </div>
                        </div>
                        <div class="focuswin-diver focuswin-clear"></div>
                    </div>
                    <div id="VideoSubPage" style="display: none;">
                         <div style="height: 30px; cursor: pointer;margin-top:10px;margin-bottom:10px;" data-bind="click: ReturnPage.bind($data, 'VideoPage')">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <div style="width: 100%;" data-bind="foreach: DetailArray">
                            <div class="manual-videosubpage-content" data-bind="click: click2Detail">
                                <img src="" style="width: 204px; height: 140px;" data-bind="attr: { src: PreviewImage }" />
                                <div class="manual-videosubpage-cover">
                                    <span data-bind="text: Title, attr: { title: Title }"></span>
                                </div>
                            </div>
                        </div>
                        <div class="focuswin-diver focuswin-clear"></div>
                    </div>
                    <div id="VideoListPage" style="display: none;">
                        <div style="height: 30px; cursor: pointer;margin-top:10px;margin-bottom:10px;" data-bind="click: ReturnTopCategory.bind($data, 'VideoListPage')">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <div style="width: 100%;" data-bind="foreach: ListDataArray">
                            <div class="manual-videoListPage-content" data-bind="click: click2Detail">
                                <img src="" style="width: 204px; height: 140px;" data-bind="attr: { src: PreviewImage }" />
                                <div class="manual-videoListPage-cover">
                                    <div><span data-bind="text: Title, attr: { title: Title }"></span></div>
                                    <div><span data-bind="text: Created"></span></div>
                                </div>
                            </div>
                        </div>
                        <div class="focuswin-diver focuswin-clear"></div>
                    </div>
                    <div id="ManualPage" style="display: none;">
                         <div style="height: 30px; cursor: pointer;margin-top:10px;margin-bottom:10px;" data-bind="click: ReturnTopCategory.bind($data, 'ManualPage')">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <div style="width: 100%;">
                            <div class="manual-manualpage-content" data-bind="click: ClickManualDoc">
                                <img src="/_layouts/15/images/256_folder.png" style="width: 150px; height: 150px;" />
                                <div class="manual-manualpage-cover">
                                    <span data-bind="text: ManualDocText"></span>
                                </div>
                            </div>
                            <div class="manual-manualpage-content" data-bind="click: ClickManualHtml">
                                <img src="/_layouts/15/images/256_folder.png" style="width: 150px; height: 150px;" />
                                <div class="manual-manualpage-cover">
                                    <span data-bind="text: ManualHtmlText"></span>
                                </div>
                            </div>
                        </div>
                        <div class="focuswin-diver focuswin-clear"></div>
                    </div>
                    <div id="ManualHtml" style="display: none;">
                         <div style="height: 30px; cursor: pointer;margin-top:10px;margin-bottom:10px;" data-bind="click: ReturnManualPage.bind($data, 'ManualHtml')">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <div class="focuswin-manualhtml-list">
                            <table class="list-table">
                                <thead>
                                    <tr>
                                        <td class="title">标题</td>
                                        <td class="publishdate">日期</td>
                                    </tr>
                                </thead>
                                <tbody data-bind="foreach: HtmlArray">
                                    <tr>
                                        <td class="title"><a href="" data-bind="attr: { href: Url }" target="_blank"><span data-bind="    text: Title"></span></a></td>
                                        <td class="publishdate"><span data-bind="text: Modified"></span></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="focuswin-clear"></div>
                    </div>
                    <div class="focuswin-doc-list" id="fileContent" style="display: none;">
                         <div style="height: 30px; cursor: pointer;margin-top:10px;margin-bottom:10px;" data-bind="click: ReturnManualPage.bind($data, 'fileContent')">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <ul data-bind='foreach: DocsArray'>
                            <li>
                                <div class="docTitle">
                                    <img data-bind="attr: { src: DocType }" />
                                    <p><a data-bind="text: Title, attr: { title: Title }, attr: { href: Url }" target="_blank"></a></p>
                                </div>
                                <p>创建者：<span data-bind="text: Author"></span></p>
                                <p>创建时间：<span data-bind="text: Created"></span></p>
                            </li>
                        </ul>
                        <div class="focuswin-clear"></div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>

<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jstree.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/HomePage/Manual.js?v=0.3"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/bootstrap.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jquery-ui-1.10.4.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        App.init();
    });
</script>
