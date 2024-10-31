
namespace test_maleki;

public static class Session
{
    public static User CurrentUser { get; set; }

    public static void Logout()
    {
        CurrentUser = null; // خروج از سیستم
    }
}
