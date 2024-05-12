using AutoMapper;
using ET.Core.Entities;
using ET.Application.Models.RouteDtos.Response;
using ET.Application.Models.RouteDtos;
using ET.Core.Enums;

namespace ET.Application.Mappers
{
    public class RouteMapper
    {
        private readonly IMapper _mapper;

        public RouteMapper()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RouteDto, Route>()
                   .ForMember(dest => dest.Status, opt => opt.MapFrom(src => RouteStatus.Pending))
                   .ForMember(dest => dest.CurrentReservations, opt => opt.MapFrom(src => 0))
                   .ForMember(dest => dest.Bus, opt => opt.MapFrom(src => new Bus { Id = src.BusId }));
                cfg.CreateMap<Route, RouteResponseDto>();
            });

            _mapper = config.CreateMapper();
        }

        public Route RouteDtoToRoute(RouteDto routeDto)
        {
            return _mapper.Map<Route>(routeDto);
        }

        public RouteResponseDto RouteToRouteDto(Route route)
        {
            return _mapper.Map<RouteResponseDto>(route);
        }
    }
}

