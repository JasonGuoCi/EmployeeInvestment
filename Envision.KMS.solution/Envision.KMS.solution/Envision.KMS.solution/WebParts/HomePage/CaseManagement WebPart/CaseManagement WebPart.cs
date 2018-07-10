using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.CaseManagement;

namespace Envision.KMS.solution.WebParts.HomePage.CaseManagement_WebPart
{
    [ToolboxItemAttribute(false)]
    public class CaseManagement_WebPart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/Envision.KMS.Solution/CaseManagement/CaseManagementCtrl.ascx";
        protected override void CreateChildControls()
        {
            CaseManagementCtrl control = Page.LoadControl(_ascxPath) as CaseManagementCtrl;
            Controls.Add(control);
        }
    }
}
