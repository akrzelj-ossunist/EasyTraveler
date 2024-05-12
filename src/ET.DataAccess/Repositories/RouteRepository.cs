using ET.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.DataAccess.Repositories
{
    public interface RouteRepository
    {
        public Route Save(Route route);

        public Route Update(Route route);

        public Route FindById(Guid id);

        public bool Delete(Route route);

        public List<Route> FilterByParams(string companyId, int page, int size, string sortBy, string startLocation, string endLocation, string startDate, string price, string bus, string status);

        public int GetTotalByParams(string companyId, string startLocation, string endLocation, string startDate, string price, string bus, string status, int size);
    }
}
