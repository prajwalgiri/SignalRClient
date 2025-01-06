using Microsoft.AspNetCore.SignalR.Client;
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
        public async Task InitializeConnection()
        {
            await WriteToLog("Initalizing connection....");
            await WriteToLog($"Connection Url:{ConnectionUrl}");
            connection = new HubConnectionBuilder()
               .WithUrl(ConnectionUrl)
               .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
                .Build();
           await WriteToLog("Connection Initalized..");
            connection.Closed += async (error) =>
            {
                await WriteToLog("Connection closed....");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }
        public ClientApp(string? url= null)
        {
            InitializeComponent();
            ConnectionUrl = url;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtUrl.Text))
            {
                MessageBox.Show("Please enter the connection url");
                return;
            }
            if(Uri.IsWellFormedUriString(txtUrl.Text, UriKind.Absolute))
            {
                MessageBox.Show("Please enter a valid url");
                return;
            }
            ConnectionUrl = txtUrl.Text;
            Task.Run(()=>InitializeConnection());
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            await WriteToLog("Starting connection....");
            await connection.StartAsync();

        }
        private async Task WriteToLog(string message)
        {
            txtLog.Invoke(new Action(() => txtLog.Text += message + Environment.NewLine));
            
        }
    }
}
