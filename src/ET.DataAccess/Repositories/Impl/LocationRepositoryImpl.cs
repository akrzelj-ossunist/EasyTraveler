using ET.Core.Entities;
using ET.DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.DataAccess.Repositories.Impl
{
    public class LocationRepositoryImpl : LocationRepository
    {
        private readonly DatabaseContext _context;

        public LocationRepositoryImpl(DatabaseContext context)
        {
            _context = context;
        }
        public bool Delete(Location location)
        {
            _context.Location.Remove(location);
            _context.SaveChanges();

            return true;
        }

        public List<Location> GetAll()
        {
            return _context.Location.ToList();
        }

        public Location GetById(Guid id)
        {
            return _context.Location.FirstOrDefault(data => data.Id == id);
        }

        public Location Save(Location location)
        {
            if (location == null) throw new ArgumentNullException("Sent location argument cannot be null!");
            _context.Location.Add(location);
            _context.SaveChanges();

            return location;
        }
    }
}
