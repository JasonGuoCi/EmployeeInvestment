using Microsoft.SharePoint.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace KMS_Copy.Service
{
    [BasicHttpBindingServiceMetadataExchangeEndpointAttribute]
    [ServiceBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public abstract class BaseService
    {
        protected virtual JsonResult Json(Object obj)
        {
            return new JsonResult() { Data = obj };
        }

        protected virtual JsonResult Json(Exception ex)
        {
            return new JsonResult() { IsError = true, ErrorMsg = ex.Message };
        }
    }

    public class SearchEntity
    {
        public bool Inherit { get; set; }
        public string ResultsPageAddress { get; set; }
        public bool ShowNavigation { get; set; }
    }
}
