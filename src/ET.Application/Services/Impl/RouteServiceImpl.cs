using ET.Application.Exceptions;
using ET.Application.Mappers;
using ET.Application.Models;
using ET.Application.Models.BusDtos;
using ET.Application.Models.BusDtos.Response;
using ET.Application.Models.RouteDtos;
using ET.Application.Models.RouteDtos.Response;
using ET.Application.Utilities;
using ET.Core.Entities;
using ET.Core.Enums;
using ET.DataAccess.Models;
using ET.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private readonly BusRepository _busRepository;
        public required AuthenticatedDto AuthenticatedDto { get; set; }

        public RouteServiceImpl(RouteRepository routeRepository, AuthenticateUser authenticateUser, RouteMapper routeMapper, BusRepository busRepository)
        {
            _routeRepository = routeRepository;
            _authenticateUser = authenticateUser;
            _routeMapper = routeMapper;
            _busRepository = busRepository;
        }

        public RouteResponseDto Create(RouteDto routeDto)
        {
            if (routeDto == null) throw new InvalidArgumentsException("Sent route create data cannot be null!");

            if (routeDto.StartLocation == routeDto.EndLocation) throw new Exception("Start location cannot be same as end location!");
            if (routeDto.StartDate >= routeDto.EndDate) throw new Exception("Start date cannot be before end date!");

            var mappedData = _routeMapper.RouteDtoToRoute(routeDto);
            mappedData.Bus = _busRepository.FindById(routeDto.BusId);
            var newData = _routeRepository.Save(mappedData);

            return _routeMapper.RouteToRouteDto(newData);
        }

        public RouteResponseDto Cancel(Guid id)
        {
            var route = _routeRepository.FindById(id);
            route.Status = RouteStatus.Canceled;    

            return _routeMapper.RouteToRouteDto(_routeRepository.Update(route));
        }

        public List<RouteResponseDto> Filter(RoutePageDto routePageDto, Dictionary<string, string> searchParams)
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            _routeRepository.UpdateStatusBasedOnDate();

            RouteFilters routeFilters = new RouteFilters
            {
                CompanyId = AuthenticatedDto.Role != UserRole.Company ? "" : AuthenticatedDto.Id.ToString(),
                StartLocation = searchParams.GetValueOrDefault("startLocation", ""),
                EndLocation = searchParams.GetValueOrDefault("endLocation", ""),
                StartDate = searchParams.GetValueOrDefault("startDate", ""),
                Price = searchParams.GetValueOrDefault("price", ""),
                Company = searchParams.GetValueOrDefault("company", ""),
                People = searchParams.GetValueOrDefault("seatsNeeded", ""),
                Status = AuthenticatedDto.Role == UserRole.User ? "Confirmed" : "",
                SortBy = searchParams.TryGetValue("sortBy", out var value) ? value : "Id",
                Page = routePageDto.Page,
                Size = routePageDto.Size
            };

            var routes = _routeRepository.FilterByParams(routeFilters);

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
            var validator = new ValidatePageableParams();
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            RouteFilters routeFilters = new RouteFilters
            {
                CompanyId = AuthenticatedDto.Role == UserRole.Company ? AuthenticatedDto.Id.ToString() : "",
                StartLocation = searchParams.GetValueOrDefault("startLocation", ""),
                EndLocation = searchParams.GetValueOrDefault("endLocation", ""),
                StartDate = searchParams.GetValueOrDefault("startDate", ""),
                Price = searchParams.GetValueOrDefault("price", ""),
                Bus = searchParams.GetValueOrDefault("bus", ""),
                Status = searchParams.GetValueOrDefault("status", ""),
                Size = validator.Validate(searchParams.GetValueOrDefault("size", "5"), 5)
            };

            return _routeRepository.GetTotalByParams(routeFilters);
        }

        public RouteResponseDto Update(Guid id, RouteDto routeDto)
        {
            if (routeDto == null) throw new InvalidArgumentsException("Sent route edit data cannot be null!");

            var route = _routeRepository.FindById(id);
            if (route == null) throw new NotFoundException("Route with sent id doesnt exist!");

            route.StartLocation = routeDto.StartLocation;
            route.EndLocation = routeDto.EndLocation;
            route.Price = routeDto.Price;
            route.StartDate = routeDto.StartDate;
            route.EndDate = routeDto.EndDate;

            var editedRoute = _routeRepository.Update(route);

            return _routeMapper.RouteToRouteDto(editedRoute);
        }

        public List<Bus> GetAvailableBuses(DateTime startDate, DateTime endDate, string starLocation)
        {
            AuthenticatedDto = _authenticateUser.CreateAuthentication();

            var companyId = AuthenticatedDto.Role != UserRole.Company ? "" : AuthenticatedDto.Id.ToString();

            if (startDate > endDate) throw new Exception("Start date cannot be after end date");

            return _routeRepository.GetAvailableBuses(startDate, endDate, companyId, starLocation);
        }

        public RouteResponseDto Confirm(Guid id)
        {
            var route = _routeRepository.FindById(id);
            route.Status = RouteStatus.Confirmed;

            return _routeMapper.RouteToRouteDto(_routeRepository.Update(route));
        }
    }
}
