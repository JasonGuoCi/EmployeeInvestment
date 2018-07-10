using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.LanguageMapping
{
    /// <summary>
    ///语言实体
    /// </summary>
    [DataContract]
    public class LanguageEntity
    {
        public LanguageEntity() { }
        public LanguageEntity(SPListItem item, string langflag)
        {
            if (item != null)
            {
                Model = item[_Field_KMS_Model] + "";
                DefaultValue = langflag == "1033" ? (item[_Field_KMS_EnglishName] + "") : (item[_Field_KMS_ChineseName] + "");
                Title = item[_Title] + "";
            }
        }
        /// <summary>
        ///模块
        /// </summary>
        [DataMember]
        public string Model { set; get; }
        /// <summary>
        ///显示值
        /// </summary>
        [DataMember]
        public string DefaultValue { set; get; }
        /// <summary>
        ///标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
        [DataMember]

        private static string _Title = "Title";
        private static string _Field_KMS_Model = "Field_KMS_Model";
        private static string _Field_KMS_ChineseName = "Field_KMS_ChineseName";
        private static string _Field_KMS_EnglishName = "Field_KMS_EnglishName";
    }

     [DataContract]
    /// <summary>
    /// 
    /// </summary>
    public class LanguageParamEntity
    {
        /// <summary>
        ///网页地址
        /// </summary>
        [DataMember]
        public string weburl { set; get; }
        /// <summary>
        ///模块
        /// </summary>
        [DataMember]
        public string model { set; get; }
        /// <summary>
        ///关键字
        /// </summary>
        [DataMember]
        public string key { set; get; }
        /// <summary>
        ///语种
        /// </summary>
        [DataMember]
        public string lang { set; get; }
    }

    public class LangEntity
    {

    }
    /// <summary>
    ///左侧导航
    /// </summary>
    [DataContract]
    public class leftnavEntity
    {
        public leftnavEntity() { }
        public leftnavEntity(string _i1,string _i2,string _i3,string _i4,string _i5) {
            item1 = _i1;
            item2 = _i2;
            item3 = _i3;
            item4 = _i4;
            item5 = _i5;
        }

        [DataMember]
        public string item1 { set; get; }

        [DataMember]
        public string item2 { set; get; }
        [DataMember]
        public string item3 { set; get; }
        [DataMember]
        public string item4 { set; get; }
        [DataMember]
        public string item5 { set; get; }
    }

}
