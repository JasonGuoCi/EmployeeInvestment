using Envision.EmployeeInvestmentplan.Utility.Enums;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.EmployeeInvestmentplan.Utility.Utilities
{
    public class ListItemsHelper
    {
        public static string GetListItems()
        {
            try
            {

                string weburl = SPContext.Current.Web.Url;
                List<object> InvestmentList = new List<object>();
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    using (SPSite spSite = new SPSite(weburl))
                    {
                        spSite.AllowUnsafeUpdates = true;
                        using (SPWeb web = spSite.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            string 
                            SPListItemCollection announcementListIitem = GetInvestmentListItems(web, EnvisionPagesConfig.EnvsionAnnouncement);
                            foreach (SPListItem listitem in announcementListIitem)
                            {
                                InvestmentList.Add(new
                                {
                                    id = listitem.ID,
                                    title = listitem.Title,
                                    publishedDate = listitem["PublishedDate"] == null ? "" : IBUtils.ObjectToDateTime(listitem["PublishedDate"]).ToString("MM-dd")
                                });
                            }
                            web.AllowUnsafeUpdates = false;
                        }
                        spSite.AllowUnsafeUpdates = false;
                    }
                });
                return Util.WriteJsonpToResponse(ResponseStatus.Success, AnnouncementList);
            }
            catch (OptionException)
            {
                return Util.WriteJsonpToResponse(ResponseStatus.Noneffect);
            }
            catch (Exception exception)
            {
                return Util.WriteJsonpToResponse(ResponseStatus.Exception, exception.Message);
            }
        }

        private static SPListItemCollection GetInvestmentListItems(SPWeb web, string listTitle)
        {
            string viewFields = @"<FieldRef Name='Title' />
                                    <FieldRef Name='PublishedDate' />";
            string orderBy = @"<FieldRef Name='IsTop' Ascending='False'/>
                                     <FieldRef Name='PublishedDate' Ascending='False'/>";
            return GetListItems(web, listTitle, null, viewFields, "", orderBy, 10);
        }
        public static SPListItemCollection GetListItems(SPWeb web, string title, string scope, string viewFields, string where, string orderBy, int? rowLimit)
        {
            SPList list = web.Lists[title];
            var query = new SPQuery
            {
                ViewXml = string.Format(@"<View{0}>", string.IsNullOrEmpty(scope) ? "" : string.Format(@" Scope=""{0}""", scope)) +
                              (string.IsNullOrEmpty(viewFields) ? "" : ("<ViewFields>" + viewFields + "</ViewFields>")) +
                              (!string.IsNullOrEmpty(where) || !string.IsNullOrEmpty(orderBy) ? "<Query>" : "") +
                              (!string.IsNullOrEmpty(where) ? "<Where>" + where + "</Where>" : "") +
                              (!string.IsNullOrEmpty(orderBy) ? "<OrderBy>" + orderBy + "</OrderBy>" : "") +
                              (!string.IsNullOrEmpty(where) || !string.IsNullOrEmpty(orderBy) ? "</Query>" : "") +
                              (rowLimit.HasValue ? string.Format("<RowLimit>{0}</RowLimit>", rowLimit.Value) : "") +
                          "</View>"
            };
            SPListItemCollection listItemCollection = list.GetItems(query);
            return listItemCollection;
        }


        public static string GetConfigItem(string title, SPWeb web)
        {
            string value = string.Empty;
            SPList spList = web.Lists.TryGetList(Static.StaticPara.ConfigListTitle);
            if (spList != null)
            {
                SPQuery qry = new SPQuery();
                qry.Query =
                @"   <Where>
                      <Eq>
                         <FieldRef Name='Title' />
                         <Value Type='Text'>" + title + @"</Value>
                      </Eq>
                   </Where>";
                SPListItemCollection listItems = spList.GetItems(qry);

                if (listItems.Count > 0)
                {
                    foreach (SPListItem item in listItems)
                    {
                        value = item["Value"].ToString();
                    }
                }
            }

            return value;

        }
    }
}
