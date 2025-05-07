namespace LogAnalizerServer;

public class DatabaseConnectionManager
{
    private static string _currentConnectionString = string.Empty;

    public static string CurrentConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_currentConnectionString))
            {
               
                var defaultDbPath = Path.Combine(Directory.GetCurrentDirectory(), "Databases", "default.db");
                _currentConnectionString = $"Data Source={defaultDbPath};";
            }

            return _currentConnectionString;
        }
    }

    public static void SetConnectionString(string newConnectionString)
    {
        _currentConnectionString = newConnectionString;
    }
}
