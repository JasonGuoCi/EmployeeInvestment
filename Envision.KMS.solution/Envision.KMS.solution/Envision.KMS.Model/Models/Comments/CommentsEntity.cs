using Envision.KMS.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Comments
{
    [DataContract]
    public class CommentsEntity
    {
        /// <summary>
        ///评论实体
        /// </summary>
        public CommentsEntity() { }
        public CommentsEntity(KMSComment dbItem)
        {
            Id = dbItem.Id + "";
            CreatedBy = dbItem.CreatedBy;
            Created = dbItem.Created;
            Comments = dbItem.Comments;
        }

        public CommentsEntity(KMSForum dbItem)
        {
            Id = dbItem.Id + "";
            CreatedBy = dbItem.CreatedBy;
            Created = dbItem.Created;
            Comments = dbItem.Comments;
        }
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        /// 评论者
        /// </summary>
        [DataMember]
        public string CreatedBy { set; get; }
        /// <summary>
        /// 评论时间
        /// </summary>
        [DataMember]
        public DateTime Created { set; get; }
        /// <summary>
        /// 评论内容
        /// </summary>
        [DataMember]
        public string Comments { set; get; }
    }
}
