using Microsoft.AspNetCore.Mvc;
using SmartMedical.BLL;
using SmartMedical.Model;
using SmartMedical.Model.Join;
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
        /// 医生端登录
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
        /// <param name="phone">手机号</param>
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
        /// 医生端注册  设置密码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [Route("zhuce"), HttpGet]
        public IActionResult ZhuCe(string phone,string password) 
        {

            return Ok();
        }
        /// <summary>
        /// 获取医院列表
        /// </summary>
        /// <returns></returns>
        [Route("gethospital"), HttpGet]
        public IActionResult GetHospital() 
        {
            List<Hospital> list = _bll.GetHospital();
            return Ok(new { data=list});
        }
        /// <summary>
        /// 获取医师等级
        /// </summary>
        /// <returns></returns>
        [Route("getdoctorlv"),HttpGet]
        public IActionResult GetDoctorLv() 
        {
            List<DoctorLv> list = _bll.GetDoctorLv();
            return Ok(new { data=list});
        }
        /// <summary>
        /// 获取医生端诊断管理
        /// </summary>
        /// <returns></returns>
        [Route("getdiagnose"),HttpGet]
        public IActionResult GetDiagnose() 
        {
            List<Diagnose> list = _bll.GetDiagnose();
            return Ok(new {data=list});
        }
        /// <summary>
        /// 获取直播列表
        /// </summary>
        /// <returns></returns>
        [Route("getlives"),HttpGet]
        public IActionResult GetLives() 
        {
            List<Live> list = _bll.GetLives();
            return Ok(new { data=list});
        }
        /// <summary>
        /// 获取接诊台列表各个字段
        /// </summary>
        /// <returns></returns>
        [Route("getinquiry"),HttpGet]
        public IActionResult GetInquiry() 
        {
            List<GetInquiry> list = _bll.GetInquiry();
            return Ok(new { data=list});
        }
    }
}
