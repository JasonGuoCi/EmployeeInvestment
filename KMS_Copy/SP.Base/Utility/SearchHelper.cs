using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Base.Utility
{
    public class SearchHelper
    {
        public static string And(params string[] conditions)
        {
            return ConcatConditions("And", conditions);
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
        public static string Contains(string fieldName, ValueType valueType, object value)
        {
            return Condition("Contains", fieldName, valueType, value);
        }
        public static string OrderBy(string where, string fieldName, bool ascending)
        {
            return where + OrderBy(fieldName, ascending);
        }

        public static string OrderBy(string fieldName, bool ascending)
        {
            return string.Format("<OrderBy><FieldRef Name=\"{0}\" Ascending=\"{1}\"/></OrderBy>", fieldName, ascending);
        }

        public static string Where(string conditions)
        {
            return "<Where>" + conditions + "</Where>";
        }
        public static string Eq(string fieldName, ValueType valueType, object value)
        {
            return Condition("Eq", fieldName, valueType, value);
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
                value = ConvertDateTime((DateTime)value);
            }

            return string.Format("<{0}><FieldRef Name='{1}'{4}/><Value Type='{2}'{5}>{3}</Value></{0}>", op, fieldName, Type2Keyword(valueType), value, placeHolderLookup, placeHolderIncludeTime);
        }

        public static string IsNotNull(string fieldName)
        {
            return string.Format("<IsNotNull><FieldRef Name='{0}'/></IsNotNull>", fieldName);
        }
        public static string ConvertDateTime(DateTime date)
        {

            return SPUtility.CreateISO8601DateTimeFromSystemDateTime(date);
        }
        public static string Or(params string[] conditions)
        {
            return ConcatConditions("Or", conditions);
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

    }
}
