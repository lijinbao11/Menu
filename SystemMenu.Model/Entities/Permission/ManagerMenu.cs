using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SystemMenu.Model.Entities.Permission
{
    [Table("bee_managerMenu")]
    public class ManagerMenu
    {
        public int Mid { get; set; }
        public virtual Manager Manager { get; set; }
        public int Meid { get; set; }
        public virtual SystemMenuEntity SystemMenu { get; set; }

    }
}
