using KMS_Copy.Service.Interface;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMS_Copy.Model.Models.Announcement;
using KMS_Copy.Model.Models.Settings;
using KMS_Copy.Model.Models.Navigation;
using SP.Base.Utility;

namespace KMS_Copy.Service.Services
{
    public class GlobalService2 : BaseService, IGlobalService
    {
        /// <summary>
        /// 获取通知
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult GetAnnouncementsItems(PagingEntity obj)
        {
            try
            {
                AnnouncementMgr mgr = new AnnouncementMgr();
                return Json(mgr.GetAnnouncementItems(obj));
            }
            catch (Exception ex)
            {

                return Json(ex);
            }
        }

        public JsonResult GetGlobalNavigation(PagingEntity obj)
        {
            try
            {
                NavigationMgr nmgr = new NavigationMgr();
                SettingsMgr mgr = new SettingsMgr();
                SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "IsNavigation");
                if (setting.DefaultValue == "1")
                {
                    return Json(nmgr.GetNavigation(obj));
                }
                else if (setting.DefaultValue == "2")
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

        public JsonResult GetSiteNavigation(PagingEntity obj)
        {
            try
            {
                NavigationMgr nmgr = new NavigationMgr();
                SettingsMgr smgr = new SettingsMgr();
                SettingsEntity settingEntity = smgr.GetSystemSetting(obj.weburl, "IsNavigation");
                if (settingEntity.DefaultValue == "1")
                {
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


        public string GetJson(string data)
        {
            return data;
        }

        public string GetNavigation(string listname)
        {
            using (SPSite site = new SPSite("http://sharepointlipan:567/"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    var nav = "";
                    SPList list = web.Lists[listname];
                    SPListItemCollection items = list.GetItems();
                    foreach (SPListItem item in items)
                    {
                        nav = nav + item.Title;
                    }
                    return nav;
                }
            }
        }


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

    }
}
