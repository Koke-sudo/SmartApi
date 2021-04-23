using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
    /// <summary>
    /// 医院表
    /// </summary>
    public class Hospital
    {
        public int id { get; set; }
        public string HospitalCode { get; set; }
        public string HospitalName { get; set; }
    }
}
