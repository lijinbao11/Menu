﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMenu.Model.ViewModel
{
    /// <summary>
    /// 菜单结果对象
    /// </summary>
    public class MenusInfoResultDTO
    {
        /// <summary>
        /// 权限菜单树
        /// </summary>
        public List<SysTemMenus> MenuInfo { get; set; }

        /// <summary>
        /// logo
        /// </summary>
        public LogoInfo LogoInfo { get; set; } 

        /// <summary>
        /// Home
        /// </summary>
        public HomeInfo HomeInfo { get; set; }

       
    }
}
