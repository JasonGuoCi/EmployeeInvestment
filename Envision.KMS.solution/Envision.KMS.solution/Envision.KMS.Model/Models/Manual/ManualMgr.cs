using Envision.KMS.Model.Models.Announcement;
using Focuswin.SP.Base.Utility;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Manual
{
    public class ManualMgr
    {
        /// <summary>
        ///获取视频教程列表
        /// </summary>
        public List<ManualFolder> GetVideoFolders(PagingEntity obj)
        {
            List<ManualFolder> maualfolders = new List<ManualFolder>();
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[obj.listname];
                    SPFolder rootfolder = list.RootFolder;
                    SPFolderCollection folders = rootfolder.SubFolders;
                    foreach (SPFolder folder in folders)
                    {
                        if (folder.Item != null)
                            maualfolders.Add(new ManualFolder { Title = folder.Name, Created = folder.Item["Created"] + "", UniqueId = folder.UniqueId + "" });
                    }
                }
            }
            return maualfolders;
        }
        /// <summary>
        ///通过GUID获取视频教程列表
        /// </summary>
        public List<ManualVideo> GetVideosByFolderId(PagingEntity obj)
        {
            List<ManualVideo> manualvideos = new List<ManualVideo>();
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[obj.listname];
                    SPQuery query = new SPQuery();
                    SPFolder subfolder = web.GetFolder(new Guid(obj.Id));
                    query.Folder = subfolder;
                    query.Query = SearchHelper.OrderBy(SearchHelper.Where("<BeginsWith><FieldRef Name='ContentTypeId' /><Value  Type='ContentTypeId'>0x0120D520A808</Value></BeginsWith>"), "Modified", false);
                    SPListItemCollection items = list.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        string previewurl = item["AlternateThumbnailUrl"] + "";
                        if (!string.IsNullOrEmpty(previewurl))
                        {
                            previewurl = previewurl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[1];
                        }
                        manualvideos.Add(new ManualVideo { Title = item.Title, Name = item["Name"] + "", Created = item["Created"] + "", PreviewImage = previewurl, UniqueId = item.UniqueId + "", Path = item.Folder.ServerRelativeUrl });
                    }
                    return manualvideos;
                }
            }
        }
        /// <summary>
        ///通过创建时间获取视频教程列表
        /// </summary>
        public List<ManualVideo> GetVideosByCreated(PagingEntity obj)
        {
            List<ManualVideo> manualvideos = new List<ManualVideo>();
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[obj.listname];
                    SPQuery query = new SPQuery();
                    query.ViewAttributes = "Scope=\"Recursive\"";
                    query.Query = SearchHelper.OrderBy(SearchHelper.Where("<BeginsWith><FieldRef Name='ContentTypeId' /><Value  Type='ContentTypeId'>0x0120D520A808</Value></BeginsWith>"), "Created", false);
                    SPListItemCollection items = list.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        string previewurl = item["AlternateThumbnailUrl"] + "";
                        if (!string.IsNullOrEmpty(previewurl))
                        {
                            previewurl = previewurl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[1];
                        }
                        manualvideos.Add(new ManualVideo { Title = item.Title, Name = item["Name"] + "", Created = item["Created"] + "", PreviewImage = previewurl, UniqueId = item.UniqueId + "", Path = item.Folder.ServerRelativeUrl });
                    }
                    return manualvideos;
                }
            }
        }
        /// <summary>
        ///获取文档教程列表
        /// </summary>
        public List<ManualDocumentEntity> GetManualDocument(PagingEntity obj)
        {
            List<ManualDocumentEntity> manualdocs = new List<ManualDocumentEntity>();
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[obj.listname];
                    SPQuery query = new SPQuery();
                    query.ViewAttributes = "Scope=\"Recursive\"";
                    query.ViewFields = @"<FieldRef Name='FileLeafRef' /><FieldRef Name='Title' /><FieldRef Name='Name' /><FieldRef Name='Created' /><FieldRef Name='ContentTypeId' /><FieldRef Name='Author' /><FieldRef Name='FileRef' />";
                    query.Query = SearchHelper.OrderBy(SearchHelper.Where("<BeginsWith><FieldRef Name='ContentTypeId' /><Value  Type='ContentTypeId'>0x0101</Value></BeginsWith>"), "Created", false);
                    SPListItemCollection items = list.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        manualdocs.Add(new ManualDocumentEntity(item));
                    }
                    return manualdocs;
                }
            }
        }
        /// <summary>
        ///获取网页教程列表
        /// </summary>
        public List<ManualHtmlEntity> GetManualHtml(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_Link\"/><FieldRef Name=\"Field_KMS_EnglishLink\"/><FieldRef Name=\"Field_KMS_Status\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.OrderBy(SearchHelper.Where(SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published")), "Modified", false);
            if (!string.IsNullOrEmpty(obj.Search))
            {
                caml = SearchHelper.OrderBy(SearchHelper.Where(
                    SearchHelper.And(
                     SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published"),
                    SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                    )), "Modified", false);
            }
            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, obj.listname, views, caml, obj.TakeCount, obj.LimitRows);
            List<ManualHtmlEntity> _ritems = new List<ManualHtmlEntity>();
            foreach (SPListItem _item in items)
            {
                _ritems.Add(new ManualHtmlEntity(_item, obj.Language, false));
            }
            return _ritems;
        }
        /// <summary>
        ///获取网页教程实体
        /// </summary>
        public ManualHtmlEntity GetManualHtmlItem(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"Field_KMS_Link\"/><FieldRef Name=\"Field_KMS_EnglishLink\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_HtmlContent\"/><FieldRef Name=\"Field_KMS_Status\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_EnglishHtmlContent\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.Where(
                SearchHelper.Eq("ID", SearchHelper.ValueType.Text, obj.Id));
            return new ManualHtmlEntity(ListItemHelper.ExecuteCAMLToRetrieveSingleListItem(obj.weburl, obj.listname, views, caml), obj.Language, true);
        }
        /// <summary>
        ///获取网页教程总数
        /// </summary>
        public int GetManualHtmlCount(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_Status\"/>";
            string caml = SearchHelper.Where(SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published"));
            if (!string.IsNullOrEmpty(obj.Search))
            {
                caml = SearchHelper.Where(
                    SearchHelper.And(
                    SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published"),
                    SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                    ));
            }
            return ListItemHelper.ExecuteCAMLToRetrieveListItemsCount(obj.weburl, "ManualHtmlLibrary", views, caml);
        }
        /// <summary>
        ///获取文档教程总数
        /// </summary>
        public int GetManualDocCount(PagingEntity obj)
        {
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["ManualDocs"];
                    SPQuery query = new SPQuery();
                    query.ViewAttributes = "Scope=\"Recursive\"";
                    query.ViewFields = @"<FieldRef Name='FileLeafRef' /><FieldRef Name='Title' /><FieldRef Name='ContentTypeId' /><FieldRef Name='Author' /><FieldRef Name='FileRef' />";
                    query.Query =SearchHelper.Where("<BeginsWith><FieldRef Name='ContentTypeId' /><Value  Type='ContentTypeId'>0x0101</Value></BeginsWith>");
                    SPListItemCollection items = list.GetItems(query);
                    return items.Count;
                }
            }
        }
    }
}
