/*评论功能*/
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
    /*回复功能*/
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
/*视频的播放及评论*/
var videoMgr = function () {
    var self = this;
    self.Topcount = ko.observable(1);
    self.TotalCount = ko.observable(0);
    self.CurrentwikiId = ko.observable();
    self.LimitRows = ko.observable(10);
    self.IsAdmin = ko.observable(false);
    self.keywords = ko.observable();
    self.CommentsTitle = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Comments' : '评论');
    self.CommentsBtn = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Submit' : '提交评论');
    self.IsSearch = ko.observable(false);
    self.CommentsArray = ko.observableArray();
    self.Load = function () {
        self.loadComments(SP2013.Utility.queryString.uniqueId);
    }
    /*判断是否为管理员*/
    self.validateUser = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Admin/Validation";
        SP2013.Utility.ReturnJson(SP2013.O365.getJSON(method)).then(function (d) {
            if (d)
                self.IsAdmin(d);
        });
    }

    self.CommentsPayBackDisplay = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Reply' : '回复');
    self.loadComments = function (itemId) {
        self.CurrentwikiId(itemId);
        self.InitCommentsCount();
        self.InitComments();
    }
    /*评论总数*/
    self.InitCommentsCount = function () {
        self.TotalCount(0);
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Comments/ItemsCount";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            Id: self.CurrentwikiId(),
        })).then(function (d) {
            if (d) {
                self.TotalCount(d);
                self.initPagination();
            }
        });
    }
    /*评论详情*/
    self.InitComments = function () {
        self.CommentsArray.removeAll();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Comments/Items";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            Id: self.CurrentwikiId(),
            LimitRows: self.LimitRows(),
            TakeCount: self.Topcount()
        })).then(function (d) {
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.CommentsArray.push(new commentEntity(item,self));
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

    function pageselectCallback(page_index, jq) {
        self.Topcount(page_index + 1);
        self.InitComments();
    }
    /*评论分页*/
    self.initPagination = function () {
        $("#Pagination").pagination(self.TotalCount(), {
            callback: pageselectCallback,
            items_per_page: self.LimitRows(),
            prev_text: SP2013.O365.currentLang() == '1033' ? 'Previous' : "前一页",
            next_text: SP2013.O365.currentLang() == '1033' ? 'Next' : "后一页",
            num_edge_entries: 1,
            start_page: SP2013.O365.currentLang() == '1033' ? 'First' : "首页",
            end_page: SP2013.O365.currentLang() == '1033' ? 'Last' : "末页",
            num_display_entries: 10,
        });
    };
}

var VideoMgr = new videoMgr();
VideoMgr.validateUser();
VideoMgr.Load();
ko.applyBindings(VideoMgr, document.getElementById('videoplayer'));