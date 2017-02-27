using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using Envision.EmployeeInvestmentplan.Utility.Enums;
using Envision.EmployeeInvestmentplan.Utility.Utilities;

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

                switch (methodName)
                {
                    case InvestMethodName.SaveItem:
                        contents = PortalHandler.GetWeather();
                        break;
                    case InvestMethodName.Getitems:
                        contents = PortalHandler.GetAnnouncement();
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
