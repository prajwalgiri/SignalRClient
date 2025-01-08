using Microsoft.AspNetCore.SignalR.Client;
using SignalRServerApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalRClientApp
{
    public partial class ClientApp : Form
    {
        HubConnection connection;
        internal string ConnectionUrl { get; set; }
        internal string Token { get; set; }
        public async Task InitializeConnection()
        {
            GetToken();
            await WriteToLog("Initalizing connection....");
            await WriteToLog($"Connection Url:{ConnectionUrl}");
            connection = new HubConnectionBuilder()
               .WithUrl(ConnectionUrl, options =>
               {
                  options.Headers.Add("Authorization", $"Bearer {Token}");
               })
               .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
                .Build();
            await WriteToLog("Connection Initalized..");
            connection.Closed += Connection_Closed;
            connection.On<string>(HubMessageType.LoginFailed, HandleFailedLogin);
            connection.On<ServerResponse>(HubMessageType.InvokeClientAction, HandleClientAction);
           await WriteToLog("Starting connection....");
            await connection.StartAsync();
            await WriteToLog("Connection started....");
        }
        public ClientApp(string token = "")
        {
            InitializeComponent();
            Token = token;
            WriteToLogSync($"Token:{token}");
            txtUrl.Text = "https://localhost:7109/connectionhub";
            InitializeConnection();
        }
        private  void GetToken()
        {
            HttpClient client = new HttpClient();
            HttpContent httpContent = new StringContent(JsonSerializer.Serialize(new { Username = "test", Password = "test" }), Encoding.UTF8, "application/json");
            var response = client.PostAsync(ConnectionUrl.Remove(ConnectionUrl.Length-13)+ "login/authenticate", httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                var user = response.Content.ReadAsStringAsync().Result;
                var obj = JsonSerializer.Deserialize<User>(user);
                WriteToLogSync("User:" + user);
                Token= obj.token;
            }
        }

        private async void HandleFailedLogin(string msg)
        {
            //Handle the success login here
            await WriteToLog(msg);

        }
        private async Task Connection_Closed(Exception ex)
        {
            await WriteToLog("Connection closed....");
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
            await WriteToLog("Connection restarted....");
        }
        private async Task HandleClientAction(ServerResponse response)
        {

        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            
            ConnectionUrl = txtUrl.Text;
            //Task.Run(() => InitializeConnection());
            InitializeConnection();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {

          await connection.StartAsync();
        }
        private async Task WriteToLog(string message)
        {
            txtLog.Invoke(new Action(() => txtLog.Text += message + Environment.NewLine));

        }
        private  void WriteToLogSync(string message)
        {
            txtLog.Text += message + Environment.NewLine;

        }
    }
}
