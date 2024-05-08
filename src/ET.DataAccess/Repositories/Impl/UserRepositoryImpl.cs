using ET.Core.Entities;
using ET.DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ET.DataAccess.Repositories.Impl
{
    public class UserRepositoryImpl : UserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepositoryImpl(DatabaseContext context)
        {
            _context = context;
        }

        public User FindByEmail(string email)
        {
            if (email == null) throw new ArgumentNullException("Sent email argument cannot be null!");

            return _context.User.FirstOrDefault(user => user.Email == email);
        }

        public User FindById(int id)
        {
            return _context.User.FirstOrDefault(user => user.Id == id);
        }

        public User Save(User user)
        {
            if (user == null) throw new ArgumentNullException("Sent user argument cannot be null!");
            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User Update(User user)
        {
            if (user == null) throw new ArgumentNullException("Sent user argument cannot be null!");

            _context.User.Update(user);
            _context.SaveChanges();
            
            return user;
        }
    }
}
