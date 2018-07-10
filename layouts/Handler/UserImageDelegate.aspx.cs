using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;

namespace Envision.KMS.solution.Layouts.Envision.KMS.Solution.Handler
{
    public partial class UserImageDelegate : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            string siteurl=SPContext.Current.Web.Url;
            using (SPSite site = new SPSite(siteurl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    
                }
            }
        }
    }
}
