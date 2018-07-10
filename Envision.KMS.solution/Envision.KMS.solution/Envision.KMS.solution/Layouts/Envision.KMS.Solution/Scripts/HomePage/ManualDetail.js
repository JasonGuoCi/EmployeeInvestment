var manualDetailMgr = function () {
    var self = this;
    self.Title = ko.observable();
    self.ContentHtml = ko.observable();
    self.Modified = ko.observable();
    self.Load = function () {
        var method = "/_vti_bin/Envision.KMS.Service/KMS.Global.svc/KMS/Manual/Html/Detail";
        var opts = {
            weburl: SP2013.O365.currentWebAbsoluteUrl(),
            listname: 'ManualHtmlLibrary',
            Id: SP2013.Utility.queryString.manualId,
            Language: SP2013.O365.currentLang()
        }
        SP2013.Utility.ReturnJson(SP2013.O365.postJSON(method, opts)).then(function (d) {
            if (d) {
                self.Title(d.Title);
                self.ContentHtml(d.Content);
                self.Modified(SP2013.Utility.formatDate(d.Modified));
            }
        });
    }
}

var ManualDetailMgr = new manualDetailMgr();
ManualDetailMgr.Load();
ko.applyBindings(ManualDetailMgr, document.getElementById('manualdetail'));