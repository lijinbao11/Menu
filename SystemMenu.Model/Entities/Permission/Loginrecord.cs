using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SystemMenu.Model.Entities.Permission
{
    /// <summary>
    /// 登录记录信息表
    /// </summary>
    [Table("bee_login_record")]
    public class Loginrecord
    {
        /// <summary>
        /// 主键  
        /// </summary>
        public int Id{ get; set; }
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
        public virtual Manager Manager { get; set; }


        //https://blog.csdn.net/u011872945/article/details/72957162?utm_medium=distribute.pc_aggpage_search_result.none-task-blog-2~all~baidu_landing_v2~default-1-72957162.nonecase&utm_term=c#%E5%AE%9E%E4%BD%93%E7%B1%BB%E4%B8%BB%E5%A4%96%E9%94%AE%E5%85%B3%E7%B3%BB%E8%AE%BE%E7%BD%AE&spm=1000.2123.3001.4430


    }
}
