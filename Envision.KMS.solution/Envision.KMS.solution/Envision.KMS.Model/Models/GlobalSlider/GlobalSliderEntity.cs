using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.GlobalSlider
{
    /// <summary>
    ///全局导航实体
    /// </summary>
     [DataContract]
    public class GlobalSliderEntity
    {
        /// <summary>
        ///编号
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        ///GUID
        /// </summary>
        [DataMember]
        public string UniqueId { set; get; }
        /// <summary>
        ///标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
        /// <summary>
        ///描述
        /// </summary>
        [DataMember]
        public string Description { set; get; }
        /// <summary>
        ///图片地址
        /// </summary>
        [DataMember]
        public string ImgUrl { set; get; }
        /// <summary>
        ///链接地址
        /// </summary>
        [DataMember]
        public string Link { set; get; }

    }

     [DataContract]
     public class GlobalContactEntity
     {
         /// <summary>
         ///编号
         /// </summary>
         [DataMember]
         public string Id { set; get; }
         /// <summary>
         ///GUID
         /// </summary>
         [DataMember]
         public string UniqueId { set; get; }
         /// <summary>
         ///标题
         /// </summary>
         [DataMember]
         public string Title { set; get; }
         /// <summary>
         ///名称
         /// </summary>
         [DataMember]
         public string Name { set; get; }
         /// <summary>
         ///
         /// </summary>
         [DataMember]
         public string Contact { set; get; }
         [DataMember]
         public string Extend1 { set; get; }
         [DataMember]
         public string Extend2 { set; get; }
         [DataMember]
         public string Extend3 { set; get; }
     }
}
