using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Announcement
{
    [DataContract]
 
    public class AnnouncementEntity
    {
        /// <summary>
        /// 通告实体
        /// </summary>
        public AnnouncementEntity() { }
        /// <summary>
        /// 判断通告中英文显示
        /// </summary>
        public AnnouncementEntity(SPListItem item, string langflag)
        {
            if (item != null)
            {
                UniqueId = item[_GUID] + "";
                Id = item[_Id] + "";
                Title = langflag == "1033" ? (item[_EnglishTitle] + "") : (item[_Title] + "");
                Content = langflag == "1033" ? (item[_EnglishContent] + "") : (item[_Content] + "");
                Created = DateTime.Parse(item[_Created] + "");
                Modified = DateTime.Parse(item[_Modified] + "");
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        /// GUID
        /// </summary>
        [DataMember]
        public string UniqueId { set; get; }
        /// <summary>
        /// 通告标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
        /// <summary>
        /// 通告内容
        /// </summary>
        [DataMember]
        public string Content { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime Created { set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember]
        public DateTime Modified { set; get; }

        private static string _Id = "ID";
        private static string _GUID = "GUID";
        private static string _Title = "Title";
        private static string _EnglishTitle = "Field_KMS_EnglishTitle";
        private static string _EnglishContent = "Field_KMS_MultiEnglishContent";
        private static string _Content = "Field_KMS_MultiContent";
        private static string _Modified = "Modified";
        private static string _Created = "Created";
    }


    [DataContract]
    /// <summary>
    /// 页面实体
    /// </summary>
    public class PagingEntity
    {
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
        /// <summary>
        ///最大条数
        /// </summary>
        [DataMember]
        public int LimitRows { set; get; }
        /// <summary>
        ///当前条数
        /// </summary>
        [DataMember]
        public int TakeCount { set; get; }
        /// <summary>
        ///搜索内容
        /// </summary>
        [DataMember]
        public string Search { set; get; }
        /// <summary>
        ///实体id
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        ///回复Id
        /// </summary>
        [DataMember]
        public string CategoryId { set; get; }
        /// <summary>
        /// 百科id
        /// </summary>
        [DataMember]
        public string WikiId { set; get; }
        /// <summary>
        /// 语言
        /// </summary>
        [DataMember]
        public string Language { set; get; }
    }
}
