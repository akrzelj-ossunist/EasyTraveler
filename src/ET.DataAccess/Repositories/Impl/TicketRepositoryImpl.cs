using ET.Core.Entities;
using ET.Core.Enums;
using ET.DataAccess.Models;
using ET.DataAccess.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ET.DataAccess.Repositories.Impl
{
    public class TicketRepositoryImpl : TicketRepository
    {
        private readonly DatabaseContext _context;

        public TicketRepositoryImpl(DatabaseContext context)
        {
            _context = context;
        }

        public Ticket Buy(Ticket ticket)
        {
            if (ticket == null) throw new ArgumentNullException("Sent ticket argument cannot be null!");

            _context.Ticket.Add(ticket);

            var route = _context.Route.FirstOrDefault(r  => r.Id == ticket.Route.Id);
            route.CurrentReservations += 1; 
            _context.Route.Update(route);

            _context.SaveChanges();

            return ticket;
        }
        public List<Ticket> Filter(TicketFilters ticketFilters)
        {
            DateTime startDateParsed = !string.IsNullOrEmpty(ticketFilters.StartDate) ? DateTime.Parse(ticketFilters.StartDate).ToUniversalTime().AddDays(1) : DateTime.MinValue;
            DateTime boughtDateParsed = !string.IsNullOrEmpty(ticketFilters.BoughtDate) ? DateTime.Parse(ticketFilters.BoughtDate).ToUniversalTime().AddDays(1) : DateTime.MinValue;
            ticketFilters.Status = Enum.TryParse(ticketFilters.Status, out TicketStatus status) ? status.ToString() : "";


            var query = from ticket in _context.Ticket.Include(t => t.Route).Include(u => u.User)
                        where (ticket.User.Id.ToString().Equals(ticketFilters.UserId))
                        && (string.IsNullOrEmpty(ticketFilters.Price) || ticket.Price.ToString().Equals(ticketFilters.Price))
                        && (string.IsNullOrEmpty(ticketFilters.StartLocation) || ticket.Route.StartLocation.ToLower().Contains(ticketFilters.StartLocation.ToLower()) && ticketFilters.StartLocation.Length > 2)
                        && (string.IsNullOrEmpty(ticketFilters.EndLocation) || ticket.Route.EndLocation.ToLower().Contains(ticketFilters.EndLocation.ToLower()) && ticketFilters.EndLocation.Length > 2)
                        && (string.IsNullOrEmpty(ticketFilters.StartDate) || startDateParsed.Date == ticket.Route.StartDate.Date)
                        && (string.IsNullOrEmpty(ticketFilters.BoughtDate) || boughtDateParsed.Date == ticket.BoughtDate.Date)
                        && (string.IsNullOrEmpty(ticketFilters.Status) || ticket.Status.Equals(status))
                        select ticket;

            var result = SortList(ticketFilters.SortBy, query).Skip(ticketFilters.Page * ticketFilters.Size).Take(ticketFilters.Size).ToList();
            Console.WriteLine($"FilterByParams: Found {result.Count} buses matching the criteria.");

            return result;
        }

        public int GetTotalPages(TicketFilters ticketFilters)
        {
            DateTime startDateParsed = !string.IsNullOrEmpty(ticketFilters.StartDate) ? DateTime.Parse(ticketFilters.StartDate).ToUniversalTime().AddDays(1) : DateTime.MinValue;
            DateTime boughtDateParsed = !string.IsNullOrEmpty(ticketFilters.BoughtDate) ? DateTime.Parse(ticketFilters.BoughtDate).ToUniversalTime().AddDays(1) : DateTime.MinValue;
            ticketFilters.Status = Enum.TryParse(ticketFilters.Status, out TicketStatus status) ? status.ToString() : "";


            var query = from ticket in _context.Ticket.Include(r => r.Route).Include(u => u.User)
                        where (ticket.User.Id.ToString().Equals(ticketFilters.UserId))
                        && (string.IsNullOrEmpty(ticketFilters.Price) || ticket.Price.ToString().Equals(ticketFilters.Price))
                        && (string.IsNullOrEmpty(ticketFilters.StartLocation) || ticket.Route.StartLocation.ToLower().Contains(ticketFilters.StartLocation.ToLower()) && ticketFilters.StartLocation.Length > 2)
                        && (string.IsNullOrEmpty(ticketFilters.EndLocation) || ticket.Route.EndLocation.ToLower().Contains(ticketFilters.EndLocation.ToLower()) && ticketFilters.EndLocation.Length > 2)
                        && (string.IsNullOrEmpty(ticketFilters.StartDate) || startDateParsed.Date == ticket.Route.StartDate.Date)
                        && (string.IsNullOrEmpty(ticketFilters.BoughtDate) || boughtDateParsed.Date == ticket.BoughtDate.Date)
                        && (string.IsNullOrEmpty(ticketFilters.Status) || ticket.Status.Equals(status))
                        select ticket;

            return (int)Math.Ceiling((double)query.Count() / ticketFilters.Size);
        }

        public Ticket Update(Ticket ticket)
        {
            if (ticket == null) throw new ArgumentNullException("Sent ticket argument cannot be null!");

            _context.Ticket.Update(ticket);
            _context.SaveChanges();

            return ticket;
        }

        private static IQueryable<Ticket> SortList(string sortBy, IQueryable<Ticket> query)
        {
            switch (sortBy)
            {
                case "boughtDate":
                    query = query.OrderBy(ticket => ticket.BoughtDate);
                    break;
                case "startDate":
                    query = query.OrderBy(ticket => ticket.Route.StartDate);
                    break;
                case "price":
                    query = query.OrderBy(ticket => ticket.Price);
                    break;
                default:
                    query = query.OrderBy(ticket => ticket.Id);
                    break;
            }
            return query;
        }

        public Ticket FindById(Guid id)
        {
            return _context.Ticket.FirstOrDefault(ticket => ticket.Id == id);
        }
    }
}
