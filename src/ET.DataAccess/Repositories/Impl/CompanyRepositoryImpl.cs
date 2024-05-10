using ET.Core.Entities;
using ET.DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.DataAccess.Repositories.Impl
{
    public class CompanyRepositoryImpl : CompanyRepository
    {
        private readonly DatabaseContext _context;

        public CompanyRepositoryImpl(DatabaseContext context)
        {
            _context = context;
        }

        public Company FindByEmail(string email)
        {
            if (email == null) throw new ArgumentNullException("Sent email argument cannot be null!");

            return _context.Company.FirstOrDefault(data => data.Email == email);
        }

        public Company FindById(Guid id)
        {
            return _context.Company.FirstOrDefault(data => data.Id == id);
        }

        public Company Save(Company company)
        {
            if (company == null) throw new ArgumentNullException("Sent company argument cannot be null!");
            _context.Company.Add(company);
            _context.SaveChanges();

            return company;
        }

        public Company Update(Company company)
        {
            if (company == null) throw new ArgumentNullException("Sent company argument cannot be null!");

            _context.Company.Update(company);
            _context.SaveChanges();

            return company;
        }

        public bool Delete(Company company)
        {
            _context.Company.Remove(company);
            _context.SaveChanges();

            return true;
        }
    }
}
