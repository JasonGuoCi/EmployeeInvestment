using Envision.KMS.Model.Models.Announcement;
using Focuswin.SP.Base.Utility;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Navigation
{
    public class NavigationMgr
    {   
        /// <summary>
        ///获取网页导航
        /// </summary>
        public List<NavigationEntity> GetNavigation(PagingEntity obj)
        {
            obj.Id="0";
            return GetRecusiveNavigation(obj);
        }
        /// <summary>
        ///获取导航列表
        /// </summary>
        List<NavigationEntity> GetRecusiveNavigation(PagingEntity obj)
        {
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    List<NavigationEntity> nav = new List<NavigationEntity>();
                    SPQuery query = new SPQuery();
                    query.Query = SearchHelper.OrderBy(SearchHelper.Where(SearchHelper.Eq("Field_KMS_ParentNo", SearchHelper.ValueType.Text, obj.Id)), "Field_KMS_Order", true);
                    SPList list = web.Lists[obj.listname];
                    SPListItemCollection items = list.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        NavigationEntity navitem = new NavigationEntity();
                        navitem.Title = obj.Language == "1033" ? (item["Field_KMS_EnglishTitle"] + "") : (item["Title"] + "");
                        navitem.LinkUrl = item["Field_KMS_GlobalLink"] + "";
                        navitem.SeriesNo = item["Field_KMS_SeriesNo"] + "";
                        obj.Id = item["Field_KMS_SeriesNo"] + "";
                        navitem.Children = GetRecusiveNavigation(obj);
                        nav.Add(navitem);
                    }
                    return nav;
                }
            }
        }
    }
}
