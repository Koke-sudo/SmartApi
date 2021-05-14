using Newtonsoft.Json;
using SmartMedical.DAL;
using SmartMedical.Model;
using SmartMedical.Model.Join;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SmartMedical.BLL
{
    public class SmartMedicalBLL
    {
        DBHelper _db;
        public SmartMedicalBLL(DBHelper db)
        {
            _db = db;
        }

        

        #region 患者模块
        //患者登录
        public int Login(string phone, string password)
        {
            string sql = $"select * from patient where patientphone='{phone}' and patientpassword='{password}'";
            DataSet ds = _db.GetDateSet(sql);
            int h = ds.Tables[0].Rows.Count;
            return h;
        }
        //获取所有患者信息
        public List<Patient1> GetPatients()
        {
            string sql = "select * from patient";
            DataSet ds = _db.GetDateSet(sql);
            List<Patient1> list = _db.TableToList<Patient1>(ds.Tables[0]);
            return list;
        }
        //验证患者手机号是否存在
        public DataSet ZhuCe(string phone)
        {
            string sql = $"select * from patient where patientphone='{phone}'";
            return _db.GetDateSet(sql);
        }

        //注册患者
        public int ZhuceIn(string phone, string password)
        {
            string sql = $"insert into patient(patientphone,patientpassword) values ('{phone}','{password}') ";
            return _db.ExecuteNonQuery(sql);
        }
        #endregion




        #region 医生模块
        //医生登录
        public int DoctorLogin(string phone, string password)
        {
            string sql = $"select * from doctor where userphone='{phone}' and userpassword='{password}'";
            DataSet ds = _db.GetDateSet(sql);
            return ds.Tables[0].Rows.Count;
        }
        //医生注册 先查有没有此手机号
        public int GetByPhone(string phone)
        {
            string sql = $"select * from doctor where userphone ='{phone}'";
            DataSet ds = _db.GetDateSet(sql);
            return ds.Tables[0].Rows.Count;
        }
        //若不存在此手机号则插入此用户数据  带密码
        public int InsertDoctorPhone(string phone, string password)
        {
            string sql = $"insert into doctor (userphone,userpassword) values('{phone}','{password}')";
            return _db.ExecuteNonQuery(sql);
        }
        //注册第二步 完善资料
        public int UpdDoctorByPhone(InsertDoctor m)
        {
            string sql = $"update doctor set doctorname='{m.DoctorName}',doctorlv={m.DoctorLv},doctoridcard='{m.DoctorIdCard}',doctorhospital='{m.DoctorHospital}',doctoridcardimg='{m.DoctorIdCardImg}',doctorzgzs='{m.DoctorZgzs}',doctoryyzs='{m.DoctorYyzs}' where userphone='{m.DoctorPhone}'";
            return _db.ExecuteNonQuery(sql);
        }
        //获取医院列表
        public List<Hospital> GetHospital()
        {
            string sql = $"select * from hospital";
            List<Hospital> list = _db.TableToList<Hospital>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        //获取医师等级列表
        public List<DoctorLv> GetDoctorLv()
        {
            string sql = $"select * from doctorlv";
            List<DoctorLv> list = _db.TableToList<DoctorLv>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        //诊断管理显示
        public List<Diagnose> GetDiagnose()
        {
            string sql = $"select ROW_NUMBER() over(order by InquiryDate) as i,InquiryDate, sum(InquiryPrice) as Price,count(*) as PatientNum from Inquiry group by InquiryDate";
            List<Diagnose> list = _db.TableToList<Diagnose>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        //直播列表
        public List<Live> GetLives(string liveName)
        {
            string sql = "select ROW_NUMBER() over(order by LiveId) i,* from live where 1=1";
            if (!string.IsNullOrEmpty(liveName))
            {
                sql = sql + $" and livetitle like '%{liveName}%'";
            }
            List<Live> list = _db.TableToList<Live>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        //获取诊断台各个字段数据
        public List<GetInquiry> GetInquiry()
        {
            string sql = "select  ROW_NUMBER() over(order by a.patientname) as i,a.patientcode,c.InquiryComment,c.InquiryState,c.InquiryDate,c.InquiryPrice,a.PatientName,a.patientPhone,c.InquiryMessage,a.PatientAge,c.InquiryRemark,a.PatientSex,a.createdate,a.PatientHeight,a.PatientWeight,b.Kidney,b.Marriage,b.Bith,b.DiseasesHistory,b.Liver,d.Diagnose from Patient a join HealthFile b on a.PatientCode =b.PatientCode join Inquiry c on a.PatientCode = c.PatientCode join Report d on a.PatientCode = d.PatientCode";
            List<GetInquiry> list = _db.TableToList<GetInquiry>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        //获取个人档案 patient join health
        public List<GetHealth> GetHealth(string patientcode)
        {
            string sql = $"select HealthFileId,Kidney,Marriage,Bith,DiseasesHistory,Liver,a.* from Patient a join HealthFile b on a.PatientCode=b.PatientCode where a.patientcode='{patientcode}'";
            List<GetHealth> list = _db.TableToList<GetHealth>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        //订单流水列表
        public List<Orders_Doctor> GetOrders_Doctor(string starttime, string endtime)
        {
            string sql = $"select * from doctororders where OrderCreateTime between '{starttime}' and' {endtime}'";
            List<Orders_Doctor> list = _db.TableToList<Orders_Doctor>(_db.GetDateSet(sql).Tables[0]);
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    list[0].ShouRu += item.OrderPrice;
                }
            }
            return list;
        }
        //获取诊断列表
        public List<GetInquiryByDate> GetInquiryByDate(string date="")
        {
            string sql = $"select ROW_NUMBER() over(order by a.patientcode) i,a.PatientCode,a.InquiryDate,a.Diagnose,InquiryPrice,PatientName,InquiryRemark,PatientAge,a.InquiryMessage,a.InquiryComment from inquiry a join Patient b on a.PatientCode = b.PatientCode where inquirydate = '{date}'";
            List<GetInquiryByDate> list = _db.TableToList<GetInquiryByDate>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        //根据哪一天查询评价
        public List<GetCommentByDate> GetCommentByDate(string date = "")
        {
            string sql = $" select ROW_NUMBER() over(order by a.inquiryid) i,a.InquiryDate,b.PatientName,a.InquiryRemark,a.InquiryComment,starcomment from Inquiry a join Patient b on a.PatientCode = b.PatientCode where InquiryDate = '{date}'";
            List<GetCommentByDate> list = _db.TableToList<GetCommentByDate>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        //查余额  医生
        public List<Wallet_Doctor> GetWalletByDoctorCode(string DoctorCode="") 
        {
            string sql = $"select * from wallet b left join doctor a  on a.doctorcode=b.doctorcode where a.doctorcode='{DoctorCode}'";
            List<Wallet_Doctor> list = _db.TableToList<Wallet_Doctor>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        #endregion




        #region 管理员模块
        //患者管理列表
        public List<Admin_Patient> GetAdminPatient()
        {
            string sql = $"select a.PatientCode,b.PatientName,b.PatientAge,b.PatientPhone,sum(a.InquiryPrice) PriceSum,count(*) InquiryNum from Inquiry a join patient b on a.PatientCode = b.PatientCode group by a.PatientCode,b.PatientName,b.PatientAge,b.PatientPhone";
            return _db.TableToList<Admin_Patient>(_db.GetDateSet(sql).Tables[0]);
        }
        //获取管理员端直播列表
        public List<GetLives_Admin> GetLives_Admin()
        {
            string sql = "select LiveCode,b.DoctorCode,LiveCreateTime,LiveTitle,LiveImg,LivePeopleNum,DoctorName,HospitalName,OName from live a join doctor b on a.DoctorCode = b.DoctorCode join Hospital c on b.DoctorHospital = c.HospitalCode join Office d on b.DoctorOffice = d.Id";
            return _db.TableToList<GetLives_Admin>(_db.GetDateSet(sql).Tables[0]);
        }
        public int Login(Admin1 m)
        {
            string sql = $"select * from admin where adminname='{m.AdminName}' and adminpassword='{m.AdminPassWord}'";
            DataSet ds = _db.GetDateSet(sql);
            int h = ds.Tables[0].Rows.Count;
            return h;
        }
        public List<Patient1> GetShow() //患者显示
        {
            string sql = "select a.PatientCode,b.PatientId,b.PatientName,b.PatientAge,b.PatientPhone,sum(a.InquiryPrice) PriceSum,count(*) InquiryNum from Inquiry a join patient b on a.PatientCode=b.PatientCode group by a.PatientCode,b.PatientId,b.PatientName,b.PatientAge,b.PatientPhone";
            DataSet dt = _db.GetDateSet(sql);
            List<Patient1> list = _db.TableToList<Patient1>(dt.Tables[0]);
            return list;
        }
        public List<Patient1> GetXians(int sid)//患者显示查看
        {
            string sql = $"select * from Patient a join  Doctor b on a.DoctorId=b.DoctorId join DoctorLv c on b.DoctorLv=c.DoctorLvId join Hospital d on b.DoctorHospital=d.HospitalCode join Inquiry e on a.patientcode=e.PatientCode   where PatientId=({sid})";
            DataSet dt = _db.GetDateSet(sql);
            List<Patient1> list = _db.TableToList<Patient1>(dt.Tables[0]);
            return list;
        }
        public List<Patient1> GetFanTian(int sid)
        {
            string sql = $"select *from HealthFile join Patient on HealthFile.PatientCode=Patient.PatientCode join Inquiry on Inquiry.PatientCode=Patient.PatientCode join Report on Report.PatientCode=Patient.PatientCode where Patient.PatientId=({sid})";
            DataSet dt = _db.GetDateSet(sql);
            List<Patient1> list = _db.TableToList<Patient1>(dt.Tables[0]);
            return list;
        }
        public List<Doctor> GetDoctor(string name = "")
        {
            string sql = $"select * from Doctor where 1=1";
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql + $"and UserPhone='{name}'";
            }
            else
            {
                sql = sql + $"  and   DoctorName like '&{name}&'";
            }

            DataSet dt = _db.GetDateSet(sql);
            List<Doctor> list = _db.TableToList<Doctor>(dt.Tables[0]);
            return list;

        }
        public List<Doctor> GetDoctor(int id = -1, string name = "", int tid = -1, int age = -1)
        {
            string sql = $"select * from Doctor join Hospital on Doctor.DoctorHospital=Hospital.HospitalCode join DoctorLv on Doctor.DoctorLv=DoctorLv.DoctorLvId  where 1=1";
            if (id != -1)
            {
                sql = sql + $" and State={id}";
            }
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql + $" and Hospital.HospitalName like '%{name}%'";
            }
            if (tid != -1)
            {
                sql = sql + $" and DoctorLv={tid}";
            }
            if (age != -1)
            {
                sql = sql + $" and DoctorYear={age}";
            }
            DataSet dt = _db.GetDateSet(sql);
            List<Doctor> list = _db.TableToList<Doctor>(dt.Tables[0]);
            return list;
        }
        public List<Patient1> GetPatient(string name = "")
        {
            string sql = $"select * from Patient where 1=1";
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql + $" and PatientPhone='{name}' or   PatientName like '%{name}%'";
            }
            DataSet dt = _db.GetDateSet(sql);
            List<Patient1> list = _db.TableToList<Patient1>(dt.Tables[0]);
            return list;
        }

        public List<GType> GTypes(string name, int id = -1)
        {
            string sql = $"select * from GType where 1=1";
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql + $" and TName like '%{name}%'";
            }
            if (id != -1)
            {
                sql = sql + $" and Tid={id}";
            }
            DataSet dt = _db.GetDateSet(sql);
            List<GType> list = _db.TableToList<GType>(dt.Tables[0]);
            return list;

        }
        public List<GType> GTypes(int id = -1)
        {
            string sql = $"select * from GType where Tid={id}";
            DataSet dt = _db.GetDateSet(sql);
            List<GType> list = _db.TableToList<GType>(dt.Tables[0]);
            return list;
        }

        public List<Patient1> PatientFt(int id)
        {
            string sql = $"select * from Patient where PatientId={id}";
            DataSet dt = _db.GetDateSet(sql);
            List<Patient1> list = _db.TableToList<Patient1>(dt.Tables[0]);
            return list;
        }
        public int PatientUpd(Patient1 s)
        {

            string sql = $"update Patient set PatientCode='{s.PatientCode}',PatientName='{s.PatientName}',PatientPhone='{s.PatientPhone}',PatientPassWord='{s.PatientPassWord}' where PatientId={s.PatientId}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public List<DoctorLv> GetDoctorLv1()
        {
            string sql = $"select * from DoctorLv where 1=1";
            DataSet dt = _db.GetDateSet(sql);
            List<DoctorLv> list = _db.TableToList<DoctorLv>(dt.Tables[0]);
            return list;
        }

        public int DoctorReason(Doctor s)
        {
            string sql = $"update Doctor set DoctorReason='{s.DoctorReason}',State={s.State} where DoctorId={s.DoctorId}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public List<Order> GetOrders(string name = "", int id = -1, int tid = -1, int nid = -1)
        {
            string sql = $" select * from AdmintOrders join Patient on AdmintOrders.PatientCode=Patient.PatientCode join AdmintOrderState on AdmintOrders.OrderState=AdmintOrderState.OrderStateId where 1=1";
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql + $" and Orders.OrderId={name}";
            }
            if (id != -1)
            {
                sql = sql + $" and Orders.OrderState={id}";
            }
            if (tid != -1)
            {
                sql = sql + $" and Orders.PaymentState={tid}";
            }
            if (nid != -1)
            {
                sql = sql + $" and Order.DeliverState={nid}";
            }
            DataSet dt = _db.GetDateSet(sql);
            List<Order> list = _db.TableToList<Order>(dt.Tables[0]);
            return list;
        }

        public List<Goods> GetGoods(string name = "", int id = -1, int tid = -1)
        {
            string sql = $"select * from Goods join GType on Goods.GoodType=GType.TId where 1=1";
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql + $" and Goods.GoodName like '%{name}%'";
            }
            if (id != -1)
            {
                sql = sql + $" and Goods.GoodType={id}";
            }
            if (tid != -1)
            {
                sql = sql + $" and Goods.GoodState={tid}";
            }
            DataSet dt = _db.GetDateSet(sql);
            List<Goods> list = _db.TableToList<Goods>(dt.Tables[0]);
            return list;
        }
        public int GoodShelves(int id)
        {
            string sql = $"update Goods set GoodState=1 where GoodId={id}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public int GoodShelf(int id)
        {
            string sql = $"update Goods set GoodState=0 where GoodId={id}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public List<Goods> GoodReverse(int id)
        {
            string sql = $"select * from Goods where GoodId={id}";
            DataSet dt = _db.GetDateSet(sql);
            List<Goods> list = _db.TableToList<Goods>(dt.Tables[0]);
            return list;
        }
        public int GoodUpd(Goods s)
        {
            string sql = $"update Goods set GoodName='{s.GoodName}',GoodImg='{s.GoodImg}',GoodPrice={s.GoodPrice},GoddsBrief='{s.GoddsBrief}',GoddsService='{s.GoddsService}' where GoodId={s.GoodId}";
            int list = _db.ExecuteNonQuery(sql);
            return list;
        }
        public int DoctorDel(int id)
        {
            string sql = $"delete Doctor where DoctorId={id}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public List<Doctor> DoctorFt(int id)
        {
            string sql = $"select * from Doctor join Hospital on Doctor.DoctorHospital=Hospital.HospitalCode join DoctorLv on Doctor.DoctorLv=DoctorLv.DoctorLvId  where  DoctorId={id}";
            DataSet dt = _db.GetDateSet(sql);
            List<Doctor> list = _db.TableToList<Doctor>(dt.Tables[0]);
            return list;
        }

        public int DoctorAdd(Doctor s)
        {
            string sql = $"insert into Doctor(DoctorCode,DoctorName,DoctorHospital,DoctorLv,UserPhone,UserPassword) values('{s.DoctorCode}','{s.DoctorName}',{s.DoctorHospital},{s.DoctorLv},'{s.UserPhone}','{s.UserPassword}')";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public int DoctorUpd(Doctor s)
        {

            string sql = $"update Doctor set DoctorCode='{s.DoctorCode}',DoctorName='{s.DoctorName}',DoctorHospital={s.DoctorHospital},DoctorLv={s.DoctorLv},UserPhone='{s.UserPhone}',UserPassword='{s.UserPassword}' where DoctorId={s.DoctorId}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        #region//方法
        public List<Seckill> list(string name = "", int id = -1)
        {
            string sql = $"select * from Seckill where 1=1 ";
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql + $" and SeckillName like '%{name}%'";
            }
            if (id != -1)
            {
                sql = sql + $" and seckillId={id}";
            }
            var ds = _db.GetDateSet(sql);
            var json = JsonConvert.SerializeObject(ds.Tables[0]);
            List<Seckill> list = JsonConvert.DeserializeObject<List<Seckill>>(json);
            return list;
        }
        public List<Goods> goods(int state = -1, int id = -1, string name = "")
        {
            string sql = $"select * from goods a join Gtype b on a.GoodType=b.TId where 1=1 ";
            if (id != -1)
            {
                sql = sql + $" and SeckillId={id}";
            }
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql + $" and GoodName like '%{name}%'";
            }
            if (state != -1)
            {
                sql = sql + $" and GoodState={state}";
            }
            var ds = _db.GetDateSet(sql);
            var json = JsonConvert.SerializeObject(ds.Tables[0]);
            List<Goods> list = JsonConvert.DeserializeObject<List<Goods>>(json);
            return list;
        }
        public int UpdateState(int goodseckill, int id)//小修改
        {
            string sql = $"update Goods set GoodState=GoodState-1,GoodSeckill={goodseckill} where GoodId={id}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public int SeckillUpdate(int id)
        {
            string sql = $"update seckill set seckillstate=seckillstate-1 where seckillid={id}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public int Delete(int id)//删除
        {
            string sql = $"delete from Goods where GoodId={id}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public List<Goods> GoodFt(int id)   //商品反填
        {
            string sql = $"select * from Goods where GoodId={id}";
            var ds = _db.GetDateSet(sql);
            var json = JsonConvert.SerializeObject(ds.Tables[0]);
            List<Goods> list = JsonConvert.DeserializeObject<List<Goods>>(json);
            return list;

        }
        public int GoodUpdate(Goods g)   //修改商品
        {
            string sql = $"update Goods set GoodCode='{g.GoodCode}',GoodName='{g.GoodName}',GoodImg='{g.GoodImg}',GoodPrice={g.GoodPrice},GoodSeckill={g.GoodSeckill} where GoodId={g.GoodId}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public int SeckillDel(int id)
        {
            string sql = $"delete from seckill where seckillid={id}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public int SeckillAdd(Seckill s)
        {
            string sql = $"insert into seckill(SeckillName,SeckillSatrt,SeckillEnd,SeckillState) values('{s.SeckillName}','{s.SeckillSatrt}','{s.SeckillEnd}',{(s.SeckillState == true ? 1 : 0)})";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        public int SeckillUpdate(Seckill s)
        {
            string sql = $"update seckill set SeckillName='{s.SeckillName}',SeckillSatrt='{s.SeckillSatrt}',SeckillEnd='{s.SeckillEnd}' where SeckillId={s.SeckillId}";
            int h = _db.ExecuteNonQuery(sql);
            return h;
        }
        #region//绑定
        //绑定医生等级
        public List<DoctorLv> BangDoctorLv()
        {
            string sql = $"select * from DoctorLv";
            var ds = _db.GetDateSet(sql);
            var json = JsonConvert.SerializeObject(ds.Tables[0]);
            List<DoctorLv> list = JsonConvert.DeserializeObject<List<DoctorLv>>(json);
            return list;
        }
        //绑定医生所属医院
        public List<Hospital> BangHospitalcs()
        {
            string sql = $"select * from Hospitalcs";
            var ds = _db.GetDateSet(sql);
            var json = JsonConvert.SerializeObject(ds.Tables[0]);
            List<Hospital> list = JsonConvert.DeserializeObject<List<Hospital>>(json);
            return list;
        }
        #endregion

        #region//方法
        public List<Doctor> List(string Yiyuan = "", string Office = "", int id = -1, string YiShi = "", string Patient = "", int patientid = -1)//显示医生列表
        {
            string sql = $"select * from doctor a join DoctorLv b on a.DoctorLv = b.DoctorLvId join Hospital c on a.DoctorHospital = c.HospitalCode  join Office d on a.DoctorOffice = d.Id  join Patient e on  a.DoctorId = e.DoctorId where 1 = 1 ";
            if (!string.IsNullOrEmpty(Yiyuan))
            {
                sql = sql + $" and HospitalName like '%{Yiyuan}%'";
            }
            if (!string.IsNullOrEmpty(Office))
            {
                sql = sql + $" and OName like '%{Office}%'";
            }
            if (id != -1)
            {
                sql = sql + $" and DoctorLv={id}";
            }
            if (!string.IsNullOrEmpty(YiShi))
            {
                sql = sql + $" and DoctorName like '%{YiShi}%'";
            }
            if (!string.IsNullOrEmpty(Patient))
            {
                sql = sql + $" and PatientName like '%{Patient}%'";
            }
            if (id != -1)
            {
                sql = sql + $" and Patientid={patientid}";
            }
            var ds = _db.GetDateSet(sql);

            var json = JsonConvert.SerializeObject(ds.Tables[0]);
            List<Doctor> list = JsonConvert.DeserializeObject<List<Doctor>>(json);
            return list;
        }
        #endregion
        #endregion

        #endregion
        public class LoginTel
        {
            //接口测试地址（未上线前测试环境使用）
            private static String url = "http://www.etuocloud.com/gatetest.action";

            //应用 app_key
            private static String APP_KEY = "Fn140K4CFh2T94QSqyv3UR4AiX569klk";
            //应用 app_secret
            private static String APP_SECRET = "RIcNdAZFQqaj6lQEqKsWz6Y60jfY8TLPYPHAZAHi5fVe9frBucL0F4qIZVx6SXVG";

            //接口响应格式 json或xml
            private static String FORMAT = "json";
            /// <summary>
            /// 发生短信验证码
            /// </summary>
            /// <param name="to">手机号</param>
            /// <param name="template">短信模板ID</param>
            /// <param name="smscode">验证码</param>
            /// <returns></returns>
            public string sendSmsCode(string to, string smscode)
            {

                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("app_key", APP_KEY);
                parameters.Add("view", FORMAT);
                parameters.Add("method", "cn.etuo.cloud.api.sms.simple");
                parameters.Add("out_trade_no", "");//商户订单号，可空
                parameters.Add("to", to);
                parameters.Add("template", "1");
                parameters.Add("smscode", smscode);
                parameters.Add("sign", getsign(parameters));
                return HttpClient.HttpPost(url, parameters);

            }
            /// <summary>
            /// 获取param签名
            /// </summary>
            /// <param name="sParams"></param>
            /// <returns></returns>
            private static string getsign(NameValueCollection parameters)
            {
                SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
                foreach (string key in parameters.Keys)
                {
                    sParams.Add(key, parameters[key]);
                }

                string sign = string.Empty;
                StringBuilder codedString = new StringBuilder();
                foreach (KeyValuePair<string, string> temp in sParams)
                {
                    if (temp.Value == "" || temp.Value == null || temp.Key.ToLower() == "sign")
                    {
                        continue;
                    }

                    if (codedString.Length > 0)
                    {
                        codedString.Append("&");
                    }
                    codedString.Append(temp.Key.Trim());
                    codedString.Append("=");
                    codedString.Append(temp.Value.Trim());
                }

                // 应用key
                codedString.Append(APP_SECRET);
                string signkey = codedString.ToString();
                sign = GetMD5(signkey, "utf-8");

                return sign;
            }

            //md5
            private static string GetMD5(string encypStr, string charset)
            {
                string retStr;
                MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

                //创建md5对象
                byte[] inputBye;
                byte[] outputBye;

                //使用XXX编码方式把字符串转化为字节数组．
                try
                {
                    inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
                }
                catch (Exception)
                {
                    inputBye = System.Text.Encoding.UTF8.GetBytes(encypStr);
                }
                outputBye = m5.ComputeHash(inputBye);

                retStr = System.BitConverter.ToString(outputBye);
                retStr = retStr.Replace("-", "").ToUpper();

                //  return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ConvertString, "MD5").ToLower(); ;

                return retStr;
            }



        }
        public class HttpClient
        {
            /// <summary>
            /// POST请求与获取结果  
            /// </summary>
            /// <param name="Url"></param>
            /// <param name="parameters"></param>
            /// <returns></returns>
            public static string HttpPost(string Url, NameValueCollection parameters)
            {
                return HttpPost(Url, toParaData(parameters));
            }



            //调用http接口,接口编码为utf-8
            private static string toParaData(NameValueCollection parameters)
            {

                //设置参数，并进行URL编码
                StringBuilder codedString = new StringBuilder();
                foreach (string key in parameters.Keys)
                {
                    // codedString.Append(HttpUtility.UrlEncode(key));
                    codedString.Append(key);
                    codedString.Append("=");
                    codedString.Append(HttpUtility.UrlEncode(parameters[key], System.Text.Encoding.UTF8));
                    codedString.Append("&");
                }
                string paraUrlCoded = codedString.Length == 0 ? string.Empty : codedString.ToString().Substring(0, codedString.Length - 1);


                return paraUrlCoded;
            }


            /// <summary>  
            /// POST请求与获取结果  
            /// </summary>  
            public static string HttpPost(string Url, string postDataStr)
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

                //request.ContentLength = postDataStr.Length;
                //StreamWriter writer = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8);
                // writer.Write(postDataStr);
                // writer.Flush();


                //将URL编码后的字符串转化为字节
                byte[] payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
                request.ContentLength = payload.Length;
                Stream writer = request.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();

                //获得响应流
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));

                string retString = reader.ReadToEnd();
                return retString;
            }



        }
    }
}
