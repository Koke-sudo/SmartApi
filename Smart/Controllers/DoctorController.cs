using Microsoft.AspNetCore.Mvc;
using SmartMedical.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmartMedical.BLL.SmartMedicalBLL;

namespace SmartApi.Controllers
{
    [ApiController]
    [Route("SmartMedical/Doctor")]
    public class DoctorController : Controller
    {
        SmartMedicalBLL _bll;
        LoginTel _logintel;
        public DoctorController(SmartMedicalBLL bll, LoginTel logintel)
        {
            _bll = bll;
            _logintel = logintel;
        }
        [Route("doctorlogin"),HttpPost]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IActionResult DoctorLogin(string phone,string password) 
        {
            int h = _bll.DoctorLogin(phone,password);
            return Ok(new { msg=h>0?"登录成功!":"账号或密码错误!",state=h>0?true:false});
        }
        /// <summary>
        /// 手机验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [Route("phone"),HttpPost]
        public IActionResult Phone(string phone) 
        {
            Random r = new Random();
            string code = r.Next(1000,9999).ToString();
            var str = _logintel.sendSmsCode(phone,code);
            return Ok(new { data=str,code=code});
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("zhuce"),HttpPost]
        public IActionResult ZhuCe() 
        {
            return Ok();
        }
    }
}
