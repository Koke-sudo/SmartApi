using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model.Join
{
    //接诊台列表
    public class GetInquiry
    {
        public DateTime InquiryDate { get; set; }
        public string InquiryDateStr { get { return this.InquiryDate.ToString("yyyy-MM-dd"); } set { } }
        public int InquiryPrice { get; set; }
        public string PatientName { get; set; }
        public bool PatientSex { get; set; }
        public string PatientSexStr { get { return this.PatientSex == true ? "男" : "女"; } set { } }
        public string InquiryMessage { get; set; }
        public int PatientAge { get; set; }
        public string InquiryReamrk { get; set; }
        public string InquiryComment { get; set; }
        public bool InquiryState { get; set; }
        public int PatientHeight { get; set; }
        public int PatientWeight { get; set; }
        public string Kidney { get; set; }
        public string Marriage { get; set; }
        public string Bith { get; set; }
        public string DiseasesHistory { get; set; }
        public string Liver { get; set; }
        public string Diagnose { get; set; }
    }
}
