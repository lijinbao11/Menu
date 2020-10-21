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
        //public virtual ICollection<Manager> Manager { get; set; }

        public virtual Manager Manager { get; set; }
        


    }
}
