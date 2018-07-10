using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Wikipedia;

namespace Envision.KMS.solution.WebParts.HomePage.Wikipedia_Webpart
{
    [ToolboxItemAttribute(false)]
    public class Wikipedia_Webpart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/Envision.KMS.Solution/Wikipedia/WikipediaCtrl.ascx";
        protected override void CreateChildControls()
        {
            WikipediaCtrl control = Page.LoadControl(_ascxPath) as WikipediaCtrl;
            Controls.Add(control);
        }
    }
}
