using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.MailTemplate
{
    /// <summary>
    ///邮件模板实体
    /// </summary>
    [DataContract]
    public class MailTemplateEntity
    {
        public MailTemplateEntity() { }
        public MailTemplateEntity(SPListItem item)
        {
            if (item != null)
            {
                Title = item[_Title] + "";
                Subject = item[_Field_Email_Subject] + "";
                var commentsField = item.ParentList.Fields.GetFieldByInternalName(_Field_Email_Content);
                var commentsDesc = item[commentsField.Id];
                Body = commentsField.GetFieldValueAsText(commentsDesc);
            }
        }
        /// <summary>
        ///标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
        /// <summary>
        ///说明
        /// </summary>
        [DataMember]
        public string Subject { set; get; }
        /// <summary>
        ///邮件内容
        /// </summary>
        [DataMember]
        public string Body { set; get; }

        private static string _Title = "Title";
        private static string _Field_Email_Subject = "Field_Email_Subject";
        private static string _Field_Email_Content = "Field_Email_Content";
    }
}
