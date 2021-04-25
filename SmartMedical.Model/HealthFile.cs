using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
    //档案信息
    public class HealthFile
    {
        public int HealthFileId { get; set; }
        public int PatientCode { get; set; }
        public int Kidney { get; set; }
        public int Marriage { get; set; }
        public int Bith { get; set; }
        public int DiseasesHistory { get; set; }
        public int Liver { get; set; }
    }
}
