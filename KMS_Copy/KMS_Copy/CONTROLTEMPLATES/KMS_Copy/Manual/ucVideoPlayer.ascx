<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucVideoPlayer.ascx.cs" Inherits="KMS_Copy.CONTROLTEMPLATES.KMS_Copy.Manual.ucVideoPalyer" %>
<%@ Register TagPrefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/KMS_Copy/Manual/ucUpladDoc.ascx" TagName="UserControlB" TagPrefix="uc1" %>
<link href="/_layouts/15/KMS_Copy/Styles/Site.css" rel="stylesheet" />
<link href="/_layouts/15/KMS_Copy/Styles/jplayer/blue.monday/css/jplayer.blue.monday.css" rel="stylesheet" />
<link href="/_layouts/15/KMS_Copy/Styles/uploadify/uploadify.css" rel="stylesheet" />
<style type="text/css">
    .mediaPlayerContainer, .mediaPlayerContainer object {
        position: inherit;
        float: none;
        min-height: 400px;
    }

    th, td {
        margin: 0;
        border: 1px solid #333;
        padding: 3px 5px;
        min-width: 50px;
        padding: 0;
    }

    .mask {
        position: absolute;
        top: 0px;
        filter: alpha(opacity=60);
        background-color: #777;
        z-index: 1002;
        left: 0px;
        opacity: 0.5;
        -moz-opacity: 0.5;
    }
</style>

<div id="videoplayer" style="margin: 0px auto; max-width: 1000px; position: relative;">
    <div style="width: 100%; margin-top: 15px" id="videocontainer">
        <div id="jp_container_1" class="jp-video jp-video-360p" role="application" aria-label="media player" style="display: none; width: 100%;">
            <div class="jp-type-single">
                <div id="jquery_jplayer_1" class="jp-jplayer"></div>
                <div class="jp-gui">
                    <div class="jp-video-play">
                        <button class="jp-video-play-icon" role="button" tabindex="0">play</button>
                    </div>
                    <div class="jp-interface">
                        <div class="jp-progress">
                            <div class="jp-seek-bar">
                                <div class="jp-play-bar"></div>
                            </div>
                        </div>
                        <div class="jp-current-time" role="timer" aria-label="time">&nbsp;</div>
                        <div class="jp-duration" role="timer" aria-label="duration">&nbsp;</div>
                        <div class="jp-controls-holder">
                            <div class="jp-controls">
                                <button class="jp-play" role="button" tabindex="0">play</button>
                                <button class="jp-stop" role="button" tabindex="0">stop</button>
                            </div>
                            <div class="jp-volume-controls">
                                <button class="jp-mute" role="button" tabindex="0">mute</button>
                                <button class="jp-volume-max" role="button" tabindex="0">max volume</button>
                                <div class="jp-volume-bar">
                                    <div class="jp-volume-bar-value"></div>
                                </div>
                            </div>
                            <div class="jp-toggles">
                                <button class="jp-repeat" role="button" tabindex="0">repeat</button>
                                <button class="jp-full-screen" role="button" tabindex="0">full screen</button>
                            </div>
                        </div>
                        <div class="jp-details">
                            <div class="jp-title" aria-label="title">&nbsp;</div>
                        </div>

                    </div>
                </div>
                <div class="jp-no-solution">
                    <span>Update Required</span>
                    To play the media you will need to either update your browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/" target="_blank">Flash plugin</a>.
	
                </div>
            </div>
        </div>
        <div id="a1" style="width: 960px; margin-left: auto; margin-right: auto;"></div>
        <div class="focuswin-clear"></div>
    </div>
    <div id="mp3div" style="padding-top: 10px; display: none">

        <div id="player_2322" style="display: inline-block;">
            <a href="http://get.adobe.com/flashplayer/">You need to install the Flash plugin</a> - <a href="http://www.webestools.com/">http://www.webestools.com/</a>
        </div>
    </div>
    <div class="focuswin-diver" style="margin-top: 10px;"></div>
    <div class="focuswin-comment" id="iddisplaycomment">
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
                            <span class="glyphicon glyphicon-remove" style="float: right; cursor: pointer; margin-left: 10px;" data-bind="visible: $root.IsAdmin(), click: RemoveComment"></span>
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
    <uc1:UserControlB ID="UserControlB1" runat="server" />
