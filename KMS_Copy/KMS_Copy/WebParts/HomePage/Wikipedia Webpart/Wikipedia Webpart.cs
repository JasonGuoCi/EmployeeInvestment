using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using KMS_Copy.CONTROLTEMPLATES.KMS_Copy.Wikipedia;

namespace KMS_Copy.WebParts.HomePage.Wikipedia_Webpart
{
    [ToolboxItemAttribute(false)]
    public class Wikipedia_Webpart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/KMS_Copy/Wikipedia/ucWikipedia.ascx";
        protected override void CreateChildControls()
        {
            ucWikipedia control = Page.LoadControl(_ascxPath) as ucWikipedia;
            Controls.Add(control);
        }
    }
}
