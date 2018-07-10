using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SPWCF
{
    [ServiceContract]
    public interface ITest
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetJson({data})")]
        string GetJson(string data);


        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetNavigation({listname})")]
        string GetNavigation(string listname);

        [OperationContract]
        [WebGet(UriTemplate = "/WCF/Global/GetSearchUrl", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        string GetSystemSettings();


        //[OperationContract]
        //[WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "AddNewItem({listname})")]
        //string AddNewItem(string listname);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddNewItem({listname})", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        string AddNewItem(string listname);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/WCF/Site/Navigation/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        string AddNewItem2(string listname);

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/WCF/Site/Navigationinfo", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        //string AddNewItem3(string listname, int maxid);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/WCF/Site/AddNewItem4", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        string AddNewItem4(ParEntity para);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/WCF/Site/GetWish", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        String GetWish(string value1, string value2, string value3, int value4);


    }
}
