using Microsoft.AspNetCore.Mvc;
using SmartMedical.BLL;
using SmartMedical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMedical.Controllers
{
    [ApiController]
    [Route("SmartMedical/Examine")]
    public class ExamineController : Controller
    {
        SmartMedicalBLL _bll;
        public ExamineController(SmartMedicalBLL bll)
        {
            _bll = bll;
        }


        /// <summary>
        /// 审核管理的医师显示、查询
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        [Route("getdoctord"),HttpGet]
        public IActionResult GetDoctorD(int pageindex = 1, int id=-1,string name="",int age=-1,int tid=-1)
        {
            List<Doctor> list = _bll.GetDoctor(id,name,tid,age);
            int pagesize = 5;

            var page = new PagesDto(pageindex, pagesize, list.Count);
            var _list = list.Skip((pageindex - 1) * pagesize).Take(pagesize);
            return Ok(new { data = _list, pages = page });
        }
        /// <summary>
        ///  医师管理所有反填数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("reverse"),HttpPost]
        public IActionResult Reverse(int id)
        {
            List<Doctor> list = _bll.DoctorFt(id);
            return Ok(new { data = list});
        }
        /// <summary>
        ///  医师管理审核通过修改数据，添加不通过审批
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("doctorreason"), HttpPost]
        public IActionResult DoctorReason(Doctor s)
        {
            int h = _bll.DoctorReason(s);
            return Ok(h);
        }


























        //public string GetNumber(int MerchantID)''''''''''''''[.
        // {
        //生成订单号
        //订单号生成原则：年（4位）+月（2位）+日（2位）+时（2位）+分（2位）+秒（2位）+商家编号（5位，不够左补0）+5位随机数，2018 10 10 21 30 2 1      00001 43261

        //商家编号（5位，不够左补0）
        // string merchant = MerchantID.ToString();
        //  merchant = merchant.PadLeft(5, '0');     // 共5位，之前用0补齐

        //string num = GetRandomString(5);//自动生成一个5位随机数

        // string ordernum = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") +
        // DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss") +
        // merchant + num;

        //return ordernum;
        // }
    }
}
