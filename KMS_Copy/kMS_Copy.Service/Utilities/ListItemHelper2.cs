using KMS_Copy.Model.Models.Navigation;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS_Copy.Service.Utilities
{
    public class ListItemHelper2
    {
        /// <summary>
        /// get system settings list item 
        /// </summary>
        /// <param name="webUrl"></param>
        /// <param name="listName"></param>
        /// <returns></returns>
        public static SPListItem GetSystemSettingItem(string webUrl, string listName, string key)
        {
            SPListItem item = null;
            SPWeb web = null;

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(webUrl))
                {
                    using (web = site.OpenWeb())
                    {
                        SPList list = web.Lists[listName];
                        SPQuery query = new SPQuery();
                        query.Query = @"   <Where>
                                          <Eq>
                                             <FieldRef Name='Title' />
                                             <Value Type='Text'>" + key + @"</Value>
                                          </Eq>
                                       </Where>";
                        query.ViewFields = @"<FieldRef Name='Title' /><FieldRef Name='Field_KMS_DefaultValue' /><FieldRef Name='Field_KMS_Description' />";
                        SPListItemCollection items = list.GetItems(query);
                        if (items.Count > 0)
                        {
                            item = items[0];
                        }
                    }
                }
            });
            return item;
        }
        public static SPListItem GetSiteNavigation(string webUrl, string listName, string language)
        {
            SPListItem itemTarget = null;
            //SPWeb web = null;

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(webUrl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList spList = web.Lists.TryGetList(listName);
                        if (spList != null)
                        {
                            SPQuery qry = new SPQuery();
                            qry.Query =
                            @"   <Where>
                                  <Eq>
                                     <FieldRef Name='Title' />
                                     <Value Type='Text'>首页</Value>
                                  </Eq>
                               </Where>";
                            SPListItemCollection listItems = spList.GetItems(qry);
                            foreach (SPListItem item in listItems)
                            {

                                /*itemTarget = item["Field_KMS_GlobalLink"] + "";*/
                                itemTarget = item;
                            }
                        }

                    }
                }
            });
            return itemTarget;
        }
    }
}
