using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model.Join
{
    //查询哪一天的素有评价
    public class GetCommentByDate
    {
        public int i { get; set; }
        public DateTime InquiryDate { get; set; }
        public string PatientName { get; set; }
        public string InquiryRemark { get; set; }
        public string InquiryComment { get; set; }
        public int starcomment { get; set; }
    }
}
