using Focuswin.SP.Base.Utility;
using Microsoft.SharePoint.Client.Services;
//* ===================================
//* 类名称：BaseService
//* 类描述：
//* 创建人：Ryan Ren
//* 创建时间：2015/9/30 9:57:42
//* 修改人：
//* 修改时间：
//* 修改备注：
//* @version 1.0
//* ===================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Service
{
    /// <summary>
    /// 
    /// </summary>
    [BasicHttpBindingServiceMetadataExchangeEndpointAttribute]
    [ServiceBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public abstract class BaseService
    {
        protected virtual JsonResult Json(Object obj)
        {
            return new JsonResult() { Data = obj };
        }

        protected virtual JsonResult Json(Exception ex)
        {
            return new JsonResult() { IsError = true, ErrorMsg = ex.Message };
        }
    }


    public class SearchEntity {
        public bool Inherit { set; get; }
        public string ResultsPageAddress { set; get; }
        public bool ShowNavigation { set; get; } 
    }
}
