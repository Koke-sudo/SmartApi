using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
    /// <summary>
    /// 直播表
    /// </summary>
    public class Live
    {
        public int i { get; set; }
        public int LiveId { get; set; }
        public string LiveImg { get; set; }
        public string LiveTitle { get; set; }
        public int LivePeopleNum { get; set; }
        public DateTime LiveStartTime { get; set; }
        public string LiveStartTimeStr { get { return this.LiveStartTime.ToString("yyyy-MM-dd"); } set { } }
        public DateTime LiveEndTime { get; set; }
        public string LiveEndTimeStr { get { return this.LiveEndTime.ToString("yyyy-MM-dd"); } set { } }
    }
}
