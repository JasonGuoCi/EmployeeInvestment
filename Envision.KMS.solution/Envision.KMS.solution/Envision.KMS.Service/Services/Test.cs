using Envision.KMS.Model.Models.CaseManagement;
using Envision.KMS.Service.Interface;
using Focuswin.SP.Base.Utility;
using Microsoft.SharePoint.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Service.Services
{
    [BasicHttpBindingServiceMetadataExchangeEndpointAttribute]
    [ServiceBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Test :ITest
    {

        public string TestVideo(string index)
        {
            throw new NotImplementedException();
        }

        List<string> ITest.Test1()
        {
            return new List<string>();
        }

    }
}
