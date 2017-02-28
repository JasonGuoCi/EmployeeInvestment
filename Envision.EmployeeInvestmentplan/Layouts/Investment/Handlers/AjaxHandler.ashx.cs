using System;
using Microsoft.SharePoint;
using System.Web;
using Envision.EmployeeInvestmentplan.Utility.Enums;
using Envision.EmployeeInvestmentplan.Utility.Biz;
using System.Collections.Generic;
using Envision.EmployeeInvestmentplan.Utility.Utilities;

namespace Envision.EmployeeInvestmentplan.Layouts.Investment.Handlers
{
    public partial class AjaxHandler : IHttpHandler
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //}

        public bool IsReusable
        {

            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            //throw new NotImplementedException();
            context.Response.ContentType = "application/Json";
            string content;
            try
            {
                string employee = context.Request["Employee"];
                string amountF = context.Request["AmountF"];
                string amountB = context.Request["AmountB"];

                InvestMethodName methodName = (InvestMethodName)Convert.ToInt32(context.Request["MethodName"]);

                switch (methodName)
                {
                    case InvestMethodName.AddItem:
                        InvestModel dataList = new InvestModel();
                        dataList.employee = employee;
                        dataList.amountB = amountB;
                        dataList.amountF = amountF;
                        content = ListItemsHelper.AddListItem(dataList);

                        break;
                    case InvestMethodName.GetItems:
                        content = ListItemsHelper.GetListItems();
                        break;
                    default:
                        content = Util.WriteJsonpToResponse(ResponseStatus.Failure, "Url Is Error!");
                        break;
                }
            }
            catch (Exception ex)
            {
                content = Util.WriteJsonpToResponse(ResponseStatus.Exception, ex.Message);
                //throw;
            }
            context.Response.Write(content);
        }

    }
}
