using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMedical.Model
{
    /// <summary>
    /// 患者表
    /// </summary>
    [Table("patient")]
    public class Patient
    {
        public int PatientId       { get; set; }
        public string PatientCode     { get; set; }
        public string PatientIdCard     { get; set; }
        public string PatientName     { get; set; }
        public string PatientSex     { get; set; }
        public int PatientAge      { get; set; }
        public int PatientHeight   { get; set; }
        public int PatientWeight   { get; set; }
        public string PatientPhone    { get; set; }
        public string PatientPassWord { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
