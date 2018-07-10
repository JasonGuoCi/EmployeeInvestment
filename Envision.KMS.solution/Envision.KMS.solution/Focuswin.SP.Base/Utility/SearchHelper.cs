using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint;

namespace Focuswin.SP.Base.Utility
{
    public static class SearchHelper
    {

        public static SPListItem QueryByID(SPList list, int id)
        {
            SPListItem result = null;
            SPQuery q = new SPQuery();
            q.Query = SearchHelper.Where(EqID(id));
            q.RowLimit = 1;
            var items = list.GetItems(q);
            if (items.Count > 0)
                result = items[0];

            return result;
        }

        public static string EqID(int id)
        {
            return Eq("ID", ValueType.Number, id);
        }

        public static string CurrentUserOrGroup(string field)
        {
            return Or(Membership(field), Eq(field, ValueType.Integer, CurrentUser));
        }

        public static string Membership(string field)
        {
            return "<Membership Type=\"CurrentUserGroups\"><FieldRef Name=\"" + field + "\"/></Membership>";
        }

        public const string CurrentUser = "<UserID Type=\"Integer\"/>";

        public static string User(int id)
        {
            return string.Format("<UserID Type=\"Integer\">{0}</UserID>", id);
        }

        private static string ConcatConditions(string method, params string[] conditions)
        {
            string condition = "";
            if (conditions.Length > 1)
            {
                condition = conditions[0];
                for (int i = 1; i < conditions.Length; i++)
                {
                    if (string.IsNullOrEmpty(condition))
                    {
                        condition = conditions[i];
                    }
                    else if (string.IsNullOrEmpty(conditions[i]))
                    {

                    }
                    else
                    {
                        condition = "<" + method + ">" + condition + conditions[i] + "</" + method + ">";
                    }
                }
            }
            return condition;
        }

        public static string And(params string[] conditions)
        {
            return ConcatConditions("And", conditions);
        }

        public static string Or(params string[] conditions)
        {
            return ConcatConditions("Or", conditions);
        }

        public static string GroupBy(string fieldName)
        {
            return string.Format("<GroupBy><FieldRef Name=\"{0}\"/></GroupBy>", fieldName);
        }

        public static string OrderBy(string where, IEnumerable<OrderBys> orderBys)
        {
            return where + OrderBy(orderBys);
        }

        public static string OrderBy(IEnumerable<OrderBys> orderBys)
        {
            string fields = "";
            if (orderBys != null && orderBys.Count() > 0)
            {
                foreach (var order in orderBys)
                {
                    fields += string.Format("<FieldRef Name=\"{0}\" Ascending=\"{1}\" />", order.Field, order.Ascending);
                }
                return string.Format("<OrderBy>{0}</OrderBy>", fields);
            }
            else
            {
                return "";
            }
        }

        public static string OrderBy(string fieldName, bool ascending)
        {
            return string.Format("<OrderBy><FieldRef Name=\"{0}\" Ascending=\"{1}\" /></OrderBy>", fieldName, ascending);
        }

        public static string OrderBy(string where, string fieldName, bool ascending)
        {
            return where + OrderBy(fieldName, ascending);
        }

        public static string Condition(string op, string fieldName, ValueType valueType, object value)
        {
            string placeHolderLookup = "";
            string placeHolderIncludeTime = "";

            if (valueType == ValueType.Lookup || valueType == ValueType.UserID)
            {
                placeHolderLookup = " " + Caml_LookUpId;
            }
            else if (valueType == ValueType.DateTime)
            {
                placeHolderIncludeTime = " " + Caml_IncludeTime;
            }

            if (value is DateTime)
            {
                value = CovertDateTime((DateTime)value);
            }

            return string.Format("<{0}><FieldRef Name='{1}'{4}/><Value Type='{2}'{5}>{3}</Value></{0}>", op, fieldName, Type2Keyword(valueType), value, placeHolderLookup, placeHolderIncludeTime);
        }

        public static string OnlyFolder()
        {
            return Eq("FSObjType", ValueType.Number, "1");
        }

