using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SP.Base.Utility
{
    public static class GeneralHelper
    {
        public static T FromJSON<T>(string input)
        {
            return new JavaScriptSerializer().Deserialize<T>(input);
        }
    }
}
