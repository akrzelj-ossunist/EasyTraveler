using ET.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Core.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public DateTime BoughtDate { get; set; }
        public User User { get; set; }
        public Route Route { get; set; } 
        public TicketStatus status { get; set; }
    }
}
