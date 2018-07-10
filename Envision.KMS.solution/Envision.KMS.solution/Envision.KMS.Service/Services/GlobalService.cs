using Envision.KMS.Service.Interface;
using Focuswin.SP.Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Envision.KMS.Model.Models.Announcement;
using System.ServiceModel.Activation;
using Envision.KMS.Model.Models.Wikipedia;
using Envision.KMS.Model.Models.Manual;
using Microsoft.SharePoint;
using Envision.KMS.Model.Models.LanguageMapping;
using Envision.KMS.Model.Models.Comments;
using Envision.KMS.Model.Models.Settings;
using Envision.KMS.Model.Models.GlobalSlider;
using Envision.KMS.Model.Models.Navigation;
using Envision.KMS.Model.Models.Forum;
using Envision.KMS.Model.Models.CaseManagement;

namespace Envision.KMS.Service.Services
{
    public class GlobalService : BaseService, IGlobalService
    {
        Logger log = Logger.GetLogger(typeof(GlobalService));
        /// <summary>
        /// 获取通告
        /// </summary>
        public JsonResult GetAnnouncementsItems(PagingEntity obj)
        {
            try
            {
                AnnouncementMgr mgr = new AnnouncementMgr();
                return Json(mgr.GetAnnouncements(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetAnnouncementsItems obj siteurl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        /// 获取通告数量
        /// </summary>
        public JsonResult GetAnnouncementsCount(PagingEntity obj)
        {
            try
            {
                AnnouncementMgr mgr = new AnnouncementMgr();
                return Json(mgr.GetAnnouncementsCount(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetAnnouncementsItemsCount obj siteurl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        public string Test1()
        {
            return "OK";
        }
        /// <summary>
        /// 获取词条树
        /// </summary>
        public JsonResult GetWikiTree(PagingEntity obj)
        {
            try
            {
                WikipediaMgr mgr = new WikipediaMgr();
                return Json(mgr.GetDocNode(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetWikiTree obj siteurl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        /// 获取词条内容
        /// </summary>
        public JsonResult GetWikiContent(PagingEntity obj)
        {
            try
            {
                WikipediaMgr mgr = new WikipediaMgr();
                return Json(mgr.GetContentById(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetWikiContent obj id:" + obj.Id, ex);
                return Json(ex);
            }
        }
        /// <summary>
        /// 获取视频教程列表
        /// </summary>
        public JsonResult GetManualFolders(PagingEntity obj)
        {
            try
            {
                ManualMgr mgr = new ManualMgr();
                return Json(mgr.GetVideoFolders(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetManualFolders obj url:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        /// 获取视频教程
        /// </summary>
        public JsonResult GetManualVideos(PagingEntity obj)
        {
            try
            {
                ManualMgr mgr = new ManualMgr();
                return Json(mgr.GetVideosByFolderId(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetManualVideos obj folder id:" + obj.Id, ex);
                return Json(ex);
            }
        }
        /// <summary>
        /// 通过创建时间获取视频教程
        /// </summary>
        public JsonResult GetManualVideosByCreated(PagingEntity obj)
        {
            try
            {
                ManualMgr mgr = new ManualMgr();
                return Json(mgr.GetVideosByCreated(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetManualVideosByCreated obj url:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///通过创建时间获取文本教程
        /// </summary>
        public JsonResult GetManualdocsByCreated(PagingEntity obj)
        {
            try
            {
                ManualMgr mgr = new ManualMgr();
                return Json(mgr.GetManualDocument(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetManualdocsByCreated obj url:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///通过创建时间获取网页教程
        /// </summary>
        public JsonResult GetManualHtmlByCreated(PagingEntity obj)
        {
            try
            {
                ManualMgr mgr = new ManualMgr();
                return Json(mgr.GetManualHtml(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetManualHtmlByCreated obj url:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///通过GUID获取网页教程
        /// </summary>
        public JsonResult GetManualHtmlById(PagingEntity obj)
        {
            try
            {
                ManualMgr mgr = new ManualMgr();
                return Json(mgr.GetManualHtmlItem(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetManualHtmlById obj Id:" + obj.Id, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取sharepoint搜索页面
        /// </summary>
        public JsonResult GetSearchUrl()
        {
            try
            {
                string url = string.Empty;
                string specificUrl = SPContext.Current.Web.AllProperties["SRCH_SB_SET_WEB"] + "";
                if (!string.IsNullOrEmpty(specificUrl))
                {
                    SearchEntity entity = GeneralHelper.FromJSON<SearchEntity>(specificUrl);
                    url = entity.ResultsPageAddress + "?k=";
                }
                else if (!string.IsNullOrEmpty(SPContext.Current.Site.RootWeb.AllProperties["SRCH_ENH_FTR_URL"] + ""))
                {
                    url = SPContext.Current.Site.RootWeb.AllProperties["SRCH_ENH_FTR_URL"] + "/results.aspx?k=";
                }
                else
                {
                    url = SPContext.Current.Web.Url + "/_layouts/15/osssearchresults.aspx?k=";
                }
                return Json(url);
            }
            catch (Exception ex)
            {
                log.Error("GetSearchUrl:", ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取中(英)文显示值
        /// </summary>
        public JsonResult GetResourceId(LanguageParamEntity obj)
        {
            try
            {
                LanguageMgr mgr = new LanguageMgr();
                return Json(mgr.GetResourceValue(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetManualHtmlById obj key:" + obj.key, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取左侧导航栏显示值
        /// </summary>
        public JsonResult GetLeftNavResourceId(LanguageParamEntity obj)
        {
            try
            {
                LanguageMgr mgr = new LanguageMgr();
                obj.key = "wikipedia";
                string wikiresource = mgr.GetResourceValue(obj);
                obj.key = "Operation";
                string Operationresource = mgr.GetResourceValue(obj);
                obj.key = "Forum";
                string Forumresource = mgr.GetResourceValue(obj);
                obj.key = "ForumSub1";
                string ForumSub1resource = mgr.GetResourceValue(obj);
                obj.key = "ForumSub2";
                string ForumSub2resource = mgr.GetResourceValue(obj);
                return Json(new leftnavEntity(wikiresource, Operationresource, Forumresource, ForumSub1resource, ForumSub2resource));
            }
            catch (Exception ex)
            {
                log.Error("GetLeftNavResourceId obj url:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///通过关键字获取词条关键字
        /// </summary>
        public JsonResult SearchWikiTree(PagingEntity obj)
        {
            try
            {
                WikipediaMgr mgr = new WikipediaMgr();
                return Json(mgr.SearchWikiByKeywords(obj));
            }
            catch (Exception ex)
            {
                log.Error("SearchWikiTree obj keywords:" + obj.Search, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///通过GUID获取评论列表
        /// </summary>
        public JsonResult GetCommentsById(PagingEntity obj)
        {
            try
            {
                CommentsMgr mgr = new CommentsMgr();
                return Json(mgr.GetCommentsByGUID(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetCommentsById obj id:" + obj.Id, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取评论总数
        /// </summary>
        public JsonResult GetCommentsTotalCount(PagingEntity obj)
        {
            try
            {
                CommentsMgr mgr = new CommentsMgr();
                return Json(mgr.GetCommentsTotalCount(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetCommentsTotalCount obj id:" + obj.Id, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///添加评论
        /// </summary>
        public JsonResult InsertCommentsById(PagingEntity obj)
        {
            try
            {
                CommentsMgr mgr = new CommentsMgr();
                return Json(mgr.Insert(obj));
            }
            catch (Exception ex)
            {
                log.Error("InsertCommentsById obj id:" + obj.Id, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///判断是不是管理员
        /// </summary>
        public JsonResult ValidateUser()
        {
            try
            {
                using (SPWeb web = new SPSite(SPContext.Current.Web.Url).OpenWeb())
                {
                    SettingsMgr mgr = new SettingsMgr();
                    SettingsEntity setting = mgr.GetSystemSetting(web.Url, "Admin Group");
                    bool isMember = false;
                    isMember = web.IsCurrentUserMemberOfGroup(web.Groups[setting.DefaultValue].ID);
                    return Json(isMember);
                }
            }
            catch (Exception ex)
            {
                log.Error("ValidateUser user:" + SPContext.Current.Web.CurrentUser.LoginName, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///删除评论
        /// </summary>
        public JsonResult DeleteCommentsById(PagingEntity obj)
        {
            try
            {
                CommentsMgr mgr = new CommentsMgr();
                return Json(mgr.Delete(obj));
            }
            catch (Exception ex)
            {
                log.Error("DeleteCommentsById obj id:" + obj.Id, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取文本类教程总数
        /// </summary>
        public JsonResult GetManualTotalCount(PagingEntity obj)
        {
            try
            {
                ManualMgr mgr = new ManualMgr();
                int html = mgr.GetManualHtmlCount(obj);
                int docs = mgr.GetManualDocCount(obj);
                return Json(html + docs);
            }
            catch (Exception ex)
            {
                log.Error("GetManualTotalCount obj url:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取中英文列表 
        /// </summary>
        public JsonResult GetManualResourceId(LanguageParamEntity obj)
        {
            try
            {
                LanguageMgr mgr = new LanguageMgr();
                return Json(mgr.GetManualResource(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetManualResourceId obj model:" + obj.model, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取全局导航
        /// </summary>
        public JsonResult GetGlobalSilders(PagingEntity obj)
        {
            try
            {
                GlobalSliderMgr mgr = new GlobalSliderMgr();
                return Json(mgr.GetGlobalSliders(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetGlobalSilders obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取网站联系人
        /// </summary>
        public JsonResult GetGlobalContacts(PagingEntity obj)
        {
            try
            {
                GlobalSliderMgr mgr = new GlobalSliderMgr();
                return Json(mgr.GetGlobalContacts(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetGlobalContacts obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取首页导航
        /// </summary>
        public JsonResult GetGlobalNavigation(PagingEntity obj)
        {
            try
            {
                NavigationMgr mgr = new NavigationMgr();
                return Json(mgr.GetNavigation(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetGlobalNavigation obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取站点导航
        /// </summary>
        public JsonResult GetSiteNavigation(PagingEntity obj)
        {
            try
            {
                NavigationMgr nmgr = new NavigationMgr();
                SettingsMgr mgr = new SettingsMgr();
                SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "IsNavigation");
                if(setting.DefaultValue == "1")
                    return Json(nmgr.GetNavigation(obj));
                else if (setting.DefaultValue == "2")
                {
                    obj.weburl = SPContext.Current.Site.Url;
                    return Json(nmgr.GetNavigation(obj));
                }
                else
                   return  Json(new List<NavigationEntity>());
                
            }
            catch (Exception ex)
            {
                log.Error("GetSiteNavigation obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取话题列表
        /// </summary>
        public JsonResult GetForums(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetForums(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForums obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取话题数量
        /// </summary>
        public JsonResult GetForumsCount(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetForumCount(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForumsCount obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///通过GUID获取话题
        /// </summary>
        public JsonResult GetForumById(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetForumItem(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForumById obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取话题类型
        /// </summary>
        public JsonResult GetForumTypes(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetForumType(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForumTypes obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///添加话题
        /// </summary>
        public JsonResult InsertForum(ForumEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.InsertForum(obj));
            }
            catch (Exception ex)
            {
                log.Error("InsertForum obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///根据评论者数量获取话题
        /// </summary>
        public JsonResult GetForumsByAuthorCount(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetForumByAuthorCount(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForumsByAuthorCount obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///通过GUID获取话题评论
        /// </summary>
        public JsonResult GetForumCommentsById(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetCommentsByGUID(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForumCommentsById obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取评论总数
        /// </summary>
        public JsonResult GetForumCommentsTotalCount(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetCommentsTotalCount(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForumCommentsTotalCount obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///添加评论
        /// </summary>
        public JsonResult InsertForumCommentsById(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.InsertComments(obj));
            }
            catch (Exception ex)
            {
                log.Error("InsertForumCommentsById obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///删除话题评论
        /// </summary>
        public JsonResult DeleteForumCommentsById(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.DeleteComments(obj));
            }
            catch (Exception ex)
            {
                log.Error("DeleteForumCommentsById obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///添加话题浏览数
        /// </summary>
        public JsonResult InsertForumView(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                mgr.InsertView(obj);
                return Json(1);
            }
            catch (Exception ex)
            {
                log.Error("InsertForumView obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///根据修改时间获取话题
        /// </summary>
        public JsonResult GetForumsByModified(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetForumsByModified(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForumsByModified obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///根据作者获取话题
        /// </summary>
        public JsonResult GetForumsByAuthor(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetForumsByAuthor(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForumsByAuthor obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///根据回复获取话题
        /// </summary>
        public JsonResult GetForumsByReply(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetForumsByReply(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForumsByReply obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///根据回复获取话题数量
        /// </summary>
        public JsonResult GetForumsByReplyCount(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.GetForumsCountByReply(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetForumsByReplyCount obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///设置话题置顶
        /// </summary>
        public JsonResult SetForumTop(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                mgr.SetTop(obj);
                return Json(1);
            }
            catch (Exception ex)
            {
                log.Error("SetForumTop obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///删除话题
        /// </summary>
        public JsonResult DeleteForum(PagingEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.DeleteForum(obj));
            }
            catch (Exception ex)
            {
                log.Error("DeleteForum obj Id:" + obj.Id, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///编辑话题
        /// </summary>
        public JsonResult EditForum(ForumEntity obj)
        {
            try
            {
                ForumMgr mgr = new ForumMgr();
                return Json(mgr.EditForum(obj));
            }
            catch (Exception ex)
            {
                log.Error("DeleteForum obj Id:" + obj.Id, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///获取case类型
        /// </summary>
        public JsonResult GetCaseTypes(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetCastTypes(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetCaseTypes obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///添加case
        /// </summary>
        public JsonResult InsertCase(CaseManagementEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.InsertCase(obj));
            }
            catch (Exception ex)
            {
                log.Error("InsertCase obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///获取我申请的case
        /// </summary>
        public JsonResult GetMyApplyCase(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetMyApply(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetMyApplyCase obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///通过case编号获取case
        /// </summary>
        public JsonResult GetCaseById(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetCaseById(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetCaseById obj Id:" + obj.Id, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///删除case
        /// </summary>
        public JsonResult DeleteCaseById(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.DeleteCase(obj));
            }
            catch (Exception ex)
            {
                log.Error("DeleteCase obj Id:" + obj.Id, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///获取任务列表
        /// </summary>
        public JsonResult GetTaskItems(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetTask(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetTaskItems obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///获取我申请的case的数量
        /// </summary>
        public JsonResult GetMyApplyCaseCount(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetMyApplyCount(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetMyApplyCount obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取case任务数量
        /// </summary>
        public JsonResult GetTaskItemsCount(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetTaskCount(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetTaskItemsCount obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///根据caseGUID获取case
        /// </summary>
        public JsonResult GetCaseByUniqueId(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetCaseByUniqueId(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetCaseByUniqueId obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///处理任务
        /// </summary>
        public JsonResult DealTaskItem(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.DealTask(obj));
            }
            catch (Exception ex)
            {
                log.Error("DealTaskItem obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///转办任务
        /// </summary>
        public JsonResult TransferTaskItem(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.TransferTask(obj));
            }
            catch (Exception ex)
            {
                log.Error("TransferTaskItem obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///获取所有问题列表
        /// </summary>
        public JsonResult GetCaseQueryItems(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetQueryItems(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetCaseQueryItems obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///设置case置顶
        /// </summary>
        public JsonResult SetCaseTop(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                mgr.SetTop(obj);
                return Json(1);
            }
            catch (Exception ex)
            {
                log.Error("SetCaseTop obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///获取所有问题总数
        /// </summary>
        public JsonResult GetCaseQueryItemsCount(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetQueryItemsCount(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetCaseQueryItemsCount obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///发送邮件给负责人
        /// </summary>
        public JsonResult SendMailtoApprove(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.SendMailforApprove(obj));
            }
            catch (Exception ex)
            {
                log.Error("SendMailtoApprove obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }

        /// <summary>
        ///获取所有还没处理的case
        /// </summary>
        public JsonResult GetAllApplyCase(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetAllApply(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetAllApplyCase obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        ///获取所有还没处理的case数量
        /// </summary>
        public JsonResult GetAllApplyCaseCount(PagingEntity obj)
        {
            try
            {
                CaseManagementMgr mgr = new CaseManagementMgr();
                return Json(mgr.GetAllApplyCount(obj));
            }
            catch (Exception ex)
            {
                log.Error("GetAllApplyCaseCount obj weburl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
    }
}
 