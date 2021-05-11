using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
    //医生端订单流水
    public class Orders_Doctor
    {
        public int OrderId { get; set; }
        public DateTime OrderCreateTime { get; set; }
        public string OrderCode { get; set; }
        public int ShouRu { get; set; } = 0;
        public bool OrderType { get; set; }
        public int OrderPrice { get; set; }
    }
}
