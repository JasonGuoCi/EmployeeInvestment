using Envision.KMS.Model.Models.Announcement;
using Envision.KMS.Model.Models.Settings;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Comments
{
    public class CommentsMgr
    {
        /// <summary>
        /// 通过GUID获取评论列表
        /// </summary>
        public List<CommentsEntity> GetCommentsByGUID(PagingEntity obj)
        {
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (CommentsLinqDataContext db = new CommentsLinqDataContext(setting.DefaultValue))
            {
                return db.KMSComments.Where(p => p.ItemId == obj.Id).OrderBy(p => p.Created)
                    .Skip(obj.LimitRows * (obj.TakeCount - 1)).Take(obj.LimitRows)
                    .Select(p => new CommentsEntity(p)).ToList();
            }
        }
        /// <summary>
        /// 获取评论总数
        /// </summary>
        public int GetCommentsTotalCount(PagingEntity obj)
        {
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (CommentsLinqDataContext db = new CommentsLinqDataContext(setting.DefaultValue))
            {
                return db.KMSComments.Where(p => p.ItemId == obj.Id).Select(p => p.Id).Count();
            }
        }
        /// <summary>
        /// 添加评论
        /// </summary>
        public int Insert(PagingEntity obj)
        {
            int i = 0;
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (CommentsLinqDataContext db = new CommentsLinqDataContext(setting.DefaultValue))
            {
                KMSComment item = new KMSComment()
                {
                    Comments = obj.Search,
                    Created = DateTime.Now,
                    CreatedBy = SPContext.Current.Web.CurrentUser.Name,
                    ItemId = obj.Id
                };
                db.KMSComments.InsertOnSubmit(item);
                db.SubmitChanges();
                i = item.Id;
            }
            return i;
        }
        /// <summary>
        /// 删除评论
        /// </summary>

        public int Delete(PagingEntity obj)
        {
            int i = int.Parse(obj.Id);
            SettingsMgr mgr = new SettingsMgr();
            SettingsEntity setting = mgr.GetSystemSetting(obj.weburl, "DBConnection");
            using (CommentsLinqDataContext db = new CommentsLinqDataContext(setting.DefaultValue))
            {
                KMSComment item = db.KMSComments.Where(p => p.Id == i).FirstOrDefault();
                db.KMSComments.DeleteOnSubmit(item);
                db.SubmitChanges();
            }
            return i;
        }
    }
}
