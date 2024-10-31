using System;
using test_maleki;

class Program
{
    static void Main(string[] args)
    {
        var userRepository = new UserRepository();

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
                        string username = GetValue(parts, 1);
                        string password = GetValue(parts, 2);
                        var newUser = new User(username, password);
                        userRepository.Add(newUser);
                        Console.WriteLine("Registration successful!");
                        break;

                    case "login":
                        username = GetValue(parts, 1);
                        password = GetValue(parts, 2);
                        var user = userRepository.GetByUsername(username);
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
                        if (Session.CurrentUser == null)
                        {
                            Console.WriteLine("You must be logged in to change status.");
                            break;
                        }
                        string status = GetValue(parts, 1);
                        userRepository.ChangeStatus(Session.CurrentUser, status);
                        Console.WriteLine($"Status changed to {status}.");
                        break;

                    case "search":
                        string searchUsername = GetValue(parts, 1);
                        var foundUsers = userRepository.SearchByUsernamePrefix(searchUsername);
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
                        string oldPassword = GetValue(parts, 1);
                        string newPassword = GetValue(parts, 2);
                        userRepository.ChangePassword(Session.CurrentUser, oldPassword, newPassword);
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
}
