using KMS_Copy.Model.Models.Announcement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace KMS_Copy.Service.Interface
{
    [ServiceContract]
    interface IGlobalService
    {
        /// <summary>
        /// 获取搜索页面
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "/KMS/Global/GetSearchUrl", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetSearchUrl();
        /// <summary>
        /// 获取网页导航
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Navigation/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetGlobalNavigation(PagingEntity obj);
        /// <summary>
        /// 获取通知
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Announcement/Items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetAnnouncementsItems(PagingEntity obj);
        /// <summary>
        /// 获取首页导航
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Site/Navigation/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetSiteNavigation(PagingEntity obj);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetJson({data})")]
        string GetJson(string data);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetNavigation({listname})")]
        string GetNavigation(string listname);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/WCF/Site/AddNewItem4", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        string AddNewItem4(PagingEntity obj);
    }
}
