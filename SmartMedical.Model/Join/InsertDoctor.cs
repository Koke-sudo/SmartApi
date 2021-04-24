using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model.Join
{
    //完善医生资料  注册2页面
    public class InsertDoctor
    {
        public string DoctorName { get; set; }
        public string DoctorPhone { get; set; }
        public int DoctorLv { get; set; }
        public string DoctorIdCard { get; set; }
        public int DoctorHospital { get; set; }
        public string DoctorIdCardImg { get; set; }
        public string DoctorZgzs { get; set; }
        public string DoctorYyzs { get; set; }
    }
}