        public static string WithoutFolder()
        {
            return Eq("FSObjType", ValueType.Number, "0");
        }

        public static string Eq(string fieldName, ValueType valueType, object value)
        {
            return Condition("Eq", fieldName, valueType, value);
        }
        public static string Gt(string fieldName, ValueType valueType, object value)
        {
            return Condition("Gt", fieldName, valueType, value);
        }
        public static string Lt(string fieldName, ValueType valueType, object value)
        {
            return Condition("Lt", fieldName, valueType, value);
        }
        public static string Geq(string fieldName, ValueType valueType, object value)
        {
            return Condition("Geq", fieldName, valueType, value);
        }
        public static string Leq(string fieldName, ValueType valueType, object value)
        {
            return Condition("Leq", fieldName, valueType, value);
        }
        public static string Neq(string fieldName, ValueType valueType, object value)
        {
            return Condition("Neq", fieldName, valueType, value);
        }

        public static string Contains(string fieldName, ValueType valueType, object value)
        {
            return Condition("Contains", fieldName, valueType, value);
        }

        public static string IsNull(string fieldName)
        {
            return string.Format("<IsNull><FieldRef Name='{0}'/></IsNull>", fieldName);
        }
        public static string IsNotNull(string fieldName)
        {
            return string.Format("<IsNotNull><FieldRef Name='{0}'/></IsNotNull>", fieldName);
        }
        public static string BeginsWith(string fieldName, ValueType valueType, string value)
        {
            return Condition("BeginsWith", fieldName, valueType, value);
        }
        public static string CovertDateTime(DateTime date)
        {
            return SPUtility.CreateISO8601DateTimeFromSystemDateTime(date);
        }

        public static string Where(string conditions)
        {
            return "<Where>" + conditions + "</Where>";
        }

        public static string In(string fieldName, ValueType valueType, IEnumerable<string> values)
        {
            return In(fieldName, valueType, values.Select(t => (object)t));
        }

        public static string In(string fieldName, ValueType valueType, IEnumerable<object> values)
        {
            string placeholder = "";

            if (valueType == ValueType.Lookup || valueType == ValueType.UserID)
            {
                placeholder += Caml_LookUpId;
            }

            string caml = "";
            caml += "<In>";
            caml += string.Format("<FieldRef Name='{0}' {1}/>", fieldName, placeholder);
            caml += "<Values>";
            if (values != null && values.Count() > 0)
            {
                foreach (object value in values)
                {
                    string str;
                    if (value is DateTime)
                    {
                        str = CovertDateTime((DateTime)value);
                    }
                    else
                    {
                        str = value + "";
                    }

                    caml += string.Format("<Value Type='{0}'>{1}</Value>", Type2Keyword(valueType), str);
                }
            }
            else
            {
                caml += string.Format("<Value Type='{0}'></Value>", Type2Keyword(valueType));
            }
            caml += "</Values>";
            caml += "</In>";

            return caml;
        }

        public static string ViewFields(IEnumerable<string> fields)
        {
            string caml = "";
            foreach (string s in fields)
            {
                caml += string.Format("<FieldRef Name='{0}'/>", s);
            }
            return caml;
        }

        /// <summary>
        /// Only contains ID column
        /// </summary>
        public static string MinimalViewFields
        {
            get { return ViewFields(new string[] { "ID" }); }
        }

        /// <summary>
        /// GEQ(begin) and LEQ(end)
        /// </summary>
        /// <param name="field"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="includingTime"></param>
        /// <returns></returns>
        public static string InDateRange(string field, DateTime begin, DateTime end, bool includingTime)
        {
            ValueType type = includingTime ? ValueType.DateTime : ValueType.Date;
            string query = "";
            if (begin != DateTime.MinValue)
            {
                query = Geq(field, type, CovertDateTime(begin));
            }

            if (end != DateTime.MinValue)
            {
                query = And(query, Leq(field, type, CovertDateTime(end)));
            }

            return query;
        }


