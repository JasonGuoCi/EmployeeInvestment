using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Focuswin.SP.Base.Utility;
using Microsoft.SharePoint;

namespace Envision.KMS.Model.Models.Announcement
{
    public class AnnouncementMgr
    {
        /// <summary>
        /// 获取所有的通告
        /// </summary>
        public List<AnnouncementEntity> GetAnnouncements(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_MultiContent\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_MultiEnglishContent\"/><FieldRef Name=\"Field_KMS_Status\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
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
            List<AnnouncementEntity> _ritems = new List<AnnouncementEntity>();
            foreach (SPListItem _item in items)
            {
                _ritems.Add(new AnnouncementEntity(_item, obj.Language));
            }
            return _ritems;
        }
        /// <summary>
        /// 获取单个通告实体
        /// </summary>
        public AnnouncementEntity GetAnnouncementItem(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_MultiContent\"/><FieldRef Name=\"Field_KMS_Status\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_MultiEnglishContent\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.Where(
                SearchHelper.Eq("ID", SearchHelper.ValueType.Text, obj.Id));
            return new AnnouncementEntity(ListItemHelper.ExecuteCAMLToRetrieveSingleListItem(obj.weburl, obj.listname, views, caml), obj.Language);
        }
        /// <summary>
        /// 获取通告总数
        /// </summary>
        public int GetAnnouncementsCount(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_Status\"/>";
            string caml = SearchHelper.Where(SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "1"));
            if (!string.IsNullOrEmpty(obj.Search))
            {
                caml = SearchHelper.Where(
                    SearchHelper.And(
                    SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published"),
                    SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                    ));
            }
            return ListItemHelper.ExecuteCAMLToRetrieveListItemsCount(obj.weburl, obj.listname, views, caml);
        }
    }
}
