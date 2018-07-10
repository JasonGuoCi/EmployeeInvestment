using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.HomePage;

namespace Envision.KMS.solution.WebParts.HomePage.GlobalPage_WebPart
{
    [ToolboxItemAttribute(false)]
    public class GlobalPage_WebPart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/Envision.KMS.Solution/HomePage/GlobalPageCtrl.ascx";
        protected override void CreateChildControls()
        {
            GlobalPageCtrl control = Page.LoadControl(_ascxPath) as GlobalPageCtrl;
            Controls.Add(control);
        }
    }
}
