using ET.Application.Models.BusDtos.Response;
using ET.Application.Models.BusDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET.Application.Models.RouteDtos.Response;
using ET.Application.Models.RouteDtos;

namespace ET.Application.Services
{
    public interface RouteService
    {
        public RouteResponseDto Create(RouteDto routeDto);
        public RouteResponseDto Update(Guid id, RouteDto routeDto);
        public bool Delete(Guid id);
        public RouteResponseDto GetById(Guid id);
        public int GetTotal(Dictionary<string, string> searchParams);
        public List<RouteResponseDto> Filter(RoutePageDto routePageDto, Dictionary<string, string> searchParams);
    }
}
