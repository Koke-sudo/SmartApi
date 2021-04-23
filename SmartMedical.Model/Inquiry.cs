using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
    /// <summary>
    /// 接诊列表
    /// </summary>
    public class Inquiry
    {
        public int InquiryId { get; set; }
        public DateTime InquiryDate { get; set; }
        public bool InquiryState { get; set; }
        public string DoctorCode { get; set; }
        public string PatientCode { get; set; }
        public int InquiryPrice { get; set; }
        public string InquiryRemark { get; set; }
        public string InquiryMessage { get; set; }
        public string InquiryComment { get; set; }
    }
}
