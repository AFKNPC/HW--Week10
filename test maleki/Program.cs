using System;
using System.Text.RegularExpressions;
using test_maleki;
using test_maleki.Dapper;
using test_maleki.Entities;

class Program
{
    static void Main()
    {
        var dapServises = new DapperServices();

        while (true)
        {
            Console.Write("Enter command: ");
            string command = Console.ReadLine();
            string[] parts = command.Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);

            string Option = parts[0].Trim();

            try
            {
                switch (Option.ToLower())
                {
                    case "register":
                        string username = CleanString(GetValue(parts, 1),1);
                        string password = CleanString(GetValue(parts, 2),1);
                        var newUser = new User(username, password);
                        dapServises.Add(newUser);
                        Console.WriteLine("Registration successful!");
                        break;

                    case "login":
                        username = CleanString(GetValue(parts, 1), 1);
                        password = CleanString(GetValue(parts, 2), 1);
                        var user = dapServises.GetByUsername(username);
                        if (user != null && user.Password == password)
                        {
                            Session.CurrentUser = user;
                            Console.WriteLine("Login successful!");
                        }
                        else
                        {
                            Console.WriteLine("Login failed! Invalid username or password.");
                        }
                        break;

                    case "change":
                        bool isavailable=false;
                        
                        if (Session.CurrentUser == null)
                        {
                            Console.WriteLine("You must be logged in to change status.");
                            break;
                        }
                        string input = CleanString(GetValue(parts, 1), 1);
                        if (input == "available")
                        {
                            isavailable = true;
                            Console.WriteLine($"Status changed to {input}.");
                        }
                        else if (input == "not available")
                        {
                            isavailable = false;
                            Console.WriteLine($"Status changed to {input}.");
                        }
                        else
                        {
                            Console.WriteLine("format example(Change --status [available],Change --status [not available])");
                            throw new Exception("wrong format");
                        }
                        dapServises.ChangeStatus(Session.CurrentUser, isavailable);
                        
                        break;

                    case "search":
                        string searchUsername = CleanString(GetValue(parts, 1), 1);
                        var foundUsers = dapServises.SearchByUsernamePrefix(searchUsername);
                        if (foundUsers.Count == 0)
                        {
                            Console.WriteLine("No users found.");
                        }
                        else
                        {
                            foreach (var foundUser in foundUsers)
                            {
                                Console.WriteLine($"{foundUser.Username} | status: {foundUser.Status}");
                            }
                        }
                        break;

                    case "changepassword":
                        if (Session.CurrentUser == null)
                        {
                            Console.WriteLine("You must be logged in to change password.");
                            break;
                        }
                        string oldPassword = CleanString(GetValue(parts, 1), 1);
                        string newPassword = CleanString(GetValue(parts, 2), 1);
                        dapServises.ChangePassword(Session.CurrentUser, oldPassword, newPassword);
                        Console.WriteLine("Password changed successfully.");
                        break;

                    case "logout":
                        Session.Logout();
                        Console.WriteLine("Logged out successfully.");
                        break;

                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    static string GetValue(string[] parts, int index)
    {
        if (index >= parts.Length)
        {
            throw new Exception("Missing value for key.");
        }
        return parts[index].Trim();
    }
    public static string CleanString(string input,int index)
    {

        string[] result = input.Split(' ');
        string final = GetValue(result, index);

        if (final == null || final != null && final.Substring(0) == " ")
        {
            throw new Exception(" wrong format of command, make sure you write in right format");
            return null;
        }
        if (final == null || final == "")
        {
            throw new Exception(" wrong format of command, make sure you write in right format");
            return null;
        }

        return final;
    }
}
