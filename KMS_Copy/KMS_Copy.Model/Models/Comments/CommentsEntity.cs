using KMS_Copy.Model.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace KMS_Copy.Model.Models.Comments
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
        [DataMember]
        public string Id { set; get; }
        [DataMember]
        public string CreatedBy { set; get; }
        [DataMember]
        public DateTime Created { set; get; }
        [DataMember]
        public string Comments { set; get; }
    }
}
