using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Manual;

namespace Envision.KMS.solution.WebParts.HomePage.Manual_WebPart
{
    [ToolboxItemAttribute(false)]
    public class Manual_WebPart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/Envision.KMS.Solution/Manual/ManualContentCtrl.ascx";
        protected override void CreateChildControls()
        {
            ManualContentCtrl control = Page.LoadControl(_ascxPath) as ManualContentCtrl;
            Controls.Add(control);
        }
    }
}
