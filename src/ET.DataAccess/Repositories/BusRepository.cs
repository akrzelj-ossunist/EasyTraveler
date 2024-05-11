using ET.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.DataAccess.Repositories
{
    public interface BusRepository
    {
        public Bus Save(Bus bus);

        public Bus Update(Bus bus);

        public Bus FindById(Guid id);

        public bool Delete(Bus bus);

        public List<Bus> FindAll(string companyId, int page, int size, string sortBy);

        public List<Bus> FilterByParams(string companyId, string name, string seats, string isAvailable, string company, int page, int size, string sortBy);
    }
}
