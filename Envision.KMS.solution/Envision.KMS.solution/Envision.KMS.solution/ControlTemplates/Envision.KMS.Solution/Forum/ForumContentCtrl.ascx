<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumContentCtrl.ascx.cs" Inherits="Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Forum.ForumContentCtrl" %>
<link href="/_layouts/15/Envision.KMS.Solution/Styles/bootstrap.css" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/Site.css" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/themes/default/style.min.css" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/themes/nav/style.css" rel="stylesheet" />
<style type="text/css">

</style>
<div id="forumcontent">
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
                <div class="focuswin-content" style="padding-top: 25px; padding-left: 40px; padding-right: 40px;">
                    <div id="detailform" style="display: none;">
                        <div style="height: 30px; cursor: pointer;" data-bind="click: detailreturnforum">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <div style="padding: 8px 20px;">
                            <div style="position: relative;">
                                <h2 data-bind="text: detailTitle" style="color: #0a507a"></h2>
                                <div data-bind="visible: IsAdmin()" style="float: right;">
                                    <a href="javascript:void();" data-bind="visible: !CurrentTop(), click: SetTop.bind($data, '1')">置顶</a>
                                    <a href="javascript:void();" data-bind="visible: CurrentTop(), click: SetTop.bind($data, '0')">取消置顶</a>
                                </div>
                                <div data-bind="visible: IsAdmin() || IsAuthor()" style="float: right;">
                                    <a href="javascript:void();" data-bind="click: deleteItem">删除|</a>
                                </div>
                                <div data-bind="visible: IsAuthor()" style="float: right;">
                                    <a href="javascript:void();" data-bind="click: editTransfer">编辑|</a>
                                </div>
                            </div>
                            <div style="margin-top: 30px; margin-bottom: 10px; border: 1px dashed #000;"></div>
                            <div data-bind="html: detailContent" class="wikicontentdetail"></div>
                            <div class="focuswin-diver" style="margin-top: 10px; margin-bottom: 10px;"></div>
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
                                    <div id="Pagination2" class="quotes"></div>
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
                    <div id="editform" style="display: none;">
                        <div style="width: 600px; margin-top: 40px; margin-left: auto; margin-right: auto;">
                            <div class="form-group">
                                <label for="exampleInputEmail2">标题</label>
                                <input type="email" class="form-control" id="exampleInputEmail2" placeholder="Title" data-bind="value: EditFormTitle">
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">内容</label>
                                <textarea class="form-control" rows="6" data-bind="value: EditFormContent"></textarea>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">标签</label>
                                <div data-bind="foreach: EditFormTypeArray">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" data-bind="checked: Ischecked"><span data-bind="    text: Title"></span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <button type="button" class="btn btn-default" data-bind="click: EditForum">提交</button>
                            <button type="button" class="btn btn-default" data-bind="click: editreturnforum">返回</button>
                        </div>
                    </div>
                    <div id="tabform">
                        <div style="height: 120px; position: relative;">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/forum-bg.png" style="width: 100%; height: 100%; position: absolute; top: 0px; left: 0px; z-index: 1;" />
                            <div style="position: absolute; top: 10px; left: 0px; z-index: 2; height: 80px; line-height: 77px; overflow: hidden;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 300px;">
                                            <div class="focuswin-forum-search" style="height: 80px; line-height: 80px; position: relative;">
                                                <div style="margin-left: 25px;">
                                                    <input type="text" style="width: 100%; height: 40px; border: 0px; padding: 0px 30px 0px 10px; font-size: 16px; line-height: 35px;" id="searchInput" />
                                                </div>
                                                <span style="position: absolute; top: 26px; height: 40px; right: -30px; font-size: 25px;" data-bind="click: SearchTree" class="glyphicon glyphicon-search"></span>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="line-height: 30px; float: left; margin-left: 40px;" data-bind="template: { name: 'forumtype-template', foreach: TypeArray }">
                                            </div>
                                            <script type="text/html" id="forumtype-template">
                                                <span class="forum-tag" data-bind="text: Title, click: ActiveTag, css: Ischecked() ? 'forum-tag active' : 'forum-tag'"></span>
                                            </script>
                                        </td>
                                    </tr>
                                </table>
                                <div class="focuswin-clear"></div>
                            </div>
                        </div>
                        <div style="position: relative;">
                            <!-- Nav tabs -->
                            <div id="forumlist">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li role="presentation" class="active" data-bind="click: switchTab.bind($data, 2)"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">最近更新</a></li>
                                    <li role="presentation" data-bind="click: switchTab.bind($data, 1)"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">最热话题</a></li>
                                    <li role="presentation" data-bind="click: switchTab.bind($data, 3)"><a href="#messages" aria-controls="messages" role="tab" data-toggle="tab">我发起的</a></li>
                                    <li role="presentation" data-bind="click: switchTab.bind($data, 4)"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">我回复的</a></li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content" style="padding: 10px 20px 40px 20px; border-left: 1px solid #e1e1e1; border-right: 1px solid #e1e1e1; border-bottom: 1px solid #e1e1e1;">
                                    <div role="tabpanel" class="tab-pane active" id="profile">
                                        <button class="btn-s" type="button" style="width: 110px; height: 40px; position: absolute; top: 0px; color: #fff; right: 0px; background-color: #1493DA" data-bind="click: newforum">发起讨论</button>
                                        <table style="width: 100%;" data-bind="foreach: DataArray">
                                            <tr style="border-bottom: 1px solid #f1f1f1;">
                                                <td style="width: 80px; height: 80px;">
                                                    <div style="margin: 15px 15px 15px 5px; width: 50px; height: 50px; border-radius: 25px; border: 1px solid #e1e1e1; overflow: hidden;">
                                                        <img style="width: 100%;" data-bind="attr: { src: Picurl }" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="margin-right: 10px;">
                                                        <div style="font-size: 14px; color: #646464;">[来自:<span data-bind="text: ForumTypeDisplay"></span>]<span style="color: #2e9fff; margin-left: 5px; font-size: 18px; cursor: pointer;" data-bind="    text: Title, click: click2Detail"></span></div>
                                                        <div style="color: #a7b4ba; font-size: 14px; height: 18px;"><span data-bind="text: Author"></span><span>.</span><span>更新于：</span><span data-bind="    text: Modified"></span></div>
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="text-align: center; font-size: 12px;">
                                                        <div style="color: #feba62;"><span data-bind="text: $root.commentsDisplay"></span>:<span data-bind="    text: CommentsCount"></span></div>
                                                        <div class="focuswin-diver"></div>
                                                        <div>
                                                            <span data-bind="text: $root.viewDisplay"></span>:<span data-bind="    text: ViewCount"></span>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="home">
                                        <table style="width: 100%;" data-bind="foreach: DataArray">
                                            <tr style="border-bottom: 1px solid #f1f1f1;">
                                                <td style="width: 80px; height: 80px;">
                                                    <div style="margin: 15px 15px 15px 5px; width: 50px; height: 50px; border-radius: 25px; border: 1px solid #e1e1e1; overflow: hidden;">
                                                        <img style="width: 100%;" data-bind="attr: { src: Picurl }" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="margin-right: 10px;">
                                                        <div style="font-size: 14px; color: #646464;">
                                                            <div style="float: left; height: 100%; margin-right: 5px;">
                                                                <img data-bind="attr: { src: ImgSource }" />
                                                            </div>
                                                            [来自:<span data-bind="text: ForumTypeDisplay"></span>]
                                                            <span style="color: #2e9fff; font-size: 18px; cursor: pointer; margin-left: 5px;" data-bind="    text: Title, click: click2Detail"></span>
                                                        </div>
                                                        <div style="color: #a7b4ba; font-size: 14px; height: 18px;"><span data-bind="text: Author"></span><span>.</span><span>发布于：</span><span data-bind="    text: Created"></span></div>
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="text-align: center; font-size: 12px;">
                                                        <div style="color: #feba62;"><span data-bind="text: $root.commentsDisplay"></span>:<span data-bind="    text: CommentsCount"></span></div>
                                                        <div class="focuswin-diver"></div>
                                                        <div>
                                                            <span data-bind="text: $root.viewDisplay"></span>:<span data-bind="    text: ViewCount"></span>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="messages">
                                        <table style="width: 100%;" data-bind="foreach: DataArray">
                                            <tr style="border-bottom: 1px solid #f1f1f1;">
                                                <td>
                                                    <div style="margin: 10px 10px 10px 0px;">
                                                        <div style="font-size: 14px; color: #646464;">[来自:<span data-bind="text: ForumTypeDisplay"></span>]<span style="margin-left: 5px; color: #2e9fff; font-size: 18px; cursor: pointer;" data-bind="    text: Title, click: click2Detail.bind($data, 'true')"></span></div>
                                                        <div style="color: #a7b4ba; font-size: 14px; height: 18px;"><span>发布于：</span><span data-bind="    text: Created"></span></div>
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="text-align: center; font-size: 12px;">
                                                        <div style="color: #feba62;"><span data-bind="text: $root.commentsDisplay"></span>:<span data-bind="    text: CommentsCount"></span></div>
                                                        <div class="focuswin-diver"></div>
                                                        <div>
                                                            <span data-bind="text: $root.viewDisplay"></span>:<span data-bind="    text: ViewCount"></span>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <%--<div role="tabpanel" class="tab-pane" id="settings">
                                         <table style="width: 100%;" data-bind="foreach: DataArray">
                                            <tr style="border-bottom: 1px solid #f1f1f1;">
                                                <td>
                                                    <div style="margin-right: 10px;">
                                                        <div style="color: #2e9fff; font-size: 18px; height: 38px; cursor: pointer;"><span data-bind="text: Title, click: click2Detail"></span></div>
                                                        <div style="color: #a7b4ba; font-size: 14px; height: 18px;"><span>最后修改于：</span><span data-bind="    text: Modified"></span></div>
                                                    </div>
                                                </td>
                                                <td style="width: 40px;">
                                                    <div style="text-align: center;">
                                                        <div style="color: #feba62;" data-bind="text: CommentsCount"></div>
                                                        <div class="focuswin-diver"></div>
                                                        <div data-bind="text: ViewCount"></div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>--%>
                                    <div data-bind="visible: DataArray().length < 1 && IsSearch()" style="display: none;">对不起，搜索不到相关的内容</div>
                                    <div id="Pagination" class="quotes"></div>
                                    <div class="focuswin-clear"></div>
                                </div>
                            </div>
                            <div id="newform" style="display: none;">
                                <div style="width: 600px; margin-top: 40px; margin-left: auto; margin-right: auto;">
                                    <form>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">标题</label>
                                            <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Title" data-bind="value: NewFormTitle">
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputPassword1">内容</label>
                                            <textarea class="form-control" rows="6" data-bind="value: NewFormContent"></textarea>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputPassword1">标签</label>
                                            <div data-bind="foreach: NewFormTypeArray">
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" data-bind="checked: Ischecked"><span data-bind="    text: Title"></span>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <button type="button" class="btn btn-default" data-bind="click: InsertForum">提交</button>
                                        <button type="button" class="btn btn-default" data-bind="click: listforum">返回</button>
                                    </form>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </td>
        </tr>
    </table>
</div>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jstree.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/tinymce/tinymce.min.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jquery.pagination.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/bootstrap.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/iscroll.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jquery-ui-1.10.4.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/HomePage/Forum.js?v=0.3"></script>
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
