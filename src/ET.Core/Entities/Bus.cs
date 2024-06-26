﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Core.Entities
{
    public class Bus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
        public bool IsAvailable { get; set; }
        public string CurrentLocation { get; set; }
        public Company Company { get; set; }
    }
}
