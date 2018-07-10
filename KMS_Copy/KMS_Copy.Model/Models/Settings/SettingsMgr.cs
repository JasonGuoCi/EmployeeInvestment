
using SP.Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS_Copy.Model.Models.Settings
{
    public class SettingsMgr
    {

        /// <summary>
        ///获取系统设置
        /// </summary>
        public SettingsEntity GetSystemSetting(string webUrl, string keyValue)
        {
            string views = "<FieldRef Name=\"Title\"/><FieldRef Name=\"Field_KMS_DefaultValue\"/><FieldRef Name=\"Field_KMS_Description\"/>";
            string caml = SearchHelper.Where(SearchHelper.Eq("Title", SearchHelper.ValueType.Text, keyValue));
            return new SettingsEntity(ListItemHelper.ExecutePrivilegesCAMLToRetrieveSingleListItem(webUrl, "SystemSettings", views, caml));
        }
    }
}
