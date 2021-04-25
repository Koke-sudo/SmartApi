using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model.Join
{
    //管理员端获取直播列表
    public class GetLives_Admin
    {
        public string LiveCode { get; set; }
        public string DoctorCode { get; set; }
        public DateTime LiveCreateTime { get; set; }
        public string LiveTitle { get; set; }
        public string LiveImg { get; set; }
        public int LivePeopleNum { get; set; }
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
        public string OName { get; set; }
    }
}
