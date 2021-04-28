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
        [Route("doctorlogin"), HttpPost]
        /// <summary>
        /// 医生端登录
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IActionResult DoctorLogin(string phone, string password)
        {
            int h = _bll.DoctorLogin(phone, password);
            return Ok(new { msg = h > 0 ? "登录成功!" : "账号或密码错误!", state = h > 0 ? true : false });
        }
        /// <summary>
        /// 手机验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <returns></returns>
        [Route("phone"), HttpPost]
        public IActionResult Phone(string phone)
        {
            Random r = new Random();
            string code = r.Next(1000, 9999).ToString();
            var str = _logintel.sendSmsCode(phone, code);
            return Ok(new { data = str, code = code });
        }

        /// <summary>
        /// 医生端注册  设置密码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [Route("zhuce"), HttpGet]
        public IActionResult ZhuCe(string phone, string password)
        {
            string msg = "";
            bool state = false;
            if (_bll.GetByPhone(phone) > 0)
            {
                msg = "手机号已注册!";
            }
            else
            {
                int h = _bll.InsertDoctorPhone(phone, password);
                msg = h > 0 ? "注册成功,请完善资料!" : "注册失败!";
                state = h > 0 ? true : false;
            }
            return Ok(new { msg = msg, state = state });
        }
        /// <summary>
        /// 完善资料页面提交
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [Route("zhuce2"), HttpPost]
        public IActionResult Zhuce2(InsertDoctor m)
        {
            int h = _bll.UpdDoctorByPhone(m);
            return Ok(new { msg = h > 0 ? "完善成功!" : "失败!", state = h > 0 ? true : false });
        }

        /// <summary>
        /// 获取医院列表
        /// </summary>
        /// <returns></returns>
        [Route("gethospital"), HttpGet]
        public IActionResult GetHospital()
        {
            List<Hospital> list = _bll.GetHospital();
            return Ok(new { data = list });
        }
        /// <summary>
        /// 获取医师等级
        /// </summary>
        /// <returns></returns>
        [Route("getdoctorlv"), HttpGet]
        public IActionResult GetDoctorLv()
        {
            List<DoctorLv> list = _bll.GetDoctorLv();
            return Ok(new { data = list });
        }
        /// <summary>
        /// 获取医生端诊断管理
        /// </summary>
        /// <returns></returns>
        [Route("getdiagnose"), HttpGet]
        public IActionResult GetDiagnose()
        {
            List<Diagnose> list = _bll.GetDiagnose();
            return Ok(new { data = list });
        }
        /// <summary>
        /// 获取直播列表
        /// </summary>
        /// <returns></returns>
        [Route("getlives"), HttpGet]
        public IActionResult GetLives()
        {
            List<Live> list = _bll.GetLives();
            return Ok(new { data = list });
        }
        /// <summary>
        /// 获取接诊台列表各个字段
        /// </summary>
        /// <returns></returns>
        [Route("getinquiry"), HttpGet]
        public IActionResult GetInquiry()
        {
            List<GetInquiry> list = _bll.GetInquiry();
            return Ok(new { data = list });
        }
        /// <summary>
        /// 获取档案信息 patient join health
        /// </summary>
        /// <returns></returns>
        [Route("gethealth"), HttpGet]
        public IActionResult GetHealth(string patientcode = "")
        {

            List<GetHealth> list = _bll.GetHealth(patientcode);
            if (!string.IsNullOrEmpty(patientcode) && patientcode != "")
            {
                list = list.Where(s => s.PatientCode.Equals(patientcode)).ToList();
            }
            return Ok(new { data = list });
        }
        /// <summary>
        /// 医生端订单流水列表
        /// </summary>
        /// <returns></returns>
        [Route("getorders_doctor"),HttpGet]
        public IActionResult GetOrders_Doctor()
        {
            List<Orders_Doctor> list = _bll.GetOrders_Doctor();
            return Ok(new { data=list});
        }
        /// <summary>
        /// 医师管理的所有查询、显示
        /// </summary>
        /// <param name="YiYuan"></param>
        /// <param name="Office"></param>
        /// <param name="id"></param>
        /// <param name="YiShi"></param>
        /// <param name="Patient"></param>
        /// <param name="patientid"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [Route("initdate"), HttpGet]
        public IActionResult Initdate(string YiYuan = "", string Office = "", int id = -1, string YiShi = "", string Patient = "", int patientid = -1, int pageindex = 1, int pagesize = 3)
        {
            var list = _bll.List(YiYuan, Office, id, YiShi, Patient, patientid);
            int count = list.Count;
            int pagecount = (int)Math.Ceiling(count * 1.0 / pagesize);

            var _list = list.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return Ok(new
            {
                data = _list,
                pages = new
                {
                    pro = pageindex > 1 ? pageindex - 1 : 1,
                    next = pageindex < pagecount ? pageindex + 1 : pagecount,
                    last = pagecount
                }
            }); ;
        }
    }
}
