using Microsoft.AspNetCore.Mvc;
using SmartMedical.BLL;
using SmartMedical.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMedical.Controllers
{
    [ApiController]
    [Route("SmartMedical/SmartApi")]
    public class SmartApiController : Controller
    {
        //private readonly ICaptcha _captcha;

        SmartMedicalBLL _bll;
        public SmartApiController(SmartMedicalBLL bll)
        {
            _bll = bll;
        }
        //
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
         [Route("login"),HttpGet]
        public IActionResult Login(Admin1 m) 
        {
            int h = _bll.Login(m);
            return Ok(new { msg=h>0?"登陆成功!":"用户名或密码不正确!",state=h>0?true:false});
        }
        //验证码 图形   未完成
        //public IActionResult CreateValidCodeImage() 
        //{
        //    //var info = _captcha.Generate("2658");
        //    //var stream = new MemoryStream(info.CaptchaByteData);
        //    //return File(stream, "image/png");
        //}
    }
}
