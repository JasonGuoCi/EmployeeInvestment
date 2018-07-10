using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focuswin.SP.Base.Utility
{
    public interface ICachable
    {
        /// <summary>
        /// Execute the action of update, e.g. read data from list
        /// </summary>
        /// <returns>Last update time ticks, e.g. list.LastItemModifiedDate.Ticks</returns>
        long UpdateCachedObject();

    }
}
