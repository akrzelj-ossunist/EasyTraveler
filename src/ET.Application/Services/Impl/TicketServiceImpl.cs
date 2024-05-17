using Azure;
using ET.Application.Exceptions;
using ET.Application.Mappers;
using ET.Application.Models;
using ET.Application.Models.RouteDtos;
using ET.Application.Models.RouteDtos.Response;
using ET.Application.Models.TicketDtos;
using ET.Application.Models.TicketDtos.Response;
using ET.Application.Utilities;
using ET.Core.Entities;
using ET.Core.Enums;
using ET.DataAccess.Models;
using ET.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Services.Impl
{
    public class TicketServiceImpl : TicketService
    {
        private readonly TicketRepository _ticketRepository;
        private readonly TicketMapper _ticketMapper;
        private readonly UserRepository _userRepository;
        private readonly RouteRepository _routeRepository;
        private readonly AuthenticateUser _authenticateUser;
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public TicketServiceImpl(TicketRepository ticketRepository, TicketMapper ticketMapper, UserRepository userRepository, RouteRepository routeRepository, AuthenticateUser authenticateUser)
        {
            _ticketRepository = ticketRepository;
            _ticketMapper = ticketMapper;
            _userRepository = userRepository;
            _routeRepository = routeRepository;
            _authenticateUser = authenticateUser;
        }

        public List<TicketResponseDto> Buy(TicketDto ticket, int ticketNum)
        {
            if (ticket == null) throw new InvalidArgumentsException("Sent ticket create data cannot be null!");

            var newTickets = new List<TicketResponseDto>();

            for (var i = 0; i < ticketNum; i++)
            {
                var mappedData = _ticketMapper.TicketDtoToTicket(ticket);
                mappedData.User = _userRepository.FindById(ticket.User);
                mappedData.Route = _routeRepository.FindById(ticket.Route);
                var newData = _ticketRepository.Buy(mappedData);
                newTickets.Add(_ticketMapper.TicketToTicketDto(newData));
            }

            return newTickets;
        }

        public List<TicketResponseDto> Filter(Dictionary<string, string> searchParams, TicketPageDto ticketPage)
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            TicketFilters ticketFilters = new TicketFilters
            {
                UserId = AuthenticatedDto.Role != UserRole.User ? "" : AuthenticatedDto.Id.ToString(),
                User = searchParams.GetValueOrDefault("user", ""),
                StartLocation = searchParams.GetValueOrDefault("startLocation", ""),
                EndLocation = searchParams.GetValueOrDefault("endLocation", ""),
                StartDate = searchParams.GetValueOrDefault("startDate", ""),
                BoughtDate = searchParams.GetValueOrDefault("boughtDate", ""),
                Price = searchParams.GetValueOrDefault("price", ""),
                Status = searchParams.GetValueOrDefault("status", ""),
                SortBy = searchParams.TryGetValue("sortBy", out var value) ? value : "Id",
                Page = ticketPage.Page,
                Size = ticketPage.Size
            };

            Console.WriteLine($"UserId: {ticketFilters.UserId}\n");
            Console.WriteLine($"User: {ticketFilters.User}\n");
            Console.WriteLine($"StartLocation: {ticketFilters.StartLocation}\n");
            Console.WriteLine($"EndLocation: {ticketFilters.EndLocation}\n");
            Console.WriteLine($"StartDate: {ticketFilters.StartDate}\n");
            Console.WriteLine($"BoughtDate: {ticketFilters.BoughtDate}\n");
            Console.WriteLine($"Price: {ticketFilters.Price}\n");
            Console.WriteLine($"Status: {ticketFilters.Status}\n");
            Console.WriteLine($"SortBy: {ticketFilters.SortBy}\n");
            Console.WriteLine($"Page: {ticketFilters.Page}\n");
            Console.WriteLine($"Size: {ticketFilters.Size}\n");

            var tickets = _ticketRepository.Filter(ticketFilters);

            return tickets.Select(_ticketMapper.TicketToTicketDto).ToList();
        }

        public int GetTotalPages(Dictionary<string, string> searchParams)
        {
            var validator = new ValidatePageableParams();
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            TicketFilters ticketFilters = new TicketFilters
            {
                UserId = AuthenticatedDto.Role != UserRole.User ? "" : AuthenticatedDto.Id.ToString(),
                User = searchParams.GetValueOrDefault("user", ""),
                StartLocation = searchParams.GetValueOrDefault("startLocation", ""),
                EndLocation = searchParams.GetValueOrDefault("endLocation", ""),
                StartDate = searchParams.GetValueOrDefault("startDate", ""),
                BoughtDate = searchParams.GetValueOrDefault("boughtDate", ""),
                Price = searchParams.GetValueOrDefault("price", ""),
                Status = searchParams.GetValueOrDefault("status", ""),
                SortBy = searchParams.TryGetValue("sortBy", out var value) ? value : "Id",
                Size = validator.Validate(searchParams.GetValueOrDefault("size", "5"), 5)
            };

            return _ticketRepository.GetTotalPages(ticketFilters);
        }

        public TicketResponseDto UpdateStatus(Guid id, TicketStatus status)
        {
            var ticket = _ticketRepository.FindById(id);
            ticket.Status = status;
            _ticketRepository.Update(ticket);
            return _ticketMapper.TicketToTicketDto(ticket);
        }
    }
}
