using ET.Core.Entities;
using ET.Core.Enums;

namespace ET.Application.Models.TicketDtos
{
    public class TicketDto
    {
        public int Price { get; set; }
        public DateTime BoughtDate { get; set; }
        public Guid User { get; set; }
        public Guid Route { get; set; }
    }
}
