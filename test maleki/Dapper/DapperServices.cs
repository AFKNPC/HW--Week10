using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_maleki.Entities;

namespace test_maleki.Dapper
{
    public class DapperServices
    {
        DapperRepository dapRepo = new DapperRepository();
        public void Add(User user)
        {
            List<User> users = dapRepo.GetUsers();
            if (users.Exists(u => u.Username == user.Username))
            {
                throw new System.Exception("register failed! username already exists.");
            }

            
            dapRepo.AddUser(user);
        }
        public User GetByUsername(string username)
        {
            var users = dapRepo.GetUsers();
            return users.Find(u => u.Username == username);
        }

        public void ChangeStatus(User user, bool status)
        {
            var users = dapRepo.GetUsers();
            var existingUser = users.Find(u => u.Username == user.Username);
            if (existingUser != null)
            {
                existingUser.Status = status;
                dapRepo.UpdateUser(user, existingUser); 
            }
        }

        public void ChangePassword(User user, string oldPassword, string newPassword)
        {
            var users = dapRepo.GetUsers();
            var existingUser = users.Find(u => u.Username == user.Username);
            if (existingUser != null)
            {
                if (existingUser.Password == oldPassword)
                {
                    existingUser.Password = newPassword;
                    dapRepo.UpdateUser(user, existingUser);
                }
                else
                {
                    throw new System.Exception("ChangePassword failed! Old password is incorrect.");
                }
            }
        }

        public List<User> SearchByUsernamePrefix(string prefix)
        {
            var users = dapRepo.GetUsers();
            return users.FindAll(u => u.Username.StartsWith(prefix));
        }
    }
}
