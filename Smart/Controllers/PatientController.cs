using Microsoft.AspNetCore.Mvc;
using SmartMedical.BLL;
using SmartMedical.DAL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static SmartMedical.BLL.SmartMedicalBLL;
using SmartMedical.Model;

namespace SmartMedical.Controllers
{
    [ApiController]
    [Route("SmartMedical/Patient")]
    public class PatientController : Controller
    {
        SmartMedicalBLL _bll;
        LoginTel _logintel;
        DBHelper _db;
        public PatientController(SmartMedicalBLL bll, LoginTel logintel, DBHelper db)
        {
            _bll = bll;
            _logintel = logintel;
            _db = db;
        }
        [Route("login"),HttpGet]
        public IActionResult Login(string phone,string password) 
        {
            string sql = $"select * from Admin where AdminName='{phone}' and AdminPassWord='{password}'";
            DataSet ds = _db.GetDateSet(sql);
            List<Admin> list = _db.TableToList<Admin>(ds.Tables[0]);
            return Ok(new { msg =list.Count>0?"登录成功!":"账号或密码错误!",state=list.Count > 0 ?true:false });
        }
        [Route("zhuce"), HttpGet]
        public IActionResult ZhuCe(string phone) 
        {
            int h = 0;
            string sql = $"select * from patient where patientphone='{phone}'";
            DataSet ds = _db.GetDateSet(sql);
            if (ds.Tables[0].Rows.Count>0)
            {
                h = 0;
            }
            else
            {
                sql = $"insert into patient(patientphone) values ('{phone}') ";
                h = _db.ExecuteNonQuery(sql);
            }
            return Ok(new { msg=h>0?"注册成功！":"手机号已存在!",state=h>0?true:false});
        }
        [Route("PhoneYZM"), HttpGet]
        public IActionResult PhoneYZM(string phone) 
        {
            Random r = new Random();
            string code = r.Next(1000,9999).ToString();
            var str = _logintel.sendSmsCode(phone,code);
            return Ok(new { data=str,code=code});
        }

        /// <summary>
        /// 患者管理显示、查询、分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sname"></param>
        /// <returns></returns>
        [Route("getshow1"),HttpGet]
        public IActionResult GetShow1(int page=1,int limit=10,string sname="")
        {
            List<Patient1> list = _bll.GetShow();
            if (!string.IsNullOrEmpty(sname))
            {
                list = list.Where(s => s.PatientName.Contains(sname)).ToList();
            }
            var nlist = list.Skip((page - 1) * limit).Take(limit);
            return Ok(new { code = 0, msg = "", count = list.Count, pages = Math.Ceiling(list.Count / 1.0 * limit), data = nlist });

        }
        /// <summary>
        /// 患者管理患者显示查看
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        [Route("GetXian"),HttpGet]
        public IActionResult GetXian(int page = 1, int limit = 10, int sid = -1)
        {
            List<Patient1> list = _bll.GetXians(sid);
            
            var nlist = list.Skip((page - 1) * limit).Take(limit);
            return Ok(new { code = 0, msg = "", count = list.Count, pages = Math.Ceiling(list.Count / 1.0 * limit), data = nlist });
        }

        /// <summary>
        /// 患者管理患者反填
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        [Route("geetfantian1"),HttpPost]
        public IActionResult GeetFanTian1(int page = 1, int limit = 10, int sid = -1)
        {
            List<Patient1> list = _bll.GetFanTian(sid);

            var nlist = list.Skip((page - 1) * limit).Take(limit);
            return Ok(new { code = 0, msg = "", count = list.Count, pages = Math.Ceiling(list.Count / 1.0 * limit), data = nlist });
        }
    }
}
