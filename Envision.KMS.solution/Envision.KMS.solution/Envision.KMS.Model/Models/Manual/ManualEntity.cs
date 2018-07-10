using Envision.KMS.Model.Models.Announcement;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Manual
{
    /// <summary>
    ///操作手册实体
    /// </summary>
    [DataContract]
    public class ManualHtmlEntity
    {
        public ManualHtmlEntity() { }
        public ManualHtmlEntity(SPListItem item, string langflag, bool IsContent)
        {
            if (item != null)
            {
                UniqueId = item[_GUID] + "";
                Id = item[_Id] + "";
                Title = langflag == "1033" ? (item[_EnglishTitle] + "") : (item[_Title] + "");
                if (IsContent)
                    Content = langflag == "1033" ? (item[_EnglishContent] + "") : (item[_Content] + "");
                Created = DateTime.Parse(item[_Created] + "");
                Modified = DateTime.Parse(item[_Modified] + "");
                LinkUrl = langflag == "1033" ? (item[_Field_KMS_EnglishLink] + "") : (item[_Field_KMS_Link] + "");
            }
        }
        /// <summary>
        ///编号
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        ///GUID
        /// </summary>
        [DataMember]
        public string UniqueId { set; get; }
        /// <summary>
        ///标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
        /// <summary>
        ///链接地址
        /// </summary>
        [DataMember]
        public string LinkUrl { set; get; }
        /// <summary>
        ///内容
        /// </summary>
        [DataMember]
        public string Content { set; get; }
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

        private static string _Id = "ID";
        private static string _GUID = "GUID";
        private static string _Title = "Title";
        private static string _EnglishTitle = "Field_KMS_EnglishTitle";
        private static string _EnglishContent = "Field_KMS_EnglishHtmlContent";
        private static string _Field_KMS_Link = "Field_KMS_Link";
        private static string _Field_KMS_EnglishLink = "Field_KMS_EnglishLink";
        private static string _Content = "Field_KMS_HtmlContent";
        private static string _Modified = "Modified";
        private static string _Created = "Created";
    }
    /// <summary>
    ///网页类教程
    /// </summary>
    [DataContract]
    public class ManualFolder
    {
        /// <summary>
        ///标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
        /// <summary>
        ///创建时间
        /// </summary>
        [DataMember]
        public string Created { set; get; }
        /// <summary>
        ///GUID
        /// </summary>
        [DataMember]
        public string UniqueId { set; get; }
    }
    /// <summary>
    ///视频类教程
    /// </summary>
    [DataContract]
    public class ManualVideo
    {
        /// <summary>
        ///标题 
        /// </summary>
        [DataMember]
        public string Title { set; get; }
        /// <summary>
        ///名称
        /// </summary>
        [DataMember]
        public string Name { set; get; }
        /// <summary>
        ///创建时间
        /// </summary>
        [DataMember]
        public string Created { set; get; }
        /// <summary>
        ///GUID
        /// </summary>
        [DataMember]
        public string UniqueId { set; get; }
        /// <summary>
        ///封面图片
        /// </summary>
        [DataMember]
        public string PreviewImage { set; get; }
        /// <summary>
        ///视频地址
        /// </summary>
        [DataMember]
        public string Path { set; get; }
    }
    /// <summary>
    ///文本类教程
    /// </summary>
    [DataContract]
    public class ManualDocumentEntity
    {
        public ManualDocumentEntity() { }
        public ManualDocumentEntity(SPListItem item)
        {
            Id = item["ID"] + "";
            if (string.IsNullOrEmpty(item["Name"] + ""))
            {
                Title = item["FileLeafRef"] + "";
            }
            else
            {
                Title = item["Name"] + "";
            }
            Created = item["Created"] + "";
            if (!string.IsNullOrEmpty(item["Author"] + ""))
            {
                Author = item["Author"] + "";
                Author = Author.Substring(Author.IndexOf('#') + 1);
            }
            DocUrl = item["FileRef"] + "";
            var doctype = item["DocIcon"] + "";
            if (doctype.Equals("pdf"))
            {
                DocIcon = "/_layouts/15/Envision.KMS.Solution/images/docu_icon02.png";
            }
            else if (doctype.Contains("doc"))
            {
                DocIcon = "/_layouts/15/Envision.KMS.Solution/images/docu_icon03.png";
            }
            else if (doctype.Contains("xl"))
            {
                DocIcon = "/_layouts/15/Envision.KMS.Solution/images/docu_icon05.png";
            }
            else if (doctype.Contains("ppt"))
            {
                DocIcon = "/_layouts/15/Envision.KMS.Solution/images/docu_icon04.png";
            }
            else
            {
                DocIcon = "/_layouts/15/Envision.KMS.Solution/images/docu_icon01.png";
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
        ///创建时间
        /// </summary>
        [DataMember]
        public string Created { set; get; }
        /// <summary>
        ///作者
        /// </summary>
        [DataMember]
        public string Author { set; get; }
        /// <summary>
        ///文本地址
        /// </summary>
        [DataMember]
        public string DocUrl { set; get; }
        /// <summary>
        ///文本图标
        /// </summary>
        [DataMember]
        public string DocIcon { set; get; }
    }
}
