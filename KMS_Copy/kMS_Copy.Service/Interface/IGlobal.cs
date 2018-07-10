using KMS_Copy.Model.Models.Announcement;
using KMS_Copy.Model.Models.Comments;
using KMS_Copy.Model.Models.GlobalSlider;
using KMS_Copy.Model.Models.LanguageMapping;
using KMS_Copy.Model.Models.Navigation;
using KMS_Copy.Model.Models.Settings;
using KMS_Copy.Model.Models.Wikipedia;
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
    [ServiceKnownType(typeof(PagingEntity))]
    [ServiceKnownType(typeof(List<AnnouncementEntity>))]
    [ServiceKnownType(typeof(List<NavigationEntity>))]
    [ServiceKnownType(typeof(List<SettingsEntity>))]
    [ServiceKnownType(typeof(List<GlobalSliderEntity>))]
    [ServiceKnownType(typeof(List<GlobalContactEntity>))]
    [ServiceKnownType(typeof(List<LanguageEntity>))]
    [ServiceKnownType(typeof(List<LanguageParamEntity>))]
    [ServiceKnownType(typeof(List<LangEntity>))]
    [ServiceKnownType(typeof(List<leftnavEntity>))]
    [ServiceKnownType(typeof(List<CommentsEntity>))]
    [ServiceKnownType(typeof(List<WikipediaEntity>))]
    [ServiceKnownType(typeof(List<TreeNodeEntity>))]
    [ServiceKnownType(typeof(List<li_AttrEntity>))]
    [ServiceKnownType(typeof(List<StateEntity>))]

    interface IGlobal
    {
        /// <summary>
        /// for test
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/WCF/Site/AddNewItem4", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        string AddNewItem4(PagingEntity obj);

        /// <summary>
        /// 获取首页导航
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Site/Navigation/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetSiteNavigation(PagingEntity obj);

        /// <summary>
        /// 获取搜索页面
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "/KMS/Global/GetSearchUrl", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetSearchUrl();

        /// <summary>
        /// 获取全局链接
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Slider/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetGlobalSilders(PagingEntity obj);

        /// <summary>
        /// 获取全局联系人
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Contact/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetGlobalContacts(PagingEntity obj);

        /// <summary>
        /// 获取全局语言显示
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/GetResourceId", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetResourceId(LanguageParamEntity obj);

        /// <summary>
        /// 获取通告
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Announcement/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetAnnouncementsItems(PagingEntity obj);

        /// <summary>
        /// 获取通告数量
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Announcement/itemcount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetAnnouncementsCount(PagingEntity obj);
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Comments/Delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        JsonResult DeleteCommentsById(PagingEntity obj);
        /// <summary>
        /// 获取词条内容
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Wikipedia/Content", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        JsonResult GetWikiContent(PagingEntity obj);
        /// <summary>
        /// 判断是不是管理员
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "/KMS/Global/Admin/Validation", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult ValidateUser();
        /// <summary>
        /// 获取词条树
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Wikipedia/tree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        JsonResult GetWikiTree(PagingEntity obj);
        /// <summary>
        /// 获取左侧导航语言显示
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/LeftNav/ResourceId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        JsonResult GetLeftNavResourceId(LanguageParamEntity obj);
        /// <summary>
        /// 根据关键字搜索词条
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Wikipedia/tree/Search", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        JsonResult SearchWikiTree(PagingEntity obj);
        /// <summary>
        /// 获取评论数量
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Comments/ItemsCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        JsonResult GetCommentsTotalCount(PagingEntity obj);
        /// <summary>
        /// 通过GUID获取评论列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Comments/Items", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        JsonResult GetCommentsById(PagingEntity obj);
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Comments/Insert", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        JsonResult InsertCommentsById(PagingEntity obj);


    }
}
