using Focuswin.SP.Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.MailTemplate
{
    public class MailTemplateMgr
    {
        /// <summary>
        ///通过标题获取邮件模板
        /// </summary>
        public MailTemplateEntity GetSystemSetting(string weburl, string keyvalue)
        {
            string views = "<FieldRef Name=\"Title\"/><FieldRef Name=\"Field_Email_Subject\"/><FieldRef Name=\"Field_Email_Content\"/>";
            string caml = SearchHelper.Where(
                SearchHelper.Eq("Title", SearchHelper.ValueType.Text, keyvalue));
            return new MailTemplateEntity(ListItemHelper.ExecutePrivilegesCAMLToRetrieveSingleListItem(weburl, "MailTemplate", views, caml));
        }
    }
}
