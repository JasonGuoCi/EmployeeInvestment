using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

using Microsoft.Office.Server.Social;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.Office.Server.Microfeed;
using Microsoft.SharePoint;
using Microsoft.Office.Server.ReputationModel;
using Microsoft.Office.Server.SocialData;

using System.Configuration;


using System.Security.Principal;
using System.Web;
using Microsoft.Web.Hosting.Administration;

namespace SharePoint.WCFService.Sample.Code
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public sealed class SampleService : ISampleService
    {


        /// <summary>
        /// This method is used to test the service whether it is been deployed properly or not.  This will not do anything functionally apart from the Logging.
        /// </summary>
        /// <param name="SampleValue"></param>
        /// <returns></returns>
        public string SampleServiceCall(string SampleValue)
        {

            //string strQueryString = HttpUtility.UrlEncode("DC07\\Administrator");

            //ULSLog.LogDebug("Entering into SampleServiceCall");
            //ULSLog.LogDebug("Leaving into SampleServiceCall");
            return "Success";
        }

    }
}
