using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
     public class Seckill
    {
        public int SeckillId { get; set; }
        public string SeckillName { get; set; }
        public bool SeckillState { get; set; }
        public DateTime SeckillStart { get; set; }
        public DateTime SeckillEnd { get; set; }
        public string SeckillImg { get; set; }
        public object SeckillSatrt { get; set; }
    }
}
