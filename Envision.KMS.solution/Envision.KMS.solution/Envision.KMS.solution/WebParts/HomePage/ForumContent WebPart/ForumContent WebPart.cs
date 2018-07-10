using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Forum;

namespace Envision.KMS.solution.WebParts.HomePage.ForumContent_WebPart
{
    [ToolboxItemAttribute(false)]
    public class ForumContent_WebPart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/Envision.KMS.Solution/Forum/ForumContentCtrl.ascx";
        protected override void CreateChildControls()
        {
            ForumContentCtrl control = Page.LoadControl(_ascxPath) as ForumContentCtrl;
            Controls.Add(control);
        }
    }
}
