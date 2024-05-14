using ET.Application.Models.RouteDtos.Response;
using ET.Application.Models.RouteDtos;
using ET.Core.Entities;

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

        /* Takes start and end date and cheks if there is already reservation within these dates, also takes start location and check if his current location is start location */
        public List<Bus> GetAvailableBuses(DateTime startDate, DateTime endDate, string starLocation);
    }
}
