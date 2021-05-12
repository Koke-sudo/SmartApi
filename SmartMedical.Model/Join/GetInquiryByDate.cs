using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model.Join
{
    //获取诊断列表
    public class GetInquiryByDate
    {
        public int i { get; set; }
        public string PatientCode { get; set; }
        public DateTime InquiryDate { get; set; }
        public int InquiryPrice { get; set; }
        public string PatientName { get; set; }
        public string InquiryRemark { get; set; }
        public int PatientAge { get; set; }
        public string InquiryMessage { get; set; }
        public string InquiryComment { get; set; }
        public string Diagnose { get; set; }
    }
}
