using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using System.Web.Script.Serialization;

namespace KMS_Copy.Layouts
{
    public partial class UploadMoreFile : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            HttpPostedFile FileUpload = context.Request.Files["FileData"];
            JavaScriptSerializer fms = new JavaScriptSerializer();
            try
            {
                if (FileUpload != null)
                {
                    var siteUrl = context.Request.Params["siteUrl"];
                    var username = SPContext.Current.Web.CurrentUser.Name;

                    SPSecurity.RunWithElevatedPrivileges(() =>
                    {
                        using (SPSite site = new SPSite(siteUrl))
                        {
                            using (SPWeb web = site.OpenWeb())
                            {
                                SPFolder folder = null;
                                var fileName = FileUpload.FileName;
                                web.AllowUnsafeUpdates = true;
                                var user = web.EnsureUser(username);

                                var documentLibrary = web.Lists[context.Request.Params["ListName"]];
                                folder = documentLibrary.RootFolder;

                                if (folder != null)
                                {
                                    if (folder.Files != null && folder.Files.Count > 0)
                                    {
                                        foreach (SPFile tempFile in folder.Files)
                                        {
                                            if (fileName.ToLower() == tempFile.Name.ToLower())
                                            {
                                                context.Response.Write(fms.Serialize(new FileMsg() { IsError = true, Msg = fileName, ErrorCode = 0 }));
                                                return;
                                            }
                                        }
                                    }

                                    var sFileName = FileUpload.FileName;
                                    var newFile = folder.Files.Add(sFileName, FileUpload.InputStream, true);
                                    folder.Update();

                                    newFile.Item.SystemUpdate();
                                    if (!newFile.CheckOutType.Equals(SPFile.SPCheckOutType.None))
                                    {
                                        newFile.CheckIn(string.Empty);
                                    }
                                    context.Response.Write(fms.Serialize(new FileMsg() { IsError = false, FileName = sFileName, FileUrl = newFile.Url, Msg = "", ErrorCode = 0 }));
                                }
                                web.AllowUnsafeUpdates = false;
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(fms.Serialize(new FileMsg() { IsError = true, Msg = FileUpload.FileName + "" + ex.Message, ErrorCode = 2 }));
            }
        }


    }

    public class FileMsg
    {
        public string FileName { get; set; }

        public string FileUrl { get; set; }

        public string Msg { get; set; }

        public bool IsError { get; set; }

        /// <summary>
        /// 1:文件重名,2:异常信息
        /// </summary>
        public int ErrorCode { get; set; }
    }
}
