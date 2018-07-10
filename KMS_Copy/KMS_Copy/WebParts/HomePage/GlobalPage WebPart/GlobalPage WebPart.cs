using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using KMS_Copy.CONTROLTEMPLATES.KMS_Copy.HomePage;

namespace KMS_Copy.WebParts.HomePage.GlobalPage_WebPart
{
    [ToolboxItemAttribute(false)]
    public class GlobalPage_WebPart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/KMS_Copy/HomePage/GlobalPageCtrl.ascx";
        protected override void CreateChildControls()
        {
            GlobalPageCtrl control = Page.LoadControl(_ascxPath) as GlobalPageCtrl;
            Controls.Add(control);
        }
    }
}
