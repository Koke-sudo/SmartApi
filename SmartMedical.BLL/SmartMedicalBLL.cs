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


        //患者模块
        #region
        //患者登录
        public int Login(string phone, string password)
        {
            string sql = $"select * from patient where patientphone='{phone}' and patientpassword='{password}'";
            DataSet ds = _db.GetDateSet(sql);
            int h = ds.Tables[0].Rows.Count;
            return h;
        }
        //获取所有患者信息
        public List<Patient> GetPatients()
        {
            string sql = "select * from patient";
            DataSet ds = _db.GetDateSet(sql);
            List<Patient> list = _db.TableToList<Patient>(ds.Tables[0]);
            return list;
        }
        //验证患者手机号是否存在
        public DataSet ZhuCe(string phone)
        {
            string sql = $"select * from patient where patientphone='{phone}'";
            return _db.GetDateSet(sql);
        }

        //注册患者
        public int ZhuceIn(string phone,string password)
        {
            string sql = $"insert into patient(patientphone,patientpassword) values ('{phone}','{password}') ";
            return _db.ExecuteNonQuery(sql);
        }
        #endregion



        //医生模块
        #region
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
        public int InsertDoctorPhone(string phone,string password) 
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
            string sql = $"select InquiryDate, sum(InquiryPrice) as Price,count(*) as PatientNum from Inquiry group by InquiryDate";
            List<Diagnose> list = _db.TableToList<Diagnose>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        //直播列表
        public List<Live> GetLives()
        {
            string sql = "select * from live";
            List<Live> list = _db.TableToList<Live>(_db.GetDateSet(sql).Tables[0]);
            return list;
        }
        //获取诊断台各个字段数据
        public List<GetInquiry> GetInquiry()
        {
            string sql = "select  ROW_NUMBER() over(order by a.patientname) as i,a.patientcode,c.InquiryDate,c.InquiryPrice,a.PatientName,c.InquiryMessage,a.PatientAge,c.InquiryRemark,a.PatientSex,a.PatientHeight,a.PatientWeight,b.Kidney,b.Marriage,b.Bith,b.DiseasesHistory,b.Liver,d.Diagnose from Patient a join HealthFile b on a.PatientCode =b.PatientCode join Inquiry c on a.PatientCode = c.PatientCode join Report d on a.PatientCode = d.PatientCode";
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
        #endregion



        //管理员模块
        #region
        //患者管理列表
        public List<Admin_Patient> GetAdminPatient() 
        {
            string sql = $"select a.PatientCode,b.PatientName,b.PatientAge,b.PatientPhone,sum(a.InquiryPrice) PriceSum,count(*) InquiryNum from Inquiry a join patient b on a.PatientCode = b.PatientCode group by a.PatientCode,b.PatientName,b.PatientAge,b.PatientPhone";
            return _db.TableToList<Admin_Patient>(_db.GetDateSet(sql).Tables[0]);
        }
        public List<GetLives_Admin> GetLives_Admin() 
        {
            string sql = "select LiveCode,b.DoctorCode,LiveCreateTime,LiveTitle,LiveImg,LivePeopleNum,DoctorName,HospitalName,OName from live a join doctor b on a.DoctorCode = b.DoctorCode join Hospital c on b.DoctorHospital = c.HospitalCode join Office d on b.DoctorOffice = d.Id";
            return _db.TableToList<GetLives_Admin>(_db.GetDateSet(sql).Tables[0]);
        }
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
