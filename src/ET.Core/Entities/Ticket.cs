using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Core.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public DateOnly BoughtDate { get; set; }
        public Bus Bus { get; set; }
        public User User { get; set; }
        public Route Route { get; set; } 

    }
}
