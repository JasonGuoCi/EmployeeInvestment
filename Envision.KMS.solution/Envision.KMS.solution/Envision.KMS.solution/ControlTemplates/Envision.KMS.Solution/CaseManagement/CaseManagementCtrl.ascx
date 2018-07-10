<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaseManagementCtrl.ascx.cs" Inherits="Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.CaseManagement.CaseManagementCtrl" %>
<link href="/_layouts/15/Envision.KMS.Solution/Styles/Site.css?v=0.1" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/themes/default/style.min.css" rel="stylesheet" />
<link href="/_layouts/15/Envision.KMS.Solution/Styles/themes/nav/style.css" rel="stylesheet" />
<div id="casecontent">
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
                <div class="focuswin-content" style="padding-left: 40px; padding-right: 40px; margin-top: 20px;">
                    <div id="toppage">
                        <ul class="nav nav-tabs" role="tablist" style="font-weight: bold;">
                            <li role="presentation" class="active" data-bind="click: ChangeCategory.bind($data, '3')"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab" data-bind="    text: QueryText"></a></li>
                            <li role="presentation" data-bind="click: ChangeCategory.bind($data, '1')"><a href="#home" aria-controls="home" role="tab" data-toggle="tab"><span data-bind="    text: MyApplicationText"></span>(<span data-bind="    text: ApplyingNum"></span>)</a></li>
                            <li role="presentation" data-bind="click: ChangeCategory.bind($data, '2')"><a href="#messages" aria-controls="messages" role="tab" data-toggle="tab"><span data-bind="    text: MyTaskText"></span>(<span data-bind="    text: PendingNum"></span>)</a></li>
                            <li role="presentation" data-bind="click: ChangeCategory.bind($data, '4'), visible: IsAdmin" style="display: none;"><a href="#allapplication" aria-controls="allapplication" role="tab" data-toggle="tab"><span data-bind="    text: AllApplicationText"></span></a></li>
                        </ul>
                        <table style="display: none;">
                            <thead style="text-align: center;">
                                <tr style="background-color: #e6e6e6; height: 40px; line-height: 40px;">
                                    <td data-bind="text: MyApplicationText, css: Category() == '1' ? 'category border active' : 'category border', click: ChangeCategory.bind($data, '1')"></td>
                                    <td data-bind="text: MyTaskText, css: Category() == '2' ? 'category border active' : 'category border', click: ChangeCategory.bind($data, '2')"></td>
                                    <td data-bind="text: QueryText, css: Category() == '3' ? 'category active' : 'category', click: ChangeCategory.bind($data, '3')"></td>
                                </tr>
                            </thead>
                        </table>
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane" id="home">
                                <div style="margin: 15px 10px;">
                                    <table>
                                        <tr>
                                            <td style="width: 60px; font-size: 16px; text-align: center;" data-bind="text: StatusText"></td>
                                            <td>
                                                <div style="margin: 0px 20px 0px 10px;">
                                                    <div data-bind="text: ApplyStatusText, css: ApplyStatus() == '申请中' ? 'caseStatus active' : 'caseStatus', click: ChangeStatus.bind($data, '申请中')">
                                                    </div>
                                                    <div data-bind="text: DoneStatusText, css: ApplyStatus() == '已完成' ? 'caseStatus active' : 'caseStatus', click: ChangeStatus.bind($data, '已完成')">
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="margin: 0px 10px;">
                                                    <select style="height: 35px; line-height: 35px;" class="form-control" data-bind="options: TypeAttr, optionsText: 'Title', optionsValue: 'Id', value: SearchType "></select>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="margin: 0px 10px;">
                                                    <input type="email" class="form-control searchInput" placeholder="" id="searchInput1" style="width: 180px;">
                                                </div>
                                            </td>
                                            <td>
                                                <button class="btn-s" type="button" style="width: 80px; height: 40px; line-height: 30px; color: #fff; background-color: #1493DA" data-bind="click: Search, text: SearchText"></button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="position: relative; padding-top: 1px;">
                                    <button class="btn-s" type="button" style="width: 110px; height: 40px; position: absolute; top: 0px; color: #fff; right: 10px; background-color: #1493DA" data-bind="click: newforum, text: NewBtnText"></button>
                                    <div style="border: 1px solid #e6e6e6; padding: 10px;margin-top: 50px;">
                                        <table style="width: 100%;">
                                            <thead>
                                                <tr style="height: 40px; line-height: 40px; font-size: 14px; border-bottom: 2px solid #f1f1f1;" data-bind="visible: IsEnglishTitle">
                                                    <td>Title</td>
                                                    <td style="width: 120px;">Type</td>
                                                    <td style="width: 150px;">Created</td>
                                                    <td style="width: 100px;" data-bind="visible: ApplyStatus() == '申请中'">Operation</td>
                                                </tr>
                                                <tr style="height: 40px; line-height: 40px; font-size: 14px; border-bottom: 2px solid #f1f1f1;" data-bind="visible: !IsEnglishTitle()">
                                                    <td>标题</td>
                                                    <td style="width: 120px;">类型</td>
                                                    <td style="width: 150px;">申请时间</td>
                                                    <td style="width: 100px;" data-bind="visible: ApplyStatus() == '申请中'">操作</td>
                                                </tr>
                                            </thead>
                                            <tbody data-bind="template: { name: 'applylist-template', foreach: DataAttr }">
                                            </tbody>
                                        </table>
                                        <script type="text/html" id="applylist-template">
                                            <tr style="border-bottom: 1px solid #f1f1f1; border-top: 1px solid #e6e6e6; height: 45px; line-height: 45px;">
                                                <td>
                                                    <span style="color: #2e9fff; font-size: 16px; cursor: pointer; margin: 0px 5px;" data-bind="text: Title, click: click2Detail"></span>
                                                </td>
                                                <td>
                                                    <span data-bind="text: CaseTypeDisplay"></span>
                                                </td>
                                                <td>
                                                    <span data-bind="text: Created"></span>
                                                </td>
                                                <td data-bind="visible: $root.ApplyStatus() == '申请中'">
                                                    <a href="javascript:void();" data-bind="click: click2Modify">编辑</a>|
                                                <a href="javascript:void();" data-bind="visible: IsSendEmail, click: click2SendMail">催办|</a>
                                                    <a href="javascript:void();" data-bind="click: click2Delete">删除</a>
                                                </td>
                                            </tr>
                                        </script>
                                        <div data-bind="visible: DataAttr().length < 1 && IsSearch()" style="display: none;">对不起，搜索不到相关的内容</div>
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="messages">
                                <div style="margin: 15px 10px;">
                                    <table>
                                        <tr>
                                            <td style="width: 60px; font-size: 16px; text-align: center;" data-bind="text: StatusText"></td>
                                            <td>
                                                <div style="margin: 0px 20px 0px 10px;">
                                                    <div data-bind="text: PendingStatusText, css: TaskStatus() == 'Pending' ? 'caseStatus active' : 'caseStatus', click: ChangeTaskStatus.bind($data, 'Pending')">
                                                    </div>
                                                    <div data-bind="text: FinishStatusText, css: TaskStatus() == 'Finish' ? 'caseStatus active' : 'caseStatus', click: ChangeTaskStatus.bind($data, 'Finish')">
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="margin: 0px 10px;">
                                                    <select style="height: 35px; line-height: 35px;" class="form-control" data-bind="options: TypeAttr, optionsText: 'Title', optionsValue: 'Id', value: SearchType "></select>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="margin: 0px 10px;">
                                                    <input type="email" class="form-control searchInput" placeholder="" id="searchInput2" style="width: 180px;">
                                                </div>
                                            </td>
                                            <td>
                                                <button class="btn-s" type="button" style="width: 80px; height: 40px; line-height: 30px; color: #fff; background-color: #1493DA" data-bind="click: Search, text: SearchText"></button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="position: relative; padding-top: 1px;">
                                    <div style="border: 1px solid #e6e6e6; padding: 10px;">
                                        <table style="width: 100%;">
                                            <thead>
                                                <tr style="height: 40px; line-height: 40px; font-size: 14px; border-bottom: 2px solid #f1f1f1;" data-bind="visible: IsEnglishTitle">
                                                    <td>Title</td>
                                                    <td style="width: 120px;">Type</td>
                                                    <td style="width: 150px;">Created</td>
                                                    <td style="width: 200px;">Proposer</td>
                                                </tr>
                                                <tr style="height: 40px; line-height: 40px; font-size: 14px; border-bottom: 2px solid #f1f1f1;" data-bind="visible: !IsEnglishTitle()">
                                                    <td>标题</td>
                                                    <td style="width: 120px;">类型</td>
                                                    <td style="width: 150px;">申请时间</td>
                                                    <td style="width: 200px;">申请人</td>
                                                </tr>
                                            </thead>
                                            <tbody data-bind="foreach: TaskAttr">
                                                <tr style="border-bottom: 1px solid #f1f1f1; border-top: 1px solid #e6e6e6; height: 45px; line-height: 45px;">
                                                    <td>
                                                        <span style="color: #2e9fff; font-size: 16px; cursor: pointer; margin: 0px 5px;" data-bind="text: Title, click: click2Detail"></span>
                                                    </td>
                                                    <td>
                                                        <span data-bind="text: CaseType"></span>
                                                    </td>
                                                    <td>
                                                        <span data-bind="text: Created"></span>
                                                    </td>
                                                    <td>
                                                        <span data-bind="text: CreatedBy"></span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <div data-bind="visible: TaskAttr().length < 1 && IsSearch()" style="display: none;">对不起，搜索不到相关的内容</div>
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane active" id="profile">
                                <div style="margin: 15px 10px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="margin: 0px 10px;">
                                                    <select style="height: 35px; line-height: 35px;" class="form-control" data-bind="options: TypeAttr, optionsText: 'Title', optionsValue: 'Id', value: SearchType "></select>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="margin: 0px 10px;">
                                                    <input type="email" class="form-control searchInput" placeholder="" id="searchInput3" style="width: 180px;">
                                                </div>
                                            </td>
                                            <td>
                                                <button class="btn-s" type="button" style="width: 80px; height: 40px; line-height: 30px; color: #fff; background-color: #1493DA" data-bind="click: Search, text: SearchText"></button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="position: relative; padding-top: 1px;">
                                    <div style="border: 1px solid #e6e6e6; padding: 10px; ">
                                        <table style="width: 100%;">
                                            <thead>
                                                <tr style="height: 40px; line-height: 40px; font-size: 14px; border-bottom: 2px solid #f1f1f1;" data-bind="visible: IsEnglishTitle">
                                                    <td>Title</td>
                                                    <td style="width: 120px;">Type</td>
                                                    <td style="width: 150px;">Created</td>
                                                </tr>
                                                <tr style="height: 40px; line-height: 40px; font-size: 14px; border-bottom: 2px solid #f1f1f1;" data-bind="visible: !IsEnglishTitle()">
                                                    <td>标题</td>
                                                    <td style="width: 120px;">类型</td>
                                                    <td style="width: 150px;">创建时间</td>
                                                </tr>
                                            </thead>
                                            <tbody data-bind="foreach: DataAttr">
                                                <tr style="border-bottom: 1px solid #f1f1f1; border-top: 1px solid #e6e6e6; height: 45px; line-height: 45px;">
                                                    <td>
                                                        <div style="float: left; height: 100%; margin-right: 5px;">
                                                            <img data-bind="attr: { src: ImgSource }" style="margin-top: 13px;" />
                                                        </div>
                                                        <span style="color: #2e9fff; font-size: 16px; cursor: pointer; margin: 0px 5px;" data-bind="text: Title, click: click2View"></span>
                                                    </td>
                                                    <td>
                                                        <span data-bind="text: CaseTypeDisplay"></span>
                                                    </td>
                                                    <td>
                                                        <span data-bind="text: Created"></span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <div data-bind="visible: DataAttr().length < 1 && IsSearch()" style="display: none;">对不起，搜索不到相关的内容</div>
                                    </div>
                                </div>
                            </div>
                            <div role="tabpanel" class="tab-pane" id="allapplication">
                                <div style="margin: 15px 10px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="margin: 0px 10px;">
                                                    <select style="height: 35px; line-height: 35px;" class="form-control" data-bind="options: TypeAttr, optionsText: 'Title', optionsValue: 'Id', value: SearchType "></select>
                                                </div>
                                            </td>
                                            <td>
                                                <div style="margin: 0px 10px;">
                                                    <input type="email" class="form-control searchInput" placeholder="" id="searchInput4" style="width: 180px;">
                                                </div>
                                            </td>
                                            <td>
                                                <button class="btn-s" type="button" style="width: 80px; height: 40px; line-height: 30px; color: #fff; background-color: #1493DA" data-bind="click: Search, text: SearchText"></button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="position: relative; padding-top: 1px;">
                                    <div style="border: 1px solid #e6e6e6; padding: 10px; margin-top: 50px;">
                                        <table style="width: 100%;">
                                            <thead>
                                                <tr style="height: 40px; line-height: 40px; font-size: 14px; border-bottom: 2px solid #f1f1f1;" data-bind="visible: IsEnglishTitle">
                                                    <td>Title</td>
                                                    <td style="width: 120px;">Type</td>
                                                    <td style="width: 150px;">Created</td>
                                                    <td style="width: 100px;">Operation</td>
                                                </tr>
                                                <tr style="height: 40px; line-height: 40px; font-size: 14px; border-bottom: 2px solid #f1f1f1;" data-bind="visible: !IsEnglishTitle()">
                                                    <td>标题</td>
                                                    <td style="width: 120px;">类型</td>
                                                    <td style="width: 150px;">创建时间</td>
                                                    <td style="width: 100px;">操作</td>
                                                </tr>
                                            </thead>
                                            <tbody data-bind="foreach: DataAttr">
                                                <tr style="border-bottom: 1px solid #f1f1f1; border-top: 1px solid #e6e6e6; height: 45px; line-height: 45px;">
                                                    <td>
                                                        <span style="color: #2e9fff; font-size: 16px; cursor: pointer; margin: 0px 5px;" data-bind="text: Title, click: click2Admin"></span>
                                                    </td>
                                                    <td>
                                                        <span data-bind="text: CaseTypeDisplay"></span>
                                                    </td>
                                                    <td>
                                                        <span data-bind="text: Created"></span>
                                                    </td>
                                                    <td data-bind="visible: $root.ApplyStatus() == '申请中'">
                                                        <a href="javascript:void();" data-bind="click: click2Delete">删除</a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <div data-bind="visible: DataAttr().length < 1 && IsSearch()" style="display: none;">对不起，搜索不到相关的内容</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="Pagination" class="quotes"></div>
                        <div class="focuswin-clear"></div>
                    </div>

                    <div id="newform" style="display: none;">
                        <div style="width: 600px; margin-left: auto; margin-right: auto;">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">标题</label>
                                    <input type="email" class="form-control" id="exampleInputEmail1" data-bind="value: NewTitle">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">内容</label>
                                    <textarea class="form-control" rows="6" data-bind="value: NewContent"></textarea>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">类型</label>
                                    <select data-bind="options: TypeNewAttr, optionsText: 'Title', value: NewType " class="form-control" style="height: 40px;"></select>
                                </div>

                                <button type="button" class="btn btn-default" data-bind="click: InsertCase">提交</button>
                                <button type="button" class="btn btn-default" data-bind="click: newforumreturn">返回</button>
                            </form>
                        </div>
                    </div>

                    <div id="editform" style="display: none;">
                        <div style="width: 600px; margin-left: auto; margin-right: auto;">
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1">标题</label>
                                    <input type="email" class="form-control" data-bind="value: EditTitle">
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">内容</label>
                                    <textarea class="form-control" rows="6" data-bind="value: EditContent"></textarea>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">类型</label>
                                    <input type="email" class="form-control" data-bind="value: EditType" readonly="readonly">
                                </div>

                                <button type="button" class="btn btn-default" data-bind="click: ModifiyCase">提交</button>
                                <button type="button" class="btn btn-default" data-bind="click: editforumreturn">返回</button>
                            </form>
                        </div>
                    </div>

                    <div id="detailform" style="display: none;">
                        <div style="height: 30px; cursor: pointer;" data-bind="click: detailreturnforum">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <div>
                            <div style="position: relative;">
                                <h2 data-bind="text: detailTitle" style="color: #0a507a"></h2>
                            </div>
                            <div style="margin-top: 30px; margin-bottom: 10px; border: 1px dashed #000;"></div>
                            <div data-bind="html: detailContent" class="wikicontentdetail"></div>
                        </div>
                        <div class="focuswin-diver" style="margin-top: 10px; margin-bottom: 10px;"></div>
                        <div class="focuswin-comment">
                            <div class="pub_title">
                                <span data-bind="text: ResultText"></span>
                            </div>
                            <div>
                                <div data-bind="html: detailReply" class="wikicontentdetail"></div>
                            </div>
                        </div>
                    </div>

                    <div id="approvalform" style="display: none;">
                        <div style="height: 30px; cursor: pointer;" data-bind="click: appoveforumreturn">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <div>
                            <div style="position: relative;">
                                <h2 data-bind="text: detailTitle" style="color: #0a507a"></h2>
                            </div>
                            <div style="margin-top: 30px; margin-bottom: 10px; border: 1px dashed #000;"></div>
                            <div data-bind="html: detailContent" class="wikicontentdetail"></div>
                        </div>
                        <div class="focuswin-diver" style="margin-top: 10px; margin-bottom: 10px;"></div>
                        <div class="focuswin-manualhtml-list" style="width: 900px; margin-bottom: 15px;" data-bind="visible: ApproveHistory().length > 0">
                            <table class="list-table">
                                <thead>
                                    <tr>
                                        <td class="publishdate">处理人</td>
                                        <td class="publishdate">操作</td>
                                        <td style="text-align: center;">处理意见</td>
                                        <td class="publishdate">日期时间</td>
                                        <td class="publishdate">回复截止时间</td>
                                    </tr>
                                </thead>
                                <tbody data-bind="foreach: ApproveHistory">
                                    <tr>
                                        <td style="text-align: center;" data-bind="text: AssignTo"></td>
                                        <td style="text-align: center; padding: 0px 8px;" data-bind="html: Result"></td>
                                        <td style="padding: 0px 8px;" data-bind="html: Comments"></td>
                                        <td style="text-align: center;" data-bind="text: SP2013.Utility.formatDate(Created)"></td>
                                        <td style="text-align: center;" data-bind="text: SP2013.Utility.formatDate(Deadline)"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="focuswin-comment" data-bind="visible: ApproveStatus() == '申请中' && TaskStatus() == 'Pending'">
                            <div>
                                <div class="approvetype" data-bind="click: SwitchApproveType.bind($data, 1), css: ApproveType() == 1 ? 'approvetype active' : 'approvetype'">回复</div>
                                <div class="approvetype" data-bind="click: SwitchApproveType.bind($data, 2), css: ApproveType() == 2 ? 'approvetype active' : 'approvetype'">转签</div>
                            </div>
                            <div class="focuswin-clear"></div>
                            <div style="display: none;" data-bind="visible: ApproveType() == 1">
                                <div style="margin: 5px 0px;">
                                    <span>处理意见</span>
                                </div>
                                <div class="form-group">
                                    <textarea class="form-control" rows="6" data-bind="value: ApproveComments" style="width: 600px;"></textarea>
                                </div>
                                <button type="button" class="btn btn-default" data-bind="click: DealTask">提交</button>
                            </div>
                            <div style="display: none;" data-bind="visible: ApproveType() == 2">
                                <div style="margin: 10px 0px;">
                                    <div style="float: left; height: 26px; line-height: 26px;">转签人：</div>
                                    <div id="peoplePickerDiv" style="float: left;"></div>
                                    <div class="focuswin-clear"></div>
                                </div>
                                <div>
                                    <span>转签意见(非必填)</span>
                                </div>
                                <div class="form-group">
                                    <textarea class="form-control" rows="6" data-bind="value: ApproveComments" style="width: 600px;"></textarea>
                                </div>
                                <button type="button" class="btn btn-default" data-bind="click: TransferTask">提交</button>
                            </div>
                        </div>
                    </div>

                    <div id="viewform" style="display: none;">
                        <div style="height: 30px; cursor: pointer;" data-bind="click: viewreturnforum">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <div>
                            <div style="position: relative;">
                                <h2 data-bind="text: detailTitle" style="color: #0a507a"></h2>
                                <div data-bind="visible: IsAdmin()" style="float: right;">
                                    <a href="javascript:void();" data-bind="visible: !CurrentTop(), click: SetTop.bind($data, '1')">置顶</a>
                                    <a href="javascript:void();" data-bind="visible: CurrentTop(), click: SetTop.bind($data, '0')">取消置顶</a>
                                </div>
                            </div>
                            <div style="margin-top: 30px; margin-bottom: 10px; border: 1px dashed #000;"></div>
                            <div data-bind="html: detailContent" class="wikicontentdetail"></div>
                        </div>
                        <div class="focuswin-diver" style="margin-top: 10px; margin-bottom: 10px;"></div>
                        <div class="focuswin-comment">
                            <div class="pub_title">
                                <span data-bind="text: ResultText"></span>
                            </div>
                            <div>
                                <div data-bind="html: detailReply" class="wikicontentdetail"></div>
                            </div>
                        </div>
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

                    <div id="adminform" style="display: none;">
                        <div style="height: 30px; cursor: pointer;" data-bind="click: adminforumreturn">
                            <img src="/_layouts/15/Envision.KMS.Solution/Images/left_round.png" style="height: 100%;" />
                        </div>
                        <div>
                            <div style="position: relative;">
                                <h2 data-bind="text: detailTitle" style="color: #0a507a"></h2>
                            </div>
                            <div style="margin-top: 30px; margin-bottom: 10px; border: 1px dashed #000;"></div>
                            <div data-bind="html: detailContent" class="wikicontentdetail"></div>
                        </div>
                        <div class="focuswin-diver" style="margin-top: 10px; margin-bottom: 10px;"></div>
                        <div class="focuswin-manualhtml-list" style="width: 900px; margin-bottom: 15px;" data-bind="visible: ApproveHistory().length > 0">
                            <table class="list-table">
                                <thead>
                                    <tr>
                                        <td class="publishdate">处理人</td>
                                        <td class="publishdate">操作</td>
                                        <td style="text-align: center;">处理意见</td>
                                        <td class="publishdate">日期时间</td>
                                        <td class="publishdate">回复截止时间</td>
                                    </tr>
                                </thead>
                                <tbody data-bind="foreach: ApproveHistory">
                                    <tr>
                                        <td style="text-align: center;" data-bind="text: AssignTo"></td>
                                        <td style="text-align: center; padding: 0px 8px;" data-bind="html: Result"></td>
                                        <td style="padding: 0px 8px;" data-bind="html: Comments"></td>
                                        <td style="text-align: center;" data-bind="text: SP2013.Utility.formatDate(Created)"></td>
                                        <td style="text-align: center;" data-bind="text: SP2013.Utility.formatDate(Deadline)"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</div>

