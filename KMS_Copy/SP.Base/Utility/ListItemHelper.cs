using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Base.Utility
{
    public class ListItemHelper
    {
        public static SPListItemCollection ExecuteCAMLToRetrieveListItemsInPages(string webUrl, string listName, string view, string caml, int pageIndex, int pageItemCount)
        {
            SPListItemCollection postDetailsItems = null;
            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList postList = web.Lists[listName];
                    SPQuery query = new SPQuery();
                    query.Query = caml;
                    if (pageIndex > 1)
                    {
                        query.ViewFields = view;
                        query.RowLimit = (uint)(pageItemCount * (pageIndex - 1));
                        postDetailsItems = postList.GetItems(query);
                        query = new SPQuery();
                        query.Query = caml;
                        string lastPageInfo = postDetailsItems.ListItemCollectionPosition.PagingInfo;
                        SPListItemCollectionPosition objSPListColPos = new SPListItemCollectionPosition(lastPageInfo);
                        query.RowLimit = uint.Parse(pageItemCount.ToString());
                        query.ListItemCollectionPosition = objSPListColPos;
                    }
                    else
                    {
                        query.RowLimit = uint.Parse(pageItemCount.ToString());
                    }

                    query.ViewFields = view;
                    postDetailsItems = postList.GetItems(query);
                }
            }
            return postDetailsItems;
        }
        public static int ExecuteCAMLToRetrieveListItemsCount(string webUrl, string listName, string view, string caml)
        {
            int count = 0;
            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList postList = web.Lists[listName];
                    SPQuery query = new SPQuery();
                    query.Query = caml;
                    query.ViewFields = view;
                    SPListItemCollection postDetailsItems = postList.GetItems(query);
                    count = postDetailsItems.Count;
                }
            }
            return count;
        }
        public static SPListItem ExecuteCAMLToRetrieveSingleListItem(string webUrl, string listName, string view, string caml)
        {
            SPListItem item = null;
            using (SPSite site = new SPSite(webUrl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[listName];
                    SPQuery query = new SPQuery();
                    query.Query = caml;
                    query.ViewFields = view;
                    SPListItemCollection postDetailsItems = list.GetItems(query);
                    if (postDetailsItems.Count > 0)
                    {
                        item = postDetailsItems[0];
                    }
                }
            }
            return item;
        }

        public static SPListItem ExecutePrivilegesCAMLToRetrieveSingleListItem(string webUrl, string listName, string view, string caml)
        {
            SPListItem item = null;
            SPWeb web = null;

            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(webUrl))
                {
                    using (web = site.OpenWeb())
                    {
                        SPList postList = web.Lists[listName];
                        SPQuery query = new SPQuery();
                        query.Query = caml;
                        query.ViewFields = view;
                        SPListItemCollection postDetailsItems = postList.GetItems(query);
                        if (postDetailsItems.Count > 0)
                        {
                            item = postDetailsItems[0];
                        }
                    }
                }
            });
            return item;
        }

        public static SPListItemCollection ExecutePrivilegesCAMLToRetrieveListItemsInPages(string weburl, string listName, string view, string caml, int pageIndex, int pageItemCount)
        {
            SPWeb sharePointWeb = null;
            SPListItemCollection postDetailsItems = null;
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite Site = new SPSite(weburl))
                {
                    using (sharePointWeb = Site.OpenWeb())
                    {
                        SPList postList = sharePointWeb.Lists[listName];
                        SPQuery query = new SPQuery();
                        query.Query = caml;
                        if (pageIndex > 1)
                        {
                            query.ViewFields = view;
                            query.RowLimit = (uint)(pageItemCount * (pageIndex - 1));
                            postDetailsItems = postList.GetItems(query);
                            query = new SPQuery();
                            query.Query = caml;
                            string lastpageinfo = postDetailsItems.ListItemCollectionPosition.PagingInfo;
                            SPListItemCollectionPosition objSPListColPos = new SPListItemCollectionPosition(lastpageinfo);
                            query.RowLimit = uint.Parse(pageItemCount.ToString());
                            query.ListItemCollectionPosition = objSPListColPos;
                        }
                        else
                        {
                            query.RowLimit = uint.Parse(pageItemCount.ToString());
                        }
                        query.ViewFields = view;
                        postDetailsItems = postList.GetItems(query);
                    }
                }
            });
            return postDetailsItems;
        }
    }
}
