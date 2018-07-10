/*交流话题*/
var ForumEntity = function (data, parent) {
    var entity = this;
    entity.Id = ko.observable();
    entity.UniqueId = ko.observable();
    entity.Title = ko.observable();
    entity.Created = ko.observable();
    entity.Modified = ko.observable();
    entity.Author = ko.observable();
    entity.ForumType = ko.observable();
    entity.ForumTypeDisplay = ko.observable();
    entity.IsTop = ko.observable();
    entity.UniqueId = ko.observable();
    entity.Picurl = ko.observable();
    entity.ViewCount = ko.observable();
    entity.CommentsCount = ko.observable();
    entity.ImgSource = ko.observable();
    entity.click2Detail = function (IsAuthor) {
        parent.LoadForumById(entity.Id());
        parent.detailforum();
        parent.CurrentwikiId(entity.UniqueId());
        parent.CurrentTitle(entity.Title());
        parent.CurrentId(entity.Id());
        parent.InsertViewCount(entity.Id());
        parent.loadComments(entity.UniqueId());
        parent.CurrentTop(entity.IsTop() == '1');
        parent.IsAuthor(IsAuthor == 'true');
    }
    if (data) {
        entity.Id(data.Id);
        entity.UniqueId(data.UniqueId);
        entity.Title(data.Title);
        entity.Created(SP2013.Utility.formatDate(data.Created));
        entity.Modified(SP2013.Utility.formatDate(data.Modified));
        entity.UniqueId(data.UniqueId);
        entity.Author(data.Author);
        entity.ForumType(data.ForumType);
        entity.ForumTypeDisplay(data.ForumTypeDisplay);
        entity.IsTop(data.IsTop);
        entity.Picurl(data.PicUrl);
        entity.ViewCount(data.ViewCount);
        entity.CommentsCount(data.CommentsCount);
        entity.ImgSource(data.IsTop == '1' ? '/_layouts/15/Envision.KMS.Solution/Images/Icon_Top20.png' : '/_layouts/15/Envision.KMS.Solution/Images/Icon_Post20.png');
    }
}
/*话题标签*/
var ForumTypeEntity = function (data, parent) {
    var entity = this;
    entity.Id = ko.observable();
    entity.Title = ko.observable();
    entity.Ischecked = ko.observable(false);
    entity.ActiveTag = function () {
        $.each(parent.TypeArray(), function (i, item) {
            if (item.Id() == entity.Id()) {
                entity.Ischecked(!entity.Ischecked());
            }
            else {
                item.Ischecked(false);
            }
        });
        parent.Topcount(0);
        parent.LoadForums();
        parent.LoadForumsCount();
    }
    if (data) {
        entity.Id(data.Id);
        entity.Title(data.Title);
    }
}
/*评论*/
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
                var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/Comments/Delete";
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
    /*回复*/
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

