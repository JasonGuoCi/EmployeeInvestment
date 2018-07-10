using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Forum
{
    [DataContract]
    public class ForumEntity
    {
        /// <summary>
        /// 话题实体
        /// </summary>
        public ForumEntity() { }
        /// <summary>
        /// 话题实体
        /// </summary>
        /// <param name="item">列表实体</param>
        /// <param name="displayname">显示值</param>
        /// <param name="picurl">图片地址</param>
        public ForumEntity(SPListItem item, string displayname, string picurl)
        {
            Id = item.ID + "";
            UniqueId = item["GUID"] + "";
            Title = item["Title"] + "";
            var eventDescField = item.ParentList.Fields.GetFieldByInternalName("Field_KMS_ForumContent");
            var eventDesc = item[eventDescField.Id];
            OrginContent = eventDescField.GetFieldValueAsText(eventDesc);
            Content = OrginContent.Replace("\r\n", "<br>");
            Content = Content.Replace("\n", "<br>");
            SPFieldLookup categories = (SPFieldLookup)item.ParentList.Fields.GetFieldByInternalName("Field_KMS_ForumType");
            SPFieldLookupValueCollection valueCollection = item[categories.Id] as SPFieldLookupValueCollection;
            int[] arrLookupValues = (from SPFieldLookupValue val in valueCollection select val.LookupId).ToArray<int>();
            string[] arrLookupValues2 = (from SPFieldLookupValue val in valueCollection select val.LookupValue).ToArray<string>();
            ForumType = string.Join(";", arrLookupValues);
            ForumTypeDisplay = string.Join(" ", arrLookupValues2);
            IsTop = item["Field_KMS_IsTop"] + "";
            Created = item["Created"] + "";
            Modified = item["Field_KMS_LastComments"] == null ? (item["Modified"] + "") : (item["Field_KMS_LastComments"] + "");
            ViewCount = item["Field_KMS_ViewCount"] + "";
            CommentsCount = item["Field_KMS_CommentsCount"] + "";
            PicUrl = string.IsNullOrEmpty(picurl) ? "/_layouts/15/images/PersonPlaceholder.96x96x32.png" : picurl;
            Author = displayname;
        }
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        /// 话题GUID
        /// </summary>
        [DataMember]
        public string UniqueId { set; get; }
        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        public string Content { set; get; }
        /// <summary>
        /// 内容源
        /// </summary>
        [DataMember]
        public string OrginContent { set; get; }
        /// <summary>
        /// 话题类型
        /// </summary>
        [DataMember]
        public string ForumType { set; get; }
        /// <summary>
        /// 话题类型显示值
        /// </summary>
        [DataMember]
        public string ForumTypeDisplay { set; get; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        [DataMember]
        public string IsTop { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public string Created { set; get; }
        /// <summary>
        ///修改时间
        /// </summary>
        [DataMember]
        public string Modified { set; get; }
        /// <summary>
        /// 作者
        /// </summary>
        [DataMember]
        public string Author { set; get; }
        /// <summary>
        /// 图片地址
        /// </summary>
        [DataMember]
        public string PicUrl { set; get; }
        /// <summary>
        /// 评论数量
        /// </summary>
        [DataMember]
        public string CommentsCount { set; get; }
        /// <summary>
        /// 页面显示数量
        /// </summary>
        [DataMember]
        public string ViewCount { set; get; }
        /// <summary>
        /// 网页地址
        /// </summary>
        [DataMember]
        public string weburl { set; get; }
        /// <summary>
        /// 列表名称
        /// </summary>
        [DataMember]
        public string listname { set; get; }
    }

    /// <summary>
    /// 话题类型实体
    /// </summary>
    [DataContract]
    public class ForumTypeEntity
    {

        public ForumTypeEntity() { }
        public ForumTypeEntity(SPListItem item, string lang)
        {
            Id = item.ID + "";
            UniqueId = item["GUID"] + "";
            Title = lang == "1033" ? (item["Field_KMS_EnglishTitle"] + "") : (item["Title"] + "");
        }
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        /// 话题类型GUID
        /// </summary>
        [DataMember]
        public string UniqueId { set; get; }
        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
    }

    public class TopEntity
    {   /// <summary>
        /// 话题ID
        /// </summary>
        public string ItemId { set; get; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Count { set; get; }
    }
}
