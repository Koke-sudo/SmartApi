using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartMedical.Model;
using SmartMedical.BLL;


namespace SmartMedical.Controllers
{
    [ApiController]
    [Route("SmartMedical/HosPital")]
    public class HosPitalController : Controller
    {
        SmartMedicalBLL _bll;
        public HosPitalController(SmartMedicalBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// 账号管理的医师账号显示、查询
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("getdoctor"),HttpGet]
        public IActionResult GetDoctor(int pageindex=1,string name="")
        {
            List<Doctor> list = _bll.GetDoctor(name);
            int pagesize = 2;

            var page = new PagesDto(pageindex, pagesize, list.Count);
            var _list = list.Skip((pageindex - 1) * pagesize).Take(pagesize);
            return Ok(new { data = _list,pages=page });
        }


        /// <summary>
        /// 账号管理的医师反填
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("reverse"), HttpPost]
        public IActionResult Reverse(int id)
        {
            List<Doctor> list = _bll.DoctorFt(id);
            return Ok(new { data = list });
        }
        /// <summary>
        /// 账号管理的医师添加和反填
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("amodify"), HttpPost]
        public IActionResult Amodify(Doctor s)
        {
            int h = 0;
            if (s.DoctorId<=0)
            {
                 h = _bll.DoctorAdd(s);
            }
            else
            {
                h = _bll.DoctorUpd(s);
            }
            return Ok(new { count=h});
        }
        /// <summary>
        /// 账号管理医师的删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("del"),HttpPost]
        public IActionResult Del(int id)
        {
            int h = _bll.DoctorDel(id);
            return Ok(h);
        }
    }
}
