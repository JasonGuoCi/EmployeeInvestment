using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace SPWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Test : ITest
    {

        public string GetJson(string data)
        {
            return "Success " + data;
        }

        public string GetNavigation(string listname)
        {
            using (SPSite site = new SPSite("http://sharepointlipan:567/"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    var nav = "";
                    SPList list = web.Lists[listname];
                    SPListItemCollection items = list.GetItems();
                    foreach (SPListItem item in items)
                    {
                        nav = nav + item.Title + "; ";
                    }
                    return nav;
                }
            }
        }

        public string GetSystemSettings()
        {
            using (SPSite site = new SPSite("http://sharepointlipan:567/"))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    var nav = "";
                    SPList list = web.Lists["SystemSettings"];
                    SPListItemCollection items = list.GetItems();
                    foreach (SPListItem item in items)
                    {
                        nav = nav + item.Title + "; ";
                    }
                    return nav;
                }
            }
        }
        public string AddNewItem(string listname)
        {
            string retVal = string.Empty;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite("http://sharepointlipan:567/"))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            SPList list = web.Lists[listname];
                            SPListItem item = list.Items.Add();
                            item["Title"] = string.Format("Test at {0}", DateTime.Now.ToString());
                            item.SystemUpdate();
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                    retVal = "operation  success! ";
                });
            }
            catch (Exception ex)
            {
                retVal += ex.Message;
            }
            return retVal;
        }

        public string AddNewItem2(string listname)
        {
            string retVal = string.Empty;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite("http://sharepointlipan:567/"))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            SPList list = web.Lists[listname];
                            SPListItem item = list.Items.Add();
                            item["Title"] = string.Format("Test at {0}", DateTime.Now.ToString());
                            item.SystemUpdate();
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                    retVal = "operation  success! ";
                });
            }
            catch (Exception ex)
            {
                retVal += ex.Message;
            }
            return retVal;
        }

        public string AddNewItem3(string listname, int maxid)
        {
            string retVal = string.Empty;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite("http://sharepointlipan:567/"))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            SPList list = web.Lists[listname];
                            SPListItem item = list.Items.Add();
                            item["Title"] = string.Format("Test at {0}", DateTime.Now.ToString());
                            item.SystemUpdate();
                            web.AllowUnsafeUpdates = false;

                            //maxid = list.GetItems().Count;
                        }
                    }
                    retVal = "operation  success! Item Count: ";
                    // retVal = "operation  success! Item Count: "+ maxid;
                });
            }
            catch (Exception ex)
            {
                retVal += ex.Message;
            }
            return retVal;
        }

        public string AddNewItem4(ParEntity para)
        {
            string retVal = string.Empty;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite("http://sharepointlipan:567/"))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            SPList list = web.Lists[para.listName];
                            SPListItem item = list.Items.Add();
                            item["Title"] = string.Format("Test at {0},{1},{2}", DateTime.Now.ToString(), para.listName, para.itemValue);
                            item.SystemUpdate();
                            web.AllowUnsafeUpdates = false;

                            //maxid = list.GetItems().Count;
                        }
                    }
                    retVal = "operation  success!" + string.Format("Test at {0},{1},{2}", DateTime.Now.ToString(), para.listName, para.itemValue);
                    //retVal = "operation  success!" + string.Format("Test at {0}", DateTime.Now.ToString() + "---" + para.value); 
                    // retVal = "operation  success! Item Count: "+ maxid;
                });
            }
            catch (Exception ex)
            {
                retVal += ex.Message;
            }
            return retVal;
        }

        public string GetWish(string value1, string value2, string value3, int value4)
        {
            return string.Format("祝您在{3}年里 {0}、{1}、{2}", value1, value2, value3, value4);
        }
    }
}
