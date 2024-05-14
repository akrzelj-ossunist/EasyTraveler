using AutoMapper;
using ET.Application.Models.UserDtos.Response;
using ET.Application.Models.UserDtos;
using ET.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET.Application.Models.LocationDtos;
using ET.Application.Models.LocationDtos.Response;

namespace ET.Application.Mappers
{
    public class LocationMapper
    {
        private readonly IMapper _mapper;

        public LocationMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LocationDto, Location>();
                cfg.CreateMap<Location, LocationResponseDto>();
            });

            _mapper = config.CreateMapper();
        }

        public Location LocationDtoToLocation(LocationDto locationDto)
        {
            return _mapper.Map<Location>(locationDto);
        }

        public LocationResponseDto LocationToLocationDto(Location location)
        {
            return _mapper.Map<LocationResponseDto>(location);
        }
    }
}
