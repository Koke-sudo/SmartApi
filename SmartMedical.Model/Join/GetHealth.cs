using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model.Join
{
    //获取档案信息 两表patient join healthfile
    public class GetHealth
    {
        public int HealthFileId { get; set; }
        public string PatientCode { get; set; }
        public string Kidney { get; set; }
        public string Marriage { get; set; }
        public DateTime CreateDate{ get; set; }
        public string CreateDateStr{ get { return this.CreateDate.ToString("yyyy-MM-dd"); } set { } }
        public string Bith { get; set; }
        public string DiseasesHistory { get; set; }
        public string PatientName { get; set; }
        public bool PatientSex { get; set; }
        public int PatientAge { get; set; }
        public int PatientHeight { get; set; }
        public int PatientWeight { get; set; }
        public string Liver { get; set; }
    }
}
