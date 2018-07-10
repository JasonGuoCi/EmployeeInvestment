<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/KMS_Copy/Manual/ucUpladDoc.ascx" TagName="UserControlB" TagPrefix="uc1" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucWikipedia.ascx.cs" Inherits="KMS_Copy.CONTROLTEMPLATES.KMS_Copy.Wikipedia.ucWikipedia" %>
<link href="/_layouts/15/KMS_Copy/Styles/Site.css" rel="stylesheet" />
<link href="/_layouts/15/KMS_Copy/Styles/themes/default/style.min.css" rel="stylesheet" />
<link href="/_layouts/15/KMS_Copy/Styles/themes/nav/style.css" rel="stylesheet" />
<link href="/_layouts/15/KMS_Copy/Styles/jquery-ui-1.10.4.css" rel="stylesheet" />
<link href="/_layouts/15/KMS_Copy/Styles/uploadify/uploadify.css" rel="stylesheet" />

<style type="text/css">
    .comment-content td, th {
        margin: 0;
        border: 1px solid #333;
        padding: 3px 5px;
        min-width: 50px;
        padding: 0;
    }
</style>

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
                                    <img src="/_layouts/15/KMS_Copy/Images/left_round.png" style="height: 100%;" />
                                </div>
                            </div>
                            <div id="wikitree" data-bind="visible: !IsSearch()"></div>
                            <div id="left-searchResult" data-bind="visible: IsSearch()"></div>
                        </div>
                    </div>
                    <div id="wikicontent" style="position: relative; overflow-x: hidden; overflow-y: auto;">
                        <div class="wikicontent-container" data-bind="visible: wikiTitle">
                            <div style="padding: 8px 20px;">
                                <div style="position: relative;">
                                    <h2 data-bind="text: wikiTitle" style="color: #0a507a; font-weight: bold;"></h2>
                                    <span style="font-size: 16px;" class="wikicontent-copyicon" data-bind="visible: WikiUrl()"><a href="#" data-toggle="modal" data-target=".bs-example-modal-lg">
                                        <span data-bind="text: SP2013.O365.currentLang() == '1033' ? '[Copy Link]' : '[复制链接]'">[Copy]</span>
                                    </a></span>
                                </div>
                                <div style="margin-top: 30px; margin-bottom: 10px; border: 1px dashed #000;"></div>
                                <div data-bind="html: wikiContent" class="wikicontentdetail"></div>
                                <div class="focuswin-diver" style="margin-top: 10px;"></div>
                                <div class="focuswin-comment">
                                    <div class="pub_title">
                                        <span data-bind="text: CommentsTitle"></span>
                                    </div>

                                    <div class="focuswin-comment-content">
                                        <ul data-bind="foreach: CommentsArray">
                                            <li>
                                                <div style="margin: 5px 5px 20px 5px;">
                                                    <div class="created">
                                                        <span data-bind="text: Create"></span>
                                                        <span style="margin-left: 15px;" data-bind="text: DisplayName"></span>
                                                        <span class="glyphicon glyphicon-remove wikicontent-removeicon" data-bind="visible: $root.IsAdmin(), click: RemoveComment"></span>
                                                        <span style="float: right; cursor: pointer;" data-bind="click: Payback, text: $root.CommentsPayBackDisplay"></span>
                                                    </div>
                                                    <div data-bind="html: Comment" class="comment-content"></div>
                                                </div>
                                                <div class="focuswin-diver"></div>
                                            </li>
                                        </ul>
                                        <div class="quotes" id="Pagination"></div>
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
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;
                        </span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel">Copy Link</h4>
                </div>
                <div class="modal-body">
                    <input id="copylinkDiv" type="text" data-bind="value: WikiUrl" style="width: 100%;" />
                </div>
            </div>
        </div>
    </div>
    <uc1:UserControlB ID="UserControlB1" runat="server"></uc1:UserControlB>
