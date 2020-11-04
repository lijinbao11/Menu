using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMenu.Model.ViewModel
{
    /// <summary>
    /// 树结构对象
    /// </summary>
    public class SysTemMenus
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public long pId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 节点地址
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// 新开Tab方式
        /// </summary>
        public string target { get; set; } = "_self";

        /// <summary>
        /// 菜单图标样式
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public List<SysTemMenus> child { get; set; }
    }
}
