﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public sealed class DiningHall
    {
        public IEnumerable<Table> Tables  { get; set; }
    }
}