</div>
<div id="mask" class="mask">
    <p style="position: absolute; top: 50%; left: 40%; margin-left: 50px; margin-top: 40px; color: azure; font-size: 16px">加载需要几秒钟的时间，请您耐心等待...</p>
</div>
<script src="/_layouts/15/KMS_Copy/Scripts/tinymce/tinymce.min.js?v=0.2"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/jquery.pagination.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/bootstrap.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/iscroll.js"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/ckplayer/ckplayer.js?v=0.1"></script>
<script src="/_layouts/15/KMS_Copy/Scripts/HomePage/Video.js?time=20160715"></script>
<%--<script type="text/javascript" src="http://www.webestools.com/page/js/flashobject.js"></script>--%>
<script src="/_layouts/15/KMS_Copy/Scripts/uploadify/jquery.uploadify.min.js"></script>

<script type="text/javascript">
    var Common = "Comments";
    jQuery(document).ready(function () {
        if (SP2013.Utility.queryString.uniqueId == undefined) {
            $("#isdiplaycomment").hide();
        }

        if (SP2013.Utility.queryString.type != "mp3") {
            var flashvars = {
                f: SP2013.Utility.queryString.extSrc,
                c: 0,
                p: 1,
                h: 0
            };
            var params = { bgcolor: '#FFF', allowFullScreen: true, allowScriptAccess: 'always', wmode: 'transparent' };
            CKobject.embedSWF('/_layouts/15/KMS_Copy/Scripts/ckplayer/ckplayer.swf', 'a1', 'ckplayer_al', '960', '540', flashvars, params);
        }
        else {
            $("#mp3div").show();

            var flashvars_2322 = {};
            var params_2322 = {
                quality: "high",
                wmode: "transparent",
                bgcolor: "#ffffff",
                allowScriptAccess: "always",
                allowFullScreen: "true",
                flashvars: "url=" + SP2013.Utility.queryString.extSrc + "&autostart=no"
            };
            var attributes_2322 = {};
            flashObject("/_layouts/15/KMS_Copy/Scripts/ckplayer/s_7.swf", "player_2322", "600", "35", "8", false, flashvars_2322, params_2322, attributes_2322);

        }

        if (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.match(/8./i) == "8.") {
            tinymce.init({
                selector: '#Comments',
                language: SP2013.O365.currentLang() == '1033' ? 'uk' : 'zh_CN',
                menu: {},
                content_css: "/_layouts/15/KMS_Copy/Styles/Site.css",
                plugins: [
                    "advlist autolink lists print preview hr image",
                    "searchreplace fullscreen insertdatetime",
                    "table contextmenu directionality emoticons template textcolor paste fullpage textcolor colorpicker textpattern"
                ],
                toolbar: "insertfile undo redo | styleselect fontselect fontsizeselect | mybutton | image | bold italic underline  strikethrough superscript subscript code removeformat | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent  | forecolor backcolor | table",
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
                menu: {},
                content_css: "/_layouts/15/KMS_Copy/Styles/Site.css",
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
                        image: "/_layouts/15/KMS_Copy/Scripts/tinymce/skins/lightgray/img/Uploadicon.png",
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
    });

    function flashObject(url, id, width, height, version, bg, flashvars, params, att) {
        var pr = '';
        var attpr = '';
        var fv = '';
        var nofv = 0;
        for (i in params) {
            pr += '<param name="' + i + '" value="' + params[i] + '" />';
            attrpr += i + '="' + params[i] + '" ';
            if (i.match(/flashvars/ig)) {
                nofv = 1;
            }
        }
        if (nofv == 0) {
            fv = '<param name="flashvars" value="';
            for (i in flashvars) {
                fv += i + '=' + escape(flashvars[i]) + '&';
            }
            fv += '" />';
        }
        htmlcode = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0" width="' + width + '" height="' + height + '">'
    + '	<param name="movie" value="' + url + '" />' + pr + fv
    + '	<embed src="' + url + '" width="' + width + '" height="' + height + '" ' + attpr + 'type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer"></embed>'
    + '</object>';

        document.getElementById(id).innerHTML = htmlcode;
        setTimeout(function () {
            $("#mask").hide();
        }, 5000);
    }
</script>
