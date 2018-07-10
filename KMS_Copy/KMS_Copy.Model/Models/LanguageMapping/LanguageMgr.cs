using Microsoft.SharePoint;
using SP.Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS_Copy.Model.Models.LanguageMapping
{
    public class LanguageMgr
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetResourceValue(LanguageParamEntity obj)
        {
            SPWeb web = null;
            string MappingValue = "";
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(obj.weburl))
                {
                    using (web = site.OpenWeb())
                    {
                        SPList list = web.Lists[obj.listName];
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


    }
}
