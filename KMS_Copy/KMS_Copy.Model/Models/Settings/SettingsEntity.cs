using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KMS_Copy.Model.Models.Settings
{
    /// <summary>
    ///网站设置 
    /// </summary>
    [DataContract]
    public class SettingsEntity
    {
        private static string _Title = "Title";
        private static string _Field_KMS_DefaultValue = "Field_KMS_DefaultValue";
        private static string _Field_KMS_Description = "Field_KMS_Description";

        public SettingsEntity() { }
        public SettingsEntity(SPListItem item)
        {
            if (item != null)
            {
                Title = item[_Title] + "";
                DefaultValue = item[_Field_KMS_DefaultValue] + "";
                Description = item[_Field_KMS_Description] + "";
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string Title { get; set; }
        /// <summary>
        /// 隐藏值
        /// </summary>
        [DataMember]
        public string DefaultValue { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [DataMember]
        public string Description { get; set; }
    }
}
