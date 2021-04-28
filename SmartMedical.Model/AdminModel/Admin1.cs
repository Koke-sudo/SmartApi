using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMedical.Model
{
    [Table("Admin")]
    public class Admin1
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminPassWord { get; set; }
    }
}
