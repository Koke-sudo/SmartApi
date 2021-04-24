using Microsoft.AspNetCore.Mvc;
using SmartMedical.BLL;
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
    }
}
