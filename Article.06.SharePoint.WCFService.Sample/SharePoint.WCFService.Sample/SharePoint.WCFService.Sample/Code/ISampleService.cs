using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ServiceModel;
using System.ServiceModel.Web;

namespace SharePoint.WCFService.Sample.Code
{
    [ServiceContract]
    public interface ISampleService
    {
        [OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Xml,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "SampleServiceCall({SampleValue})")]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "SampleServiceCall({SampleValue})")]
        string SampleServiceCall(string SampleValue);

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Xml,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "SetRatings({SKUNumber},{ListName},{RatingValue},{RatingTitle})")]
        //string SetRatings(string SKUNumber, string ListName, string RatingValue, string RatingTitle);

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Xml,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "GetRatings({SKUNumber},{ListName})")]
        //string GetRatings(string SKUNumber, string ListName);
    }
}
