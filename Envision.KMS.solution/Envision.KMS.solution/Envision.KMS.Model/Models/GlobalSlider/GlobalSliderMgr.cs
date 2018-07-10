using Envision.KMS.Model.Models.Announcement;
using Focuswin.SP.Base.Utility;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.GlobalSlider
{
    public class GlobalSliderMgr
    {
        /// <summary>
        ///获取全局导航
        /// </summary>
        public List<GlobalSliderEntity> GetGlobalSliders(PagingEntity obj)
        {
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    List<GlobalSliderEntity> sliders = new List<GlobalSliderEntity>();
                    SPQuery query = new SPQuery();
                    query.RowLimit = 5;
                    query.Query = SearchHelper.OrderBy(SearchHelper.Where(SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published")), "Field_KMS_Order", true);
                    SPList list = web.Lists[obj.listname];
                    SPListItemCollection items = list.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        GlobalSliderEntity slider = new GlobalSliderEntity();
                        slider.Id = item.ID + "";
                        slider.Title = obj.Language == "1033" ? (item["Field_KMS_EnglishTitle"] + "") : (item["Title"] + "");
                        slider.Description = obj.Language == "1033" ? (item["Field_KMS_GlobalEngDesc"] + "") : (item["Field_KMS_GlobalDesc"] + "");
                        SPFieldUrlValue imgurl = new SPFieldUrlValue(item["Field_KMS_GlobalImg"] + "");
                        slider.ImgUrl = imgurl == null ? "" : imgurl.Url;

                        SPFieldUrlValue linkurl = new SPFieldUrlValue(item["Field_KMS_GlobalLink"] + "");
                        slider.Link = linkurl == null ? "" : linkurl.Url;

                        sliders.Add(slider);
                    }
                    return sliders;
                }
            }
        }

        public List<GlobalContactEntity> GetGlobalContacts(PagingEntity obj)
        {
            using (SPSite site = new SPSite(obj.weburl))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    List<GlobalContactEntity> Contacts = new List<GlobalContactEntity>();
                    SPQuery query = new SPQuery();
                    query.RowLimit = 5;
                    query.Query = SearchHelper.OrderBy(SearchHelper.Where(
                        SearchHelper.And(
                        SearchHelper.IsNotNull("Title"),
                        SearchHelper.Eq("Field_KMS_RalatedGlobal", SearchHelper.ValueType.Lookup, obj.CategoryId))),
                        "Field_KMS_Order", true);
                    SPList list = web.Lists[obj.listname];
                    SPListItemCollection items = list.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        GlobalContactEntity contact = new GlobalContactEntity();
                        contact.Id = item.ID + "";
                        contact.Title = item["Title"] + "";
                        contact.Name = item["Field_KMS_AdminName"] + "";
                        contact.Contact = item["Field_KMS_AdminContact"] + "";
                        contact.Extend1 = item["Field_KMS_AdminField1"] + "";
                        contact.Extend2 = item["Field_KMS_AdminField2"] + "";
                        contact.Extend3 = item["Field_KMS_AdminField3"] + "";
                        Contacts.Add(contact);
                    }
                    return Contacts;
                }
            }
        }
    }
}
