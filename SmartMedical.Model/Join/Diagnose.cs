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
        public string InquiryDateStr { get { return this.InquiryDate.ToString("yyyy-MM-dd"); } set { } }
        public float Price { get; set; }
        public int PatientNum { get; set; }
        public int i { get; set; }
    }
}
