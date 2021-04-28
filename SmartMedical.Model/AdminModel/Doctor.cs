using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
    public class Doctor
    {

        public int DoctorId { get; set; }     //id
        public string DoctorCode { get; set; }  //医生编号
        public string DoctorPhone { get; set; }  //医生电话

        public int DoctorNum { get { return 120; } set { } }  //医生数量
        public int InquiryNum { get { return 235; } set { } }  //问诊数量
        public string HaoPing { get { return "99%"; } set { } }  //好评度

        public string DoctorPassword { get; set; }    //医生密码
        public string DoctorName { get; set; }      //医生姓名
        public string DoctorHeadImg { get; set; }   //医生头像
        public int DoctorLv { get; set; }    //医生等级
        public string DoctorLvName { get; set; }  //医生等级
        public int DoctorYear { get; set; }   //医生年限
        public string DoctorHospital { get; set; }    //所属医院
        public string HospitalName { get; set; }   //医院名称
        public int DoctorOffice { get; set; }      //所属科室
        public string OName { get; set; }     //科室
        public bool DoctorState { get; set; }   //医生状态
        public string DoctorZgzs { get; set; }     //医生资格证书
        public string DoctorYyzs { get; set; }     //医生执业证书
        public string PatientName { get; set; }
        public DateTime InquiryDate { get; set; }
        public string Time { get { return InquiryDate.ToString("yyyy-MM-dd"); } set { } }
        public string Diagnose { get; set; }
        public string Kidney { get; set; }//肾功能
        public string Marriage { get; set; }
        public string Bith { get; set; }//生育
        public string DiseasesHistory { get; set; }//描述
        public string Liver { get; set; }//肝功能
        public double InquiryPrice { get; set; }
        public string InquiryComment { get; set; }
        public string InquiryRemark { get; set; }
        public int PatientAge { get; set; }
        public int PatientHeight { get; set; }
        public int PatientWeight { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public string DoctorReason { get; set; }
        public int State { get; set; }


    }
}
