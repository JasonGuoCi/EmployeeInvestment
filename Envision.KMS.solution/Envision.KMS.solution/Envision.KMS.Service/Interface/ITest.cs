using Focuswin.SP.Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Service.Interface
{
    [ServiceContract]
    interface ITest
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetTest", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        List<string> Test1();

        [OperationContract]
        [WebGet(UriTemplate = "/GetVideoTest/{index}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        string TestVideo(string index);
    }
}
