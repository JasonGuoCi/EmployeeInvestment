using Focuswin.SP.Base.Utility;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.LanguageMapping
{
    public class LanguageMgr
    { /// <summary>
        ///获取显示值
        /// </summary>
        public string GetResourceValue(LanguageParamEntity obj)
        {
            SPWeb web = null;
            string MappingValue = "";
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(obj.weburl))
                {
                    using (web = site.OpenWeb())
                    {
                        SPList list = web.Lists["LanguageMapping"];
                        SPQuery query = new SPQuery();
                        query.Query = SearchHelper.Where(SearchHelper.And(SearchHelper.Eq("Field_KMS_Model", SearchHelper.ValueType.Text, obj.model), SearchHelper.Eq("Title", SearchHelper.ValueType.Text, obj.key)));
                        SPListItemCollection items = list.GetItems(query);
                        if (items != null && items.Count > 0)
                        {
                            MappingValue = obj.lang == "1033" ? (items[0]["Field_KMS_EnglishName"] + "") : (items[0]["Field_KMS_ChineseName"] + "");
                        }
                    }
                }
            });
            return MappingValue;
        }
        /// <summary>
        ///获取中英文列表
        /// </summary>
        public List<LanguageEntity> GetManualResource(LanguageParamEntity obj)
        {
            SPWeb web = null;
            List<LanguageEntity> results = new List<LanguageEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(obj.weburl))
                {
                    using (web = site.OpenWeb())
                    {
                        SPList list = web.Lists["LanguageMapping"];
                        SPQuery query = new SPQuery();
                        query.Query = SearchHelper.Where(SearchHelper.Eq("Field_KMS_Model", SearchHelper.ValueType.Text, obj.model));
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            results.Add(new LanguageEntity(item, obj.lang));
                        }
                    }
                }
            });
            return results;
        }
    }

}
