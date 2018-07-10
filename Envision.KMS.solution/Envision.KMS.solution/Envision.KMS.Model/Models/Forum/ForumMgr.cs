using Envision.KMS.Model.Models.Announcement;
using Envision.KMS.Model.Models.Comments;
using Envision.KMS.Model.Models.Settings;
using Focuswin.SP.Base.Utility;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Forum
{
    public class ForumMgr
    {
        private Object thisLock = new Object();
        private Object commentsLock = new Object();
        private Object topLock = new Object();
        /// <summary>
        ///获取话题类型
        /// </summary>
        public List<ForumTypeEntity> GetForumType(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_Order\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.OrderBy(SearchHelper.Where(SearchHelper.IsNotNull("Title")), "Field_KMS_Order", true);
            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, obj.listname, views, caml, obj.TakeCount, obj.LimitRows);
            List<ForumTypeEntity> _ritems = new List<ForumTypeEntity>();
            foreach (SPListItem _item in items)
            {
                _ritems.Add(new ForumTypeEntity(_item, obj.Language));
            }
            return _ritems;
        }
        /// <summary>
        /// 通过话题编号获取话题类型
        /// </summary>
        public SPListItem GetForumTypeById(string weburl, string listname, string id)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_Order\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.Where(
                SearchHelper.Eq("ID", SearchHelper.ValueType.Text, id));
            SPListItem item = ListItemHelper.ExecuteCAMLToRetrieveSingleListItem(weburl, listname, views, caml);
            return item;
        }
        /// <summary>
        /// 获取话题列表
        /// </summary>
        public List<ForumEntity> GetForums(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"Field_KMS_LastComments\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_ForumContent\"/><FieldRef Name=\"Field_KMS_ForumType\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/><FieldRef Name=\"Field_KMS_ViewCount\"/><FieldRef Name=\"Field_KMS_CommentsCount\"/>";
            List<SearchHelper.OrderBys> orders = new List<SearchHelper.OrderBys>();
            orders.Add(new SearchHelper.OrderBys { Ascending = false, Field = "Field_KMS_IsTop" });
            orders.Add(new SearchHelper.OrderBys { Ascending = false, Field = "Field_KMS_ViewCount" });
            string caml = SearchHelper.OrderBy(SearchHelper.Where(SearchHelper.IsNotNull("Title")), orders);

            if (!string.IsNullOrEmpty(obj.CategoryId))
            {
                string forumtypeSearch = string.Empty;
                string[] categories = obj.CategoryId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> conditions = new List<string>();
                foreach (string category in categories)
                {
                    conditions.Add(SearchHelper.Contains("Field_KMS_ForumType", SearchHelper.ValueType.Lookup, category));
                }
                if (conditions.Count > 1)
                {
                    forumtypeSearch = SearchHelper.Or(conditions.ToArray());
                }
                else
                {
                    forumtypeSearch = conditions[0];
                }
                caml = SearchHelper.OrderBy(SearchHelper.Where(
                    SearchHelper.And(
                   SearchHelper.IsNotNull("Title"),
                    forumtypeSearch
                    )), orders);
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    caml = SearchHelper.OrderBy(SearchHelper.Where(
                        SearchHelper.And(
                         forumtypeSearch,
                        SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                        )), orders);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    caml = SearchHelper.OrderBy(SearchHelper.Where(
                        SearchHelper.And(
                         SearchHelper.IsNotNull("Title"),
                        SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                        )), orders);
                }
            }
            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, obj.listname, views, caml, obj.TakeCount, obj.LimitRows);
            List<ForumEntity> _ritems = new List<ForumEntity>();
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                    UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                    foreach (SPListItem _item in items)
                    {
                        SPFieldUser userfield = (SPFieldUser)_item.ParentList.Fields.GetFieldByInternalName("Author");
                        SPFieldUserValue fieldvalue = (SPFieldUserValue)(userfield.GetFieldValue((string)_item[userfield.InternalName]));
                        UserProfile user = userProfileMgr.GetUserProfile(fieldvalue.User.LoginName);
                        _ritems.Add(new ForumEntity(_item, user.DisplayName, user["PictureURL"] == null ? "" : (user["PictureURL"].Value + "")));
                    }
                }
            }

            return _ritems;
        }
        /// <summary>
        ///通过修改时间 获取话题列表
        /// </summary>
        public List<ForumEntity> GetForumsByModified(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_ForumContent\"/><FieldRef Name=\"Field_KMS_ForumType\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/><FieldRef Name=\"Field_KMS_ViewCount\"/><FieldRef Name=\"Field_KMS_CommentsCount\"/><FieldRef Name=\"Field_KMS_LastComments\"/>";
            List<SearchHelper.OrderBys> orders = new List<SearchHelper.OrderBys>();
            orders.Add(new SearchHelper.OrderBys { Ascending = false, Field = "Field_KMS_LastComments" });
            orders.Add(new SearchHelper.OrderBys { Ascending = false, Field = "Field_KMS_IsTop" });
            string caml = SearchHelper.OrderBy(SearchHelper.Where(SearchHelper.IsNotNull("Title")), orders);

            if (!string.IsNullOrEmpty(obj.CategoryId))
            {
                string forumtypeSearch = string.Empty;
                string[] categories = obj.CategoryId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> conditions = new List<string>();
                foreach (string category in categories)
                {
                    conditions.Add(SearchHelper.Contains("Field_KMS_ForumType", SearchHelper.ValueType.Lookup, category));
                }
                if (conditions.Count > 1)
                {
                    forumtypeSearch = SearchHelper.Or(conditions.ToArray());
                }
                else
                {
                    forumtypeSearch = conditions[0];
                }
                caml = SearchHelper.OrderBy(SearchHelper.Where(
                    SearchHelper.And(
                   SearchHelper.IsNotNull("Title"),
                    forumtypeSearch
                    )), orders);
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    caml = SearchHelper.OrderBy(SearchHelper.Where(
                        SearchHelper.And(
                         forumtypeSearch,
                        SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                        )), orders);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    caml = SearchHelper.OrderBy(SearchHelper.Where(
                        SearchHelper.And(
                         SearchHelper.IsNotNull("Title"),
                        SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                        )), orders);
                }
            }
            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, obj.listname, views, caml, obj.TakeCount, obj.LimitRows);
            List<ForumEntity> _ritems = new List<ForumEntity>();
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                    UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                    foreach (SPListItem _item in items)
                    {
                        SPFieldUser userfield = (SPFieldUser)_item.ParentList.Fields.GetFieldByInternalName("Author");
                        SPFieldUserValue fieldvalue = (SPFieldUserValue)(userfield.GetFieldValue((string)_item[userfield.InternalName]));
                        UserProfile user = userProfileMgr.GetUserProfile(fieldvalue.User.LoginName);
                        _ritems.Add(new ForumEntity(_item, user.DisplayName, user["PictureURL"] == null ? "" : (user["PictureURL"].Value + "")));
                    }
                }
            }

            return _ritems;
        }
        /// <summary>
        ///通过作者 获取话题列表
        /// </summary>
        public List<ForumEntity> GetForumsByAuthor(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_ForumContent\"/><FieldRef Name=\"Field_KMS_ForumType\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/><FieldRef Name=\"Field_KMS_ViewCount\"/><FieldRef Name=\"Field_KMS_CommentsCount\"/><FieldRef Name=\"Field_KMS_LastComments\"/>";
            List<SearchHelper.OrderBys> orders = new List<SearchHelper.OrderBys>();
            orders.Add(new SearchHelper.OrderBys { Ascending = false, Field = "Field_KMS_LastComments" });
            orders.Add(new SearchHelper.OrderBys { Ascending = false, Field = "Title" });
            string caml = SearchHelper.OrderBy(SearchHelper.Where(
                 SearchHelper.And(
                    SearchHelper.Eq("Author", SearchHelper.ValueType.Integer, "<UserID />"),
                    SearchHelper.IsNotNull("Title")
                )), orders);

            if (!string.IsNullOrEmpty(obj.CategoryId))
            {
                string forumtypeSearch = string.Empty;
                string[] categories = obj.CategoryId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> conditions = new List<string>();
                foreach (string category in categories)
                {
                    conditions.Add(SearchHelper.Contains("Field_KMS_ForumType", SearchHelper.ValueType.Lookup, category));
                }
                if (conditions.Count > 1)
                {
                    forumtypeSearch = SearchHelper.Or(conditions.ToArray());
                }
                else
                {
                    forumtypeSearch = conditions[0];
                }
                caml = SearchHelper.OrderBy(SearchHelper.Where(
                    SearchHelper.And(
                    SearchHelper.Eq("Author", SearchHelper.ValueType.Integer, "<UserID />"),
                    forumtypeSearch
                    )), orders);
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    caml = SearchHelper.OrderBy(SearchHelper.Where(
                        SearchHelper.And(
                         forumtypeSearch,
                          SearchHelper.Eq("Author", SearchHelper.ValueType.Integer, "<UserID />"),
                        SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                        )), orders);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    caml = SearchHelper.OrderBy(SearchHelper.Where(
                        SearchHelper.And(
                        SearchHelper.Eq("Author", SearchHelper.ValueType.Integer, "<UserID />"),
                        SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                        )), orders);
                }
            }
            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, obj.listname, views, caml, obj.TakeCount, obj.LimitRows);
            List<ForumEntity> _ritems = new List<ForumEntity>();
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                    UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                    foreach (SPListItem _item in items)
                    {
                        SPFieldUser userfield = (SPFieldUser)_item.ParentList.Fields.GetFieldByInternalName("Author");
                        SPFieldUserValue fieldvalue = (SPFieldUserValue)(userfield.GetFieldValue((string)_item[userfield.InternalName]));
                        UserProfile user = userProfileMgr.GetUserProfile(fieldvalue.User.LoginName);
                        _ritems.Add(new ForumEntity(_item, user.DisplayName, user["PictureURL"] == null ? "" : (user["PictureURL"].Value + "")));
                    }
                }
            }

            return _ritems;
        }
        /// <summary>
        ///通过回复 获取话题列表
        /// </summary>
        public List<ForumEntity> GetForumsByReply(PagingEntity obj)
        {
            List<ForumEntity> result = new List<ForumEntity>();
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            string userlogin = GeneralHelper.resolveLogon(SPContext.Current.Web.CurrentUser.LoginName);
            using (ForumCommentsDataContext db = new ForumCommentsDataContext(setting.DefaultValue))
            {
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    var q = db.KMSForums.Where(p => p.CreatedUserName == userlogin && p.Title.Contains(obj.Search)).GroupBy(p => p.ItemId).Select(p => new { p.Key, Num = p.Max(g => g.Created) }).OrderByDescending(p => p.Num).Skip(obj.LimitRows * obj.TakeCount).Take(obj.LimitRows);
                    foreach (var i in q)
                    {
                        obj.Id = i + "";
                        result.Add(GetForumItemByUniqueId(obj));
                    }
                }
                else
                {
                    var q = db.KMSForums.Where(p => p.CreatedUserName == userlogin).GroupBy(p => p.ItemId).Select(p => new { p.Key, Num = p.Max(g => g.Created) }).OrderByDescending(p => p.Num).Skip(obj.LimitRows * obj.TakeCount).Take(obj.LimitRows);
                    foreach (var i in q)
                    {
                        obj.Id = i.Key + "";
                        result.Add(GetForumItemByUniqueId(obj));
                    }
                }

                return result;
            }
        }
        /// <summary>
        ///通过回复 获取话题数量
        /// </summary>
        public int GetForumsCountByReply(PagingEntity obj)
        {
            int count = 0;
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            string userlogin = GeneralHelper.resolveLogon(SPContext.Current.Web.CurrentUser.LoginName);
            using (ForumCommentsDataContext db = new ForumCommentsDataContext(setting.DefaultValue))
            {
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    count = db.KMSForums.Where(p => p.CreatedUserName == userlogin && p.Title.Contains(obj.Search)).GroupBy(p => p.ItemId).Select(p => new { p.Key, Num = p.Max(g => g.Created) }).Count();
                }
                else
                {
                    count = db.KMSForums.Where(p => p.CreatedUserName == userlogin).GroupBy(p => p.ItemId).Select(p => new { p.Key, Num = p.Max(g => g.Created) }).Count();
                }
                return count;
            }
        }
        /// <summary>
        ///获取话题实体
        /// </summary>
        public ForumEntity GetForumItem(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"Field_KMS_LastComments\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_ForumContent\"/><FieldRef Name=\"Field_KMS_ForumType\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/><FieldRef Name=\"Field_KMS_ViewCount\"/><FieldRef Name=\"Field_KMS_CommentsCount\"/>";
            string caml = SearchHelper.Where(
                SearchHelper.Eq("ID", SearchHelper.ValueType.Text, obj.Id));
            ForumEntity result = new ForumEntity();
            SPListItem item = ListItemHelper.ExecuteCAMLToRetrieveSingleListItem(obj.weburl, obj.listname, views, caml);
            using (SPSite site = new SPSite(obj.weburl))
            {
                SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                SPFieldUser userfield = (SPFieldUser)item.ParentList.Fields.GetFieldByInternalName("Author");
                SPFieldUserValue fieldvalue = (SPFieldUserValue)(userfield.GetFieldValue((string)item[userfield.InternalName]));
                UserProfile user = userProfileMgr.GetUserProfile(fieldvalue.User.LoginName);
                result = new ForumEntity(item, user.DisplayName, user["PictureURL"] == null ? "" : (user["PictureURL"].Value + ""));
            }
            return result;
        }
        /// <summary>
        ///通过GUID 获取话题实体
        /// </summary>
        public ForumEntity GetForumItemByUniqueId(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"Field_KMS_LastComments\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_ForumContent\"/><FieldRef Name=\"Field_KMS_ForumType\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/><FieldRef Name=\"Field_KMS_ViewCount\"/><FieldRef Name=\"Field_KMS_CommentsCount\"/>";
            string caml = SearchHelper.Where(
                SearchHelper.Eq("GUID", SearchHelper.ValueType.Text, obj.Id));
            ForumEntity result = new ForumEntity();
            SPListItem item = ListItemHelper.ExecuteCAMLToRetrieveSingleListItem(obj.weburl, obj.listname, views, caml);
            if (item != null)
            {
                using (SPSite site = new SPSite(obj.weburl))
                {
                    SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                    UserProfileManager userProfileMgr = new UserProfileManager(serviceContext);
                    SPFieldUser userfield = (SPFieldUser)item.ParentList.Fields.GetFieldByInternalName("Author");
                    SPFieldUserValue fieldvalue = (SPFieldUserValue)(userfield.GetFieldValue((string)item[userfield.InternalName]));
                    UserProfile user = userProfileMgr.GetUserProfile(fieldvalue.User.LoginName);
                    result = new ForumEntity(item, user.DisplayName, user["PictureURL"] == null ? "" : (user["PictureURL"].Value + ""));
                }
            }
            return result;
        }
        /// <summary>
        /// 获取话题数量
        /// </summary>
        public int GetForumCount(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_ForumContent\"/><FieldRef Name=\"Field_KMS_ForumType\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.Where(SearchHelper.IsNotNull("Title"));

            if (!string.IsNullOrEmpty(obj.CategoryId))
            {
                string forumtypeSearch = string.Empty;
                string[] categories = obj.CategoryId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> conditions = new List<string>();
                foreach (string category in categories)
                {
                    conditions.Add(SearchHelper.Contains("Field_KMS_ForumType", SearchHelper.ValueType.Lookup, category));
                }
                if (conditions.Count > 1)
                {
                    forumtypeSearch = SearchHelper.Or(conditions.ToArray());
                }
                else
                {
                    forumtypeSearch = conditions[0];
                }
                caml = SearchHelper.Where(
                    SearchHelper.And(
                   SearchHelper.IsNotNull("Title"),
                    forumtypeSearch
                    ));
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    caml = SearchHelper.Where(
                        SearchHelper.And(
                         forumtypeSearch,
                        SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                        ));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    caml = SearchHelper.Where(
                        SearchHelper.And(
                         SearchHelper.IsNotNull("Title"),
                        SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                        ));
                }
            }
            return ListItemHelper.ExecuteCAMLToRetrieveListItemsCount(obj.weburl, obj.listname, views, caml);
        }
        /// <summary>
        ///通过评论者数量获取话题
        /// </summary>
        public int GetForumByAuthorCount(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_ForumContent\"/><FieldRef Name=\"Field_KMS_ForumType\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.Where(SearchHelper.And(
                     SearchHelper.Eq("Author", SearchHelper.ValueType.Integer, "<UserID />"),
                    SearchHelper.IsNotNull("Title")
                ));

            if (!string.IsNullOrEmpty(obj.CategoryId))
            {
                string forumtypeSearch = string.Empty;
                string[] categories = obj.CategoryId.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> conditions = new List<string>();
                foreach (string category in categories)
                {
                    conditions.Add(SearchHelper.Contains("Field_KMS_ForumType", SearchHelper.ValueType.Lookup, category));
                }
                if (conditions.Count > 1)
                {
                    forumtypeSearch = SearchHelper.Or(conditions.ToArray());
                }
                else
                {
                    forumtypeSearch = conditions[0];
                }
                caml = SearchHelper.Where(
                    SearchHelper.And(
                   SearchHelper.Eq("Author", SearchHelper.ValueType.Lookup, SPContext.Current.Web.CurrentUser.UserId),
                    forumtypeSearch
                    ));
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    caml = SearchHelper.Where(
                        SearchHelper.And(
                         forumtypeSearch,
                         SearchHelper.Eq("Author", SearchHelper.ValueType.Integer, "<UserID />"),
                        SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                        ));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(obj.Search))
                {
                    caml = SearchHelper.Where(
                        SearchHelper.And(
                         SearchHelper.Eq("Author", SearchHelper.ValueType.Integer, "<UserID />"),
                        SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                        ));
                }
            }
            return ListItemHelper.ExecuteCAMLToRetrieveListItemsCount(obj.weburl, obj.listname, views, caml);
        }
        /// <summary>
        ///获取浏览次数最多的话题
        /// </summary>
        public List<TopEntity> GetTopViews(PagingEntity obj)
        {
            List<TopEntity> result = new List<TopEntity>();
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (ForumViewHistoryDataContext db = new ForumViewHistoryDataContext(setting.DefaultValue))
            {
                var q = db.KMSForumViewHistories.GroupBy(p => p.ItemId).Select(p => new { p.Key, Num = p.Count() }).OrderByDescending(p => p.Num).Skip(obj.LimitRows * (obj.TakeCount - 1)).Take(obj.LimitRows);
                foreach (var i in q)
                {
                    result.Add(new TopEntity { ItemId = i.Key, Count = i.Num });
                }
            }
            return result;
        }
        /// <summary>
        ///添加浏览次数
        /// </summary>
        public void InsertView(PagingEntity obj)
        {
            lock (thisLock)
            {
                using (SPSite site = new SPSite(obj.weburl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists[obj.listname];
                        SPListItem item = list.GetItemById(int.Parse(obj.Id));
                        if (item != null)
                        {
                            item["Field_KMS_ViewCount"] = int.Parse(item["Field_KMS_ViewCount"] + "") + 1;
                            web.AllowUnsafeUpdates = true;
                            item.Update();
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }

            }
        }
        /// <summary>
        ///添加评论个数
        /// </summary>
        public void InsertCommentsCount(PagingEntity obj)
        {
            lock (commentsLock)
            {
                using (SPSite site = new SPSite(obj.weburl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists[obj.listname];
                        SPListItem item = list.GetItemById(int.Parse(obj.CategoryId));
                        if (item != null)
                        {
                            item["Field_KMS_CommentsCount"] = int.Parse(item["Field_KMS_CommentsCount"] + "") + 1;
                            item["Field_KMS_LastComments"] = DateTime.Now;
                            web.AllowUnsafeUpdates = true;
                            item.Update();
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }

            }
        }
        /// <summary>
        ///添加话题
        /// </summary>
        public int InsertForum(ForumEntity obj)
        {
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[obj.listname];
                    SPListItem item = list.Items.Add();
                    item["Title"] = obj.Title;
                    item["Field_KMS_ForumContent"] = obj.Content;
                    SPFieldLookupValueCollection lookups = new SPFieldLookupValueCollection();
                    string[] categories = obj.ForumType.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string category in categories)
                    {
                        SPListItem lookupitem = GetForumTypeById(obj.weburl, "ForumType", category);
                        if (lookupitem != null)
                            lookups.Add(new SPFieldLookupValue(lookupitem.ID, lookupitem["Title"] + ""));
                    }
                    item["Field_KMS_ForumType"] = lookups;
                    item["Field_KMS_LastComments"] = DateTime.Now;
                    item["Field_KMS_ViewCount"] = 0;
                    item["Field_KMS_CommentsCount"] = 0;
                    web.AllowUnsafeUpdates = true;
                    item.Update();
                    web.AllowUnsafeUpdates = false;
                    return item.ID;
                }
            }
        }
        /// <summary>
        ///通过GUID获取评论列表
        /// </summary>
        public List<CommentsEntity> GetCommentsByGUID(PagingEntity obj)
        {
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (ForumCommentsDataContext db = new ForumCommentsDataContext(setting.DefaultValue))
            {
                return db.KMSForums.Where(p => p.ItemId == obj.Id).OrderBy(p => p.Created)
                    .Skip(obj.LimitRows * (obj.TakeCount - 1)).Take(obj.LimitRows)
                    .Select(p => new CommentsEntity(p)).ToList();
            }
        }
        /// <summary>
        ///获取评论总数
        /// </summary>
        public int GetCommentsTotalCount(PagingEntity obj)
        {
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (ForumCommentsDataContext db = new ForumCommentsDataContext(setting.DefaultValue))
            {
                return db.KMSForums.Where(p => p.ItemId == obj.Id).Select(p => p.Id).Count();
            }
        }
        /// <summary>
        ///添加评论
        /// </summary>
        public int InsertComments(PagingEntity obj)
        {
            int i = 0;
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (ForumCommentsDataContext db = new ForumCommentsDataContext(setting.DefaultValue))
            {
                KMSForum item = new KMSForum()
                {
                    Comments = obj.Search,
                    Created = DateTime.Now,
                    CreatedBy = SPContext.Current.Web.CurrentUser.Name,
                    ItemId = obj.Id,
                    CreatedUserName = GeneralHelper.resolveLogon(SPContext.Current.Web.CurrentUser.LoginName),
                    Title = obj.WikiId
                };
                db.KMSForums.InsertOnSubmit(item);
                db.SubmitChanges();
                i = item.Id;
            }
            InsertCommentsCount(obj);
            return i;
        }
        /// <summary>
        ///删除评论
        /// </summary>
        public int DeleteComments(PagingEntity obj)
        {
            int i = int.Parse(obj.Id);
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (ForumCommentsDataContext db = new ForumCommentsDataContext(setting.DefaultValue))
            {
                KMSForum item = db.KMSForums.Where(p => p.Id == i).FirstOrDefault();
                db.KMSForums.DeleteOnSubmit(item);
                db.SubmitChanges();
            }
            return i;
        }
        /// <summary>
        ///置顶
        /// </summary>
        public void SetTop(PagingEntity obj)
        {
            lock (topLock)
            {
                using (SPSite site = new SPSite(obj.weburl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists[obj.listname];
                        SPListItem item = list.GetItemById(int.Parse(obj.Id));
                        if (item != null)
                        {
                            item["Field_KMS_IsTop"] = obj.Search;
                            web.AllowUnsafeUpdates = true;
                            item.Update();
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }

            }
        }
        /// <summary>
        ///删除话题
        /// </summary>
        public int DeleteForum(PagingEntity obj)
        {
            int i = int.Parse(obj.Id);
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[obj.listname];
                    SPListItem item = list.GetItemById(i);
                    if (item != null)
                    {
                        string UniqueId = item["GUID"] + "";
                        web.AllowUnsafeUpdates = true;
                        item.Delete();
                        web.AllowUnsafeUpdates = false;
                        SettingsMgr mgr = new SettingsMgr();
                        SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
                        using (ForumCommentsDataContext db = new ForumCommentsDataContext(setting.DefaultValue))
                        {
                            IEnumerable<KMSForum> commentsitem = db.KMSForums.Where(p => p.ItemId == UniqueId);
                            if (commentsitem.Count() > 0)
                            {
                                db.KMSForums.DeleteAllOnSubmit(commentsitem);
                                db.SubmitChanges();
                            }
                        }
                    }
                }
            }
            return i;
        }
        /// <summary>
        ///编辑话题
        /// </summary>
        public int EditForum(ForumEntity obj)
        {
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[obj.listname];
                    SPListItem item = list.GetItemById(int.Parse(obj.Id));
                    if (item != null)
                    {
                        item["Title"] = obj.Title;
                        item["Field_KMS_ForumContent"] = obj.Content;
                        SPFieldLookupValueCollection lookups = new SPFieldLookupValueCollection();
                        string[] categories = obj.ForumType.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string category in categories)
                        {
                            SPListItem lookupitem = GetForumTypeById(obj.weburl, "ForumType", category);
                            if (lookupitem != null)
                                lookups.Add(new SPFieldLookupValue(lookupitem.ID, lookupitem["Title"] + ""));
                        }
                        item["Field_KMS_ForumType"] = lookups;
                        item["Field_KMS_LastComments"] = DateTime.Now;
                        web.AllowUnsafeUpdates = true;
                        item.Update();
                        web.AllowUnsafeUpdates = false;
                    }
                    return item.ID;
                }
            }
        }
    }
}
