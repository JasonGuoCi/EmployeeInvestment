using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Envision.KMS.solution.ControlTemplates.Envision.KMS.Solution.Manual
{
    public partial class VideoPlayerCtrl : UserControl
    {
        public string src;
        public string imgsrc;
        protected void Page_Load(object sender, EventArgs e)
        {
            src = Request.Params["extSrc"] + "";
            imgsrc = Request.Params["img"] + "";
            //idPageMediaPlayer.MediaSource = src;
            //idPageMediaPlayer.PreviewImageSource = imgsrc;
        }
    }
}
