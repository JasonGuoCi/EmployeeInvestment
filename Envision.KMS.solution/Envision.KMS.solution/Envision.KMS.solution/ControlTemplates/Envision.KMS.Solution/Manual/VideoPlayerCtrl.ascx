<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoPlayerCtrl.ascx.cs" Inherits="Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Manual.VideoPlayerCtrl" %>
<%@ Register TagPrefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<link href="/_layouts/15/Envision.KMS.Solution/Styles/Site.css" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/jplayer/blue.monday/css/jplayer.blue.monday.css" rel="stylesheet" />
<style type="text/css">
    .mediaPlayerContainer, .mediaPlayerContainer object {
        position: inherit;
        float: none;
        min-height: 400px;
    }
</style>
<div id="videoplayer" style="margin: 0px auto; max-width: 1000px; position: relative;">
    <div style="width: 100%;" id="videocontainer">
        <%--<PublishingWebControls:MediaWebPart runat="server" IsPreviewImageSourceOverridenForVideoSet="true" DisplayMode="Overlay" AutoPlay="True" Loop="True" ShowEmbedControl="False" ID="idPageMediaPlayer" __MarkupType="vsattributemarkup" __WebPartId="{471C834D-20D8-4F24-AD85-79FB3822F60D}" WebPart="true" partorder="2"></PublishingWebControls:MediaWebPart>--%>

        <div id="jp_container_1" class="jp-video jp-video-360p" role="application" aria-label="media player" style="display:none;">
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
        <div id="a1" style="width:960px;margin-left:auto;margin-right:auto;"></div>
        <div class="focuswin-clear"></div>

    </div>
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
</div>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/tinymce/tinymce.min.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jquery.pagination.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/bootstrap.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/iscroll.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jplayer/jquery.jplayer.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/ckplayer/ckplayer.js?v=0.1"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/HomePage/Video.js?time=20160715"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        tinymce.init({
            selector: '#Comments',
            menu: {
            },
            content_css: "/_layouts/15/Envision.KMS.Solution/Styles/Site.css",
        });

        var flashvars = {
            f: SP2013.Utility.queryString.extSrc,
            c: 0,
            p: 1,
            h: 0
        };
        var params = { bgcolor: '#FFF', allowFullScreen: true, allowScriptAccess: 'always', wmode: 'transparent' };
        CKobject.embedSWF('/_layouts/15/Envision.KMS.Solution/Scripts/ckplayer/ckplayer.swf', 'a1', 'ckplayer_a1', '960', '540', flashvars, params);

    });
</script>
