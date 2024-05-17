using ET.Application.Models.TicketDtos;
using ET.Application.Models.TicketDtos.Response;
using ET.Core.Entities;
using ET.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Services
{
    public interface TicketService
    {
        public List<TicketResponseDto> Buy(TicketDto ticket, int num);
        public List<TicketResponseDto> Filter(Dictionary<string, string> searchParams, TicketPageDto ticketPage);
        public TicketResponseDto UpdateStatus(Guid id, TicketStatus status);
        public int GetTotalPages(Dictionary<string, string> searchParams);
    }
}
