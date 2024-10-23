using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Talabat_Core.Order_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "pending")]
        pending,
        [EnumMember(Value = "PaymentSucceded")]
        PaymentSucceded, 
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed,
    }
}
