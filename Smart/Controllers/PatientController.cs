using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SmartMedical.BLL;
using SmartMedical.DAL;
using SmartMedical.Model;
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

namespace SmartMedical.Controllers
{
    [ApiController]
    [Route("SmartMedical/Patient")]
    public class PatientController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        SmartMedicalBLL _bll;
        LoginTel _logintel;
        public PatientController(SmartMedicalBLL bll, LoginTel logintel)
        {
            _bll = bll;
            _logintel = logintel;
        }

        /// <summary>
        /// 患者登录
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("login"),HttpPost]
        public IActionResult Login(string phone,string password)
        {
            int h = _bll.Login(phone,password);
            return Ok(new { msg =h>0?"登录成功!":"账号或密码错误!",state=h > 0 ?true:false });
        }

        /// <summary>
        /// 患者注册  手机号注册
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Route("zhuce"),HttpPost]
        public IActionResult Zhuce(string phone) 
        {
            int h = 0;
            
            DataSet ds = _bll.ZhuCe(phone);
            if (ds.Tables[0].Rows.Count>0)
            {
                h = 0;
            }
            else
            {
                h = _bll.ZhuceIn(phone);
            }
            return Ok(new { msg=h>0?"注册成功！":"手机号已存在!",state=h>0?true:false});
        }

        /// <summary>
        /// 患者验证码登录
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Route("phoneyzm"),HttpPost]
        public IActionResult PhoneYZM(string phone) 
        {
            Random r = new Random();
            string code = r.Next(1000,9999).ToString();
            var str = _logintel.sendSmsCode(phone,code);
            return Ok(new { data=str,code=code});
        }
        /// <summary>
        /// 获取患者列表
        /// </summary>
        /// <returns></returns>
        [Route("getpatients"),HttpGet]
        public IActionResult GetPatients() 
        {
            List<Patient> list = _bll.GetPatients();
            return Ok(list);
        }
    }
}
