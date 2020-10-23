using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SystemMenu.Model.Entities.Permission
{
    /// <summary>
    /// 菜单表
    /// </summary>
    [Table("bee_system_menu")]
    public class SystemMenuEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        [Required]
        public int Pid { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否菜单
        /// </summary>
        public bool Status { get; set; }

        public virtual ICollection<ManagerMenu> ManagerMenus { get; set; }
    }
}
