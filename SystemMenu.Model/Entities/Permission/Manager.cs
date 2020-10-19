using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SystemMenu.Model.Entities.Permission
{
    [Table("bee_manager")]
    public class Manager
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否 启用
        /// </summary>
        public bool IsEnable { get; set; }
      
    }
}
