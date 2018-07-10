using Envision.KMS.Model.Models.Announcement;
using Envision.KMS.Model.Models.CaseManagement;
using Envision.KMS.Model.Models.Comments;
using Envision.KMS.Model.Models.Forum;
using Envision.KMS.Model.Models.GlobalSlider;
using Envision.KMS.Model.Models.LanguageMapping;
using Envision.KMS.Model.Models.Manual;
using Envision.KMS.Model.Models.Navigation;
using Envision.KMS.Model.Models.Wikipedia;
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
    [ServiceKnownType(typeof(PagingEntity))]
    [ServiceKnownType(typeof(List<AnnouncementEntity>))]
    [ServiceKnownType(typeof(List<TreeNodeEntity>))]
    [ServiceKnownType(typeof(List<ManualFolder>))]
    [ServiceKnownType(typeof(List<ManualVideo>))]
    [ServiceKnownType(typeof(List<ManualHtmlEntity>))]
    [ServiceKnownType(typeof(List<CommentsEntity>))]
    [ServiceKnownType(typeof(List<LanguageEntity>))]
    [ServiceKnownType(typeof(List<GlobalSliderEntity>))]
    [ServiceKnownType(typeof(List<GlobalContactEntity>))]
    [ServiceKnownType(typeof(ManualHtmlEntity))]
    [ServiceKnownType(typeof(WikipediaEntity))]
    [ServiceKnownType(typeof(leftnavEntity))]
    [ServiceKnownType(typeof(LanguageParamEntity))]
    [ServiceKnownType(typeof(List<ManualDocumentEntity>))]
    [ServiceKnownType(typeof(List<NavigationEntity>))]
    [ServiceKnownType(typeof(List<ForumEntity>))]
    [ServiceKnownType(typeof(ForumEntity))]
    [ServiceKnownType(typeof(CaseManagementEntity))]
    [ServiceKnownType(typeof(List<ForumTypeEntity>))]
    [ServiceKnownType(typeof(List<CastTypeEntity>))]
    [ServiceKnownType(typeof(List<CaseManagementEntity>))]
    [ServiceKnownType(typeof(List<CaseTaskEntity>))]
    [ServiceKnownType(typeof(CaseTaskEntity))]
    interface IGlobalService
    {
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
        /// 获取词条树
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Wikipedia/tree", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetWikiTree(PagingEntity obj);
        /// <summary>
        /// 根据关键字搜索词条
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Wikipedia/tree/Search", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult SearchWikiTree(PagingEntity obj);
        /// <summary>
        /// 获取词条内容
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Wikipedia/Content", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetWikiContent(PagingEntity obj);
        /// <summary>
        /// 获取通告
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Manual/Video/Folders", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetManualFolders(PagingEntity obj);
        /// <summary>
        /// 获取视频类操作教程
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Manual/Video/Folder", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetManualVideos(PagingEntity obj);
        /// <summary>
        /// 根据创建时间获取视频教程
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Manual/Videos/Created", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetManualVideosByCreated(PagingEntity obj);
        /// <summary>
        /// 根据创建时间获取文档教程
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Manual/Docs/Created", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetManualdocsByCreated(PagingEntity obj);
        /// <summary>
        /// 根据创建时间获取网页教程
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Manual/Html/Created", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetManualHtmlByCreated(PagingEntity obj);
        /// <summary>
        /// 获取网页内容
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Manual/Html/Detail", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetManualHtmlById(PagingEntity obj);
        /// <summary>
        /// 获取教程总数
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Manual/TotalCount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetManualTotalCount(PagingEntity obj);
        /// <summary>
        /// 获取搜索页面
        /// </summary>
        [OperationContract]
        [WebGet(UriTemplate = "/KMS/Global/GetSearchUrl", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetSearchUrl();
        /// <summary>
        /// 获取全局语言显示
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/GetResourceId", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetResourceId(LanguageParamEntity obj);
        /// <summary>
        /// 获取操作教程语言显示
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Manual/ResourceId", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetManualResourceId(LanguageParamEntity obj);
        /// <summary>
        /// 获取左侧导航语言显示
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/LeftNav/ResourceId", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetLeftNavResourceId(LanguageParamEntity obj);
        /// <summary>
        /// 获取评论
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Comments/Items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetCommentsById(PagingEntity obj);
        /// <summary>
        /// 获取评论数量
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Comments/ItemsCount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetCommentsTotalCount(PagingEntity obj);
        /// <summary>
        /// 添加评论
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Comments/Insert", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult InsertCommentsById(PagingEntity obj);
        /// <summary>
        /// 删除评论
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Comments/Delete", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult DeleteCommentsById(PagingEntity obj);
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
        /// 获取网页导航
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Global/Navigation/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetGlobalNavigation(PagingEntity obj);
        /// <summary>
        /// 获取首页导航
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Site/Navigation/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetSiteNavigation(PagingEntity obj);
        /// <summary>
        /// 判断是否为管理员
        /// </summary>
        [OperationContract]
        [WebGet(UriTemplate = "/KMS/Global/Admin/Validation", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult ValidateUser();
        /// <summary>
        /// 获取话题
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForums(PagingEntity obj);
        /// <summary>
        /// 根据修改时间获取话题
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/items/Modified", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForumsByModified(PagingEntity obj);
        /// <summary>
        /// 获取话题数量
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/itemscount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForumsCount(PagingEntity obj);
        /// <summary>
        /// 根据作者获取话题
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/ForumAuthor/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForumsByAuthor(PagingEntity obj);
        /// <summary>
        /// 根据作者获取话题数量
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/ForumAuthor/itemscount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForumsByAuthorCount(PagingEntity obj);
        /// <summary>
        /// 根据回复数获取话题
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/ForumReply/items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForumsByReply(PagingEntity obj);
        /// <summary>
        ///根据回复获取话题数量
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/ForumReply/itemscount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForumsByReplyCount(PagingEntity obj);
        /// <summary>
        ///根据GUID获取话题
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/item", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForumById(PagingEntity obj);
        /// <summary>
        ///获取话题标签
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/type", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForumTypes(PagingEntity obj);
        /// <summary>
        ///添加话题
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/Insert", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult InsertForum(ForumEntity obj);
        /// <summary>
        ///添加话题浏览数
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/View", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult InsertForumView(PagingEntity obj);
        /// <summary>
        ///话题置顶
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/SetTop", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult SetForumTop(PagingEntity obj);
        /// <summary>
        ///删除话题
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/Delete", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult DeleteForum(PagingEntity obj);
        /// <summary>
        ///编辑话题
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/Edit", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult EditForum(ForumEntity obj);
        /// <summary>
        ///根据GUID获取评论
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/Comments/Items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForumCommentsById(PagingEntity obj);
        /// <summary>
        ///获取评论总数
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/Comments/ItemsCount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetForumCommentsTotalCount(PagingEntity obj);
        /// <summary>
        ///根据GUID添加话题评论
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/Comments/Insert", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult InsertForumCommentsById(PagingEntity obj);
        /// <summary>
        ///根据GUID删除评论
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/KMS/Forum/Comments/Delete", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult DeleteForumCommentsById(PagingEntity obj);
        /// <summary>
        ///获取软件问题及建议类型
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Types", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetCaseTypes(PagingEntity obj);
        /// <summary>
        ///添加软件问题及建议
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Insert/Item", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult InsertCase(CaseManagementEntity obj);
        /// <summary>
        ///获取我申请的软件问题及建议
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Items/Apply", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetMyApplyCase(PagingEntity obj);
        /// <summary>
        ///获取所有我申请还没处理的case
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Items/ApplyCount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetMyApplyCaseCount(PagingEntity obj);
        /// <summary>
        ///获取所有还没处理的case
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Items/AllApply", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetAllApplyCase(PagingEntity obj);
        /// <summary>
        ///获取所有还没处理的case数量
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Items/AllApplyCount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetAllApplyCaseCount(PagingEntity obj);
        /// <summary>
        ///根据GUID获取case详情
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Item/Detail", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetCaseById(PagingEntity obj);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Item/DetailByUnique", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetCaseByUniqueId(PagingEntity obj);
        /// <summary>
        ///根据GUID删除case
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Item/Delete", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult DeleteCaseById(PagingEntity obj);
        /// <summary>
        ///获取我代办的任务
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Task/Items", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetTaskItems(PagingEntity obj);
        /// <summary>
        ///获取我代办的任务数量
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Task/ItemsCount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetTaskItemsCount(PagingEntity obj);
        /// <summary>
        ///处理任务
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Task/Deal", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult DealTaskItem(PagingEntity obj);
        /// <summary>
        ///转办任务
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Task/Transfer", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult TransferTaskItem(PagingEntity obj);
        /// <summary>
        ///获取所有的问题
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Items/Query", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetCaseQueryItems(PagingEntity obj);
        /// <summary>
        ///获取所有问题的数量
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Items/QueryCount", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult GetCaseQueryItemsCount(PagingEntity obj);
        /// <summary>
        ///发送邮件事件
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Task/SendMail", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult SendMailtoApprove(PagingEntity obj);
        /// <summary>
        ///设置case置顶
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Case/Items/SetTop", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        JsonResult SetCaseTop(PagingEntity obj);
    }
}
