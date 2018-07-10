/*对评论的加载与删除*/
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
/*格林百科及中英文显示*/
var wikiMgr = function () {
    var self = this;
    self.wikiTitle = ko.observable();
    self.wikiContent = ko.observable();
    self.CurrentwikiId = ko.observable();
    self.CurrentcategoryId = ko.observable();
    self.CurrentcontentId = ko.observable();
    self.NavTitle = ko.observable();
    self.TreeTitle = ko.observable();
    self.Topcount = ko.observable(1);
    self.IsAdmin = ko.observable(false);
    self.TotalCount = ko.observable(0);
    self.IsEnglishTitle = ko.observable(SP2013.O365.currentLang() == '1033' ? true : false);
    self.LimitRows = ko.observable(10);
    self.keywords = ko.observable();
    self.CommentsTitle = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Comments' : '评论');
    self.CommentsBtn = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Submit' : '提交评论');
    self.IsSearch = ko.observable(false);
    self.WikiUrl = ko.computed(function () {
        if (self.CurrentcategoryId() && self.CurrentcontentId())
            return SP2013.Utility.concatUrl(SP2013.O365.currentWebAbsoluteUrl(), '/Pages/Projects/Wikipedia.aspx?CategoryId=' + self.CurrentcategoryId() + "&WikiId=" + self.CurrentcontentId());
        else
            return '';
    });
    self.CommentsArray = ko.observableArray();
    /*获取词条内容*/
    self.Load = function () {
        var detailmethod = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Wikipedia/Content";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            LimitRows: 100,
            TakeCount: 0,
            Language: SP2013.O365.currentLang()
        }
        if (SP2013.Utility.queryString.CategoryId && SP2013.Utility.queryString.WikiId) {
            opts = {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                LimitRows: 100,
                TakeCount: 0,
                CategoryId: SP2013.Utility.queryString.CategoryId,
                WikiId: SP2013.Utility.queryString.WikiId
            }
        }
        self.InitWikiTree(opts);

        if (SP2013.Utility.queryString.CategoryId && SP2013.Utility.queryString.WikiId) {
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(detailmethod, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                listname: "WikipediaLibrary",
                Id: SP2013.Utility.queryString.WikiId,
                Language: SP2013.O365.currentLang()
            })).then(function (detail) {
                self.wikiTitle(detail.Title);
                self.wikiContent(detail.HtmlContent);
                self.loadComments(detail.UniqueId);
                self.CurrentcategoryId(detail.CategoryId);
                self.CurrentcontentId(detail.Id);
            });
        }
    }
    /*判断用户是否为管理员*/
    self.validateUser = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Admin/Validation";
        SP2013.Utility.ReturnJson(SP2013.O365.getJSON(method)).then(function (d) {
            if (d)
                self.IsAdmin(d);
        });
    }
    /*获取词条内容包括词条树及词条内容*/
    self.InitWikiTree = function (opts) {
        self.IsSearch(false);
        if (!opts) {
            opts = {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                LimitRows: 100,
                TakeCount: 0,
                Language: SP2013.O365.currentLang()
            }
        }
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Wikipedia/tree";
        var detailmethod = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Wikipedia/Content";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            $("#wikitree").jstree('destroy');
            $("#wikitree").jstree({
                'core': {
                    'data': d
                }
            }).bind('click.jstree', function (event) {
                var eventNodeName = event.target.nodeName;
                if (eventNodeName == "A") {
                    if ($(event.target).parents('li').attr('relatedId')) {
                        self.wikiTitle('');
                        self.wikiContent('');
                        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(detailmethod, {
                            weburl: SP2013.O365.currentWebAbsoluteUrl(),
                            listname: "WikipediaLibrary",
                            Id: $(event.target).parents('li').attr('relatedId'),
                            Language: SP2013.O365.currentLang()
                        })).then(function (detail) {
                            self.wikiTitle(detail.Title);
                            self.wikiContent(detail.HtmlContent);
                            self.loadComments(detail.UniqueId);
                            self.CurrentcontentId(detail.Id);
                            self.CurrentcategoryId(detail.CategoryId);
                        });
                    }
                }
            });
        });
    }
    /*左侧导航相关设置*/
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
                                "li_attr": { 'url': 'Wikipedia.aspx' },
                                'state': { 'selected': true }
                            },
                            {
                                "text": d.item2,
                                "li_attr": { 'url': 'Manual.aspx' }
                            },
                             {
                                 "text": d.item3,
                                 "children": [
                                       {
                                           "text": d.item4,
                                           "li_attr": { 'url': 'ForumContent.aspx' }
                                       },
                                       {
                                           "text": d.item5,
                                           "li_attr": { 'url': 'CaseManagement.aspx' }
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
            model: '百科',
            key: 'Tree Title',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.TreeTitle(d);
        });
    }
    /*搜索词条内容后显示的树形词条内容*/
    self.SearchTree = function () {
        self.keywords($("#searchInput").val());
        if (self.keywords()) {
            self.IsSearch(true);
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Wikipedia/tree/Search";
            var detailmethod = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Wikipedia/Content";
            var opts = {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                LimitRows: 100,
                TakeCount: 0,
                Language: SP2013.O365.currentLang(),
                Search: self.keywords()
            }
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
                $("#left-searchResult").jstree('destroy');
                if (d && d.length > 0) {
                    $("#left-searchResult").jstree({
                        'core': {
                            'data': d
                        }
                    }).bind('click.jstree', function (event) {
                        var eventNodeName = event.target.nodeName;
                        if (eventNodeName == "A") {
                            if ($(event.target).parents('li').attr('relatedId')) {
                                self.wikiTitle('');
                                self.wikiContent('');
                                SP2013.Utility.ReturnJson(SP2013.O365.postJSON(detailmethod, {
                                    weburl: SP2013.O365.currentWebAbsoluteUrl(),
                                    listname: "WikipediaLibrary",
                                    Id: $(event.target).parents('li').attr('relatedId'),
                                    Language: SP2013.O365.currentLang(),
                                    Search: self.keywords()
                                })).then(function (detail) {
                                    self.wikiTitle(detail.Title);
                                    self.wikiContent(detail.HtmlContent);
                                    self.loadComments(detail.UniqueId);
                                    self.CurrentcontentId(detail.Id);
                                    self.CurrentcategoryId(detail.CategoryId);
                                });
                            }
                        }
                    });
                    var liId = d[0].li_attr.relatedId;
                    if (liId) {
                        self.wikiTitle('');
                        self.wikiContent('');
                        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(detailmethod, {
                            weburl: SP2013.O365.currentWebAbsoluteUrl(),
                            listname: "WikipediaLibrary",
                            Id: liId,
                            Language: SP2013.O365.currentLang(),
                            Search: self.keywords()
                        })).then(function (detail) {
                            self.wikiTitle(detail.Title);
                            self.wikiContent(detail.HtmlContent);
                            self.loadComments(detail.UniqueId);
                            self.CurrentcontentId(detail.Id);
                            self.CurrentcategoryId(detail.CategoryId);
                        });
                    }
                }
                else {
                    $("#left-searchResult").text("对不起，搜索不到相关的词条");
                }
            });
        }
        else {
            self.InitWikiTree();
        }
    }
    self.CommentsPayBackDisplay = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Reply' : '回复');
    /*加载评论*/
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
            self.TotalCount(d);
            self.initPagination();
        });
    }
    /*获取评论*/
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
                    self.CommentsArray.push(new commentEntity(item, self));
                });
            }
            //$("#wikicontent").getNiceScroll().resize();
        });
    }
    /*添加评论*/
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
    /*页面切换显示*/
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
    self.Navigate2Root = function () {
        window.location.href = SP2013.O365.currentWebAbsoluteUrl();
    }
    self.ReturnTree = function () {
        self.InitWikiTree();
    }
}


