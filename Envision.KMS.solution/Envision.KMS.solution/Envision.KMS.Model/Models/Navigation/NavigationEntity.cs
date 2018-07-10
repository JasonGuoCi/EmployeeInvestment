using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.KMS.Model.Models.Navigation
{
    public class NavigationEntity
    {
        /// <summary>
        ///标题
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        ///链接地址
        /// </summary>
        public string LinkUrl { set; get; }
        /// <summary>
        ///排序
        /// </summary>
        public string SeriesNo { set; get; }
        /// <summary>
        ///子节点
        /// </summary>

        public List<NavigationEntity> Children { set; get; }
    }
}
