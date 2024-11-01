using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Xml;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using test_maleki.Entities;

namespace test_maleki.file;

public class UserRepository
{
    private string filePath = @"C:\Users\-Nima\Desktop\C#\مکتب\HW- Week10\HW- Week10\users.txt";

    public UserRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "."); // ایجاد فایل اگر وجود نداشته باشد
        }
    }

    public void Add(User user)
    {
        List<User> users = GetAll();
        if (users.Exists(u => u.Username == user.Username))
        {
            throw new Exception("register failed! username already exists.");
        }

        users.Add(user);
        SaveAll(users);
    }

    public List<User> GetAll()
    {
        string jsonData = File.ReadAllText(filePath);


        if (string.IsNullOrEmpty(jsonData))
        {
            return new List<User>();
        }


        return JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
    }

    private void SaveAll(List<User> users)
    {
        var jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
    }

    public User GetByUsername(string username)
    {
        var users = GetAll();
        return users.Find(u => u.Username == username);
    }

    public void ChangeStatus(User user, bool status)
    {
        var users = GetAll();
        var existingUser = users.Find(u => u.Username == user.Username);
        if (existingUser != null)
        {
            existingUser.Status = status;
            SaveAll(users);
        }
    }

    public void ChangePassword(User user, string oldPassword, string newPassword)
    {
        var users = GetAll();
        var existingUser = users.Find(u => u.Username == user.Username);
        if (existingUser != null)
        {
            if (existingUser.Password == oldPassword)
            {
                existingUser.Password = newPassword;
                SaveAll(users);
            }
            else
            {
                throw new Exception("ChangePassword failed! Old password is incorrect.");
            }
        }
    }

    public List<User> SearchByUsernamePrefix(string prefix)
    {
        var users = GetAll();
        return users.FindAll(u => u.Username.StartsWith(prefix));
    }
}
