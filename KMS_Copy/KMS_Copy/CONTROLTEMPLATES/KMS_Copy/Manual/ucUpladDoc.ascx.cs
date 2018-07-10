using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace KMS_Copy.CONTROLTEMPLATES.KMS_Copy.Manual
{
    public partial class ucUpladDoc : UserControl
    {
        public string src;
        public string imgsrc;
        protected void Page_Load(object sender, EventArgs e)
        {
            src = Request.Params["extsrc"] + "";
            imgsrc = Request.Params["img"] + "";
        }
    }
}
