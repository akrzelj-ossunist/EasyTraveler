using ET.Core.Entities;
using ET.DataAccess.Persistence;

namespace ET.DataAccess.Repositories.Impl
{
    public class BusRepositroyImpl : BusRepository
    {
        private readonly DatabaseContext _context;

        public BusRepositroyImpl(DatabaseContext context)
        {
            _context = context;
        }

        public bool Delete(Bus bus)
        {
            _context.Bus.Remove(bus);
            _context.SaveChanges();

            return true;
        }

        public Bus FindById(Guid id)
        {
            return _context.Bus.FirstOrDefault(bus => bus.Id == id);
        }

        public Bus Save(Bus bus)
        {
            if (bus == null) throw new ArgumentNullException("Sent bus argument cannot be null!");
            _context.Bus.Add(bus);
            _context.SaveChanges();

            return bus;
        }

        public Bus Update(Bus bus)
        {
            if (bus == null) throw new ArgumentNullException("Sent bus argument cannot be null!");

            _context.Bus.Update(bus);
            _context.SaveChanges();

            return bus;
        }

        public List<Bus> FilterByParams(string companyId, string name, string seats, string isAvailable, string company, int page, int size, string sortBy)
        {

            bool isAvailableValue;
            bool isAvailableParsed = bool.TryParse(isAvailable, out isAvailableValue);

            var query = from bus in _context.Bus
                        where (string.IsNullOrEmpty(name) || bus.Name.ToLower().Contains(name.ToLower()) && name.Length > 2)
                        && (string.IsNullOrEmpty(companyId) || bus.Company.Id.ToString().Equals(companyId))
                        && (string.IsNullOrEmpty(seats) || bus.Seats.ToString().Equals(seats))
                        && (string.IsNullOrEmpty(isAvailable) || (isAvailableParsed && bus.IsAvailable == isAvailableValue))
                        && (string.IsNullOrEmpty(company) || bus.Company.Name.ToLower().Contains(company.ToLower()) && company.Length > 2)
                        select bus;

            return SortList(sortBy, query).Skip(page * size)
                                          .Take(size)
                                          .ToList();
        }

        public List<Bus> FindAll(string companyId, int page, int size, string sortBy)
        {
            var query = from bus in _context.Bus
                        where (string.IsNullOrEmpty(companyId) || bus.Company.Id.ToString().Equals(companyId))
                        select bus;

            return SortList(sortBy, query)
                    .Skip(page * size)
                    .Take(size)
                    .ToList();
        }


        private static IQueryable<Bus> SortList(string sortBy, IQueryable<Bus> query)
        {
            switch (sortBy)
            {
                case "name":
                    query = query.OrderBy(bus => bus.Name);
                    break;
                case "seats":
                    query = query.OrderBy(bus => bus.Seats);
                    break;
                case "isAvailable":
                    query = query.OrderBy(bus => bus.IsAvailable);
                    break;
                case "company":
                    query = query.OrderBy(bus => bus.Company.Name);
                    break;
                default:
                    query = query.OrderBy(bus => bus.Id);
                    break;
            }
            return query;
        }
    }
}
