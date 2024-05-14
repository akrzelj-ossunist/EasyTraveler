using ET.Core.Entities;
using ET.DataAccess.Persistence;

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
            DateTime startDateParsed = DateTime.Parse(startDate);

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
            DateTime startDateParsed = DateTime.Parse(startDate);

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

            // Retrieve the existing Bus entity if it exists
            var existingBus = _context.Bus.Find(route.Bus.Id);
            if (existingBus != null)
            {
                // Use the existing Bus entity
                route.Bus = existingBus;
            }

            Console.WriteLine(route.Bus.Name + "READY FOR CREATION");
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

        public List<Bus> GetAvailableBuses(DateTime startDate, DateTime endDate, string companyId, string startLocation)
        {
            var query = from bus in _context.Bus
                        where (string.IsNullOrEmpty(companyId) || bus.Company.Id.ToString() == companyId)
                            && !(
                                from route in _context.Route
                                where route.Bus.Id == bus.Id
                                    && !(endDate < route.StartDate || startDate > route.EndDate)
                                select route
                            ).Any()
                            && (bus.CurrentLocation.ToLower().Equals(startLocation.ToLower()))
                        select bus;

            return query.ToList();
        }

    }
}
