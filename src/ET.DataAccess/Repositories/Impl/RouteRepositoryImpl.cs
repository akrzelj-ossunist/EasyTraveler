using ET.Core.Entities;
using ET.DataAccess.Persistence;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ET.DataAccess.Repositories.Impl
{
    public class RouteRepositoryImpl : RouteRepository
    {
        private readonly DatabaseContext _context;

        public RouteRepositoryImpl(DatabaseContext context)
        {
            _context = context;
        }

        public bool Delete(Route route)
        {
            _context.Route.Remove(route);
            _context.SaveChanges();

            return true;
        }

        public List<Route> FilterByParams(string companyId, int page, int size, string sortBy, string startLocation, string endLocation, string startDate, string price, string bus, string status)
        {
            DateOnly startDateParsed = DateOnly.Parse(startDate);

            var query = from route in _context.Route
                        where (string.IsNullOrEmpty(startLocation) || route.StartLocation.ToLower().Contains(startLocation.ToLower()) && startLocation.Length > 2)
                        && (string.IsNullOrEmpty(companyId) || route.Bus.Company.Id.ToString().Equals(companyId))
                        && (string.IsNullOrEmpty(endLocation) || route.EndLocation.ToLower().Contains(endLocation) && endLocation.Length > 2)
                        && (string.IsNullOrEmpty(startDate) || startDateParsed == route.StartDate)
                        && (string.IsNullOrEmpty(price) || route.Price.ToString().Equals(price))
                        && (string.IsNullOrEmpty(bus) || route.Bus.Name.ToLower().Contains(bus) && bus.Length > 2)
                        && (string.IsNullOrEmpty(status) || route.Status.ToString().ToLower().Equals(status))
                        select route;

            var result = SortList(sortBy, query).Skip(page * size).Take(size).ToList();
            Console.WriteLine($"FilterByParams: Found {result.Count} routes matching the criteria.");

            return result;
        }

        public Route FindById(Guid id)
        {
            return _context.Route.FirstOrDefault(route => route.Id == id);
        }

        public int GetTotalByParams(string companyId, string startLocation, string endLocation, string startDate, string price, string bus, string status, int size)
        {
            DateOnly startDateParsed = DateOnly.Parse(startDate);

            var query = from route in _context.Route
                        where (string.IsNullOrEmpty(startLocation) || route.StartLocation.ToLower().Contains(startLocation.ToLower()) && startLocation.Length > 2)
                        && (string.IsNullOrEmpty(companyId) || route.Bus.Company.Id.ToString().Equals(companyId))
                        && (string.IsNullOrEmpty(endLocation) || route.EndLocation.ToLower().Contains(endLocation) && endLocation.Length > 2)
                        && (string.IsNullOrEmpty(startDate) || startDateParsed == route.StartDate)
                        && (string.IsNullOrEmpty(price) || route.Price.ToString().Equals(price))
                        && (string.IsNullOrEmpty(bus) || route.Bus.Name.ToLower().Contains(bus) && bus.Length > 2)
                        && (string.IsNullOrEmpty(status) || route.Status.ToString().ToLower().Equals(status))
                        select bus;

            return (int)Math.Ceiling((double)query.Count() / size);
        }

        public Route Save(Route route)
        {
            if (route == null) throw new ArgumentNullException("Sent route argument cannot be null!");
            _context.Route.Add(route);
            _context.SaveChanges();

            return route;
        }

        public Route Update(Route route)
        {
            if (route == null) throw new ArgumentNullException("Sent route argument cannot be null!");

            _context.Route.Update(route);
            _context.SaveChanges();

            return route;
        }

        private static IQueryable<Route> SortList(string sortBy, IQueryable<Route> query)
        {
            switch (sortBy)
            {
                case "startDate":
                    query = query.OrderBy(data => data.StartDate);
                    break;
                case "bus":
                    query = query.OrderBy(data => data.Bus.Name);
                    break;
                case "status":
                    query = query.OrderBy(data => data.Status);
                    break;
                case "startLocation":
                    query = query.OrderBy(data => data.StartLocation);
                    break;
                case "price":
                    query = query.OrderBy(data => data.Price);
                    break;
                default:
                    query = query.OrderBy(data => data.Id);
                    break;
            }
            return query;
        }
    }
}
