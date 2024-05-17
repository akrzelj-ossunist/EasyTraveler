using AutoMapper;
using ET.Application.Models.RouteDtos.Response;
using ET.Application.Models.RouteDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET.Application.Models.TicketDtos;
using ET.Core.Entities;
using ET.Application.Models.TicketDtos.Response;
using ET.Core.Enums;

namespace ET.Application.Mappers
{
    public class TicketMapper
    {
        private readonly IMapper _mapper;

        public TicketMapper()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TicketDto, Ticket>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => TicketStatus.Valid))
                    .ForMember(dest => dest.BoughtDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                    .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User { Id = src.User }))
                    .ForMember(dest => dest.Route, opt => opt.MapFrom(src => new Route { Id = src.Route }));
                cfg.CreateMap<Ticket, TicketResponseDto>();
            });

            _mapper = config.CreateMapper();
        }

        public Ticket TicketDtoToTicket(TicketDto ticketDto)
        {
            return _mapper.Map<Ticket>(ticketDto);
        }

        public TicketResponseDto TicketToTicketDto(Ticket ticket)
        {
            return _mapper.Map<TicketResponseDto>(ticket);
        }
    }
}
