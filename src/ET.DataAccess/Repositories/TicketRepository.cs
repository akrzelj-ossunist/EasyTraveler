using ET.Core.Entities;
using ET.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ET.DataAccess.Repositories
{
    public interface TicketRepository
    {
        public Ticket Buy(Ticket ticket);
        public List<Ticket> Filter(TicketFilters ticketFilters);
        public Ticket Update(Ticket ticket);
        public Ticket FindById(Guid id);
        public int GetTotalPages(TicketFilters ticketFilters);
    }
}
