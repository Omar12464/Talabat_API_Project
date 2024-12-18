﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat_Core.Models;

namespace Talabat_Core.Order_Aggregate
{
    public class DeliveryMethod:ModelBase
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string description, string deliveryTime, decimal cost)
        {
            ShortName = shortName;
            Description = description;
            DeliveryTime = deliveryTime;
            Cost = cost;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string  DeliveryTime { get; set; }
        public decimal Cost { get; set; }
        //public Order order { get; set; }
    }
}
