using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using KMS_Copy.CONTROLTEMPLATES.KMS_Copy.HomePage;

namespace KMS_Copy.WebParts.HomePage.Announcement_Webpart
{
    [ToolboxItemAttribute(false)]
    public class Announcement_Webpart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/KMS_Copy/HomePage/AnnouncementCtrl.ascx";
        
        protected override void CreateChildControls()
        {
            AnnouncementCtrl control = Page.LoadControl(_ascxPath) as AnnouncementCtrl;
            Controls.Add(control);
        }
    }
}
