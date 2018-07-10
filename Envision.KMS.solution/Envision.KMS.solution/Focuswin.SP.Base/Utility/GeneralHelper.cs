//* ===================================
//* 类名称：GeneralHelper
//* 类描述：
//* 创建人：Ryan Ren
//* 创建时间：2015/10/1 11:11:04
//* 修改人：
//* 修改时间：
//* 修改备注：
//* @version 1.0
//* ===================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Focuswin.SP.Base.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static class GeneralHelper
    {
        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;
            stream.Position = 0;
            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                stream.Position = originalPosition;
            }
        }

        public static string ToString(object obj)
        {
            string result = "";
            if (obj is IDictionary)
            {
                var sb = new StringBuilder();
                var dict = obj as IDictionary;
                foreach (var key in dict.Keys)
                {
                    sb.AppendFormat("Key:{0} Value:{1} ;", ToString(key), ToString(dict[key]));
                }
                result = string.Format("IDictionary of {0}:\n {1}\n", obj.GetType(), sb);
            }
            else
            {
                result += obj;
            }

            return result;
        }

        public static string resolveLogon(string logon)
        {
            if (!string.IsNullOrEmpty(logon) && logon.Contains(@"i:0#"))
            {
                return logon.Split('\\')[1].ToUpper();
            }
            else
            {
                return logon;
            }
        }

        public static string ToJSON(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public static T FromJSON<T>(string input)
        {
            return new JavaScriptSerializer().Deserialize<T>(input);
        }

        public static string ConcatUrlString(string first, string last)
        {
            if (string.IsNullOrEmpty(first))
            {
                return last;
            }
            if (string.IsNullOrEmpty(last))
            {
                return first;
            }
            if (first[first.Length - 1] == '/')
            {
                if (last[0] == '/')
                    return first + last.Substring(1);
                else
                    return first + last;
            }
            else
            {
                if (last[0] == '/')
                    return first + last;
                else
                    return first + "/" + last;
            }
        }
    }
}
