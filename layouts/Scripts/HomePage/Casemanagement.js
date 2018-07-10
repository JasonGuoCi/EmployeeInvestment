/*软件问题及建议*/
var CaseEntity = function (data, parent) {
    var entity = this;
    entity.Id = ko.observable();
    entity.UniqueId = ko.observable();
    entity.Title = ko.observable();
    entity.Created = ko.observable();
    entity.Modified = ko.observable();
    entity.Author = ko.observable();
    entity.CaseType = ko.observable();
    entity.CaseTypeDisplay = ko.observable();
    entity.IsTop = ko.observable();
    entity.Status = ko.observable();
    entity.IsSendEmail = ko.observable(true);
    entity.ImgSource = ko.observable();
    entity.click2Detail = function () {
        parent.detailform();
        parent.LoadCaseById(entity.Id());
        parent.CurrentId(entity.Id())
    }
    /*催办按钮事件*/
    entity.click2SendMail = function () {
        var r = confirm("确认发送邮件催办？");
        if (r == true) {
            if (entity.Id()) {
                entity.IsSendEmail(false);
                parent.SendMail(entity.Id());
            }
        }
    }
    /*所有的ase链接地址*/
    entity.click2View = function () {
        parent.viewform();
        parent.LoadCaseById(entity.Id());
        parent.CurrentId(entity.Id());
        parent.CurrentTop(entity.IsTop() == '1');
        parent.loadComments(entity.UniqueId());
    }
    /*修改case*/
    entity.click2Modify = function () {
        parent.editforum();
        parent.LoadCaseById(entity.Id());
        parent.CurrentId(entity.Id())
    }
    /*删除case*/
    entity.click2Delete = function () {
        var r = confirm("确认删除该贴？");
        if (r == true) {
            if (entity.Id()) {
                var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Item/Delete";
                SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                    weburl: SP2013.O365.currentWebAbsoluteUrl(),
                    Id: entity.Id(),
                    listname: "CaseManagement",
                    CategoryId: parent.Category() == '4' ? 'IsAdmin' : ''
                })).then(function (d) {
                    if (d) {
                        if (parent.Category() == '4') {
                            parent.LoadAllApplication();
                            parent.LoadAllApplicationCount();
                        }
                        else {
                            parent.LoadMyApply();
                            parent.LoadMyApplyCount();
                        }
                        parent.LoadNumber();
                    }
                });
            }
        }
    }
    /*我申请的case地址*/
    entity.click2Admin = function () {
        parent.adminforum();
        parent.LoadCaseByUniqueId(entity.UniqueId());
        parent.CurrentId(entity.Id());
    }
    if (data) {
        entity.Id(data.Id);
        entity.UniqueId(data.UniqueId);
        entity.Title(data.Title);
        entity.Created(SP2013.Utility.formatDate(data.Created));
        entity.Author(data.Author);
        entity.CaseType(data.CaseType);
        entity.CaseTypeDisplay(data.CaseTypeDisplay);
        entity.IsTop(data.IsTop);
        entity.Status(data.Status)
        entity.ImgSource(data.IsTop == '1' ? '/_layouts/15/Envision.KMS.Solution/Images/Icon_Top20.png' : '/_layouts/15/Envision.KMS.Solution/Images/Icon_Post20.png');
    }
}
/*处理任务按钮事件*/
var TaskEntity = function (data, parent) {
    var entity = this;
    entity.Id = ko.observable();
    entity.Title = ko.observable();
    entity.CreatedBy = ko.observable();
    entity.CaseType = ko.observable();
    entity.CaseId = ko.observable();
    entity.Created = ko.observable();
    entity.click2Detail = function () {
        parent.ApproveType(1);
        parent.approveforum();
        parent.LoadCaseByUniqueId(entity.CaseId());
        parent.CurrentTaskId(entity.Id());
    }
    if (data) {
        entity.Id(data.Id);
        entity.Title(data.CaseTitle);
        entity.CreatedBy(data.CreatedBy);
        entity.CaseType(data.CaseType);
        entity.CaseId(data.CaseId);
        entity.Created(SP2013.Utility.formatDate(data.Created));
    }
}
/*评论处理*/
var commentEntity = function (data, parent) {
    var entity = this;
    entity.Id = ko.observable();
    entity.Comment = ko.observable();
    entity.DisplayName = ko.observable();
    entity.Create = ko.observable();
    entity.RemoveComment = function () {
        var r = confirm("确认删除该评论？");
        if (r == true) {
            if (entity.Id()) {
                var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Comments/Delete";
                SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                    weburl: SP2013.O365.currentWebAbsoluteUrl(),
                    Id: entity.Id()
                })).then(function (d) {
                    if (d)
                        parent.InitComments();
                });
            }

        }
    }
    entity.Payback = function () {
        tinymce.activeEditor.setContent('<div class="focuswin-qutoes"><div>@' + entity.DisplayName() + '</div><div>' + entity.Comment() + '</div></div><p></p>');
    }
    if (data) {
        entity.Id = ko.observable(data.Id);
        entity.Comment(data.Comments);
        entity.DisplayName(data.CreatedBy);
        entity.Create(SP2013.Utility.formateFloatDate(data.Created));
    }
}
/*软件问题及建议的设置及显示*/
var caseMgr = function () {
    var self = this;
    self.IsAdmin = ko.observable(false);
    self.IsEnglishTitle = ko.observable(SP2013.O365.currentLang() == '1033' ? true : false);
    self.CurrentId = ko.observable();
    self.CurrentwikiId = ko.observable();
    self.NavTitle = ko.observable();
    self.MyApplicationText = ko.observable();
    self.MyTaskText = ko.observable();
    self.QueryText = ko.observable();
    self.StatusText = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Status' : '状态');
    self.ApplyStatusText = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Applying' : '申请中');
    self.DoneStatusText = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Finished' : '已完成');
    self.SearchText = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Search' : '搜索');
    self.NewBtnText = ko.observable(SP2013.O365.currentLang() == '1033' ? 'New' : '新建');
    self.PendingStatusText = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Pending' : '待处理');
    self.FinishStatusText = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Finished' : '已完成');
    self.ResultText = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Outcome' : '处理结果');
    self.AllApplicationText = ko.observable();
    self.ApplyingNum = ko.observable(0);
    self.PendingNum = ko.observable(0);

    self.TypeAttr = ko.observableArray();
    self.TypeNewAttr = ko.observableArray();
    self.DataAttr = ko.observableArray();
    self.TaskAttr = ko.observableArray();
    self.NewTitle = ko.observable();
    self.NewContent = ko.observable();
    self.NewType = ko.observable();
    self.CurrentTop = ko.observable();

    self.LimitRows = ko.observable(8);
    self.keywords = ko.observable();
    self.Topcount = ko.observable(0);
    self.TotalCount = ko.observable(0);


    self.ApplyStatus = ko.observable("申请中");
    self.ChangeStatus = function (status) {
        self.ApplyStatus(status);
        self.keywords('');
        self.SearchType('');
        self.LoadMyApply();
        self.LoadMyApplyCount();
        self.LoadNumber();
    }

    self.TaskStatus = ko.observable("Pending");
    self.ChangeTaskStatus = function (status) {
        self.TaskStatus(status);
        self.keywords('');
        self.SearchType('');
        self.LoadTask();
        self.LoadTaskCount();
        self.LoadNumber();
    }

    self.Category = ko.observable("3");
    self.ChangeCategory = function (category) {
        self.Category(category);
        self.keywords('');
        self.SearchType('');
        self.Topcount(0);
        self.LoadNumber();
        if (category == '1') {
            self.LoadMyApply();
            self.LoadMyApplyCount();
        }
        else if (category == '2') {
            self.LoadTask();
            self.LoadTaskCount();
        }
        else if (category == '3') {
            self.LoadQuery();
            self.LoadQueryCount();
        }
        else if (category == '4') {
            self.LoadAllApplication();
            self.LoadAllApplicationCount();
        }
    }

    self.SearchType = ko.observable();
    self.Searchkey = ko.observable();
    self.IsSearch = ko.observable(false);

    self.detailTitle = ko.observable();
    self.detailContent = ko.observable();
    self.detailType = ko.observable();
    self.detailReply = ko.observable();

    self.EditTitle = ko.observable();
    self.EditContent = ko.observable();
    self.EditType = ko.observable();

    self.CurrentTaskId = ko.observable();
    self.ApproveComments = ko.observable();
    self.ApproveHistory = ko.observableArray();
    self.ApproveStatus = ko.observable();
    self.ApproveType = ko.observable(1);
    self.SwitchApproveType = function (type) {
        self.ApproveType(type);
    }
/*加载软件问题及建议标签*/
    self.TypeLoad = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Types";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseType",
            Language: SP2013.O365.currentLang()
        })).then(function (d) {
            self.TypeAttr.removeAll();
            self.TypeNewAttr.removeAll();
            self.TypeAttr.push({ Id: '', Title: 'All' })
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.TypeAttr.push({ Id: item.Id, Title: item.Title });
                    self.TypeNewAttr.push({ Id: item.Id, Title: item.Title });
                });
            }
        });
    }
    /*添加软件问题及建议*/
    self.InsertCase = function () {
        if (self.NewTitle() && self.NewContent() && self.NewType()) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Insert/Item";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                listname: "CaseManagement",
                Title: self.NewTitle(),
                Content: self.NewContent(),
                CaseType: self.NewType().Id,
                CaseTypeDisplay: self.NewType().Title
            })).then(function (d) {
                if (d && d > 0) {
                    self.LoadMyApply();
                    self.newforumreturn();
                    self.LoadMyApplyCount();
                    self.LoadNumber();
                }
            });
        } else {
            alert("请填写完整");
        }
    }
    /*编辑软件问题及建议*/
    self.ModifiyCase = function () {
        if (self.EditTitle() && self.EditContent()) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Insert/Item";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                listname: "CaseManagement",
                Title: self.EditTitle(),
                Content: self.EditContent(),
                Id: self.CurrentId()
            })).then(function (d) {
                if (d && d > 0) {
                    self.LoadMyApply();
                    self.editforumreturn();
                }
            });
        } else {
            alert("请填写完整");
        }
    }
    /*左侧导航设置与现实*/
    self.InitLeftNav = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/LeftNav/ResourceId";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '左侧导航',
            lang: SP2013.O365.currentLang()
        }
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $("#left-nav").jstree({
                    'core': {
                        'data': [
                            {
                                "text": d.item1,
                                "li_attr": { 'url': 'Wikipedia.aspx' }
                            },
                            {
                                "text": d.item2,
                                "li_attr": { 'url': 'Manual.aspx' },

                            },
                             {
                                 "text": d.item3,
                                 'state': { 'opened': true },
                                 "children": [
                                       {
                                           "text": d.item4,
                                           "li_attr": { 'url': 'ForumContent.aspx' }
                                       },
                                       {
                                           "text": d.item5,
                                           "li_attr": { 'url': 'CaseManagement.aspx' },
                                           'state': { 'selected': true }
                                       }
                                 ]
                             }
                        ]
                    }
                }).bind('click.jstree', function (event) {
                    var eventNodeName = event.target.nodeName;
                    if (eventNodeName == "A") {
                        if ($(event.target).parents('li').attr('url')) {
                            window.location.href = $(event.target).parents('li').attr('url');
                        }
                    }
                });

            }
        });
    }
    /*搜索设置与显示*/
    self.Search = function () {
        self.Topcount(0);
        if (self.Category() == '1') {
            self.keywords($("#searchInput1").val());
            self.LoadMyApply();
            self.LoadMyApplyCount();
        }
        else if (self.Category() == '2') {
            self.keywords($("#searchInput2").val());
            self.LoadTask();
            self.LoadTaskCount();
        }
        else if (self.Category() == '3') {
            self.keywords($("#searchInput3").val());
            self.LoadQuery();
            self.LoadQueryCount();
        }
        else if (self.Category() == '4') {
            self.keywords($("#searchInput4").val());
            self.LoadAllApplication();
            self.LoadAllApplicationCount();
        }
    }
    /*页面加载*/
    self.Load = function () {
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Head Title',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.NavTitle(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '论坛',
            key: 'my application display',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.MyApplicationText(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '论坛',
            key: 'my task display',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.MyTaskText(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '论坛',
            key: 'query display',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.QueryText(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '论坛',
            key: 'all application display',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.AllApplicationText(d);
        });
        if (SP2013.Utility.queryString.IsDetail == '1') {
            if (SP2013.Utility.queryString.ItemId) {
                self.detailform();
                self.LoadCaseById(SP2013.Utility.queryString.ItemId);
            }
        } else if (SP2013.Utility.queryString.IsTask == '1') {
            if (SP2013.Utility.queryString.ItemId && SP2013.Utility.queryString.TaskId) {
                self.approveforum();
                self.LoadCaseByUniqueId(SP2013.Utility.queryString.ItemId);
                self.CurrentTaskId(SP2013.Utility.queryString.TaskId);
            }
        }
        self.InitLeftNav();
        self.LoadNumber();
        self.TypeLoad();
        //self.LoadMyApply();
        //self.LoadMyApplyCount();
        self.LoadQuery();
        self.LoadQueryCount();
    }
    /*加载我申请的任务*/
    self.LoadMyApply = function () {
        self.IsSearch(false);
        self.DataAttr.removeAll();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Items/Apply";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            LimitRows: self.LimitRows(),
            TakeCount: self.Topcount(),
            Search: self.keywords(),
            WikiId: self.SearchType(),
            CategoryId: self.ApplyStatus(),
            Language: SP2013.O365.currentLang()
        })).then(function (d) {
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.DataAttr.push(new CaseEntity(item, self));
                });
            }
            if (self.keywords() || self.SearchType()) {
                self.IsSearch(true);
            }
        });
    }
    /*通过caseid获取case*/
    self.LoadCaseById = function (Id) {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Item/Detail";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            Id: Id,
            Language: SP2013.O365.currentLang()
        })).then(function (d) {
            if (d) {
                self.detailTitle(d.Title);
                self.detailContent(d.Content);
                self.detailType(d.CaseTypeDisplay);
                if (d.Status == "已完成")
                    self.detailReply(d.Reply);
                else
                    self.detailReply("问题处理中......");
                self.EditTitle(d.Title);
                self.EditContent(d.OrginContent);
                self.EditType(d.CaseTypeDisplay);
            }
        });
    }
    /*加载我的代办*/
    self.LoadTask = function () {
        self.IsSearch(false);
        self.TaskAttr.removeAll();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Task/Items";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            LimitRows: self.LimitRows(),
            TakeCount: self.Topcount(),
            Search: self.keywords(),
            WikiId: self.SearchType(),
            CategoryId: self.TaskStatus(),
            Language: SP2013.O365.currentLang()
        })).then(function (d) {
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.TaskAttr.push(new TaskEntity(item, self));
                });
            }
            if (self.keywords() || self.SearchType()) {
                self.IsSearch(true);
            }
        });
    }
    /*获取我申请问题的数量（未处理）*/
    self.LoadMyApplyCount = function () {
        self.TotalCount(0);
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Items/ApplyCount";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            LimitRows: self.LimitRows(),
            listname: "CaseManagement",
            TakeCount: self.Topcount(),
            Search: self.keywords(),
            WikiId: self.SearchType(),
            CategoryId: self.ApplyStatus(),
        })).then(function (d) {
            if (d > -1) {
                self.TotalCount(d);
            }
            self.initPagination();
        });
    }
    /*通过GUID加载case*/
    self.LoadCaseByUniqueId = function (UniqueId) {
        self.ApproveHistory.removeAll();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Item/DetailByUnique";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            Id: UniqueId,
            Language: SP2013.O365.currentLang()
        })).then(function (d) {
            if (d) {
                self.detailTitle(d.Title);
                self.detailContent(d.Content);
                self.detailType(d.CaseTypeDisplay);
                self.ApproveStatus(d.Status);
                if (d.taskitems && d.taskitems.length > 0) {
                    $.each(d.taskitems, function (i, item) {
                        self.ApproveHistory.push(item);
                    });
                }
            }
        });
    }
    /*加载我额代办*/
    self.LoadTaskCount = function () {
        self.TotalCount(0);
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Task/ItemsCount";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            LimitRows: self.LimitRows(),
            TakeCount: self.Topcount(),
            Search: self.keywords(),
            WikiId: self.SearchType(),
            CategoryId: self.TaskStatus(),
        })).then(function (d) {
            if (d > -1) {
                self.TotalCount(d);
            }
            self.initPagination();
        });
    }
    /*处理case任务*/
    self.DealTask = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Task/Deal";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            Id: self.CurrentTaskId(),
            Language: SP2013.O365.currentLang(),
            Search: self.ApproveComments()
        })).then(function (d) {
            if (d) {
                alert("回复已成功提交");
                self.appoveforumreturn();
                self.LoadTask();
                self.LoadTaskCount();
                self.LoadNumber();
            }
        });
    }
    /*加载所有已处理完成问题*/
    self.LoadQuery = function () {
        self.IsSearch(false);
        self.DataAttr.removeAll();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Items/Query";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            LimitRows: self.LimitRows(),
            TakeCount: self.Topcount(),
            Search: self.keywords(),
            WikiId: self.SearchType(),
            Language: SP2013.O365.currentLang()
        })).then(function (d) {
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.DataAttr.push(new CaseEntity(item, self));
                });
            }
            if (self.keywords() || self.SearchType()) {
                self.IsSearch(true);
            }
        });
    }
    /*获取所有已处理完成问题的总数*/
    self.LoadQueryCount = function () {
        self.TotalCount(0);
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Items/QueryCount";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            LimitRows: self.LimitRows(),
            listname: "CaseManagement",
            TakeCount: self.Topcount(),
            Search: self.keywords(),
            WikiId: self.SearchType()
        })).then(function (d) {
            if (d > -1) {
                self.TotalCount(d);
            }
            self.initPagination();
        });
    }
    /*置顶*/
    self.SetTop = function (type) {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Items/SetTop";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            Search: type,
            Id: self.CurrentId()
        })).then(function (d) {
            if (d == 1) {
                self.CurrentTop(type == '1');
                self.Topcount(0);
                if (self.Category() == '1') {
                    self.LoadMyApply();
                    self.LoadMyApplyCount();
                }
                else if (self.Category() == '2') {
                    self.LoadTask();
                    self.LoadTaskCount();
                }
                else if (self.Category() == '3') {
                    self.LoadQuery();
                    self.LoadQueryCount();
                }
            }
        });
    }
      /*转办任务*/
    self.TransferTask = function () {
        var peoplePicker = window.SPClientPeoplePicker.SPClientPeoplePickerDict.peoplePickerDiv_TopSpan;
        var users = peoplePicker.GetAllUserInfo();
        if (users.length > 0) {
            var r = confirm("确认将该任务转到" + users[0].DisplayText + "？");
            if (r == true) {
                var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Task/Transfer";
                SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                    weburl: SP2013.O365.currentWebAbsoluteUrl(),
                    listname: users[0].EntityData.Email,
                    Id: self.CurrentTaskId(),
                    Language: SP2013.O365.currentLang(),
                    WikiId: users[0].DisplayText,
                    Search: self.ApproveComments(),
                    CategoryId: users[0].Key
                })).then(function (d) {
                    if (d) {
                        alert("该问题已转签至" + users[0].DisplayText);
                        self.appoveforumreturn();
                        self.LoadTask();
                        self.LoadTaskCount();
                        self.LoadNumber();
                    }
                });
            }
        } else {
            alert("请选择转签人");
        }
    }
    /*发送邮件*/
    self.SendMail = function (Id) {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Task/SendMail";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            Id: Id,
            Language: SP2013.O365.currentLang(),
        })).then(function (d) {
            if (d && d > 0) {
                alert("已发送邮件催办");
            }
        });
    }
    /*获取申请中的任务数量*/
    self.LoadNumber = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Items/ApplyCount";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            CategoryId: "申请中",
        })).then(function (d) {
            if (d > -1) {
                self.ApplyingNum(d);
            }
        });
        method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Task/ItemsCount";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            CategoryId: "Pending",
        })).then(function (d) {
            if (d > -1) {
                self.PendingNum(d);
            }
        });
    }
    /*加载所有问题*/
    self.LoadAllApplication = function () {
        self.IsSearch(false);
        self.DataAttr.removeAll();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Items/AllApply";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "CaseManagement",
            LimitRows: self.LimitRows(),
            TakeCount: self.Topcount(),
            Search: self.keywords(),
            WikiId: self.SearchType(),
            Language: SP2013.O365.currentLang()
        })).then(function (d) {
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.DataAttr.push(new CaseEntity(item, self));
                });
            }
            if (self.keywords() || self.SearchType()) {
                self.IsSearch(true);
            }
        });
    }
    /*获取所有问题的总数*/
    self.LoadAllApplicationCount = function () {
        self.TotalCount(0);
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/Case/Items/AllApplyCount";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            LimitRows: self.LimitRows(),
            listname: "CaseManagement",
            TakeCount: self.Topcount(),
            Search: self.keywords(),
            WikiId: self.SearchType()
        })).then(function (d) {
            if (d > -1) {
                self.TotalCount(d);
            }
            self.initPagination();
        });
    }

    self.CommentsLimitRows = ko.observable(8);
    self.CommentsTopcount = ko.observable(1);
    self.CommentsTotalCount = ko.observable(0);
    self.CommentsTitle = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Comments' : '评论');
    self.CommentsBtn = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Submit' : '提交评论');
    self.CommentsArray = ko.observableArray();
    self.CommentsPayBackDisplay = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Reply' : '回复');
    self.loadComments = function (itemId) {
        self.CurrentwikiId(itemId);
        self.InitCommentsCount();
        self.InitComments();
    }
    /*获取评论总数*/
    self.InitCommentsCount = function () {
        self.TotalCount(0);
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Comments/ItemsCount";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            Id: self.CurrentwikiId(),
        })).then(function (d) {
            self.CommentsTotalCount(d);
            self.CommentsinitPagination();
        });
    }
    /*获取评论*/
    self.InitComments = function () {
        self.CommentsArray.removeAll();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Comments/Items";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            Id: self.CurrentwikiId(),
            LimitRows: self.CommentsLimitRows(),
            TakeCount: self.CommentsTopcount()
        })).then(function (d) {
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.CommentsArray.push(new commentEntity(item, self));
                });
            }
        });
    }
    /*提交评论*/
    self.submitcomments = function () {
        var comments = tinymce.activeEditor.getContent();
        if (comments) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Comments/Insert";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                Id: self.CurrentwikiId(),
                Search: comments
            })).then(function (d) {
                tinymce.activeEditor.setContent('');
                self.loadComments(self.CurrentwikiId());
            });
        }
    }
    function CommentspageselectCallback(page_index, jq) {
        self.CommentsTopcount(page_index + 1);
        self.InitComments();
    }
    /*分页*/
    self.CommentsinitPagination = function () {
        $("#Pagination2").pagination(self.CommentsTotalCount(), {
            callback: CommentspageselectCallback,
            items_per_page: self.LimitRows(),
            prev_text: SP2013.O365.currentLang() == '1033' ? 'Previous' : "前一页",
            next_text: SP2013.O365.currentLang() == '1033' ? 'Next' : "后一页",
            num_edge_entries: 1,
            start_page: SP2013.O365.currentLang() == '1033' ? 'First' : "首页",
            end_page: SP2013.O365.currentLang() == '1033' ? 'Last' : "末页",
            num_display_entries: 10,
        });
    };

    function pageselectCallback(page_index, jq) {
        self.Topcount(page_index + 1);
        if (self.Category() == '1') {
            self.LoadMyApply();
        }
        else if (self.Category() == '2') {
            self.LoadTask();
        }
        else if (self.Category() == '3') {
            self.LoadQuery();
        }
        else if (self.Category() == '4') {
            self.LoadAllApplication();
        }
    }
    self.initPagination = function () {
        $("#Pagination").pagination(self.TotalCount(), {
            callback: pageselectCallback,
            items_per_page: self.LimitRows(),
            prev_text: SP2013.O365.currentLang() == '1033' ? 'Previous' : "前一页",
            next_text: SP2013.O365.currentLang() == '1033' ? 'Next' : "后一页",
            num_edge_entries: 1,
            start_page: SP2013.O365.currentLang() == '1033' ? 'First' : "首页",
            end_page: SP2013.O365.currentLang() == '1033' ? 'Last' : "末页",
            num_display_entries: 10
        });
    };
    /*跳转到新建case*/
    self.newforum = function () {
        $("#toppage").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#newform").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
    /*跳转到我的申请*/
    self.newforumreturn = function () {
        self.NewTitle('');
        self.NewContent('');
        self.NewType('');
        $("#newform").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#toppage").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
    /*判断是否为管理员*/
    self.validateUser = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Admin/Validation";
        SP2013.Utility.ReturnJson(SP2013.O365.getJSON(method)).then(function (d) {
            if (d)
                self.IsAdmin(d);
        });
    }
    /*跳转到已完成是问题明细页面*/
    self.detailform = function () {
        $("#toppage").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#detailform").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
    /*跳转到所有问题*/
    self.detailreturnforum = function () {
        if (SP2013.Utility.queryString.IsDetail == '1') {
            window.location.href = SP2013.Utility.concatUrl(SP2013.O365.currentWebAbsoluteUrl(), '/Pages/Projects/CaseManagement.aspx');
        }
        else {
            self.detailTitle('');
            self.detailContent('');
            self.detailType('');
            self.detailReply('');
            $("#detailform").toggle('drop', { direction: "right" }, 500);
            setTimeout(function () {
                $("#toppage").toggle('drop', { direction: "left" }, 500);
            }, 500);
        }
    }
    /*跳转到编辑我的申请*/
    self.editforum = function () {
        $("#toppage").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#editform").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
    /*跳转到我的申请*/
    self.editforumreturn = function () {
        self.EditTitle('');
        self.EditContent('');
        self.EditType('');
        $("#editform").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#toppage").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
   
    /*跳转到人员选择器*/
    self.approveforum = function () {
        $("#toppage").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#approvalform").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
    /*跳转到我的申请*/
    self.appoveforumreturn = function () {
        if (SP2013.Utility.queryString.IsTask == '1') {
            window.location.href = SP2013.Utility.concatUrl(SP2013.O365.currentWebAbsoluteUrl(), '/Pages/Projects/CaseManagement.aspx');
        }
        else {
            initializePeoplePicker('peoplePickerDiv');
            self.ApproveComments('');
            $("#approvalform").toggle('drop', { direction: "right" }, 500);
            setTimeout(function () {
                $("#toppage").toggle('drop', { direction: "left" }, 500);
            }, 500);
        }
    }
    /*跳转到已审批成成的case中*/
    self.viewform = function () {
        $("#toppage").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#viewform").show();
        }, 500);
    }
    /*跳转到所有问题*/
    self.viewreturnforum = function () {
        self.detailTitle('');
        self.detailContent('');
        self.detailType('');
        self.detailReply('');
        $("#viewform").hide();
        setTimeout(function () {
            $("#toppage").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }

    self.Navigate2Root = function () {
        window.location.href = SP2013.O365.currentWebAbsoluteUrl();
    }
    /*跳转到管理员界面已审批成成的case中*/
    self.adminforum = function () {
        $("#toppage").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#adminform").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
    /*跳转到所有问题*/
    self.adminforumreturn = function () {
        $("#adminform").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#toppage").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
}
/*设置软件问题及建议滑动条*/
var frameheight = $(".project-frame-main").height();
if ((frameheight - 76) < 500)
    $("#announcement .content").css("height", frameheight - 86);

$(window).resize(function () {
    var frameheight = $(".project-frame-main").height();
    if ((frameheight - 76) < 500)
        $("#announcement .content").css("height", frameheight - 86);
});

var CaseMgr = new caseMgr();
CaseMgr.Load();
CaseMgr.validateUser();
ko.applyBindings(CaseMgr, document.getElementById('casecontent'));
$(".searchInput").keydown(function (event) {
    if (event.keyCode == 13) {
        CaseMgr.Search();
    }
})