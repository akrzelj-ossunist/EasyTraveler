using ET.Core.Entities;

namespace ET.DataAccess.Repositories
{
    public interface BusRepository
    {
        public Bus Save(Bus bus);

        public Bus Update(Bus bus);

        public Bus FindById(Guid id);

        public bool Delete(Bus bus);

        public List<Bus> FindAll(string companyId);

        public List<Bus> FilterByParams(string companyId, string name, string seats, string isAvailable, string company, int page, int size, string sortBy);

        public int GetTotalByParams(string companyId, string name, string seats, string isAvailable, string company, int size);
    }
}
