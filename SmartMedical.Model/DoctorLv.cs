using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMedical.Model
{
    /// <summary>
    /// 医师等级表
    /// </summary>
    [Table("doctorlv")]
    public class DoctorLv
    {
        public int DoctorLvId { get; set; }
        public string DoctorLvName { get; set; }
    }
}
