using KMS_Copy.Model.Models.Announcement;
using Microsoft.SharePoint;
using SP.Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMS_Copy.Model.Models.Wikipedia
{
    public class WikipediaMgr
    {
        /// <summary>
        ///获取左侧树形词条
        /// </summary>
        public List<TreeNodeEntity> GetDocNode(PagingEntity obj)
        {
            Dictionary<string, TreeNodeEntity> dic = new Dictionary<string, TreeNodeEntity>();
            List<TreeNodeEntity> Nodes = new List<TreeNodeEntity>();
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_Order\"/><FieldRef Name=\"Field_KMS_Status\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.OrderBy(SearchHelper.Where(SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published")), "Field_KMS_Order", true);
            if (!string.IsNullOrEmpty(obj.Search))
            {
                caml = SearchHelper.OrderBy(SearchHelper.Where(
                    SearchHelper.And(
                     SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published"),
                    SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                    )), "Field_KMS_Order", true);
            }
            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, "DefinitionLibrary", views, caml, obj.TakeCount, obj.LimitRows);
            foreach (SPListItem item in items)
            {
                TreeNodeEntity TreeNode = new TreeNodeEntity();
                TreeNode.text = obj.Language == "1033" ? (item["Field_KMS_EnglishTitle"] + "") : (item["Title"] + "");
                TreeNode.children = new List<TreeNodeEntity>();
                dic.Add(item["ID"].ToString(), TreeNode);
                if ((!string.IsNullOrEmpty(obj.CategoryId)) && obj.CategoryId == item["ID"].ToString())
                {
                    TreeNode.state = new StateEntity() { opened = true };
                }
                Nodes.Add(TreeNode);
            }

            SPListItemCollection ChildItems = GetChildNodes(obj);
            foreach (SPListItem childitem in ChildItems)
            {
                string lookupID = new SPFieldLookupValue(childitem["Field_KMS_Knowledge"] as String).LookupId + "";
                if (dic.ContainsKey(lookupID))
                {
                    TreeNodeEntity ChildNode = new TreeNodeEntity();
                    ChildNode.text = obj.Language == "1033" ? (childitem["Field_KMS_EnglishTitle"] + "") : (childitem["Title"] + "");
                    li_AttrEntity li_entity = new li_AttrEntity();
                    li_entity.relatedId = childitem["ID"] + "";
                    ChildNode.li_attr = li_entity;
                    if ((!string.IsNullOrEmpty(obj.WikiId)) && obj.WikiId == childitem["ID"].ToString())
                    {
                        ChildNode.state = new StateEntity() { selected = true };
                    }
                    dic[lookupID].children.Add(ChildNode);
                }
            }

            foreach (TreeNodeEntity item in Nodes)
            {
                if (item.children != null)
                    item.children = item.children.OrderBy(p => ChineseConvert.GetFirstPY(p.text)).ToList();
            }

            return Nodes;
        }
        /// <summary>
        ///获取子节点
        /// </summary>
        public SPListItemCollection GetChildNodes(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_Knowledge\"/><FieldRef Name=\"Field_KMS_Status\"/><FieldRef Name=\"Field_KMS_Order\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.OrderBy(SearchHelper.Where(SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published")), obj.Language == "1033" ? "Field_KMS_EnglishTitle" : "Title", true);
            if (!string.IsNullOrEmpty(obj.Search))
            {
                caml = SearchHelper.OrderBy(SearchHelper.Where(
                    SearchHelper.And(
                     SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published"),
                    SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search)
                    )), obj.Language == "1033" ? "Field_KMS_EnglishTitle" : "Title", true);
            }
            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, "WikipediaLibrary", views, caml, obj.TakeCount, obj.LimitRows);
            return items;
        }
        /// <summary>
        ///通过GUID获取词条内容
        /// </summary>
        public WikipediaEntity GetContentById(PagingEntity obj)
        {
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_Knowledge\"/><FieldRef Name=\"Field_KMS_Status\"/><FieldRef Name=\"Field_KMS_HtmlContent\"/><FieldRef Name=\"Field_KMS_EnglishHtmlContent\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.Where(SearchHelper.Eq("ID", SearchHelper.ValueType.Text, obj.Id));
            SPListItem item = ListItemHelper.ExecuteCAMLToRetrieveSingleListItem(obj.weburl, obj.listname, views, caml);
            return new WikipediaEntity(item, obj.Language, HighLightKeywords(obj.Language == "1033" ? (item["Field_KMS_EnglishHtmlContent"] + "") : (item["Field_KMS_HtmlContent"] + ""), obj.Search));
        }
        /// <summary>
        ///通过搜索关键字获取词条列表
        /// </summary>
        public List<TreeNodeEntity> SearchWikiByKeywords(PagingEntity obj)
        {
            List<string> dic = new List<string>();
            List<TreeNodeEntity> results = new List<TreeNodeEntity>();
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_Knowledge\"/><FieldRef Name=\"Field_KMS_Status\"/><FieldRef Name=\"Field_KMS_Order\"/><FieldRef Name=\"Field_KMS_HtmlContent\"/><FieldRef Name=\"Field_KMS_EnglishHtmlContent\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.OrderBy(SearchHelper.Where(
                    SearchHelper.And(
                     SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published"),
                     SearchHelper.Or(
                     SearchHelper.Contains("Title", SearchHelper.ValueType.Text, obj.Search),
                     SearchHelper.Contains("Field_KMS_EnglishTitle", SearchHelper.ValueType.Text, obj.Search)
                    ))), obj.Language == "1033" ? "Field_KMS_EnglishTitle" : "Title", true);
            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, "WikipediaLibrary", views, caml, obj.TakeCount, obj.LimitRows);
            foreach (SPListItem childitem in items)
            {
                TreeNodeEntity ChildNode = new TreeNodeEntity();
                ChildNode.text = obj.Language == "1033" ? (childitem["Field_KMS_EnglishTitle"] + "") : (childitem["Title"] + "");
                ChildNode.text = HighLightKeywords(ChildNode.text, obj.Search);
                li_AttrEntity li_entity = new li_AttrEntity();
                li_entity.relatedId = childitem["ID"] + "";
                ChildNode.li_attr = li_entity;
                results.Add(ChildNode);
                dic.Add(childitem["ID"] + "");
            }

            caml = SearchHelper.OrderBy(SearchHelper.Where(
                    SearchHelper.And(
                     SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published"),
                     SearchHelper.Or(SearchHelper.Contains("Field_KMS_HtmlContent", SearchHelper.ValueType.Text, obj.Search),
                     SearchHelper.Contains("Field_KMS_EnglishHtmlContent", SearchHelper.ValueType.Text, obj.Search))
                    )), obj.Language == "1033" ? "Field_KMS_EnglishTitle" : "Title", true);

            SPListItemCollection ContentItems = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(obj.weburl, "WikipediaLibrary", views, caml, obj.TakeCount, obj.LimitRows);
            foreach (SPListItem childitem in ContentItems)
            {
                if (!dic.Contains(childitem["ID"] + ""))
                {
                    TreeNodeEntity ChildNode = new TreeNodeEntity();
                    ChildNode.text = obj.Language == "1033" ? (childitem["Field_KMS_EnglishTitle"] + "") : (childitem["Title"] + "");
                    li_AttrEntity li_entity = new li_AttrEntity();
                    li_entity.relatedId = childitem["ID"] + "";
                    ChildNode.li_attr = li_entity;
                    results.Add(ChildNode);
                }
            }
            return results;
        }
        /// <summary>
        ///设置搜索关键字格式
        /// </summary>

        public string HighLightKeywords(string originContent, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return originContent;
            }
            else
            {
                List<string> keys = new List<string>();
                foreach (char item in keyword)
                {
                    keys.Add("(" + item + ")");
                }
                if (keys.Count > 0)
                {
                    string pattern = string.Join("(<[^>]*?>)*", keys.ToArray());
                    string content = Regex.Replace(originContent, pattern, new MatchEvaluator(OutPutMatch), RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    return content;
                }
                else
                {
                    return originContent;
                }
            }
        }
        /// <summary>
        ///设置词条相间格式
        /// </summary>
        private string OutPutMatch(Match match)
        {
            StringBuilder str = new StringBuilder();
            if (match.Groups.Count > 1)
            {
                for (int i = 1; i < match.Groups.Count; i++)
                {
                    if (i % 2 == 1)
                    {
                        str.Append("<font style='background-color:#FFFF00'>" + match.Groups[i].Value + "</font>");
                    }
                    else
                    {
                        str.Append(match.Groups[i].Value);
                    }
                }
            }
            return str.ToString();
        }

    }
}
