//* ===================================
//* 类名称：JsonResult
//* 类描述：
//* 创建人：Ryan Ren
//* 创建时间：2015/9/30 9:58:12
//* 修改人：
//* 修改时间：
//* 修改备注：
//* @version 1.0
//* ===================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Service
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class JsonResult
    {
        [DataMember]
        public bool IsError { get; set; }

        [DataMember]
        public string ErrorMsg { get; set; }

        [DataMember]
        public Object Data { get; set; }
    }
}
