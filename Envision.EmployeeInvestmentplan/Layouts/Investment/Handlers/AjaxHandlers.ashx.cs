using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using Envision.EmployeeInvestmentplan.Utility.Enums;
using Envision.EmployeeInvestmentplan.Utility.Utilities;
using System.Collections.Generic;
using Envision.EmployeeInvestmentplan.Utility.Biz;

namespace Envision.EmployeeInvestmentplan.Layouts.Investment.Handlers
{
    public partial class AjaxHandlers : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                //throw new NotImplementedException();
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
           
            context.Response.ContentType = "application/Json";
            string contents;
            //int listItemId;
            try
            {
                InvestMethodName methodName = (InvestMethodName)Convert.ToInt32(context.Request["MethodName"]);
                string employee = context.Request["Employee"];
                string amountF = context.Request["AmountF"];
                string amountB = context.Request["AmountB"];
                switch (methodName)
                {
                    case InvestMethodName.AddItem:
                        List<InvestModel> dataList = new List<InvestModel>();
                        
                        dataList.employee = employee;
                        dataList.amountF = amountF;
                        dataList.amountB = amountB;
                        contents = ListItemsHelper.AddListItem(dataList);
                        break;
                    case InvestMethodName.GetItems:
                        contents = ListItemsHelper.GetListItems();
                        break;
                    //case PortalMethodName.GetAnnouncementDetailed:
                    //    listItemId = IBRequest.GetQueryInt("listItemId");
                    //    contents = PortalHandler.GetAnnouncementDetailed(listItemId);
                    //    break;
                    default:
                        contents = Util.WriteJsonpToResponse(ResponseStatus.Failure, "Url Is Error!");
                        break;
                }
            }
            catch (Exception exception)
            {
                contents = Util.WriteJsonpToResponse(ResponseStatus.Exception, exception.Message);
            }
            context.Response.Write(contents);
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //}
    }
}
