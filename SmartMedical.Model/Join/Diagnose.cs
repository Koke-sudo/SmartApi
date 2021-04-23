using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model.Join
{
    /// <summary>
    /// 用来显示 获取医生端诊断管理的列表
    /// </summary>
    public class Diagnose
    {
        public DateTime InquiryDate { get; set; }
        public float Price { get; set; }
        public int PatientNum { get; set; }
    }
}