        public static string DateRangesOverlap(DateTime startDate, DateTime endDate, string startField, string endField, bool includingTime)
        {
            string caml = "";
            if (includingTime)
            {
                caml = And(Gt(endField, ValueType.DateTime, CovertDateTime(startDate)), Lt(startField, ValueType.DateTime, CovertDateTime(endDate)));
            }
            else
            {
                caml = And(Geq(endField, ValueType.Date, CovertDateTime(startDate)), Leq(startField, ValueType.Date, CovertDateTime(endDate)));
            }
            return caml;
        }

        public static string DateRangesOverlap(DateTime date, string startField, string endField, bool includingTime)
        {
            return DateRangesOverlap(date, date, startField, endField, includingTime);
        }

        public const string Caml_LookUpId = "LookupId='TRUE'";
        public const string Caml_IncludeTime = "IncludeTimeValue='TRUE'";


        public const string Type_MultiChoice = "MultiChoice";
        public const string Type_Choice = "Choice";
        public const string Type_Text = "Text";
        public const string Type_Number = "Number";
        public const string Type_Integer = "Integer";
        public const string Type_Lookup = "Lookup";
        public const string Type_Date = "Date";
        public const string Type_DateTime = "DateTime";
        public const string Type_User = "User";

        private static string Type2Keyword(ValueType type)
        {
            string result = Type_Text;
            switch (type)
            {
                case ValueType.Number:
                    result = Type_Number;
                    break;
                case ValueType.Date:
                    result = Type_Date;
                    break;
                case ValueType.DateTime:
                    result = Type_DateTime;
                    break;
                case ValueType.Integer:
                    result = Type_Integer;
                    break;
                case ValueType.Choice:
                    result = Type_Choice;
                    break;
                case ValueType.MultiChoice:
                    result = Type_MultiChoice;
                    break;
                case ValueType.Lookup:
                    result = Type_Lookup;
                    break;
                case ValueType.User:
                    result = Type_Text;
                    break;
                case ValueType.UserID:
                    result = Type_Text;
                    break;
                default:
                    break;
            }
            return result;
        }

        public enum ValueType
        {
            Text,
            MultiChoice,
            Choice,
            Number,
            Integer,
            Lookup,
            User,
            UserID,
            Date,
            DateTime
        }

        public static string Webs(WebScope scope)
        {
            switch (scope)
            {
                case WebScope.Recursive:
                    return "<Webs Scope=\"Recursive\">";
                case WebScope.SiteCollection:
                    return "<Webs Scope=\"SiteCollection\">";
                default:
                    return "<Webs Scope=\"Recursive\">";
            }
        }

        public static string Lists(ServerTemplate template)
        {
            return ListsByServerTemplate(ServerTemplateID(template));
        }

        public static string ListsHidden(bool showHidden)
        {
            return string.Format("<Lists Hidden=\"{0}\"/>", showHidden ? "TRUE" : "FALSE");
        }

        public static string Lists(BaseType type)
        {
            return ListsByBaseType(BaseTypeID(type));
        }

        public static string ListsByServerTemplate(int templateID)
        {
            return "<Lists ServerTemplate=\"" + templateID.ToString() + "\" />";
        }

        public static string ListsByBaseType(int baseTypeID)
        {
            return "<Lists BaseType=\"" + baseTypeID.ToString() + "\" />";
        }

