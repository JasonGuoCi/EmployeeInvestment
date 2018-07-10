<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WikipediaCtrl.ascx.cs" Inherits="Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Wikipedia.WikipediaCtrl" %>
<link href="/_layouts/15/Envision.KMS.Solution/Styles/Site.css" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/themes/default/style.min.css" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/themes/nav/style.css" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/jquery-ui-1.10.4.css" rel="stylesheet" />
<div id="wikipedia">
    <table style="width: 100%;">
        <tr>
            <td class="focuswin-leftnav-td">
                <div class="focuswin-leftnav" data-bind="css: IsEnglishTitle() ? 'focuswin-leftnav' : 'focuswin-leftnav chs'">
                    <div class="leftnav-header" data-bind="text: NavTitle, click: Navigate2Root"></div>
                    <div class="focuswin-diver" style="margin: 10px 20px;"></div>
                    <div id="left-nav"></div>
                </div>
            </td>
            <td>
                <div class="focuswin-content ui-widget-content" id="focuswin-content">
                    <div class="focuswin-wikinav ui-state-active" id="focuswin-wikinav">
                        <div class="focuswin-wikinav-headercontainer">
                            <div class="focuswin-wikinav-header" data-bind="css: IsEnglishTitle() ? 'focuswin-wikinav-header' : 'focuswin-wikinav-header-lg'">
                                <span data-bind="text: TreeTitle"></span>
                            </div>
                            <div class="focuswin-diver" style="margin: 10px 0px;"></div>
                            <div class="focuswin-wikinav-search">
                                <div class="focuswin-wikinav-searchcontainer">
                                    <div style="padding-right: 45px;">
                                        <input type="text" class="focuswin-wikinav-keywords" id="searchInput" />
                                    </div>
                                    <span class="focuswin-wikinav-searchicon glyphicon glyphicon-search" data-bind="click: SearchTree"></span>
                                </div>
                                <div style="height: 30px; cursor: pointer; margin-top: 10px; margin-bottom: 10px;" data-bind="click: ReturnTree, visible: IsSearch">
                                    <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                                </div>
                            </div>
                            <div id="wikitree" data-bind="visible: !IsSearch()"></div>
                            <div id="left-searchResult" data-bind="visible: IsSearch()">
                            </div>
                        </div>
                    </div>
                    <div id="wikicontent" style="position: relative; overflow-x: hidden;overflow-y:auto;">
                        <div class="wikicontent-container" data-bind="visible: wikiTitle">
                            <div style="padding: 8px 20px;">
                                <div style="position: relative;">
                                    <h2 data-bind="text: wikiTitle" style="color: #0a507a; font-weight: bold;"></h2>
                                    <span class="wikicontent-copyicon" data-bind="visible: WikiUrl() && IsAdmin()"><a href="#" data-toggle="modal" data-target=".bs-example-modal-lg">[Copy]</a></span>
                                </div>
                                <div style="margin-top: 30px; margin-bottom: 10px; border: 1px dashed #000;"></div>
                                <div data-bind="html: wikiContent" class="wikicontentdetail"></div>
                                <div class="focuswin-diver" style="margin-top: 10px;"></div>
                                <div class="focuswin-comment">
                                    <div class="pub_title">
                                        <span data-bind="text: CommentsTitle"></span>
                                    </div>

                                    <div class="focuswin-comment-content">
                                        <ul data-bind='foreach: CommentsArray'>
                                            <li>
                                                <div style="margin: 5px 5px 20px 5px;">
                                                    <div class="created">
                                                        <span data-bind="text: Create"></span>
                                                        <span style="margin-left: 15px;" data-bind="text: DisplayName"></span>
                                                        <span class="glyphicon glyphicon-remove wikicontent-removeicon" data-bind="visible: $root.IsAdmin(), click: RemoveComment"></span>
                                                        <span style="float: right; cursor: pointer;" data-bind="click: Payback, text: $root.CommentsPayBackDisplay"></span>
                                                    </div>
                                                    <div data-bind="html: Comment" class="comment-content">
                                                    </div>
                                                </div>
                                                <div class="focuswin-diver"></div>
                                            </li>
                                        </ul>
                                        <div id="Pagination" class="quotes"></div>
                                        <div class="focuswin-clear"></div>
                                    </div>
                                    <div class="submit">
                                        <textarea id="Comments"></textarea>
                                        <div style="text-align: right;">
                                            <button type="button" class="btn btn-info" data-bind="text: CommentsBtn, click: submitcomments"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Copy Link</h4>
                </div>
                <div class="modal-body">
                    <input type="text" data-bind="value: WikiUrl" style="width: 100%;" />
                </div>
            </div>
        </div>
    </div>
</div>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jstree.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/tinymce/tinymce.min.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jquery.pagination.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/bootstrap.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jquery.nicescroll.min.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jquery-ui-1.10.4.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/HomePage/Wikipedia.js?v=0.2"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        App.init();
        tinymce.init({
            selector: '#Comments',
            menu: {
            },
            content_css: "/_layouts/15/Envision.KMS.Solution/Styles/Site.css",
        });
    });
</script>
