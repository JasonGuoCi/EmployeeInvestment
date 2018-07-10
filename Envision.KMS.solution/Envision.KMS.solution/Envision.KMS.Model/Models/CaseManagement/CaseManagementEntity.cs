using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.CaseManagement
{
    [DataContract]
    public class CaseManagementEntity
    {
        /// <summary>
        /// 软件问题及建议实体
        /// </summary>
        public CaseManagementEntity() { }

        public CaseManagementEntity(SPListItem item,string casetype)
        {
            Id = item.ID + "";
            UniqueId = item["GUID"] + "";
            Title = item["Title"] + "";
            var eventDescField = item.ParentList.Fields.GetFieldByInternalName("Field_Case_Content");
            var eventDesc = item[eventDescField.Id];
            OrginContent = eventDescField.GetFieldValueAsText(eventDesc);
            Content = OrginContent.Replace("\r\n", "<br>");
            Content = Content.Replace("\n", "<br>");
            CaseType = new SPFieldLookupValue(item["Field_Case_Type"] as String).LookupId + "";
            CaseTypeDisplay = casetype;
            IsTop = item["Field_KMS_IsTop"] + "";
            Created = item["Created"] + "";
            var commentsField = item.ParentList.Fields.GetFieldByInternalName("Field_Case_Comments");
            var commentsDesc = item[commentsField.Id];
            Reply = commentsField.GetFieldValueAsText(commentsDesc);
            Reply = Reply.Replace("\r\n", "<br>").Replace("\n", "<br>");
            Status = item["Field_Case_Status"] + "";
        }
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
        ///回复意见
        /// </summary>
        [DataMember]
        public string Reply { set; get; }
        /// <summary>
        ///case内容
        /// </summary>
        [DataMember]
        public string Content { set; get; }
        /// <summary>
        ///内容源
        /// </summary>
        [DataMember]
        public string OrginContent { set; get; }
        /// <summary>
        ///case类型
        /// </summary>
        [DataMember]
        public string CaseType { set; get; }
        /// <summary>
        ///case类型显示值
        /// </summary>
        [DataMember]
        public string CaseTypeDisplay { set; get; }
        /// <summary>
        ///是否置顶
        /// </summary>

        [DataMember]
        public string IsTop { set; get; }
        /// <summary>
        ///创建时间
        /// </summary>
        [DataMember]
        public string Created { set; get; }
        /// <summary>
        ///修改时间
        /// </summary>
        [DataMember]
        public string Modified { set; get; }
        /// <summary>
        ///作者
        /// </summary>
        [DataMember]
        public string Author { set; get; }
        /// <summary>
        ///网页地址
        /// </summary>
        [DataMember]
        public string weburl { set; get; }
        /// <summary>
        ///列表名称
        /// </summary>
        [DataMember]
        public string listname { set; get; }
        /// <summary>
        ///状态
        /// </summary>
        [DataMember]
        public string Status { set; get; }
        /// <summary>
        ///任务内容
        /// </summary>

        [DataMember]
        public List<CaseTaskEntity> taskitems { set; get; }
    }
    /// <summary>
    /// 软件问题及建议类型
    /// </summary>
    [DataContract]
    public class CastTypeEntity
    {
        /// <summary>
        ///case类型编号
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        ///case类型标题
        /// </summary>
        [DataMember]
        public string Title { set; get; }
    }
    /// <summary>
    /// 软件问题及建议任务实体
    /// </summary>
    [DataContract]
    public class CaseTaskEntity
    {
        public CaseTaskEntity() { }
        public CaseTaskEntity(CaseManagementTask rd)
        {
            Id = rd.Id + "";
            CaseId = rd.CaseId;
            CaseTitle = rd.CaseTitle;
            Created = rd.Created + "";
            Deadline = rd.Created.AddDays(5) + "";
            CreatedBy = rd.CreatedBy;
            Comments = rd.Comments;
            if (!string.IsNullOrEmpty(Comments))
            { 
            Comments = Comments.Replace("\r\n", "<br>");
            Comments = Comments.Replace("\n", "<br>");
            }
            AssignTo = rd.AssignedToDisplayName;
            Result = rd.Result;
        }
        public CaseTaskEntity(CaseManagementTask rd,string typetext) {
            Id = rd.Id + "";
            CaseId = rd.CaseId;
            CaseTitle = rd.CaseTitle;
            Created = rd.Created + "";
            Deadline = rd.Created.AddDays(5) + "";
            CreatedBy = rd.CreatedBy;
            CaseType = typetext;
            Comments = rd.Comments;
            if (!string.IsNullOrEmpty(Comments))
            {
                Comments = Comments.Replace("\r\n", "<br>");
                Comments = Comments.Replace("\n", "<br>");
            }
            AssignTo = rd.AssignedToDisplayName;
            Result = rd.Result;
        }
        /// <summary>
        ///case任务编号
        /// </summary>
        [DataMember]
        public string Id { set; get; }
        /// <summary>
        ///case编号
        /// </summary>
        [DataMember]
        public string CaseId { set; get; }
        /// <summary>
        ///case标题
        /// </summary>
        [DataMember]
        public string CaseTitle { set; get; }
        /// <summary>
        ///case作者
        /// </summary>
        [DataMember]
        public string CreatedBy { set; get; }
        /// <summary>
        ///case创建时间
        /// </summary>
        [DataMember]
        public string Created { set; get; }
        /// <summary>
        ///case类型
        /// </summary>
        [DataMember]
        public string CaseType { set; get; }
        /// <summary>
        ///case内容
        /// </summary>
        [DataMember]
        public string Comments { set; get; }
        /// <summary>
        ///case负责人
        /// </summary>
        [DataMember]
        public string AssignTo { set; get; }
        /// <summary>
        ///case任务结果
        /// </summary>


        [DataMember]
        public string Result { set; get; }
        /// <summary>
        ///case任务结束时间
        /// </summary>
        [DataMember]
        public string Deadline { set; get; }
    }

}
