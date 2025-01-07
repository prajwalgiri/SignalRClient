using System.Text.Json;
using System.Text.Json.Serialization;

namespace SignalRServerApi
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }
        public static void LogError(string message)
        {
            Console.WriteLine($"Error:{message}");
        }
        public static void LogException(Exception ex)
        {
            var json = JsonSerializer.Serialize(ex);
            Console.WriteLine($"Error:{json}");
        }
    }
}
