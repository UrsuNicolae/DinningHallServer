using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinningHall.DTOs
{
    public class ReceiveOrderDto : OrderDto
    {
        public DateTime Received { get; set; }

        public DateTime PreparedIn { get; set; }
    }
}
