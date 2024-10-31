
namespace test_maleki;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Status { get; set; } 

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        Status = "not available"; 
    }
}