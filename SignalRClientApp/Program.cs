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
            string token = ""; //token will be passed as argument to the client app
            Application.Run(new ClientApp(token));
        }
        

    }
    public class User
    {
        public string token { get; set; }
    }
}