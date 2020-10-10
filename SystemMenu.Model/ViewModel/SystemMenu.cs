﻿using System;
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
        public long Id { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public long PId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 节点地址
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 新开Tab方式
        /// </summary>
        public string Target { get; set; } = "_self";

        /// <summary>
        /// 菜单图标样式
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public List<SysTemMenus> Child { get; set; }
    }
}
