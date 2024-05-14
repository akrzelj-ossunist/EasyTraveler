using ET.Application.Mappers;
using ET.Application.Models.LocationDtos;
using ET.Application.Models.LocationDtos.Response;
using ET.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Services.Impl
{
    public class LocationServiceImpl : LocationServices
    {
        private readonly LocationMapper locationMapper;
        private readonly LocationRepository locationRepository;

        public LocationServiceImpl(LocationMapper locationMapper, LocationRepository locationRepository)
        {
            this.locationMapper = locationMapper;
            this.locationRepository = locationRepository;
        }

        public LocationResponseDto Create(LocationDto locationDto)
        {
            return locationMapper.LocationToLocationDto(locationRepository.Save(locationMapper.LocationDtoToLocation(locationDto)));
        }

        public bool Delete(Guid id)
        {
            var location = locationRepository.GetById(id);
            locationRepository.Delete(location);

            return true;
        }

        public List<LocationResponseDto> GetAll()
        {
            var locationList = locationRepository.GetAll();
            return locationList.Select(locationMapper.LocationToLocationDto).ToList();
        }

        public LocationResponseDto GetById(Guid id)
        {
            return locationMapper.LocationToLocationDto(locationRepository.GetById(id));
        }
    }
}
