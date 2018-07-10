using Focuswin.SP.Base.Utility;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Settings
{
    public class SettingsMgr
    {
        /// <summary>
        ///获取系统设置
        /// </summary>
        public SettingsEntity GetSystemSetting(string weburl, string keyvalue)
        {
            string views = "<FieldRef Name=\"Title\"/><FieldRef Name=\"Field_KMS_DefaultValue\"/><FieldRef Name=\"Field_KMS_Description\"/>";
            string caml = SearchHelper.Where(
                SearchHelper.Eq("Title", SearchHelper.ValueType.Text, keyvalue));
            return new SettingsEntity(ListItemHelper.ExecutePrivilegesCAMLToRetrieveSingleListItem(weburl, "SystemSettings", views, caml));
        }
    }
}
