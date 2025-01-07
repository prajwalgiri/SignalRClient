using Microsoft.AspNetCore.SignalR.Client;
using SignalRServerApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            await WriteToLog("Starting connection....");
            await connection.StartAsync();
            await WriteToLog("Connection started....");
        }
        public ClientApp(string token = "")
        {
            InitializeComponent();
            Token = token;
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
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUrl.Text))
            {
                MessageBox.Show("Please enter the connection url");
                return;
            }
            if (Uri.IsWellFormedUriString(txtUrl.Text, UriKind.Absolute))
            {
                MessageBox.Show("Please enter a valid url");
                return;
            }
            ConnectionUrl = txtUrl.Text;
            Task.Run(() => InitializeConnection());
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {


        }
        private async Task WriteToLog(string message)
        {
            txtLog.Invoke(new Action(() => txtLog.Text += message + Environment.NewLine));

        }
    }
}
