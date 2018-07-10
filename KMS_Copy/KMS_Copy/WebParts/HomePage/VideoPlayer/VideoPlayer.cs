using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using KMS_Copy.CONTROLTEMPLATES.KMS_Copy.Manual;

namespace KMS_Copy.WebParts.HomePage.VideoPlayer
{
    [ToolboxItemAttribute(false)]
    public class VideoPlayer : WebPart
    {
        protected const string _ascxPath = @"~/_controltemplates/15/KMS_Copy/Manual/ucVideoPlayer.ascx";
        protected override void CreateChildControls()
        {
            ucVideoPalyer control = Page.LoadControl(_ascxPath) as ucVideoPalyer;
            Controls.Add(control);

        }
    }
}
