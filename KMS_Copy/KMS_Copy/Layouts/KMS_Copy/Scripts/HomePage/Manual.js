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
        entity.UniqueId(data.UniqueId);
        entity.Created(data.Created);
    }
}

var manualdocfolderEntity = function (data, parent) {
    var entity = this;
    entity.Title = ko.observable();
    entity.Created = ko.observable();
    entity.UniqueId = ko.observable();
    entity.DocIcon = ko.observable();
    entity.click2Detail = function () {
        parent.ClickManualDocDetail(entity.UniqueId());
    }
    if (data) {
        entity.Title(data.Title);
        entity.Created(data.Created);
        entity.UniqueId(data.UniqueId);
        enti.DocIcon(data.DocIcon);
    }
}

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
                    allowMaximized: true
                }
                window.open(linkurl, "_blank");
            }
        });
    }
    if (data) {
        entity.Title(data.Title);
        entity.name(data.name);
        entity.path(data.path);
        entity.Created(SP2013.Utility.formatData(data.Created));
        entity.UniqueId(data.UniqueId);
        entity.PreviewImage(data.PreviewImage);
    }
}

var manualdocEntity = function (data) {
    var entity = this;
    entity.Title = ko.observable();
    entity.Url = ko.observable();
    entity.Created = ko.observable();
    entity.Author = ko.observable();
    entity.DocType = ko.observable();
    if (data) {
        entity.Title(data.Title);
        entity.Created(SP2013.Utility.formatData(data.Created, ""));
        entity.Author(data.Author);
        if (data.Title.split('.')[1].toLowCase() == "mp3") {
            entity.Url(_spPageContextInfo.webAbsoluteUrl + "/Pages/Projects/VideoPlayer.aspx?extSrc=" + data.DocUrl + "&type=mp3&title=" + data.Title + "&uniqueId=" + data.Id);
        }
        else if (data.Title.split('.')[1].toLowCase() == "mp4") {
            entity.Url(_spPageContextInfo.webAbsoluteUrl + "/Pages/Projects/VideoPlayer.aspx?extSrc=" + data.DocUrl + "&img=" + data.DocIcon + "&uniqueId=" + data.UniqueId);
        }
        else {
            entity.Url(data.DocUrl + "?web=1");
        }
        entity.DocType(data.DocIcon);
    }
}

