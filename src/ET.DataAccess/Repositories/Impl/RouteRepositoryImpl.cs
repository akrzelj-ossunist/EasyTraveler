using ET.Core.Entities;
using ET.DataAccess.Models;
using ET.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public List<Route> FilterByParams(RouteFilters routeFilters)
        {
            DateTime startDateParsed = !string.IsNullOrEmpty(routeFilters.StartDate) ? DateTime.Parse(routeFilters.StartDate).ToUniversalTime().AddDays(1) : DateTime.MinValue;

            var peopleRes = 0;
            if (int.TryParse(routeFilters.People, out int peopleNum))
            {
                peopleRes = peopleNum;
            }

            var query = from route in _context.Route.Include(r => r.Bus).ThenInclude(b => b.Company)
                        where (string.IsNullOrEmpty(routeFilters.StartLocation) || route.StartLocation.ToLower().Contains(routeFilters.StartLocation.ToLower()) && routeFilters.StartLocation.Length > 2)
                            && (string.IsNullOrEmpty(routeFilters.CompanyId) || route.Bus.Company.Id.ToString().Equals(routeFilters.CompanyId))
                            && (string.IsNullOrEmpty(routeFilters.EndLocation) || route.EndLocation.ToLower().Contains(routeFilters.EndLocation.ToLower()) && routeFilters.EndLocation.Length > 2)
                            && (string.IsNullOrEmpty(routeFilters.StartDate) || startDateParsed.Date == route.StartDate.Date)
                            && (string.IsNullOrEmpty(routeFilters.Price) || route.Price.ToString().Equals(routeFilters.Price))
                            && (string.IsNullOrEmpty(routeFilters.Company) || route.Bus.Company.Name.ToLower().Contains(routeFilters.Company) && routeFilters.Company.Length > 2)
                            && (string.IsNullOrEmpty(routeFilters.People) || (route.Bus.Seats - route.CurrentReservations) >= peopleRes)
                            //&& (string.IsNullOrEmpty(routeFilters.Status) || route.Status.Equals(routeFilters.Status))
                        select route;

            var result = SortList(routeFilters.SortBy, query).Skip(routeFilters.Page * routeFilters.Size).Take(routeFilters.Size).ToList();
            Console.WriteLine($"FilterByParams: Found {result.Count} routes matching the criteria.");

            return result;
        }

        public Route FindById(Guid id)
        {
            return _context.Route.Include(r => r.Bus).FirstOrDefault(route => route.Id == id);
        }

        public int GetTotalByParams(RouteFilters routeFilters)
        {
            DateTime startDateParsed = !string.IsNullOrEmpty(routeFilters.StartDate) ? DateTime.Parse(routeFilters.StartDate).ToUniversalTime() : DateTime.MinValue;

            var query = from route in _context.Route
                        where (string.IsNullOrEmpty(routeFilters.StartLocation) || route.StartLocation.ToLower().Contains(routeFilters.StartLocation.ToLower()) && routeFilters.StartLocation.Length > 2)
                        && (string.IsNullOrEmpty(routeFilters.CompanyId) || route.Bus.Company.Id.ToString().Equals(routeFilters.CompanyId))
                        && (string.IsNullOrEmpty(routeFilters.EndLocation) || route.EndLocation.ToLower().Contains(routeFilters.EndLocation.ToLower()) && routeFilters.EndLocation.Length > 2)
                        && (string.IsNullOrEmpty(routeFilters.StartDate) || startDateParsed.Date == route.StartDate.Date)
                        && (string.IsNullOrEmpty(routeFilters.Price) || route.Price.ToString().Equals(routeFilters.Price))
                        && (string.IsNullOrEmpty(routeFilters.Bus) || route.Bus.Name.ToLower().Contains(routeFilters.Bus.ToLower()) && routeFilters.Bus.Length > 2)
                        && (string.IsNullOrEmpty(routeFilters.Status) || route.Status.ToString().ToLower().Equals(routeFilters.Status.ToLower()))
                        select route;

            return (int)Math.Ceiling((double)query.Count() / routeFilters.Size);
        }

        public Route Save(Route route)
        {
            if (route == null) throw new ArgumentNullException("Sent route argument cannot be null!");

            _context.Route.Add(route);

            var bus = _context.Bus.FirstOrDefault(bus => bus.Id == route.Bus.Id);
            bus.IsAvailable = false;
            _context.Bus.Update(bus);

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

        public List<Bus> GetAvailableBuses(DateTime startDate, DateTime endDate, string companyId, string startLocation)
        {
            var query = from bus in _context.Bus
                        where (string.IsNullOrEmpty(companyId) || bus.Company.Id.ToString() == companyId)
                            && !(
                                from route in _context.Route
                                where route.Bus.Id == bus.Id
                                    && !(endDate < route.StartDate || startDate > route.EndDate)
                                    && (route.Status != Core.Enums.RouteStatus.Canceled) // Exclude canceled routes
                                select route
                            ).Any()
                            && (bus.CurrentLocation.ToLower().Equals(startLocation.ToLower()))
                        select bus;

            return query.ToList();
        }

        public void UpdateStatusBasedOnDate()
        {
            DateTime currentDate = DateTime.Now.ToUniversalTime();

            // Get routes with date equal to the current date and status confirmed
            var routesToUpdate = _context.Route
                .Where(route => route.StartDate <= currentDate && route.Status == Core.Enums.RouteStatus.Confirmed)
                .ToList();

            // Update the status to inProgress
            foreach (var route in routesToUpdate)
            {
                route.Status = Core.Enums.RouteStatus.InProgress;
            }

            // Save changes to the database
            _context.SaveChanges();
        }

        public bool CheckIfBusAvailable(Guid id)
        {
            // Check if there's any route associated with the provided bus ID and has status confirmed or in progress
            var routeWithBus = _context.Route.Any(r => r.Bus.Id == id &&
                                                        (r.Status == Core.Enums.RouteStatus.Confirmed ||
                                                         r.Status == Core.Enums.RouteStatus.InProgress));

            // If there's a route with the bus ID and status confirmed or in progress, return false
            // Otherwise, return true
            return !routeWithBus;
        }

    }
}
