using System.Text.Json;
using System.Text;

namespace SignalRClientApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string token = GetToken(); //token will be passed as argument to the client app
            Application.Run(new ClientApp(token));
        }
        private static string GetToken()
        {
            HttpClient client = new HttpClient();
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(new { Username = "test", Password = "test" }), Encoding.UTF8, "application/json");
            var response = client.PostAsync("https://localhost:7109/login/authenticate", httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                var user = response.Content.ReadAsStringAsync().Result;
                var obj= JsonSerializer.Deserialize<User>(user);
                return obj.Token;
            }
            return null;
        }

    }
    public class User
    {
        public string Token { get; set; }
    }
}