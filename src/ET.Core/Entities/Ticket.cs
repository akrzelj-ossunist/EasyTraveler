using ET.Core.Enums;

namespace ET.Core.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public DateTime BoughtDate { get; set; }
        public User User { get; set; }
        public Route Route { get; set; } 
        public TicketStatus Status { get; set; }
    }
}
