using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMedical.Model
{
    /// <summary>
    /// 管理员表
    /// </summary>
    [Table("Admin")]
    public class Admin
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminPassWord { get; set; }
    }
}
