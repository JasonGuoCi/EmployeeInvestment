
using KMS_Copy.Model.Models.Announcement;
using KMS_Copy.Model.Models.GlobalSlider;
using KMS_Copy.Model.Models.Navigation;
using KMS_Copy.Model.Models.Settings;
using KMS_Copy.Service.Interface;
using KMS_Copy.Service.Utilities;
using Microsoft.SharePoint;
using SP.Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using KMS_Copy.Model.Models.LanguageMapping;
using KMS_Copy.Model.Models.Wikipedia;
using KMS_Copy.Model.Models.Comments;

namespace KMS_Copy.Service.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    class Global : BaseService, IGlobal
    {
        public string AddNewItem4(PagingEntity obj)
        {
            string retVal = string.Empty;
            obj.listname = "Navigation";
            obj.CategoryId = "AAAAA";
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite("http://sharepointlipan:567/"))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            SPList list = web.Lists[obj.listname];
                            SPListItem item = list.Items.Add();
                            item["Title"] = string.Format("Test at {0},{1},{2}", DateTime.Now.ToString(), obj.listname, obj.CategoryId);
                            item.SystemUpdate();
                            web.AllowUnsafeUpdates = false;

                            //maxid = list.GetItems().Count;
                        }
                    }
                    retVal = "operation  success!" + string.Format("Test at {0},{1},{2}", DateTime.Now.ToString(), obj.listname, obj.CategoryId);
                    //retVal = "operation  success!" + string.Format("Test at {0}", DateTime.Now.ToString() + "---" + para.value); 
                    // retVal = "operation  success! Item Count: "+ maxid;
                });
            }
            catch (Exception ex)
            {
                retVal += ex.Message;
            }
            return retVal;
        }

        public JsonResult GetSiteNavigation(PagingEntity obj)
        {
            try
            {
                //obj.weburl = SPContext.Current.Web.Url;
                //obj.weburl = "http://sharepointlipan:567/";
                NavigationMgr nmgr = new NavigationMgr();

                SettingsMgr smgr = new SettingsMgr();
                SettingsEntity settingEntity = smgr.GetSystemSetting(obj.weburl, "IsNavigation");
                //ListItemHelper.GetSystemSettingItem();
                if (settingEntity.DefaultValue == "1")
                {
                    //return Json(ListItemHelper2.GetSiteNavigation(obj.weburl, obj.listname, obj.Language));
                    return Json(nmgr.GetNavigation(obj));
                }
                else if (settingEntity.DefaultValue == "2")
                {
                    obj.weburl = SPContext.Current.Site.Url;
                    return Json(nmgr.GetNavigation(obj));
                }
                else
                {
                    return Json(new List<NavigationEntity>());
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
                // throw;
            }
        }

        /// <summary>
        /// 获取SharePoint搜索页面
        /// </summary>
        /// <returns></returns>
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
                else if (!string.IsNullOrEmpty(SPContext.Current.Site.RootWeb.AllProperties["SRCH_ENH_FTR-URL"] + " "))
                {
                    url = SPContext.Current.Site.RootWeb.AllProperties["SRCH_ENH_FTR"] + "/results.aspx?k=";
                }
                else
                {
                    url = SPContext.Current.Web.Url + "/_layouts/15/osssearchresults.aspx?k=";
                }
                return Json(url);
            }
            catch (Exception ex)
            {

                return Json(ex);
            }
        }
        /// <summary>
        /// 获取全局链接
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
                LanguageMgr lmgr = new LanguageMgr();
                return Json(lmgr.GetResourceValue(obj));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }
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
                //log.Error("GetAnnouncementsItems obj siteurl:" + obj.weburl, ex);
                return Json(ex);
            }
        }
        /// <summary>
        /// 获取通告数量
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult GetAnnouncementsCount(PagingEntity obj)
        {
            try
            {
                AnnouncementMgr mgr = new AnnouncementMgr();
                return Json(mgr.GetAnnouncementsCount(obj));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult DeleteCommentsById(PagingEntity obj)
        {
            try
            {
                CommentsMgr mgr = new CommentsMgr();
                return Json(mgr.Delete(obj));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        /// <summary>
        /// 获取词条内容
        /// </summary>
        /// <returns></returns>
        public JsonResult GetWikiContent(PagingEntity obj)
        {
            try
            {
                WikipediaMgr mgr = new WikipediaMgr();
                return Json(mgr.GetContentById(obj));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        /// <summary>
        /// 判断是不是管理员
        /// </summary>
        /// <returns></returns>
        public JsonResult ValidateUser()
        {
            try
            {
                using (SPSite site = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SettingsMgr mgr = new SettingsMgr();
                        SettingsEntity setting = mgr.GetSystemSetting(web.Url, "Admin Group");
                        bool isMember = false;
                        isMember = web.IsCurrentUserMemberOfGroup(web.Groups[setting.DefaultValue].ID);
                        return Json(isMember);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        /// <summary>
        /// 获取词条树
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult GetWikiTree(PagingEntity obj)
        {
            try
            {
                WikipediaMgr mgr = new WikipediaMgr();
                return Json(mgr.GetDocNode(obj));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        /// <summary>
        /// 获取左侧导航语言提示
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult GetLeftNavResourceId(LanguageParamEntity obj)
        {
            try
            {
                LanguageMgr mgr = new LanguageMgr();
                obj.key = "wikipedia";
                string wikiResource = mgr.GetResourceValue(obj);
                obj.key = "Operation";
                string operationResource = mgr.GetResourceValue(obj);
                obj.key = "Forum";
                string forumResource = mgr.GetResourceValue(obj);
                obj.key = "ForumSub1";
                string forumSub1Resource = mgr.GetResourceValue(obj);
                obj.key = "ForumSub2";
                string forumSub2Resource = mgr.GetResourceValue(obj);

                return Json(new leftnavEntity(wikiResource, operationResource, forumResource, forumSub1Resource, forumSub2Resource));

            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        /// <summary>
        /// 根据关键字搜索词条
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult SearchWikiTree(PagingEntity obj)
        {
            try
            {
                WikipediaMgr mgr = new WikipediaMgr();
                return Json(mgr.SearchWikiByKeywords(obj));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        /// <summary>
        /// 获取评论数量
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult GetCommentsTotalCount(PagingEntity obj)
        {
            try
            {
                CommentsMgr mgr = new CommentsMgr();
                return Json(mgr.GetCommentsTotalCount(obj));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        /// <summary>
        /// 通过GUID获取评论列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult GetCommentsById(PagingEntity obj)
        {
            try
            {
                CommentsMgr mgr = new CommentsMgr();
                return Json(mgr.GetCommentsByGUID(obj));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult InsertCommentsById(PagingEntity obj)
        {
            try
            {
                CommentsMgr mgr = new CommentsMgr();
                return Json(mgr.Insert(obj));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
    }
}
