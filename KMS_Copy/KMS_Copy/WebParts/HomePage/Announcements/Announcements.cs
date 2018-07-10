using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using KMS_Copy.CONTROLTEMPLATES.KMS_Copy.HomePage;

namespace KMS_Copy.WebParts.HomePage.Announcements
{
    [ToolboxItemAttribute(false)]
    public class Announcements : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/KMS_Copy/HomePage/AnnouncementCtrlCopy.ascx";
        protected override void CreateChildControls()
        {
            AnnouncementCtrlCopy control = Page.LoadControl(_ascxPath) as AnnouncementCtrlCopy;
            Controls.Add(control);
        }
    }
}