var manualhtmlEntity = function (data) {
    var entity = this;
    entity.Title = ko.observable();
    entity.Modified = ko.observable();
    entity.Url = ko.observable();
    if (data) {
        entity.Title(data.Title);
        entity.Modified(SP2013.Utility.formatData(data.Modified, 'isoDate'));;
        if (data.Linkurl) {
            entity.Url(data.Linkurl);
        }
        else {
            entity.Url(SP2013.Utility.concatUrl(SP2013.O365.currentWebAbsoluteUrl(), "/pages/Projects/ManualDetail.aspx?manualId=" + data.Id));
        }
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
    self.DocsFolderArray = ko.observableArray();
    self.DocsFileArray = ko.observableArray();
    self.ManualDocsArray = ko.observableArray();
    self.ManualFoldersArray = ko.observableArray();
    self.ManualTotal = ko.observable(0);
    self.isLoadManualFile = true;
    self.subCount = 0;
    self.subLastItem = ko.observable();
    self.subLastItemArray = ko.observableArray();
    self.Load = function () {
        SP2013.Utility.ReutrnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '页首',
            key: 'Head Title',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.NavTitle(d);
        });
        self.LoadManualLang();
        var method = "/_vti_bin/KMSServices/Global.svc/KMS/Manual/Video/Folders";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'VideoLibrary',
            Language: SP2013.O365.currentLang()
        };
        self.DataArray.removeAll();
        SP2013.Utility.ReutrnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
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
    self.Loadvideos = function (folderId) {
        $("#VideoPage").hide();
        $("#VideoSubPage").show();

        var method = "/_vti_bin/KMSServices/Global.svc/kms/Manual/Video/Folder";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'VideoLibrary',
            Id: folderId,
            Language: SP2013.O365.currentLang()
        };
        self.DetailArray.removeAll();
        SP2013.Utility.ReutrnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    self.DetailArray.push(new manualVideoEntity(item));
                });
            }
        });
    }

    self.LoadvideosByCreated = function () {
        var method = "/_vti_bin/KMSServices/Global.svc/KMS/Manual/Videos/Created";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'VideoLibrary',
            Language: SP2013.O365.currentLang()
        };
        self.ListDataArray.removeAll();
        SP2013.Utility.ReutrnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    self.ListDataArray.push(new manualVideoEntity(item));
                });
            }
        });
    }

    self.LoadDocsByCreated = function () {
        var method = "/_vti_bin/KMSServices/Global.svc/KMS/Manual/Docs/Created";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'ManualDocs',
            Language: SP2013.O365.currentLang()
        }

        self.DocsArray.removeAll();
        SP2013.Utility.ReutrnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    self.DocsArray.push(new manualdocEntity(item));
                });
            }
        });
    }

    self.LoadDocsByFolderId = function (folderId) {
        var method = "/_vti_bin/KMSServices/Global.svc/KMS/Manual/Docs/Folder";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'ManualDocs',
            Language: SP2013.O365.currentLang(),
            Id: folderId
        }
        self.DocsArray.removeAll();
        SP2013.Utility.ReutrnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    self.DocsArray.push(new manualdocEntity(item));
                });
            }
        });
    }

    self.LoadHtmlByCreated = function () {
        var method = "/_vti_bin/KMSServices/Global.svc/KMS/Manual/Html/Created";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'ManualHtmlLibrary',
            Language: SP2013.O365.currentLang()
        }

        self.HtmlArray.removeAll();
        SP2013.Utility.ReutrnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    self.HtmlArray.push(new manualhtmlEntity(item));
                });
            }
        });
    }

    self.LoadHtmlManualTotal = function () {
        var method = "/_vti_bin/KMSServices/Global.svc/KMS/Manual/TotalCount";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'ManualHtmlLibrary'
        }

        self.ManualTotal(0);
        SP2013.Utility.ReutrnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                self.ManualTotal(d);
            }
        });
    }

    self.LoadManualLang = function () {
        var method = "/_vti_bin/KMSServices/Global.svc/KMS/Global/Manual/ResourceId";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '操作教程',
            lang: SP2013.O365.currentLang()
        }

        SP2013.Utility.ReutrnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $.each(d, function (i, item) {
                    if (item.Title == 'Video Title') {
                        self.VideoText(item.DefaultValue);
                    }
                    else if (item.Title == 'VideoModuleText') {
                        self.VideoModuleText(item.DefaultValue);
                    }
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

    self.InitLeftNav = function () {
        var method = "/_vti_bin/KMSServices/Global.svc/KMS/Global/LeftNav/ResourceId";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '左侧导航',
            lang: SP2013.O365.currentLang()
        }

        SP2013.Utility.ReutrnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
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
                            window.Location.href = $(event.target).parents('li').attr('url');
                        }
                    }
                });
            }
        });
    }

    self.ClickTopCategory = function (id, item, data, isback) {
        self.subLastItem = item;
        if (isback == undefined) {
            self.subCount = self.subCount + 1;
            if (!self.isLoadManualFile) {
                self.subLastItemArray.push(self.subLastItem);
            }
            else {
                self.subLastItem = { Created: "", Title: "Parent", UniqueId: "0" };
                self.subLastItemArray.push(self.subLastItem);
            }
        }
        if (id == "ManualPage") {
            if (item != undefined) {
                var method = "/_vti_bin/KMSServices/Global.svc/KMS/Manual/Docs/GetDataList";
                var opts = {
                    weburl: SP2013.O365.currentWebAbsoluteUrl(),
                    listname: 'ManualDocs',
                    Language: SP2013.O365.currentLang()
                }

                $("#Pagesdiv").show();
                if (item.UniqueId != undefined & item.UniqueId != "0") {
                    opts = {
                        weburl: SP2013.O365.currentWebAbsoluteUrl(),
                        listname: 'ManualDocs',
                        Language: SP2013.O365.currentLang(),
                        Id: item.UniqueId
                    }
                    $("#Pagesdiv").hide();
                }

                self.ManualFoldersArray.removeAll();
                self.ManualDocsArray.removeAll();
                SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
                    if (d) {
                        if (d.Folders != null) {
                            $.each(d.Folders, function (i, item) {
                                self.ManualFoldersArray.push(item);
                            });
                        }

                        if (d.Files != null) {
                            $.each(d.Files, function (i, item) {
                                self.ManualDocsArray.push(new manualdocEntity(item, self));
                            });
                        }

                        if (self.isLoadManualFile) {
                            $("#toppage").hide();
                            $("#" + id).show();
                            self.isLoadManualFile = false;
                        }
                    }
                });
            }
            else {
                $("#" + id).hide();
                $("#toppage").show();
                self.isLoadManualFile = true;
            }
        }
        else {
            $("#toppage").hide();
            $("#" + id).show();
        }
    }


}