        public static int ServerTemplateID(ServerTemplate template)
        {
            switch (template)
            {
                case ServerTemplate.Genericlist:
                    return 100;
                case ServerTemplate.DocumentLibrary:
                    return 101;
                case ServerTemplate.Survey:
                    return 102;
                case ServerTemplate.LinksList:
                    return 103;
                case ServerTemplate.AnnouncementsList:
                    return 104;
                case ServerTemplate.ContactsList:
                    return 105;
                case ServerTemplate.EventsList:
                    return 106;
                case ServerTemplate.TasksList:
                    return 107;
                case ServerTemplate.DiscussionBoard:
                    return 108;
                case ServerTemplate.PictureLibrary:
                    return 109;
                case ServerTemplate.DataSources:
                    return 110;
                case ServerTemplate.SiteTemplateGallery:
                    return 111;
                case ServerTemplate.UserInformationList:
                    return 112;
                case ServerTemplate.WebPartGallery:
                    return 113;
                case ServerTemplate.ListTemplateGallery:
                    return 114;
                case ServerTemplate.XMLFormLibrary:
                    return 115;
                case ServerTemplate.MasterPagesGallery:
                    return 116;
                case ServerTemplate.NoCodeWorkflows:
                    return 117;
                case ServerTemplate.CustomWorkflowProcess:
                    return 118;
                case ServerTemplate.WikiPageLibrary:
                    return 119;
                case ServerTemplate.CustomGrid4List:
                    return 120;
                case ServerTemplate.DataConnectionLibrary:
                    return 130;
                case ServerTemplate.WorkflowHistory:
                    return 140;
                case ServerTemplate.GanttTasksList:
                    return 150;
                case ServerTemplate.MeetingSeriesList:
                    return 200;
                case ServerTemplate.MeetingAgendaList:
                    return 201;
                case ServerTemplate.MeetingAttendeesList:
                    return 202;
                case ServerTemplate.MeetingDecisionsList:
                    return 204;
                case ServerTemplate.MeetingObjectivesList:
                    return 207;
                case ServerTemplate.MeetingTextBox:
                    return 210;
                case ServerTemplate.MeetingThings2Bring:
                    return 211;
                case ServerTemplate.MeetingWorkspacePages:
                    return 212;
                case ServerTemplate.PortalSitesList:
                    return 300;
                case ServerTemplate.BlogPosts:
                    return 301;
                case ServerTemplate.BlogComments:
                    return 302;
                case ServerTemplate.BlogCategories:
                    return 303;
                case ServerTemplate.PageLibrary:
                    return 850;
                case ServerTemplate.IssueTracking:
                    return 1100;
                case ServerTemplate.AdministratorTasks:
                    return 1200;
                case ServerTemplate.PersonalDocumentLibrary:
                    return 2002;
                case ServerTemplate.PrivateDocumentLibrary:
                    return 2003;
                default:
                    return 100;
            }
        }

        public static int BaseTypeID(BaseType type)
        {
            switch (type)
            {
                case BaseType.GenericList:
                    return 0;
                case BaseType.DocumentLibrary:
                    return 1;
                case BaseType.DiscussionForum:
                    return 3;
                case BaseType.VoteASurvey:
                    return 4;
                case BaseType.IssuesList:
                    return 5;
                default:
                    return 0;
            }
        }

        public enum WebScope
        {
            Recursive,
            SiteCollection
        }

        public enum ServerTemplate
        {
            Genericlist,
            DocumentLibrary,
            Survey,
            LinksList,
            AnnouncementsList,
            ContactsList,
            EventsList,
            TasksList,
            DiscussionBoard,
            PictureLibrary,
            DataSources,
            SiteTemplateGallery,
            UserInformationList,
            WebPartGallery,
            ListTemplateGallery,
            XMLFormLibrary,
            MasterPagesGallery,
            NoCodeWorkflows,
            CustomWorkflowProcess,
            WikiPageLibrary,
            CustomGrid4List,
            DataConnectionLibrary,
            WorkflowHistory,
            GanttTasksList,
            MeetingSeriesList,
            MeetingAgendaList,
            MeetingAttendeesList,
            MeetingDecisionsList,
            MeetingObjectivesList,
            MeetingTextBox,
            MeetingThings2Bring,
            MeetingWorkspacePages,
            PortalSitesList,
            BlogPosts,
            BlogComments,
            BlogCategories,
            PageLibrary,
            IssueTracking,
            AdministratorTasks,
            PersonalDocumentLibrary,
            PrivateDocumentLibrary
        }

        public enum BaseType
        {
            GenericList,
            DocumentLibrary,
            DiscussionForum,
            VoteASurvey,
            IssuesList,
        }

        public class OrderBys
        {
            public string Field { get; set; }
            public bool Ascending { get; set; }
        }

        public static string[] Separator_Keywords = new string[] { " ", "|" };
    }
}
