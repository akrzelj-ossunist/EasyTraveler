using ET.Application.Exceptions;
using ET.Application.Mappers;
using ET.Application.Models;
using ET.Application.Models.BusDtos;
using ET.Application.Models.BusDtos.Response;
using ET.Application.Models.RouteDtos;
using ET.Application.Models.RouteDtos.Response;
using ET.Application.Utilities;
using ET.Core.Entities;
using ET.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Services.Impl
{
    public class RouteServiceImpl : RouteService
    {

        private readonly RouteRepository _routeRepository;
        private readonly AuthenticateUser _authenticateUser;
        private readonly RouteMapper _routeMapper;
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public RouteServiceImpl(RouteRepository routeRepository, AuthenticateUser authenticateUser, RouteMapper routeMapper)
        {
            _routeRepository = routeRepository;
            _authenticateUser = authenticateUser;
            _routeMapper = routeMapper;
        }

        public RouteResponseDto Create(RouteDto routeDto)
        {
            if (routeDto == null) throw new InvalidArgumentsException("Sent route create data cannot be null!");

            var newData = _routeRepository.Save(_routeMapper.RouteDtoToRoute(routeDto));

            return _routeMapper.RouteToRouteDto(newData);
        }

        public bool Delete(Guid id)
        {
            var route = _routeRepository.FindById(id);
            if (route == null) throw new NotFoundException("Route with sent id doesnt exist!");

            _routeRepository.Delete(route);

            return true;
        }

        public List<RouteResponseDto> Filter(RoutePageDto routePageDto, Dictionary<string, string> searchParams)
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            var companyId = AuthenticatedDto.Id.ToString();
            var startLocation = searchParams.GetValueOrDefault("startLocation", "");
            var endLocation = searchParams.GetValueOrDefault("endLocation", "");
            var startDate = searchParams.GetValueOrDefault("startDate", "");
            var price = searchParams.GetValueOrDefault("price", "");
            var bus = searchParams.GetValueOrDefault("bus", "");
            var status = searchParams.GetValueOrDefault("status", "");
            var sortByParam = searchParams.TryGetValue("sortBy", out var value) ? value : "Id";

            if (AuthenticatedDto.Role != Core.Enums.UserRole.Company)
                companyId = "";
            
            var routes = _routeRepository.FilterByParams(companyId, routePageDto.Page, routePageDto.Size, sortByParam, startLocation, endLocation, startDate, price, bus, status);
            return routes.Select(_routeMapper.RouteToRouteDto).ToList();
        }

        public RouteResponseDto GetById(Guid id)
        {
            var route = _routeRepository.FindById(id);

            if (route == null) throw new NotFoundException("Route with sent id doesnt exist!");

            return _routeMapper.RouteToRouteDto(route);
        }

        public int GetTotal(Dictionary<string, string> searchParams)
        {
            throw new NotImplementedException();
        }

        public RouteResponseDto Update(Guid id, RouteDto routeDto)
        {
            if (routeDto == null) throw new InvalidArgumentsException("Sent route edit data cannot be null!");

            var route = _routeRepository.FindById(id);
            if (route == null) throw new NotFoundException("Route with sent id doesnt exist!");

            var editedRoute = _routeRepository.Update(route);

            return _routeMapper.RouteToRouteDto(editedRoute);
        }
    }
}
