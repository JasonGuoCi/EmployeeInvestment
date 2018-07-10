var searchMgr = function () {
    var self = this;
    self.SearchUrl = ko.observable();
    self.SearchText = ko.observable();
    self.GoText = ko.observable(SP2013.O365.currentLang() == '1033' ? 'Go' : '搜索');
    self.IsEnglish = ko.observable(true);

    self.Load = function () {
        var lang = SP2013.O365.currentLang();
        self.IsEnglish(lang == '1033');
        var method = "/_vti_bin/KMSServices/Global.svc/KMS/Global/GetSearchUrl";
        SP2013.Utility.ReturnJson(SP2013.O365.getJSON(method)).then(function (d) {
            if (d) {
                self.SearchUrl(d);
            }
        });
        self.LoadNavigation();
    }

    self.Search = function () {
        if (self.SearchText()) {
            var url = self.SearchUrl() + self.SearchText();
            window.open(encodeURI(url), "_blank");
        }
    }

    /*切换中英文界面 */
    self.switchLang = function (type) {
        if (type == '1033') {
            if (self.IsEnglish()) {
                return;
            }
            else {
                $.cookie('Lang', '1033');
                window.location.reload();
            }
        }
        else {
            if (self.IsEnglish()) {
                $.cookie('Lang', '2025');
                window.location.reload();
            }
            else {
                return;
            }
        }
    }

    self.Navigate2Root = function () {
        window.location.href = SP2013.O365.currentWebAbsoluteUrl();
    }

    /*获取导航栏 */
    self.LoadNavigation = function () {
        var method = "/_vti_bin/KMSServices/Global.svc/KMS/Site/Navigation/items";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'Navigation',
            Language: SP2013.O365.currentLang()
        }
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                $("#rootnav").append(self.GenerateSubMenu(d));
            }
        });
    }

    self.GenerateSubMenu = function (child) {
        var htmlchild = '';
        $.each(child, function (i, item) {
            if (item.Children && item.Children.length > 0) {
                htmlchild = htmlchild + '<li class="dropdown"><a href="javascript:void(0);" class="dropdown-toggle" data-toggle="dropdown">';
                htmlchild = htmlchild + item.Title;
                htmlchild = htmlchild + '</a><ul class="dropdown-menu">';
                htmlchild = htmlchild + self.GenerateSubMenu(item.Children);
                htmlchild = htmlchild + '</ul></li>'
            }
            else {
                htmlchild = htmlchild + '<li>'
                htmlchild = htmlchild + '<a href="' + item.LinkUrl + '">' + item.Title;
                htmlchild = htmlchild + '</a></li>';
            }
        });
        return htmlchild;
    }
}

var searchMgr = new searchMgr();
searchMgr.Load();
ko.applyBindings(searchMgr, document.getElementById('SearchPart'));