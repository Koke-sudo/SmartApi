using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMedical.Model
{
     public class Order
    {
        public int Id				 { get; set; }
        public string OrderId			 { get; set; }
        public string PatientCode		 { get; set; }
        public double OrderTotal		 { get; set; }
        public double OrderActually	 { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public int OrderState      { get; set; }
        public bool PaymentState    { get; set; }
        public bool DeliverState    { get; set; }
        public bool ReceivingState  { get; set; }
        public string OrderStateName { get; set; }
    }
}
