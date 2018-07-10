/*通告实体*/
var announcmentEntity = function (data) {
    var entity = this;
    entity.Title = ko.observable();
    entity.Content = ko.observable();
    entity.Modified = ko.observable();
    if (data) {
        entity.Title(data.Title);
        entity.Content(data.Content);
        entity.Modified(SP2013.Utility.formateFloatDate(data.Modified, 'mediumDate'));
    }
}
//var myScroll = new IScroll('#announcement-content', {
//    scrollbars: true,
//    mouseWheel: true,
//    interactiveScrollbars: true,
//    shrinkScrollbars: 'scale',
//    fadeScrollbars: true,
//    disableTouch: true
//});

$('#announcement-content').niceScroll({ autohidemode:false});


var announcementMgr = function () {
    var self = this;
    self.DataAttr = ko.observableArray();
    self.TitleDisplay = ko.observable();
    self.MoreVisible = ko.observable(false);
    self.WikiEnable = ko.observable(false);
    self.WikiDisplay = ko.observable();

    self.ManualEnable = ko.observable(false);
    self.ManualDisplay = ko.observable();

    self.ForumEnable = ko.observable(false);
    self.ForumDisplay = ko.observable();

    self.PageIndex = ko.observable(1);
    self.RowLimit = ko.observable(4);

    self.Load = function () {
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '通知公告',
            key: 'Head Title',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.TitleDisplay(d);
        });

        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '通知公告',
            key: 'Wiki Title',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.WikiDisplay(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '通知公告',
            key: 'Manual Title',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.ManualDisplay(d);
        });
        SP2013.Utility.ReturnJson(SP2013.O365.resourceId({
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            model: '通知公告',
            key: 'Forum Title',
            lang: SP2013.O365.currentLang()
        })).then(function (d) {
            self.ForumDisplay(d);
        });
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Announcement/items";
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            LimitRows: self.RowLimit(),
            TakeCount: self.PageIndex(),
            listname: "Announcements",
            Language: SP2013.O365.currentLang()
        })).then(function (d) {
            self.DataAttr.removeAll();
            if (d && d.length > 0) {
                if (d.length >= self.RowLimit()) {
                    self.MoreVisible(true);
                }
                else {
                    self.MoreVisible(false);
                }
                $.each(d, function (i, item) {
                    self.DataAttr.push(new announcmentEntity(item));
                });
                $('#announcement-content').getNiceScroll().resize();
            } else {
                self.MoreVisible(false);
            }
        });
    }
    /*获取更多公告*/
    self.loadmore = function () {
        if (this.y == this.maxScrollY) {
            var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Announcement/items";
            SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, {
                weburl: SP2013.O365.currentWebAbsoluteUrl(),
                LimitRows: self.RowLimit(),
                TakeCount: self.PageIndex() + 1,
                listname: "Announcements",
                Language: SP2013.O365.currentLang()
            })).then(function (d) {
                
                if (d && d.length > 0) {
                    if (d.length >= self.RowLimit()) {
                        self.MoreVisible(true);
                    }
                    else {
                        self.MoreVisible(false);
                    }
                    self.PageIndex(self.PageIndex() + 1);
                    $.each(d, function (i, item) {
                        self.DataAttr.push(new announcmentEntity(item));
                    });
                    $('#announcement-content').getNiceScroll().resize();
                } else {
                    self.MoreVisible(false);
                }
            });
        }
    }
    self.Navigate2Page = function (page) {
        window.location.href = page;
    }

    self.enablewiki = function () {
        self.WikiEnable(true);
    }
    self.disablewiki = function () {
        self.WikiEnable(false);
    }
    self.enablemanual = function () {
        self.ManualEnable(true);
    }
    self.disablemanual = function () {
        self.ManualEnable(false);
    }
    self.enableforum = function () {
        self.ForumEnable(true);
    }
    self.disableforum = function () {
        self.ForumEnable(false);
    }
}

var frameheight = $(".project-frame-main").height();
if ((frameheight - 76) < 500)
    $("#announcement .content").css("height", frameheight - 86);
/*判断是否显示滑动块*/
$(window).resize(function () {
    var frameheight = $(".project-frame-main").height();
    if ((frameheight - 76) < 500)
        $("#announcement .content").css("height", frameheight - 86);
});

var AnnouncementMgr = new announcementMgr();
AnnouncementMgr.Load();
ko.applyBindings(AnnouncementMgr, document.getElementById('announcement'));