/*设置百科模块的宽度包括词条树以及内容*/
var WindowWidth = $(window).width();
$("#focuswin-wikinav").css("width", 200);
$("#wikicontent").css("width", WindowWidth - 285 - $("#focuswin-wikinav").width());

$(window).resize(function () {
    var WindowWidth = $(window).width();
    $("#wikicontent").css("width", WindowWidth - 285 - $("#focuswin-wikinav").width());
});
/*设置百科模块的高度*/
var WindowHeight = $(window).height();
HeaderHeight = $("#nav").height();

$("#wikicontent").css("height", WindowHeight - HeaderHeight);
$(".focuswin-wikinav").css("height", WindowHeight - HeaderHeight);

$(window).resize(function () {
    var WindowHeight = $(window).height();
    $("#wikicontent").css("height", WindowHeight - HeaderHeight);
    $(".focuswin-wikinav").css("height", WindowHeight - HeaderHeight);
});



//$("#wikicontent").niceScroll({ autohidemode: false });

$("#focuswin-wikinav").resizable({
    containment: "#focuswin-content",
    maxHeight: $("#focuswin-content").height(),
    minHeight: $("#focuswin-content").height(),
    minWidth: 200,
    resize: function (event, ui) {
        var WindowWidth = $(window).width();
        $("#wikicontent").css("width", WindowWidth - 285 - $("#focuswin-wikinav").width());
    }
});

var WikiMgr = new wikiMgr();
WikiMgr.validateUser();
WikiMgr.InitLeftNav();
WikiMgr.Load();
ko.applyBindings(WikiMgr, document.getElementById('wikipedia'));

$("#searchInput").keydown(function (event) {
    var activeElementId = document.activeElement.id;//当前处于焦点的元素的id
    if (event.keyCode == 13 && activeElementId == 'searchInput') {
        WikiMgr.SearchTree();
    }
})