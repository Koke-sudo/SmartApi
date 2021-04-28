using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
   public class Databasedictionary
    {
        /// <summary>
        /// 管理员表
        /// </summary>
        public int AdminId { get; set; }//管理员ID
        public string AdminName { get; set; }//管理员用户名
        public string AdminPassWord { get; set; }//管理员密码
        /// <summary>
        /// 医生表
        /// </summary>
        public int DoctorId { get; set; }//医生ID
        public string DoctorCode		 { get; set; }//医生编号
        public string UserPhone		 { get; set; }//医生电话
        public string UserPassword	 { get; set; }//医生密码
        public string DoctorName		 { get; set; }//医生名称
        public string DoctorHeadImg	 { get; set; }//医生头像
        public int DoctorLv		 { get; set; }//医生等级
        public int DoctorYear		 { get; set; }//医生年龄
        public int DoctorHospital	 { get; set; }//所属医院
        public int DoctorOffice	 { get; set; }//所属科室
        public bool DoctorState		 { get; set; }//审核状态（无用）
        public string DoctorZgzs		 { get; set; }//资格证书
        public string DoctorYyzs		 { get; set; }//医生执照证书
        public string DoctorIdCardImg	 { get; set; }//医生身份证
        public string DoctorIdCard	 { get; set; }//身份证号（占无用）
        public int State			 { get; set; }//审核状态
        public string DoctorCard		 { get; set; }//医生身份证号
        public string DoctorReason { get; set; }//审核医生评论
        /// <summary>
        /// 医生等级
        /// </summary>
        public int DoctorLvId { get; set; }//医生等级ID
        public int DoctorLvName { get; set; }//医生等级名称
        /// <summary>
        /// 商品表
        /// </summary>
        public int GoodId { get; set; }//商品ID
        public string GoodCode	 { get; set; }//商品ID
        public string GoodName	 { get; set; }//商品编码
        public string GoodImg		 { get; set; }//商品图片
        public int GoodPrice	 { get; set; }//商品单价
        public int GoodSeckill	 { get; set; }//商品秒杀
        public int GoodState	 { get; set; }//商品状态
        public int GoodType	 { get; set; }//商品品种
        public int SeckillId { get; set; }//秒杀ID
        /// <summary>
        /// 商品类型表
        /// </summary>
        public int TId { get; set; }//类型ID
        public string TName { get; set; }//类型名称
        public string GtypeImg { get; set; }//类型图片
        public bool GTypeState { get; set; }//类型状态
        /// <summary>
        /// 医院表
        /// </summary>
        public int ID { get; set; }//医院ID
        public int HospitalCode { get; set; }//医院编码
        public string HospitalName { get; set; }//医院名称
        /// <summary>
        /// 问诊表
        /// </summary>
        public int InquiryId { get; set; }//问诊ID
        public DateTime InquiryDate		 { get; set; }//问诊时间
        public bool InquiryState	 { get; set; }//问诊状态

        //public int DoctorCode		 { get; set; }(问诊表中医生编码)
        public string PatientCode		 { get; set; }//患者编码
        public double InquiryPrice	 { get; set; }//问诊价格
        public string InquiryRemark { get; set; }//问诊描述
        public string InquiryMessage { get; set; }//问诊通知
        public string InquiryComment { get; set; }//问诊评价
        /// <summary>
        /// 科室表
        /// </summary>
        public int Id { get; set; }//科室id
        public int OName { get; set; }//科室名称
        /// <summary>
        /// 患者表
        /// </summary>
        public int PatientId { get; set; }//患者ID

        /*public int PatientCode		 { get; set; }*///患者编码
        public string PatientName		 { get; set; }//患者名称
        public int PatientAge		 { get; set; }//患者年龄
        public int PatientHeight	 { get; set; }//患者高度
        public int PatientWeight	 { get; set; }//患者体重
        public string PatientPhone	 { get; set; }//患者手机
        public string PatientPassWord	 { get; set; }//患者密码
        public int CreateDate		 { get; set; }//创建患者时间
        public int Amount			 { get; set; }//问诊金额总价
        public int Counts			 { get; set; }//问诊次数
        public int Illness			 { get; set; }//问着疾病

        //public int DoctorId		 { get; set; } 患者（医生ID）
        public int Kidney			 { get; set; }//腰子功能
        public int Liver			 { get; set; }//肝功能
        public int Hun				 { get; set; }//婚姻状况
        public int ShengYu			 { get; set; }//生育状况
        public int Medical			 { get; set; }//病历史
        public int Describe		 { get; set; }//病情描述
        public int Diagnosis		 { get; set; }//诊断报告
        /// <summary>
        /// 秒杀表
        /// </summary>

        // public int SeckillId { get; set; }秒杀（秒杀id）
        public string SeckillName { get; set; }//秒杀名称
        public bool SeckillState { get; set; }//秒杀状态
        public DateTime SeckillSatrt { get; set; }//开始秒杀时间
        public DateTime SeckillEnd { get; set; }//秒杀结束时间
        public string SeckillImg { get; set; }//秒杀图片


    }
}
