using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMedical.Model
{
    [Table("patient")]
    public class Patient1
    {
        public int PatientId       { get; set; }
        public string PatientCode     { get; set; }
        public string PatientName     { get; set; }
        public int PatientAge      { get; set; }
        public int PatientHeight   { get; set; }
        public int PatientWeight   { get; set; }
        public string PatientPhone    { get; set; }
        public string PatientPassWord { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDates { get { return CreateDate.ToString("yyyy-MM-dd"); } set { } }
        public int Amount { get; set; }
        public int Counts { get; set; }
        public int MyProperty { get; set; }
        public DateTime InquiryDate { get; set; }
        public double InquiryPrice { get; set; }
        public string DoctorName { get; set; }

        public string DoctorLvName { get; set; }
        public string HospitalName { get; set; }
        public string Illness { get; set; }

        public int DoctorId { get; set; }
        public int ReportId { get; set; }
        public int InquiryId { get; set; }
        public string Diagnose { get; set; }

        public int HealthFileId { get; set; }
        public double PriceSum { get; set; }
        public int InquiryNum { get; set; }
        public string InquiryRemark { get; set; }
        public string InquiryMessage { get; set; }
        public string Kidney { get; set; }

        public string Marriage { get; set; }
        public string Bith { get; set; }
        public string DiseasesHistory { get; set; }
        public string Liver { get; set; }
    }
}
