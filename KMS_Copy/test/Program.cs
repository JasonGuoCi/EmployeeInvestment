using Microsoft.SharePoint;
using SP.Base.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Uri url = new Uri("http://sharepointlipan:567/_vti_bin/GetWebTitle/GetWebTitle.svc");
            //TestSvc.GetWebTitle webTitle = new TestSvc.GetWebTitle();

            //NetworkCredential nc = new NetworkCredential("enadmin", "3edc4rfv#$", "contoso");
            //webTitle.Credentials = nc;
            string siteUrl = @"http://sharepointlipan:567";
            string listname = "Announcement";
            string views = "<FieldRef Name=\"ID\"/><FieldRef Name=\"Title\"/><FieldRef Name=\"GUID\"/><FieldRef Name=\"Field_KMS_MultiContent\"/><FieldRef Name=\"Field_KMS_EnglishTitle\"/><FieldRef Name=\"Field_KMS_MultiEnglishContent\"/><FieldRef Name=\"Field_KMS_Status\"/><FieldRef Name=\"Modified\"/><FieldRef Name=\"Created\"/>";
            string caml = SearchHelper.OrderBy(SearchHelper.Where(SearchHelper.Eq("Field_KMS_Status", SearchHelper.ValueType.Text, "Published")), "Modified", false);
            int TakeCount = 1;
            int RowLimits = 4;
           // Console.WriteLine(title);

            SPListItemCollection items = ListItemHelper.ExecuteCAMLToRetrieveListItemsInPages(siteUrl,listname, views, caml, TakeCount, RowLimits);


        }
    }
}
