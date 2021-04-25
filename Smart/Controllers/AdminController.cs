using Microsoft.AspNetCore.Mvc;
using SmartMedical.BLL;
using SmartMedical.Model;
using SmartMedical.Model.Join;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Controllers
{
    [ApiController]
    [Route("SmartMedical/Admin")]
    public class AdminController : Controller
    {
        SmartMedicalBLL _bll;
        public AdminController(SmartMedicalBLL bll)
        {
            _bll = bll;
        }
        /// <summary>
        /// 管理员端患者管理列表
        /// </summary>
        /// <returns></returns>
        [Route("getadminpatient"),HttpGet]
        public IActionResult GetAdminPatient() 
        {
            List<Admin_Patient> list = _bll.GetAdminPatient();
            return Ok(new { data=list});
        }
        /// <summary>
        /// 获取患者列表
        /// </summary>
        /// <returns></returns>
        [Route("getpatients"), HttpGet]
        public IActionResult GetPatients()
        {
            List<Patient> list = _bll.GetPatients();
            return Ok(list);
        }
        /// <summary>
        /// 获取管理员端直播列表
        /// </summary>
        /// <returns></returns>
        [Route("getlives_admin"),HttpGet]
        public IActionResult GetLives() 
        {
            List<GetLives_Admin> list = _bll.GetLives_Admin();
            return Ok(new { data=list});
        }
    }
}
