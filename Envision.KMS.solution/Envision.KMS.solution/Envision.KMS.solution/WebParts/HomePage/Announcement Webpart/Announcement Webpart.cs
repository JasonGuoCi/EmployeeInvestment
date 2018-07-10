using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.HomePage;

namespace Envision.KMS.solution.WebParts.HomePage.Announcement_Webpart
{
    [ToolboxItemAttribute(false)]
    public class Announcement_Webpart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/Envision.KMS.Solution/HomePage/AnnouncementCtrl.ascx";
        protected override void CreateChildControls()
        {
            AnnouncementCtrl control = Page.LoadControl(_ascxPath) as AnnouncementCtrl;
            Controls.Add(control);
        }
    }
}
