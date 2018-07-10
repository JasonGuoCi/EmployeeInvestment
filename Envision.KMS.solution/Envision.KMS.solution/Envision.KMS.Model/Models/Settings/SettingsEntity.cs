using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Settings
{
    /// <summary>
    ///网站设置 
    /// </summary>
    [DataContract]
    public class SettingsEntity
    {
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
        ///标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
        /// <summary>
        ///隐藏值
        /// </summary>
        [DataMember]
        public string DefaultValue { set; get; }
        /// <summary>
        ///说明
        /// </summary>
        [DataMember]
        public string Description { set; get; }

        private static string _Title = "Title";
        private static string _Field_KMS_DefaultValue = "Field_KMS_DefaultValue";
        private static string _Field_KMS_Description = "Field_KMS_Description";
    }
}
