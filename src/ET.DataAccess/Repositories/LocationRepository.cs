using ET.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.DataAccess.Repositories
{
    public interface LocationRepository
    {
        public Location Save(Location location);
        public bool Delete(Location location);
        public Location GetById(Guid id);
        public List<Location> GetAll();
    }
}