var forumMgr = function () {
    var self = this;

    //中英文切换
    self.viewDisplay = ko.observable();
    self.commentsDisplay = ko.observable();

    self.ListCategory = ko.observable(2);
    self.CurrentwikiId = ko.observable(false);
    self.CurrentTop = ko.observable();
    self.CurrentTitle = ko.observable();
    self.CurrentId = ko.observable();
    self.IsAdmin = ko.observable(false);
    self.IsAuthor = ko.observable(false);
    self.IsSearch = ko.observable(false);
    self.LimitRows = ko.observable(8);
    self.LimitCommentsRows = ko.observable(8);
    self.keywords = ko.observable();
    self.Topcount = ko.observable(0);
    self.CommentsTopcount = ko.observable(1);
    self.IsEnglishTitle = ko.observable(SP2013.O365.currentLang() == '1033' ? true : false);
    self.TotalCount = ko.observable(0);
    self.TotalCommentsCount = ko.observable(0);
    function pageselectCallback(page_index, jq) {
        self.Topcount(page_index + 1);
        self.LoadForums();
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

    function CommentspageselectCallback(page_index, jq) {
        self.CommentsTopcount(page_index + 1);
        self.InitComments();
    }
    self.initCommentsPagination = function () {
        $("#Pagination2").pagination(self.TotalCommentsCount(), {
            callback: CommentspageselectCallback,
            items_per_page: self.LimitRows(),
            prev_text: SP2013.O365.currentLang() == '1033' ? 'Previous' : "前一页",
            next_text: SP2013.O365.currentLang() == '1033' ? 'Next' : "后一页",
            num_edge_entries: 1,
            start_page: SP2013.O365.currentLang() == '1033' ? 'First' : "首页",
            end_page: SP2013.O365.currentLang() == '1033' ? 'Last' : "末页",
            num_display_entries: 10
        });
    }

    self.NavTitle = ko.observable();
    self.NavWiki = ko.observable();
    self.DataArray = ko.observableArray();
    self.TypeArray = ko.observableArray();

    self.NewFormTypeArray = ko.observableArray();
    self.NewFormTitle = ko.observable();
    self.NewFormContent = ko.observable();
    /*添加话题*/
    self.InsertForum = function () {
        var category = '';
        if (self.NewFormTypeArray().length > 0) {
            $.each(self.NewFormTypeArray(), function (i, item) {
                if (item.Ischecked())
                    category = category + item.Id() + ";";
            });
        }
        if (category) {

            if (self.NewFormTitle() && category) {
                var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/Insert";
                SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                    weburl: SP2013.O365.currentWebAbsoluteUrl(),
                    Title: self.NewFormTitle(),
                    listname: "Forum",
                    Content: self.NewFormContent(),
                    ForumType: category
                })).then(function (d) {
                    if (d) {
                        self.NewFormTitle('');
                        self.NewFormContent('')
                        $.each(self.NewFormTypeArray(), function (i, item) {
                            item.Ischecked(false);
                        });
                        self.listforum();
                        self.LoadForums();
                        self.LoadForumsCount();
                    }
                });
            }
            else {
                alert("请填写完整");
            }
        }
        else {
            alert("请勾选一个或多个话题对应的标签");
        }
    }

    self.EditFormTitle = ko.observable();
    self.EditFormContent = ko.observable();
    self.EditFormTypeArray = ko.observableArray();
    /*编辑话题*/
    self.EditForum = function () {
        var category = '';
        if (self.EditFormTypeArray().length > 0) {
            $.each(self.EditFormTypeArray(), function (i, item) {
                if (item.Ischecked())
                    category = category + item.Id() + ";";
            });
        }
        if (category) {
            if (self.EditFormTitle() && category) {
                var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/Edit";
                SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                    weburl: SP2013.O365.currentWebAbsoluteUrl(),
                    Title: self.EditFormTitle(),
                    listname: "Forum",
                    Content: self.EditFormContent(),
                    ForumType: category,
                    Id: self.CurrentId()
                })).then(function (d) {
                    if (d) {
                        self.CurrentId('');
                        self.EditFormTitle('');
                        self.EditFormContent('')
                        $.each(self.EditFormTypeArray(), function (i, item) {
                            item.Ischecked(false);
                        });
                        self.LoadForums();
                        self.LoadForumsCount();
                        self.editreturnforum();
                    }
                });
            }
            else {
                alert("请填写完整");
            }
        }
        else {
            alert("请勾选一个或多个话题对应的标签");
        }
    }
    /*记录浏览次数*/
    self.InsertViewCount = function (ItemId) {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/View";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "Forum",
            Id: ItemId
        })).then(function (d) {
            if (d)
                self.listforum();
        });
    }
    /*判断用户是否为管理员*/
    self.validateUser = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Admin/Validation";
        SP2013.Utility.ReturnJson(SP2013.O365.getJSON(method)).then(function (d) {
            if (d)
                self.IsAdmin(d);
        });
    }

    self.detailTitle = ko.observable();
    self.detailContent = ko.observable();
    self.detailType = ko.observable();

    self.CommentsTitle = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Comments' : '评论');
    self.CommentsBtn = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Submit' : '提交评论');
    self.CommentsArray = ko.observableArray();
    self.CommentsPayBackDisplay = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Reply' : '回复');
    /*加载评论*/
    self.loadComments = function (itemId) {
        self.CurrentwikiId(itemId);
        self.InitCommentsCount();
        self.InitComments();
    }
    /*获取评论数量*/
    self.InitCommentsCount = function () {
        self.TotalCommentsCount(0);
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/Comments/ItemsCount";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            Id: self.CurrentwikiId()
        })).then(function (d) {
            self.TotalCommentsCount(d);
            self.initCommentsPagination();
        });
    }
    /*获取评论*/
    self.InitComments = function () {
        self.CommentsArray.removeAll();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/Comments/Items";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            Id: self.CurrentwikiId(),
            LimitRows: self.LimitCommentsRows(),
            TakeCount: self.CommentsTopcount()
        })).then(function (d) {
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.CommentsArray.push(new commentEntity(item, self));
                });
            }
            //detailScroll.refresh();
        });
    }
    /*添加评论*/
    self.submitcomments = function () {
        var comments = tinymce.activeEditor.getContent();
        if (comments) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/Comments/Insert";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                Id: self.CurrentwikiId(),
                Search: comments,
                listname: "Forum",
                CategoryId: self.CurrentId(),
                WikiId: self.CurrentTitle()
            })).then(function (d) {
                tinymce.activeEditor.setContent('');
                self.loadComments(self.CurrentwikiId());
            });
        }
    }
    /*切换tab*/
    self.switchTab = function (type) {
        self.ListCategory(type);
        self.Topcount(0);
        self.LoadForums();
    }
    /*加载论坛*/
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
            key: 'view display',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.viewDisplay(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '论坛',
            key: 'reply display',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.commentsDisplay(d);
        });
        self.LoadForumsTypes();
        self.LoadForums();
        self.InitLeftNav();
        self.LoadForumsCount();
    }
    /*根据不同的条件加载话题*/
    self.LoadForums = function () {
        self.DataArray.removeAll();
        var category = '';
        if (self.TypeArray().length > 0) {
            $.each(self.TypeArray(), function (i, item) {
                if (item.Ischecked())
                    category = category + item.Id() + ";";
            });
        }
        self.IsSearch(false);
        if (self.ListCategory() == 1) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/Items";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                CategoryId: category,
                listname: "Forum",
                LimitRows: self.LimitRows(),
                TakeCount: self.Topcount(),
                Search: self.keywords()
            })).then(function (d) {
                if (d && d.length > 0) {
                    $.each(d, function (i, item) {
                        self.DataArray.push(new ForumEntity(item, self));
                    });
                } else {
                    if (self.keywords()) {
                        self.IsSearch(true);
                    }
                }
            });
        } else if (self.ListCategory() == 2) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/Items/Modified";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                CategoryId: category,
                listname: "Forum",
                LimitRows: self.LimitRows(),
                TakeCount: self.Topcount(),
                Search: self.keywords()
            })).then(function (d) {
                if (d && d.length > 0) {
                    $.each(d, function (i, item) {
                        self.DataArray.push(new ForumEntity(item, self));
                    });
                } else {
                    if (self.keywords()) {
                        self.IsSearch(true);
                    }
                }
            });
        } else if (self.ListCategory() == 3) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/ForumAuthor/Items";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                CategoryId: category,
                listname: "Forum",
                LimitRows: self.LimitRows(),
                TakeCount: self.Topcount(),
                Search: self.keywords()
            })).then(function (d) {
                if (d && d.length > 0) {
                    $.each(d, function (i, item) {
                        self.DataArray.push(new ForumEntity(item, self));
                    });
                } else {
                    if (self.keywords()) {
                        self.IsSearch(true);
                    }
                }
            });
        } else if (self.ListCategory() == 4) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/ForumReply/Items";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                CategoryId: category,
                listname: "Forum",
                LimitRows: self.LimitRows(),
                TakeCount: self.Topcount(),
                Search: self.keywords()
            })).then(function (d) {
                if (d && d.length > 0) {
                    $.each(d, function (i, item) {
                        self.DataArray.push(new ForumEntity(item, self));
                    });
                } else {
                    if (self.keywords()) {
                        self.IsSearch(true);
                    }
                }
            });
        }
    }
    /*加载点击的话题*/
    self.LoadForumById = function (ItemId) {
        self.detailTitle('');
        self.detailContent('');
        var category = '';
        if (self.TypeArray().length > 0) {
            $.each(self.TypeArray(), function (i, item) {
                if (item.Ischecked())
                    category = category + item.Id() + ";";
            });
        }
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/item";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            CategoryId: category,
            listname: "Forum",
            Id: ItemId,
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            if (d) {
                self.detailTitle(d.Title);
                self.detailContent(d.Content);
                self.detailType(d.ForumType);
                self.EditFormTitle(d.Title);
                self.EditFormContent(d.OrginContent);
                if (d.ForumType) {
                    var typearr = d.ForumType.split(';');
                    $.each(self.EditFormTypeArray(), function (i, item) {
                        $.each(typearr, function (i, type) {
                            if (item.Id() == type) {
                                item.Ischecked(true);
                            }
                        });
                    });
                }
                //detailScroll.refresh();
            }
        });

    }
    /*加载话题数量*/
    self.LoadForumsCount = function () {
        self.TotalCount(0);
        var category = '';
        if (self.TypeArray().length > 0) {
            $.each(self.TypeArray(), function (i, item) {
                if (item.Ischecked())
                    category = category + item.Id() + ";";
            });
        }
        if (self.ListCategory() == 1 || self.ListCategory() == 2) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/itemscount";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                CategoryId: category,
                listname: "Forum",
                Search: self.keywords()
            })).then(function (d) {
                if (d) {
                    self.TotalCount(d);
                    self.initPagination();
                }
            });
        } else if (self.ListCategory() == 3) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/ForumAuthor/itemscount";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                CategoryId: category,
                listname: "Forum",
                Search: self.keywords()
            })).then(function (d) {
                if (d) {
                    self.TotalCount(d);
                    self.initPagination();
                }
            });
        } else if (self.ListCategory() == 4) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/ForumReply/itemscount";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                CategoryId: category,
                listname: "Forum",
                Search: self.keywords()
            })).then(function (d) {
                if (d) {
                    self.TotalCount(d);
                    self.initPagination();
                }
            });
        }
    }
    /*加载话题标签*/
    self.LoadForumsTypes = function () {
        self.TypeArray.removeAll();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/type";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "ForumType",
            LimitRows: 100,
            TakeCount: 0,
            Language: SP2013.O365.currentLang()
        })).then(function (d) {
            if (d && d.length > 0) {
                $.each(d, function (i, item) {
                    self.TypeArray.push(new ForumTypeEntity(item, self));
                    self.NewFormTypeArray.push(new ForumTypeEntity(item, self));
                    self.EditFormTypeArray.push(new ForumTypeEntity(item, self));
                });
            }
        });
    }
    /*左侧导航栏设置*/
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
                                           "li_attr": { 'url': 'ForumContent.aspx' },
                                           'state': { 'selected': true }
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
    }

    self.SearchTree = function () {
        self.keywords($("#searchInput").val());

        self.Topcount(0);
        self.LoadForums();
        self.LoadForumsCount();
    }

    self.Navigate2Root = function () {
        window.location.href = SP2013.O365.currentWebAbsoluteUrl();
    }
    /*添加话题*/
    self.newforum = function () {
        $("#forumlist").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#newform").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }

    self.listforum = function () {
        $("#newform").hide();
        setTimeout(function () {
            $("#forumlist").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }

    self.detailforum = function () {
        $("#tabform").hide();
        setTimeout(function () {
            $("#newform").hide();
            $("#detailform").show();
        }, 500);
    }

    self.detailreturnforum = function () {
        $("#detailform").hide();
        self.reloadItem();
        setTimeout(function () {
            $("#newform").hide();
            $("#tabform").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }

    self.editTransfer = function () {

        $("#tabform").hide();
        $("#detailform").hide();
        setTimeout(function () {
            $("#newform").hide();
            $("#editform").show();
        }, 500);
    }

    self.editreturnforum = function () {
        $("#editform").hide();
        setTimeout(function () {
            $("#newform").hide();
            $("#tabform").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }

    self.deleteItem = function () {
        var r = confirm("确认删除该贴？");
        if (r == true) {
            if (self.CurrentId()) {
                var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/Delete";
                SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                    weburl: SP2013.O365.currentWebAbsoluteUrl(),
                    Id: self.CurrentId(),
                    listname: "Forum",
                })).then(function (d) {
                    if (d) {
                        self.LoadForums();
                        self.LoadForumsCount();
                        self.detailreturnforum();
                    }
                });
            }
        }
    }

    self.SetTop = function (type) {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/SetTop";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: "Forum",
            Search: type,
            Id: self.CurrentId()
        })).then(function (d) {
            if (d == 1) {
                self.CurrentTop(type == '1');
                self.LoadForums();
            }
        });
    }

    self.reloadItem = function () {
        $.each(self.DataArray(), function (i,item) {
            if (item.Id() == self.CurrentId())
            {
                var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Forum/item";
                SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                    weburl: SP2013.O365.currentWebAbsoluteUrl(),
                    listname: "Forum",
                    Id: item.Id(),
                    lang: SP2013.O365.currentLang()
                })).then(function (d) {
                    if (d) {
                        item.ViewCount(d.ViewCount);
                        item.CommentsCount(d.CommentsCount);
                    }
                });
                return;
            }
        });
    }
}

//var detailScroll = new IScroll('#detailform', {
//    scrollbars: true,
//    mouseWheel: true,
//    interactiveScrollbars: true,
//    shrinkScrollbars: 'scale',
//    fadeScrollbars: true,
//    disableTouch: true
//});

var ForumMgr = new forumMgr();
ForumMgr.validateUser();
ForumMgr.Load();
ko.applyBindings(ForumMgr, document.getElementById('forumcontent'));

$("#searchInput").keydown(function (event) {
    var activeElementId = document.activeElement.id;//当前处于焦点的元素的id
    if (event.keyCode == 13 && activeElementId == 'searchInput') {
        ForumMgr.SearchTree();
    }
})