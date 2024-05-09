using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Core.Entities
{
    public class Bus
    {
        public Guid Id { get; set; }
        public string Seats { get; set; }
        public Company Company { get; set; }
        public Route Route { get; set; }
    }
}
