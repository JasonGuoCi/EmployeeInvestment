using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Wikipedia
{
    /// <summary>
    ///格林百科实体 
    /// </summary>
    [DataContract]
    public class WikipediaEntity
    {
        public WikipediaEntity() { }
        public WikipediaEntity(SPListItem item, string langflag,string content)
        {
            if (item != null)
            {
                Id = item["ID"] + "";
                Title = langflag == "1033" ? (item[_Field_KMS_EnglishTitle] + "") : (item["Title"] + "");
                HtmlContent = content;
                Created = DateTime.Parse(item[_Created] + "");
                Modified = DateTime.Parse(item[_Modified] + "");
                UniqueId = item["GUID"] + "";

                CategoryId = new SPFieldLookupValue(item["Field_KMS_Knowledge"] as String).LookupId + "";

            }
        }
        /// <summary>
        ///编号
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        ///标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
        /// <summary>
        ///GUID
        /// </summary>
        [DataMember]
        public string UniqueId { set; get; }
        /// <summary>
        ///词条内容
        /// </summary>
        [DataMember]
        public string HtmlContent { set; get; }
        /// <summary>
        ///评论ID
        /// </summary>
        [DataMember]
        public string CategoryId { set; get; }
        /// <summary>
        ///创建时间
        /// </summary>
        [DataMember]
        public DateTime Created { set; get; }
        /// <summary>
        ///修改时间
        /// </summary>
        [DataMember]
        public DateTime Modified { set; get; }

        private static string _Field_KMS_EnglishTitle = "Field_KMS_EnglishTitle";
        private static string _Field_KMS_HtmlContent = "Field_KMS_HtmlContent";
        private static string _Field_KMS_EnglishHtmlContent = "Field_KMS_EnglishHtmlContent";
        private static string _Modified = "Modified";
        private static string _Created = "Created";
    }

    public class TermEntity
    {

    }

    /// <summary>
    ///树节点实体
    /// </summary>
    [DataContract]
    public class TreeNodeEntity
    {
        /// <summary>
        ///文本
        /// </summary>
        [DataMember]
        public string text { get; set; }
        /// <summary>
        ///状态
        /// </summary>
        [DataMember]
        public StateEntity state { get; set; }
        /// <summary>
        ///图标
        /// </summary>
        [DataMember]
        public string icon { get; set; }
        /// <summary>
        ///数组
        /// </summary>
        [DataMember]
        public li_AttrEntity li_attr { get; set; }
        [DataMember]
        public string a_attr { get; set; }
        /// <summary>
        ///子节点
        /// </summary>
        [DataMember]
        public List<TreeNodeEntity> children { get; set; }
    }

    [DataContract]
    public class StateEntity
    {
        /// <summary>
        ///是否打开
        /// </summary>
        [DataMember]
        public bool opened { get; set; }
        /// <summary>
        ///是否被选中
        /// </summary>
        [DataMember]
        public bool selected { set; get; }
    }

    [DataContract]
    public class li_AttrEntity
    {
        /// <summary>
        ///关联编号
        /// </summary>
        [DataMember]
        public string relatedId { get; set; }
    }

}
