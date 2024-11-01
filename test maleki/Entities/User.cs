namespace test_maleki.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;


    }
    public User() { }

}