</div>
<script src="/_layouts/15/KMS_Copy/Scripts/jstree.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/tinymce/tinymce.min.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/jquery.pagination.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/bootstrap.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/jquery.nicescroll.min.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/jquery-ui-1.10.4.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/HomePage/Wikipedia.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/uploadify/jquery.uploadify.min.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        App.init();
        if (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.match(/8./i) == "8.") {
            tinymce.init({
                selector: '#Comments',
                language: SP2013.O365.currentLang() == '1033' ? 'uk' : 'zh_CN',
                menu: {

                },
                content_ss: "_layouts/15/KMS_Copy/Styles/Site.css",
                plugins: [
                "advlist autolink lists  print preview hr image",
                "searchreplace fullscreen insertdatetime ",
                "table contextmenu directionality emoticons template textcolor paste fullpage textcolor colorpicker textpattern"],
                toolbar: "insertfile undo redo | styleselect fontselect fontsizeselect | mybutton | image | bold italic underline strikethrough superscript subscript code removeformat | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent  | forecolor backcolor | table",
                setup: function (editor) {
                    editor.addButton('mybutton', {
                        text: SP2013.O365.currentLang() == '1033' ? 'Upload Attachment' : '上传附件',
                        icon: false,
                        onclick: function () {
                            $("#Upload").modal('show');
                            $("#Upload-block").show();
                        }
                    });
                },
                height: 200,
                font_formats: "微软雅黑=微软雅黑;宋体=宋体;黑体=黑体;华文楷体=华文楷体;隶书=隶书;幼圆=幼圆;Arial=arial,helvetica,sans-serif;Comic Sans MS=comic sans ms,sans-serif;Courier New=courier new,courier;Tahoma=tahoma,arial,helvetica,sans-serif;Times New Roman=times new roman,times;Verdana=verdana,geneva;Webdings=webdings;Wingdings=wingdings,zapf dingbats",
                fontsize_formats: "8pt 10pt 12pt 14pt 18pt 24pt 36pt",
                convert_fonts_to_spans: true

            });
        }
        else {
            tinymce.init({
                selector: '#Comments',
                language: SP2013.O365.currentLang() == '1033' ? 'uk' : 'zh_CN',
                menu: {
                },
                content_css: "_layouts/15/KMS_Copy/Styles/Site.css",
                plugins: [
                  "advlist autolink lists  print preview hr image",
                  "searchreplace fullscreen insertdatetime ",
                   "table contextmenu directionality emoticons template textcolor paste fullpage textcolor colorpicker textpattern"
                ],
                toolbar: "insertfile undo redo | styleselect fontselect fontsizeselect | mybutton | image | bold italic underline strikethrough superscript subscript code removeformat | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent  | forecolor backcolor | table",
                //toolbar: "insertfile undo redo | styleselect fontselect fontsizeselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent  | forecolor backcolor | table ",
                setup: function (editor) {
                    editor.addButton('mybutton', {
                        title: SP2013.O365.currentLang() == '1033' ? 'Upload Attachment' : "上传附件",
                        image: "_layouts/15/KMS_Copy/Scripts/tinymce/skins/lightgray/img/Uploadicon.png",
                        onclick: function () {
                            $("#Upload").modal('show');
                            $("#Upload-block").show();
                        }
                    });
                },
                height: 200,
                font_formats: "微软雅黑=微软雅黑;宋体=宋体;黑体=黑体;华文楷体=华文楷体;隶书=隶书;幼圆=幼圆;Arial=arial,helvetica,sans-serif;Comic Sans MS=comic sans ms,sans-serif;Courier New=courier new,courier;Tahoma=tahoma,arial,helvetica,sans-serif;Times New Roman=times new roman,times;Verdana=verdana,geneva;Webdings=webdings;Wingdings=wingdings,zapf dingbats",
                fontsize_formats: "8pt 10pt 12pt 14pt 18pt 24pt 36pt",
                convert_fonts_to_spans: true
            });
        }
        $("#copylinkDiv").click(function () {
            this.select();
        });

    });
</script>