<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jstree.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/tinymce/tinymce.min.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/bootstrap.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jquery.pagination.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/jquery-ui-1.10.4.js"></script>
<script src="/_layouts/15/Envision.KMS.Solution/Scripts/HomePage/Casemanagement.js?v=0.4"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        App.init();
        // Specify the unique ID of the DOM element where the
        // picker will render.
        initializePeoplePicker('peoplePickerDiv');
        tinymce.init({
            selector: '#Comments',
            menu: {
            },
            content_css: "/_layouts/15/Envision.KMS.Solution/Styles/Site.css",
        });
    });

    // Render and initialize the client-side People Picker.
    function initializePeoplePicker(peoplePickerElementId) {

        // Create a schema to store picker properties, and set the properties.
        var schema = {};
        schema['PrincipalAccountType'] = 'User,DL';
        schema['SearchPrincipalSource'] = 15;
        schema['ResolvePrincipalSource'] = 15;
        schema['AllowMultipleValues'] = false;
        schema['MaximumEntitySuggestions'] = 50;
        schema['Width'] = '280px';

        // Render and initialize the picker. 
        // Pass the ID of the DOM element that contains the picker, an array of initial
        // PickerEntity objects to set the picker value, and a schema that defines
        // picker properties.
        this.SPClientPeoplePicker_InitStandaloneControlWrapper(peoplePickerElementId, null, schema);
    }

    // Query the picker for user information.
    function getUserInfo() {

        // Get the people picker object from the page.
        var peoplePicker = this.SPClientPeoplePicker.SPClientPeoplePickerDict.peoplePickerDiv_TopSpan;

        // Get information about all users.
        var users = peoplePicker.GetAllUserInfo();
        var userInfo = '';
        for (var i = 0; i < users.length; i++) {
            var user = users[i];
            for (var userProperty in user) {
                userInfo += userProperty + ':  ' + user[userProperty] + '<br>';
            }
        }
        $('#resolvedUsers').html(userInfo);

        // Get user keys.
        var keys = peoplePicker.GetAllUserKeys();
        $('#userKeys').html(keys);

        // Get the first user's ID by using the login name.
        getUserId(users[0].Key);
    }

    // Get the user ID.
    function getUserId(loginName) {
        var context = new SP.ClientContext.get_current();
        this.user = context.get_web().ensureUser(loginName);
        context.load(this.user);
        context.executeQueryAsync(
             Function.createDelegate(null, ensureUserSuccess),
             Function.createDelegate(null, onFail)
        );
    }

    function ensureUserSuccess() {
        $('#userId').html(this.user.get_id());
    }

    function onFail(sender, args) {
        alert('Query failed. Error: ' + args.get_message());
    }
</script>
