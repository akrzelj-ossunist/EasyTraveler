using ET.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.DataAccess.Repositories
{
    public interface CompanyRepository
    {
        public Company Save(Company company);

        public Company Update(Company company);

        public Company FindById(Guid id);

        public Company FindByEmail(string email);

        public bool Delete(Company company);
    }
}
