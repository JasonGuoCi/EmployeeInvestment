//* ===================================
//* 类名称：ManagerBase
//* 类描述：
//* 创建人：Ryan Ren
//* 创建时间：2015/10/2 18:02:02
//* 修改人：
//* 修改时间：
//* 修改备注：
//* @version 1.0
//* ===================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focuswin.SP.Base.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class ManagerBase
    {
        public ManagerBase()
        {
        }

        public ManagerBase(string SettingStr)
        {
            connectstr = SettingStr;
        }

        public string connectstr { set; get; }
    }
}
