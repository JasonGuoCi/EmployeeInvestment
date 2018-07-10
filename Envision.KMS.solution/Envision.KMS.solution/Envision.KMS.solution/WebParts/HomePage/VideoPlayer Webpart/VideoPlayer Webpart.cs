using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Manual;

namespace Envision.KMS.solution.WebParts.HomePage.VideoPlayer_Webpart
{
    [ToolboxItemAttribute(false)]
    public class VideoPlayer_Webpart : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/Envision.KMS.Solution/Manual/VideoPlayerCtrl.ascx";
        protected override void CreateChildControls()
        {
            VideoPlayerCtrl control = Page.LoadControl(_ascxPath) as VideoPlayerCtrl;
            Controls.Add(control);
        }
    }
}
