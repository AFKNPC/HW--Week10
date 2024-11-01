using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_maleki.Entities;

namespace test_maleki.file;


public interface IUserRepository
{
    void Add(User user);
    List<User> GetAll();
    User GetByUsername(string username);
    void ChangeStatus(User user, string status);
    void ChangePassword(User user, string oldPassword, string newPassword);
    List<User> SearchByUsernamePrefix(string prefix);
}
