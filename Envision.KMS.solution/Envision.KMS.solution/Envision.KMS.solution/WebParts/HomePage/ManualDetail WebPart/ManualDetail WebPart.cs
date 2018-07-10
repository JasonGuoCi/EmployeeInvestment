using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Manual;

namespace Envision.KMS.solution.WebParts.HomePage.ManualDetail_WebPart
{
    [ToolboxItemAttribute(false)]
    public class ManualDetail_WebPart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/Envision.KMS.Solution/Manual/ManualHtmlDetailCtrl.ascx";
        protected override void CreateChildControls()
        {
            ManualHtmlDetailCtrl control = Page.LoadControl(_ascxPath) as ManualHtmlDetailCtrl;
            Controls.Add(control);
        }
    }
}
