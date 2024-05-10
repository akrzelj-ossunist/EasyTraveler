using ET.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.DataAccess.Repositories
{
    public interface UserRepository
    {
        public User Save(User user);

        public User Update(User user);

        public User FindById(Guid id);

        public User FindByEmail(string email);

        public bool Delete(User user);
    }
}
