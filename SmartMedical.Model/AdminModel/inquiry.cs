using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
   public class inquiry
    {
        public int InquiryId { get; set; }
        public int InquiryDate { get; set; }
        public int InquiryComment { get; set; }
        public int DoctorCode { get; set; }
        public int PatientCode { get; set; }
        public int InquiryPrice { get; set; }

    }
}
