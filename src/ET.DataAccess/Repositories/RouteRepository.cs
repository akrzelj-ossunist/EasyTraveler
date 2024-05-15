using ET.Core.Entities;
using ET.DataAccess.Models;
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

        public List<Route> FilterByParams(RouteFilters routeFilters);

        public int GetTotalByParams(RouteFilters routeFilters);

        public List<Bus> GetAvailableBuses(DateTime startDate, DateTime endDate, string companyId, string startLocation);

        public void UpdateStatusBasedOnDate();
        public bool CheckIfBusAvailable(Guid id);
    }
}
