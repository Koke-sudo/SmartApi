using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
     public class Goods
    {
        public int GoodId { get; set; }
        public string GoodCode { get; set; }
        public string GoodName { get; set; }
        public string GoodImg { get; set; }
        public int GoodGoodPrice { get; set; }
        public int GoodSeckill { get; set; }
        public bool GoodState { get; set; }
        public int GoodTyppe { get; set; }
        public int Sekilld { get; set; }
        public int GoodPrice { get; set; }
        public string GoddsBrief { get; set; }
        public string GoddsService { get; set; }
    }
}
