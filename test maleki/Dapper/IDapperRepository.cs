using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_maleki.Entities;

namespace test_maleki.Dapper
{
    public interface IDapperRepository
    {
        public List<User> GetUsers();
        public void AddUser(User user);

        public void UpdateUser(User oldUser, User newUser);
    }
    

}
