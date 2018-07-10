/*操作手册*/
var manualfolderEntity = function (data, parent) {
    var entity = this;
    entity.Title = ko.observable();
    entity.Created = ko.observable();
    entity.UniqueId = ko.observable();
    entity.click2Detail = function () {
        parent.Loadvideos(entity.UniqueId());
    }
    if (data) {
        entity.Title(data.Title);
        entity.Created(data.Created);
        entity.UniqueId(data.UniqueId);
    }
}
/*视频操作教程*/
var manualVideoEntity = function (data, parent) {
    var entity = this;
    entity.Title = ko.observable();
    entity.name = ko.observable();
    entity.path = ko.observable();
    entity.Created = ko.observable();
    entity.UniqueId = ko.observable();
    entity.PreviewImage = ko.observable();
    entity.click2Detail = function () {
        var method = "/Web/GetFolderByServerRelativeUrl('" + entity.path() + "')/files?$select=ServerRelativeUrl";
        method = encodeURI(method);
        SP2013.O365.apiGet(method, null, SP2013.O365.currentWebAbsoluteUrl()).then(function (result) {
            if (result && result.d.results.length > 0) {
                var srcurl = result.d.results[0].ServerRelativeUrl;
                var linkurl = _spPageContextInfo.webAbsoluteUrl + "/Pages/Projects/VideoPlayer.aspx?extSrc=" + srcurl + "&img=" + entity.PreviewImage() + "&uniqueId=" + entity.UniqueId();
                var options = {
                    title: entity.Title(),
                    url: encodeURI(linkurl),
                    showMaximized: false,
                    allowMaximize: true,
                }
                window.open(linkurl, "_blank");
                //OpenPopUpPageWithDialogOptions(options);
            }
        });
    }
    if (data) {
        entity.Title(data.Title);
        entity.name(data.Name);
        entity.path(data.Path);
        entity.Created(SP2013.Utility.formatDate(data.Created));
        entity.UniqueId(data.UniqueId);
        entity.PreviewImage(data.PreviewImage);
    }
}
/*文本类操作教程*/
var manualdocEntity = function (data) {
    var entity = this;
    entity.Title = ko.observable();
    entity.Url = ko.observable();
    entity.Created = ko.observable();
    entity.Author = ko.observable();
    entity.DocType = ko.observable();
    if (data) {
        entity.Title(data.Title);
        entity.Created(SP2013.Utility.formatDate(data.Created, ""));
        entity.Author(data.Author);
        entity.Url(data.DocUrl + "?web=1");
        entity.DocType(data.DocIcon);
    }
}
/*网页类操作教程*/
var manualhtmlEntity = function (data) {
    var entity = this;
    entity.Title = ko.observable();
    entity.Url = ko.observable();
    entity.Modified = ko.observable();
    if (data) {
        entity.Title(data.Title);
        entity.Modified(SP2013.Utility.formateFloatDate(data.Modified, 'isoDate'));
        if (data.LinkUrl)
            entity.Url(data.LinkUrl);
        else
            entity.Url(SP2013.Utility.concatUrl(SP2013.O365.currentWebAbsoluteUrl(), "/pages/projects/ManualDetail.aspx?manualId=" + data.Id));
    }
}

