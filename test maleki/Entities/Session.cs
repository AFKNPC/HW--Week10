namespace test_maleki.Entities;

public static class Session
{
    public static User CurrentUser { get; set; }

    public static void Logout()
    {
        CurrentUser = null; // خروج از سیستم
    }
}
