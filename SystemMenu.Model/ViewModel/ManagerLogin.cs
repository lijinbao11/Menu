using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMenu.Model.ViewModel
{
    public class ManagerLogin
    {
         public int Id { get; set; }
        /// <summary>
        /// 登录IP
        /// </summary>
        public string IPconfig { get; set; }
        /// <summary>
        /// 端口 
        /// </summary>
        public int COMport { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 登录记录外键
        /// </summary>
        public int Mid { get; set; } 
        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }
    }
}
