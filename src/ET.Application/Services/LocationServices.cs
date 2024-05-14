using ET.Application.Models.LocationDtos;
using ET.Application.Models.LocationDtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Services
{
    public interface LocationServices
    {
        public LocationResponseDto Create(LocationDto locationDto);
        public bool Delete(Guid id);
        public LocationResponseDto GetById(Guid id);
        public List<LocationResponseDto> GetAll();
    }
}
