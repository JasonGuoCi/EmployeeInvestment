using Envision.KMS.Model.Models.Announcement;
using Envision.KMS.Model.Models.MailTemplate;
using Envision.KMS.Model.Models.Settings;
using Focuswin.SP.Base.Utility;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.CaseManagement
{
    public class CaseManagementMgr
    {
        private Object topLock = new Object();
        /// <summary>
        /// 获取case类型
        /// </summary>
        public List<CastTypeEntity> GetCastTypes(PagingEntity obj)
        {
            List<CastTypeEntity> results = new List<CastTypeEntity>();
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[obj.listname];
                    SPQuery query = new SPQuery();
                    query.ViewFields = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"Field_Case_User\"/><FieldRef Name=\"Field_Case_EnglishTitle\"/>";
                    query.Query = SearchHelper.Where(SearchHelper.IsNotNull("Title"));
                    SPListItemCollection items = list.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        results.Add(new CastTypeEntity() { Id = item["ID"] + "", Title = obj.Language == "1033" ? (item["Field_Case_EnglishTitle"] + "") : (item["Title"] + "") });
                    }
                }
            }
            return results;
        }
        /// <summary>
        ///通过case ID 获取case类型
        /// </summary>
        /// <param name="weburl">网页地址</param>
        /// <param name="listname">列表名称</param>
        ///<param name="Id">case编号</param> 
        public SPListItem GetCaseTypeById(string weburl, string listname, string Id)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"Field_Case_User\"/><FieldRef Name=\"Field_Case_EnglishTitle\"/>";
            string caml = SearchHelper.Where(
                SearchHelper.Eq("ID", SearchHelper.ValueType.Text, Id));
            SPListItem item = ListItemHelper.ExecuteCAMLToRetrieveSingleListItem(weburl, listname, views, caml);
            return item;
        }
         /// <summary>
        ///添加case到sharepoint列表并添加任务到数据库CaseManagementTask表中以及发送邮件给case类型对应的负责人
        /// </summary>
        public int InsertCase(CaseManagementEntity obj)
        {

            SPListItem item = null;
            if (string.IsNullOrEmpty(obj.Id))
            {
                SPFieldUserValue fieldvalue = null;
                string taskId = "";
                using (SPSite site = new SPSite(obj.weburl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {  
                        SPList list = web.Lists[obj.listname];
                        item = list.Items.Add();
                        item["Field_Case_Status"] = "申请中";
                        SPFieldLookupValue lookup = new SPFieldLookupValue(int.Parse(obj.CaseType), obj.CaseTypeDisplay);
                        item["Field_Case_Type"] = lookup;
                        item["Title"] = obj.Title;
                        item["Field_Case_Content"] = obj.Content;
                        web.AllowUnsafeUpdates = true;
                        item.Update();
                        web.AllowUnsafeUpdates = false;

                        SPListItem CaseType = GetCaseTypeById(obj.weburl, "CaseType", obj.CaseType);
                        SPFieldUser userfield = (SPFieldUser)CaseType.ParentList.Fields.GetFieldByInternalName("Field_Case_User");
                        fieldvalue = (SPFieldUserValue)(userfield.GetFieldValue((string)CaseType[userfield.InternalName]));
                        SettingsMgr mgr = new SettingsMgr();
                        SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
                        using (CaseManagementDataContext db = new CaseManagementDataContext(setting.DefaultValue))
                        {
                            CaseManagementTask newtask = new CaseManagementTask
                            {
                                AssignedToAccount = GeneralHelper.resolveLogon(fieldvalue.User.LoginName),
                                AssignedToDisplayName = fieldvalue.User.Name,
                                CaseId = item["GUID"] + "",
                                CaseTitle = obj.Title,
                                Created = DateTime.Now,
                                Modified = DateTime.Now,
                                CaseType = obj.CaseType,
                                Status = "Pending",
                                WebID = web.ID + "",
                                CreatedBy = SPContext.Current.Web.CurrentUser.Name,
                                CreatedByAccount = GeneralHelper.resolveLogon(SPContext.Current.Web.CurrentUser.Name),
                                AssignedToEmail = fieldvalue.User.Email
                            };
                            db.CaseManagementTasks.InsertOnSubmit(newtask);
                            db.SubmitChanges();
                            taskId = newtask.Id + "";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(fieldvalue.User.Email))
                {
                    MailTemplateMgr mailMgr = new MailTemplateMgr();
                    MailTemplateEntity mailTemplate = mailMgr.GetSystemSetting(obj.weburl, "Created");
                    SendEmail(fieldvalue.User.Email, mailTemplate.Subject, ReplaceMailTemplate(mailTemplate.Body,
                        SPContext.Current.Web.CurrentUser.Name,
                        obj.Title,
                        DateTime.Now.ToString("yyyy年MM月dd日 HH:mm"),
                        GeneralHelper.ConcatUrlString(obj.weburl, "Pages/Projects/CaseManagement.aspx?IsTask=1&ItemId=" + item["GUID"] + "&TaskId=" + taskId)
                        , fieldvalue.User.Name));
                }
            }
            else
            {
                using (SPSite site = new SPSite(obj.weburl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists[obj.listname];
                        item = list.GetItemById(int.Parse(obj.Id));
                        if (item != null)
                        {
                            item["Title"] = obj.Title;
                            item["Field_Case_Content"] = obj.Content;
                            web.AllowUnsafeUpdates = true;
                            item.Update();
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
            }

            return item.ID;
        }
        /// <summary>
        /// 获取用户申请的case
        /// </summary>
        public List<CaseManagementEntity> GetMyApply(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_Case_Content\"/><FieldRef Name=\"Field_Case_Type\"/><FieldRef Name=\"Field_Case_Comments\"/><FieldRef Name=\"Field_Case_Status\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/>";
            List<string> conditions = new List<string>();
            conditions.Add(SearchHelper.Eq("Field_Case_Status", SearchHelper.ValueType.Text, obj.CategoryId));
            conditions.Add(SearchHelper.Eq("Author", SearchHelper.ValueType.Integer, "<UserID />"));
            if (!string.IsNullOrEmpty(obj.WikiId))
            {
                conditions.Add(SearchHelper.Eq("Field_Case_Type", SearchHelper.ValueType.Lookup, obj.WikiId));
            }
            if (!string.IsNullOrEmpty(obj.Search))
            {
                conditions.Add(SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search));
            }
            string caml = SearchHelper.OrderBy(
                SearchHelper.Where(
                SearchHelper.And(conditions.ToArray()))
                , "Created", false);
            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, obj.listname, views, caml, obj.TakeCount, obj.LimitRows);
            List<CaseManagementEntity> _ritems = new List<CaseManagementEntity>();
            foreach (SPListItem _item in items)
            {
                string CaseType = new SPFieldLookupValue(_item["Field_Case_Type"] as String).LookupId + "";
                SPListItem casetypeitem = GetCaseTypeById(obj.weburl, "CaseType", CaseType);
                if (casetypeitem != null)
                    _ritems.Add(new CaseManagementEntity(_item, obj.Language == "1033" ? (casetypeitem["Field_Case_EnglishTitle"] + "") : (casetypeitem["Title"] + "")));
                else
                    _ritems.Add(new CaseManagementEntity(_item, new SPFieldLookupValue(_item["Field_Case_Type"] as String).LookupValue + ""));
            }
            return _ritems;
        }
        /// <summary>
        /// 获取用户申请的case数量  
        /// </summary>
        public int GetMyApplyCount(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_Case_Content\"/><FieldRef Name=\"Field_Case_Type\"/><FieldRef Name=\"Field_Case_Comments\"/><FieldRef Name=\"Field_Case_Status\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/>";
            List<string> conditions = new List<string>();
            conditions.Add(SearchHelper.Eq("Field_Case_Status", SearchHelper.ValueType.Text, obj.CategoryId));
            conditions.Add(SearchHelper.Eq("Author", SearchHelper.ValueType.Integer, "<UserID />"));
            if (!string.IsNullOrEmpty(obj.WikiId))
            {
                conditions.Add(SearchHelper.Eq("Field_Case_Type", SearchHelper.ValueType.Lookup, obj.WikiId));
            }
            if (!string.IsNullOrEmpty(obj.Search))
            {
                conditions.Add(SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search));
            }
            string caml = SearchHelper.Where(SearchHelper.And(conditions.ToArray()));
            return ListItemHelper.ExecuteCAMLToRetrieveListItemsCount(obj.weburl, obj.listname, views, caml);
        }
        /// <summary>
        /// 获取所有还没处理的case任务
        /// </summary>

        public List<CaseManagementEntity> GetAllApply(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_Case_Content\"/><FieldRef Name=\"Field_Case_Type\"/><FieldRef Name=\"Field_Case_Comments\"/><FieldRef Name=\"Field_Case_Status\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/>";
            List<string> conditions = new List<string>();
            conditions.Add(SearchHelper.Eq("Field_Case_Status", SearchHelper.ValueType.Text, "申请中"));
            conditions.Add(SearchHelper.IsNotNull("Title"));
            if (!string.IsNullOrEmpty(obj.WikiId))
            {
                conditions.Add(SearchHelper.Eq("Field_Case_Type", SearchHelper.ValueType.Lookup, obj.WikiId));
            }
            if (!string.IsNullOrEmpty(obj.Search))
            {
                conditions.Add(SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search));
            }
            string caml = SearchHelper.OrderBy(
                SearchHelper.Where(
                SearchHelper.And(conditions.ToArray()))
                , "Created", false);
            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, obj.listname, views, caml, obj.TakeCount, obj.LimitRows);
            List<CaseManagementEntity> _ritems = new List<CaseManagementEntity>();
            foreach (SPListItem _item in items)
            {
                string CaseType = new SPFieldLookupValue(_item["Field_Case_Type"] as String).LookupId + "";
                SPListItem casetypeitem = GetCaseTypeById(obj.weburl, "CaseType", CaseType);
                if (casetypeitem != null)
                    _ritems.Add(new CaseManagementEntity(_item, obj.Language == "1033" ? (casetypeitem["Field_Case_EnglishTitle"] + "") : (casetypeitem["Title"] + "")));
                else
                    _ritems.Add(new CaseManagementEntity(_item, new SPFieldLookupValue(_item["Field_Case_Type"] as String).LookupValue + ""));
            }
            return _ritems;
        }
        /// <summary>
        /// 获取所有还没处理的case任务数量
        /// </summary>

        public int GetAllApplyCount(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_Case_Content\"/><FieldRef Name=\"Field_Case_Type\"/><FieldRef Name=\"Field_Case_Comments\"/><FieldRef Name=\"Field_Case_Status\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/>";
            List<string> conditions = new List<string>();
            conditions.Add(SearchHelper.Eq("Field_Case_Status", SearchHelper.ValueType.Text, "申请中"));
            conditions.Add(SearchHelper.IsNotNull("Title"));
            if (!string.IsNullOrEmpty(obj.WikiId))
            {
                conditions.Add(SearchHelper.Eq("Field_Case_Type", SearchHelper.ValueType.Lookup, obj.WikiId));
            }
            if (!string.IsNullOrEmpty(obj.Search))
            {
                conditions.Add(SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search));
            }
            string caml = SearchHelper.Where(SearchHelper.And(conditions.ToArray()));
            return ListItemHelper.ExecuteCAMLToRetrieveListItemsCount(obj.weburl, obj.listname, views, caml);
        }
        /// <summary>
        /// 通过case编号获取case
        /// </summary>
        public CaseManagementEntity GetCaseById(PagingEntity obj)
        {
            CaseManagementEntity result = new CaseManagementEntity();
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_Case_Content\"/><FieldRef Name=\"Field_Case_Type\"/><FieldRef Name=\"Field_Case_Status\"/><FieldRef Name=\"Field_Case_Comments\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/>";
            string caml = SearchHelper.Where(
                SearchHelper.Eq("ID", SearchHelper.ValueType.Text, obj.Id));
            SPListItem item = ListItemHelper.ExecuteCAMLToRetrieveSingleListItem(obj.weburl, obj.listname, views, caml);
            if (item != null)
            {
                string CaseType = new SPFieldLookupValue(item["Field_Case_Type"] as String).LookupId + "";
                SPListItem casetypeitem = GetCaseTypeById(obj.weburl, "CaseType", CaseType);
                if (casetypeitem != null)
                    result = new CaseManagementEntity(item, obj.Language == "1033" ? (casetypeitem["Field_Case_EnglishTitle"] + "") : (casetypeitem["Title"] + ""));
                else
                {
                    result = new CaseManagementEntity(item, new SPFieldLookupValue(item["Field_Case_Type"] as String).LookupValue + "");
                }
                SettingsMgr mgr = new SettingsMgr();
                SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
                using (CaseManagementDataContext db = new CaseManagementDataContext(setting.DefaultValue))
                {
                    List<CaseTaskEntity> Comments = new List<CaseTaskEntity>();
                    Comments = db.CaseManagementTasks.Where(p => p.CaseId == obj.Id && p.Status == "Finish").OrderByDescending(p => p.Created).Select(p => new CaseTaskEntity(p)).ToList();
                    result.taskitems = Comments;
                }
            }
            return result;
        }
        /// <summary>
        /// 通过caseGUID获取case
        /// </summary>

        public CaseManagementEntity GetCaseByUniqueId(PagingEntity obj)
        {
            CaseManagementEntity result = new CaseManagementEntity();
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_Case_Content\"/><FieldRef Name=\"Field_Case_Type\"/><FieldRef Name=\"Field_Case_Status\"/><FieldRef Name=\"Field_Case_Comments\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/>";
            string caml = SearchHelper.Where(
                SearchHelper.Eq("GUID", SearchHelper.ValueType.Text, obj.Id));
            SPListItem item = ListItemHelper.ExecuteCAMLToRetrieveSingleListItem(obj.weburl, obj.listname, views, caml);
            if (item != null)
            {
                string CaseType = new SPFieldLookupValue(item["Field_Case_Type"] as String).LookupId + "";
                SPListItem casetypeitem = GetCaseTypeById(obj.weburl, "CaseType", CaseType);
                if (casetypeitem != null)
                    result = new CaseManagementEntity(item, obj.Language == "1033" ? (casetypeitem["Field_Case_EnglishTitle"] + "") : (casetypeitem["Title"] + ""));
                else
                    result = new CaseManagementEntity(item, new SPFieldLookupValue(item["Field_Case_Type"] as String).LookupValue + "");
                SettingsMgr mgr = new SettingsMgr();
                SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
                using (CaseManagementDataContext db = new CaseManagementDataContext(setting.DefaultValue))
                {
                    List<CaseTaskEntity> Comments = new List<CaseTaskEntity>();
                    Comments = db.CaseManagementTasks.Where(p => p.CaseId == obj.Id && p.Status == "Finish").OrderByDescending(p => p.Created).Select(p => new CaseTaskEntity(p)).ToList();
                    result.taskitems = Comments;
                }
            }
            return result;
        }
        /// <summary>
        /// 删除case
        /// </summary>
        public int DeleteCase(PagingEntity obj)
        {
            SPFieldUserValue fieldvalue = null;
            int i = int.Parse(obj.Id);
            string Approvername = "";
            string ApproverEmail = "";
            string CaseTitle = "";
            string Created = "";
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[obj.listname];
                    SPListItem item = list.GetItemById(i);
                    if (item != null)
                    {
                        string UniqueId = item["GUID"] + "";
                        SPFieldUser userfield = (SPFieldUser)item.ParentList.Fields.GetFieldByInternalName("Author");
                        fieldvalue = (SPFieldUserValue)(userfield.GetFieldValue((string)item[userfield.InternalName]));
                        CaseTitle = item["Title"] + "";
                        Created = (DateTime.Parse(item["Created"] + "")).ToString("yyyy/MM/dd HH:mm");
                        web.AllowUnsafeUpdates = true;
                        item.Delete();
                        web.AllowUnsafeUpdates = false;
                        SettingsMgr mgr = new SettingsMgr();
                        SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
                        using (CaseManagementDataContext db = new CaseManagementDataContext(setting.DefaultValue))
                        {
                            IEnumerable<CaseManagementTask> commentsitem = db.CaseManagementTasks.Where(p => p.CaseId == UniqueId);
                            if (commentsitem.Count() > 0)
                            {
                                foreach (CaseManagementTask pendingtask in commentsitem)
                                {
                                    if (pendingtask.Status == "Pending")
                                    {
                                        Approvername = pendingtask.AssignedToDisplayName;
                                        ApproverEmail = pendingtask.AssignedToEmail;
                                    }
                                }
                                db.CaseManagementTasks.DeleteAllOnSubmit(commentsitem);
                                db.SubmitChanges();
                            }
                        }

                        if (obj.CategoryId == "IsAdmin")//判断是不是管理员，如果是管理员就发送邮件给作者和审批者，不是的话就只发送邮件给审批者
                        {
                            if (!string.IsNullOrEmpty(fieldvalue.User.Email))
                            {
                                MailTemplateMgr mailMgr = new MailTemplateMgr();
                                MailTemplateEntity mailTemplate = mailMgr.GetSystemSetting(obj.weburl, "AdminDeleteToAuthor");
                                SendEmail(fieldvalue.User.Email, mailTemplate.Subject, ReplaceMailTemplate(mailTemplate.Body,
                                    fieldvalue.User.Name,
                                    CaseTitle,
                                    Created,
                                    "",
                                    Approvername));
                            }
                            if (!string.IsNullOrEmpty(ApproverEmail))
                            {
                                MailTemplateMgr mailMgr = new MailTemplateMgr();
                                MailTemplateEntity mailTemplate = mailMgr.GetSystemSetting(obj.weburl, "AdminDeleteToApprover");
                                SendEmail(fieldvalue.User.Email, mailTemplate.Subject, ReplaceMailTemplate(mailTemplate.Body,
                                    fieldvalue.User.Name,
                                    CaseTitle,
                                    Created,
                                    "",
                                    Approvername));
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ApproverEmail))
                            {
                                MailTemplateMgr mailMgr = new MailTemplateMgr();
                                MailTemplateEntity mailTemplate = mailMgr.GetSystemSetting(obj.weburl, "DeleteToApprover");
                                SendEmail(fieldvalue.User.Email, mailTemplate.Subject, ReplaceMailTemplate(mailTemplate.Body,
                                    fieldvalue.User.Name,
                                    CaseTitle,
                                    Created,
                                    "",
                                    Approvername));
                            }
                        }
                        
                    }
                }
            }
            return i;
        }
        /// <summary>
        /// 置顶
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
        /// 获取任务列表
        /// </summary>
        public List<CaseTaskEntity> GetTask(PagingEntity obj)
        {
            string webId = SPContext.Current.Web.ID + "";
            List<CaseTaskEntity> results = new List<CaseTaskEntity>();
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            string loginname = GeneralHelper.resolveLogon(SPContext.Current.Web.CurrentUser.LoginName);
            using (CaseManagementDataContext db = new CaseManagementDataContext(setting.DefaultValue))
            {
                IEnumerable<CaseManagementTask> tasks = null;
                if (!string.IsNullOrEmpty(obj.WikiId))
                {
                    if (!string.IsNullOrEmpty(obj.Search))
                    {
                        tasks = db.CaseManagementTasks.Where(p => p.AssignedToAccount == loginname && p.Status == obj.CategoryId && p.WebID == webId && p.CaseType == obj.WikiId && p.CaseTitle.Contains(obj.Search)).OrderByDescending(p => p.Created).Skip(obj.LimitRows * obj.TakeCount).Take(obj.LimitRows);
                    }
                    else
                    {
                        tasks = db.CaseManagementTasks.Where(p => p.AssignedToAccount == loginname && p.Status == obj.CategoryId && p.WebID == webId && p.CaseType == obj.WikiId).OrderByDescending(p => p.Created).Skip(obj.LimitRows * obj.TakeCount).Take(obj.LimitRows);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(obj.Search))
                    {
                        tasks = db.CaseManagementTasks.Where(p => p.AssignedToAccount == loginname && p.Status == obj.CategoryId && p.WebID == webId && p.CaseTitle.Contains(obj.Search)).OrderByDescending(p => p.Created).Skip(obj.LimitRows * obj.TakeCount).Take(obj.LimitRows);
                    }
                    else
                    {
                        tasks = db.CaseManagementTasks.Where(p => p.AssignedToAccount == loginname && p.Status == obj.CategoryId && p.WebID == webId).OrderByDescending(p => p.Created).Skip(obj.LimitRows * obj.TakeCount).Take(obj.LimitRows);
                    }
                }

                foreach (CaseManagementTask task in tasks)
                {
                    SPListItem casetype = GetCaseTypeById(obj.weburl, "CaseType", task.CaseType);
                    if (casetype != null)
                        results.Add(new CaseTaskEntity(task, obj.Language == "1033" ? (casetype["Field_Case_EnglishTitle"] + "") : (casetype["Title"] + "")));
                    else
                        results.Add(new CaseTaskEntity(task, task.CaseType));
                }
            }
            return results;
        }
        /// <summary>
        /// 处理case任务
        /// </summary>
        public int DealTask(PagingEntity obj)
        {
            SPFieldUserValue fieldvalue = null;
            string created = "";
            int taskid = int.Parse(obj.Id);
            string itemId = "";
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (CaseManagementDataContext db = new CaseManagementDataContext(setting.DefaultValue))
            {
                CaseManagementTask newtask = db.CaseManagementTasks.Where(p => p.Id == taskid).FirstOrDefault();
                if (newtask != null)
                {
                    SPQuery query = new SPQuery();
                    query.Query = SearchHelper.Where(
                        SearchHelper.Eq("GUID", SearchHelper.ValueType.Text, newtask.CaseId));
                    using (SPSite site = new SPSite(obj.weburl))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList list = web.Lists[obj.listname];
                            SPListItemCollection listitems = list.GetItems(query);
                            if (listitems.Count > 0)
                            {
                                SPListItem listitem = listitems[0];
                                itemId = listitem.ID + "";

                                SPFieldUser userfield = (SPFieldUser)listitem.ParentList.Fields.GetFieldByInternalName("Author");
                                fieldvalue = (SPFieldUserValue)(userfield.GetFieldValue((string)listitem[userfield.InternalName]));
                                created = listitem["Created"] + "";
                                listitem["Field_Case_Comments"] = obj.Search;
                                listitem["Field_Case_Status"] = "已完成";
                                web.AllowUnsafeUpdates = true;
                                listitem.Update();
                                web.AllowUnsafeUpdates = false;
                            }
                        }
                    }
                    newtask.Status = "Finish";
                    newtask.Result = "处理完成";
                    newtask.Comments = obj.Search;
                    newtask.Modified = DateTime.Now;
                }
                db.SubmitChanges();
                if (!string.IsNullOrEmpty(fieldvalue.User.Email))
                {
                    MailTemplateMgr mailMgr = new MailTemplateMgr();
                    MailTemplateEntity mailTemplate = mailMgr.GetSystemSetting(obj.weburl, "Finish");
                    SendEmail(fieldvalue.User.Email, mailTemplate.Subject, ReplaceMailTemplate(mailTemplate.Body,
                        fieldvalue.User.Name,
                        newtask.CaseTitle,
                        (DateTime.Parse(created)).ToString("yyyy/MM/dd HH:mm"),
                        GeneralHelper.ConcatUrlString(obj.weburl, "Pages/Projects/CaseManagement.aspx?IsDetail=1&ItemId=" + itemId),
                        SPContext.Current.Web.CurrentUser.Name));
                }
            }
            return taskid;
        }
        /// <summary>
        /// 转办任务
        /// </summary>
        public int TransferTask(PagingEntity obj)
        {
            int taskid = int.Parse(obj.Id);
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (CaseManagementDataContext db = new CaseManagementDataContext(setting.DefaultValue))
            {
                CaseManagementTask oldtask = db.CaseManagementTasks.Where(p => p.Id == taskid).FirstOrDefault();
                if (oldtask != null)
                {
                    oldtask.Status = "Finish";
                    oldtask.Result = "将问题处理转到<br/>" + obj.WikiId;
                    oldtask.Comments = obj.Search;
                    oldtask.Modified = DateTime.Now;

                    CaseManagementTask newtask = new CaseManagementTask
                    {
                        AssignedToAccount = GeneralHelper.resolveLogon(obj.CategoryId),
                        AssignedToDisplayName = obj.WikiId,
                        CaseId = oldtask.CaseId,
                        CaseTitle = oldtask.CaseTitle,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CaseType = oldtask.CaseType,
                        Status = "Pending",
                        WebID = oldtask.WebID,
                        CreatedBy = oldtask.CreatedBy,
                        CreatedByAccount = oldtask.CreatedByAccount,
                        AssignedToEmail = obj.listname
                    };
                    db.CaseManagementTasks.InsertOnSubmit(newtask);
                    db.SubmitChanges();
                    if (!string.IsNullOrEmpty(obj.listname))
                    {
                        MailTemplateMgr mailMgr = new MailTemplateMgr();
                        MailTemplateEntity mailTemplate = mailMgr.GetSystemSetting(obj.weburl, "Transfer");
                        SendEmail(obj.listname, mailTemplate.Subject, ReplaceMailTemplate(mailTemplate.Body,
                            newtask.CreatedBy,
                            newtask.CaseTitle,
                            DateTime.Now.ToString("yyyy/MM/dd HH:mm"),
                            GeneralHelper.ConcatUrlString(obj.weburl, "Pages/Projects/CaseManagement.aspx?IsTask=1&ItemId=" + newtask.CaseId + "&TaskId=" + newtask.Id),
                            SPContext.Current.Web.CurrentUser.Name));
                    }
                }

            }
            return taskid;
        }
        /// <summary>
        /// 获取任务数量 
        /// </summary>
        public int GetTaskCount(PagingEntity obj)
        {
            string webId = SPContext.Current.Web.ID + "";
            int results = 0;
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            string loginname = GeneralHelper.resolveLogon(SPContext.Current.Web.CurrentUser.LoginName);
            using (CaseManagementDataContext db = new CaseManagementDataContext(setting.DefaultValue))
            {
                if (!string.IsNullOrEmpty(obj.WikiId))
                {
                    if (!string.IsNullOrEmpty(obj.Search))
                    {
                        results = db.CaseManagementTasks.Where(p => p.AssignedToAccount == loginname && p.Status == obj.CategoryId && p.WebID == webId && p.CaseType == obj.WikiId && p.CaseTitle.Contains(obj.Search)).Count();
                    }
                    else
                    {
                        results = db.CaseManagementTasks.Where(p => p.AssignedToAccount == loginname && p.Status == obj.CategoryId && p.WebID == webId && p.CaseType == obj.WikiId).Count();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(obj.Search))
                    {
                        results = db.CaseManagementTasks.Where(p => p.AssignedToAccount == loginname && p.Status == obj.CategoryId && p.WebID == webId && p.CaseTitle.Contains(obj.Search)).Count();
                    }
                    else
                    {
                        results = db.CaseManagementTasks.Where(p => p.AssignedToAccount == loginname && p.Status == obj.CategoryId && p.WebID == webId).Count();
                    }
                }
            }
            return results;
        }
        /// <summary>
        /// 获取所有问题列表
        /// </summary>

        public List<CaseManagementEntity> GetQueryItems(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_Case_Content\"/><FieldRef Name=\"Field_Case_Type\"/><FieldRef Name=\"Field_Case_Comments\"/><FieldRef Name=\"Field_Case_Status\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/>";
            List<string> conditions = new List<string>();
            conditions.Add(SearchHelper.Eq("Field_Case_Status", SearchHelper.ValueType.Text, "已完成"));
            conditions.Add(SearchHelper.IsNotNull("Title"));
            if (!string.IsNullOrEmpty(obj.WikiId))
            {
                conditions.Add(SearchHelper.Eq("Field_Case_Type", SearchHelper.ValueType.Lookup, obj.WikiId));
            }
            if (!string.IsNullOrEmpty(obj.Search))
            {
                conditions.Add(SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search));
            }
            List<SearchHelper.OrderBys> orders = new List<SearchHelper.OrderBys>();
            orders.Add(new SearchHelper.OrderBys { Ascending = false, Field = "Field_KMS_IsTop" });
            orders.Add(new SearchHelper.OrderBys { Ascending = false, Field = "Created" });
            string caml = SearchHelper.OrderBy(
                SearchHelper.Where(
                SearchHelper.And(conditions.ToArray()))
                , orders);

            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, obj.listname, views, caml, obj.TakeCount, obj.LimitRows);
            List<CaseManagementEntity> _ritems = new List<CaseManagementEntity>();
            foreach (SPListItem _item in items)
            {
                string CaseType = new SPFieldLookupValue(_item["Field_Case_Type"] as String).LookupId + "";
                SPListItem casetypeitem = GetCaseTypeById(obj.weburl, "CaseType", CaseType);
                if (casetypeitem != null)
                    _ritems.Add(new CaseManagementEntity(_item, obj.Language == "1033" ? (casetypeitem["Field_Case_EnglishTitle"] + "") : (casetypeitem["Title"] + "")));
                else
                    _ritems.Add(new CaseManagementEntity(_item, obj.Language == "1033" ? (casetypeitem["Field_Case_EnglishTitle"] + "") : (casetypeitem["Title"] + "")));
            }
            return _ritems;
        }
        /// <summary>
        /// 获取所有问题数量
        /// </summary>

        public int GetQueryItemsCount(PagingEntity obj)
        {
            string webId = SPContext.Current.Web.ID + "";
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_Case_Content\"/><FieldRef Name=\"Field_Case_Type\"/><FieldRef Name=\"Field_Case_Comments\"/><FieldRef Name=\"Field_Case_Status\"/><FieldRef Name=\"Field_KMS_IsTop\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/><FieldRef Name=\"Author\"/>";
            List<string> conditions = new List<string>();
            conditions.Add(SearchHelper.Eq("Field_Case_Status", SearchHelper.ValueType.Text, "已完成"));
            conditions.Add(SearchHelper.IsNotNull("Title"));
            if (!string.IsNullOrEmpty(obj.WikiId))
            {
                conditions.Add(SearchHelper.Eq("Field_Case_Type", SearchHelper.ValueType.Lookup, obj.WikiId));
            }
            if (!string.IsNullOrEmpty(obj.Search))
            {
                conditions.Add(SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search));
            }
            string caml = SearchHelper.Where(SearchHelper.And(conditions.ToArray()));
            return ListItemHelper.ExecuteCAMLToRetrieveListItemsCount(obj.weburl, obj.listname, views, caml);
        }
        /// <summary>
        /// 发送邮件给负责人
        /// </summary>

        public int SendMailforApprove(PagingEntity obj)
        {
            CaseManagementTask newtask = null;
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[obj.listname];
                    SPListItem item = list.GetItemById(int.Parse(obj.Id));
                    string UniqueId = item["GUID"] + "";
                    SettingsMgr mgr = new SettingsMgr();
                    SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
                    using (CaseManagementDataContext db = new CaseManagementDataContext(setting.DefaultValue))
                    {
                        newtask = db.CaseManagementTasks.Where(p => p.CaseId == UniqueId && p.Status == "Pending").FirstOrDefault();
                    }
                }
            }
            if (newtask != null)
            {
                MailTemplateMgr mailMgr = new MailTemplateMgr();
                MailTemplateEntity mailTemplate = mailMgr.GetSystemSetting(obj.weburl, "Reminder");
                if (!string.IsNullOrEmpty(newtask.AssignedToEmail))
                {
                    SendEmail(newtask.AssignedToEmail, mailTemplate.Subject, ReplaceMailTemplate(mailTemplate.Body,
                        SPContext.Current.Web.CurrentUser.Name,
                        newtask.CaseTitle,
                        newtask.Created.ToString("yyyy/MM/dd HH:mm"),
                        GeneralHelper.ConcatUrlString(obj.weburl, "Pages/Projects/CaseManagement.aspx?IsTask=1&ItemId=" + newtask.CaseId + "&TaskId=" + newtask.Id),
                        newtask.AssignedToDisplayName));
                }
                return newtask.Id;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 设置邮件格式并写到日志里
        /// </summary>
        private Boolean SendEmail(string to, string subject, string body)
        {
            Logger log = Logger.GetLogger(typeof(CaseManagementMgr));
            try
            {
                Guid siteid = SPContext.Current.Site.ID;
                SPUrlZone sitezone = SPContext.Current.Site.Zone;
                Guid webid = SPContext.Current.Web.ID;
                bool flag = false;
                SPSecurity.RunWithElevatedPrivileges(
                  delegate()
                  {
                      using (SPSite site = new SPSite(siteid, sitezone))
                      {
                          using (SPWeb web = site.OpenWeb(webid))
                          {
                              flag = SPUtility.SendEmail(web, true, false, to, subject, body);
                          }
                      }
                  });
                log.Debug("SendEmail to:" + to + "</br> Subject:" + subject + "</br> Body:" + body);
                return flag;
            }
            catch (System.Exception exp)
            {
                log.Error("SendEmail Error to:" + to, exp);
                return false;
            }
        }
        /// <summary>
        /// 替换邮件模板
        /// </summary>
        private string ReplaceMailTemplate(string originContent, string author, string title, string created, string linkUrl, string approver = "")
        {
            if (!string.IsNullOrEmpty(originContent))
            {
                originContent = originContent.Replace("[Author]", author);
                originContent = originContent.Replace("[Title]", title);
                originContent = originContent.Replace("[Created]", created);
                originContent = originContent.Replace("[Approver]", approver);
                originContent = originContent.Replace("[LinkUrl]", linkUrl);
                originContent = originContent.Replace("[CurrentDate]", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
            }
            return originContent;
        }

    }
}