var manualMgr = function () {
    var self = this;
    self.NavTitle = ko.observable();
    self.NavWiki = ko.observable();
    self.VideoText = ko.observable();
    self.VideoModuleText = ko.observable();
    self.VideoModuleDesc = ko.observable();
    self.VideoCreatedText = ko.observable();
    self.VideoCreatedDesc = ko.observable();
    self.CategoriesText = ko.observable();
    self.IsEnglishTitle = ko.observable(SP2013.O365.currentLang() == '1033' ? true : false);
    self.VideoCountText = ko.observable();
    self.ManualText = ko.observable();
    self.UserModuleText = ko.observable();
    self.UserModuleDesc = ko.observable();
    self.ManualCountText = ko.observable();

    self.ManualDocText = ko.observable();
    self.ManualHtmlText = ko.observable();

    self.DataArray = ko.observableArray();
    self.DetailArray = ko.observableArray();
    self.ListDataArray = ko.observableArray();
    self.DocsArray = ko.observableArray();
    self.HtmlArray = ko.observableArray();
    self.ManualTotal = ko.observable(0);
    self.Load = function () {
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Head Title',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.NavTitle(d);
        });
        self.LoadManualLang();
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Manual/Video/Folders";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'VideoLibrary',
            Language: SP2013.O365.currentLang()
        }
        self.DataArray.removeAll();
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    self.DataArray.push(new manualfolderEntity(item, self));
                });
            }
        });
        self.InitLeftNav();
        self.LoadHtmlManualTotal();
        self.LoadvideosByCreated();
    }
    /*获取视频教程*/
    self.Loadvideos = function (folderId) {
        $("#VideoPage").toggle('slide', { direction: "up" }, 500);
        setTimeout(function () {
            $("#VideoSubPage").toggle('slide', { direction: "down" }, 500);
        }, 500);
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Manual/Video/Folder";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'VideoLibrary',
            Id: folderId,
            Language: SP2013.O365.currentLang()
        }
        self.DetailArray.removeAll();
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    self.DetailArray.push(new manualVideoEntity(item));
                });
            }
        });
    }
    /*通过创建时间获取视频教程*/
    self.LoadvideosByCreated = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Manual/Videos/Created";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'VideoLibrary',
            Language: SP2013.O365.currentLang()
        }
        self.ListDataArray.removeAll();
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    self.ListDataArray.push(new manualVideoEntity(item));
                });
            }
        });
    }
    /*根据创建时间获取文本类操作教程*/
    self.LoadDocsByCreated = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Manual/Docs/Created";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'ManualDocs',
            Language: SP2013.O365.currentLang()
        }
        self.DocsArray.removeAll();
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    self.DocsArray.push(new manualdocEntity(item));
                });
            }
        });
    }
    /*根据创建时间获取网页类操作教程*/
    self.LoadHtmlByCreated = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Manual/Html/Created";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'ManualHtmlLibrary',
            Language: SP2013.O365.currentLang()
        }
        self.HtmlArray.removeAll();
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    self.HtmlArray.push(new manualhtmlEntity(item));
                });
            }
        });
    }
    /*根据创建时间获取网页类操作教程总数*/
    self.LoadHtmlManualTotal = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Manual/TotalCount";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'ManualHtmlLibrary'
        }
        self.ManualTotal(0);
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                self.ManualTotal(d);
            }
        });
    }
    /*获取操作教程中英文显示*/
    self.LoadManualLang = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Global/Manual/ResourceId";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '操作教程',
            lang: SP2013.O365.currentLang()
        }
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    if (item.Title == 'Video Title')
                        self.VideoText(item.DefaultValue);
                    else if (item.Title == 'VideoModuleText')
                        self.VideoModuleText(item.DefaultValue);
                    else if (item.Title == 'VideoModuleDesc')
                        self.VideoModuleDesc(item.DefaultValue);
                    else if (item.Title == 'CategoriesText')
                        self.CategoriesText(item.DefaultValue);
                    else if (item.Title == 'VideoCreatedText')
                        self.VideoCreatedText(item.DefaultValue);
                    else if (item.Title == 'VideoCreatedDesc')
                        self.VideoCreatedDesc(item.DefaultValue);
                    else if (item.Title == 'VideoCountText')
                        self.VideoCountText(item.DefaultValue);
                    else if (item.Title == 'Manual Title')
                        self.ManualText(item.DefaultValue);
                    else if (item.Title == 'UserModuleText')
                        self.UserModuleText(item.DefaultValue);
                    else if (item.Title == 'UserModuleDesc')
                        self.UserModuleDesc(item.DefaultValue);
                    else if (item.Title == 'ManualCountText')
                        self.ManualCountText(item.DefaultValue);
                    else if (item.Title == 'ManualDocText')
                        self.ManualDocText(item.DefaultValue);
                    else if (item.Title == 'ManualHtmlText')
                        self.ManualHtmlText(item.DefaultValue);
                });
            }
        });

    }
    /*左侧导航设置与显示*/
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
                                'state': { 'selected': true }
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

                //$("#j1_3").mouseover(function () {
                //    $(this).attr("'class", "jstree-node  jstree-last jstree-open");
                //});
                //$("#j1_3").mouseout(function () {
                //    $(this).attr("'class", "jstree-node  jstree-last jstree-closed");
                //});
            }
        });
    }
    /*跳转到子类中*/
    self.ClickTopCategory = function (id) {
        $("#toppage").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#" + id).toggle('drop', { direction: "left" }, 500);
        }, 500);

    }
    /*跳转回父类*/
    self.ReturnTopCategory = function (id) {
        self.LoadvideosByCreated();
        $("#" + id).toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#toppage").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
    /*跳转到视频分类*/
    self.ReturnPage = function () {
        $("#VideoSubPage").toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#VideoPage").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
    /*返回教程主页*/
    self.ReturnManualPage = function (id) {
        $("#" + id).toggle('drop', { direction: "right" }, 500);
        setTimeout(function () {
            $("#ManualPage").toggle('drop', { direction: "left" }, 500);
        }, 500);
    }
    /*跳转到网页文档中*/
    self.ClickManualHtml = function (id) {
        self.LoadHtmlByCreated();
        $("#ManualPage").toggle('drop', { direction: "up" }, 500);
        setTimeout(function () {
            $("#ManualHtml").toggle('drop', { direction: "down" }, 500);
        }, 500);

    }
    /*跳转到文档文件夹中*/
    self.ClickManualDoc = function (id) {
        self.LoadDocsByCreated();
        $("#ManualPage").toggle('drop', { direction: "up" }, 500);
        setTimeout(function () {
            $("#fileContent").toggle('drop', { direction: "down" }, 500);
        }, 500);

    }

    self.Navigate2Root = function () {
        window.location.href = SP2013.O365.currentWebAbsoluteUrl();
    }
}


var ManualMgr = new manualMgr();
ManualMgr.Load();
ko.applyBindings(ManualMgr, document.getElementById('manualcontent'));