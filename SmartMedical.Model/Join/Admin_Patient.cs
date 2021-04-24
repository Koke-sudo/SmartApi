using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model.Join
{
    //管理员端患者管理列表
    public class Admin_Patient
    {
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientPhone { get; set; }
        public int PriceSum { get; set; }
        public int InquiryNum { get; set; }
    }
}
