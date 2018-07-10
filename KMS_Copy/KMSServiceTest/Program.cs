using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KMSServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public static void GetWebTitle()
        {
            //设置代理
            //BasicHttpBinding myBinding = new BasicHttpBinding();
            //myBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            //myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            //EndpointAddress ea = new EndpointAddress(http://sharepointlipan:567/_vti_bin/KMS_Copy.Service/Global.svc);
            ////WCF服务的方法调用
            //KMSService.GlobalServiceClient nav = new KMSService.GlobalServiceClient(myBinding, ea)；
            
            //KMSService.GlobalServiceClient 
            //GetWebTitleService.GetWebTitleClient up = new GetWebTitleService.GetWebTitleClient(myBinding, ea);
            //up.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            ////和注释的两种授权方式，都可以
            //NetworkCredential nc = new NetworkCredential("username", "password", "domain");
            //up.ClientCredentials.Windows.ClientCredential = nc;
            ////up.ClientCredentials.Windows.ClientCredential.UserName = "domain\\username ";
            ////up.ClientCredentials.Windows.ClientCredential.Password = "password";
            //Console.WriteLine("WCF调用结果：" + up.GetSPWebTitle("http://weburl", ""));
        }
    }
}
