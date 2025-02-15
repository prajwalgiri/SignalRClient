﻿using Microsoft.AspNetCore.SignalR.Client;
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
        internal ActionType CurrentAction { get; set; }= ActionType.Step1;
        public async Task InitializeConnection()
        {
            GetToken();
             WriteToLog("Initalizing connection....");
             WriteToLog($"Connection Url:{ConnectionUrl}");
            connection = new HubConnectionBuilder()
               .WithUrl(ConnectionUrl, options =>
               {
                  options.Headers.Add("Authorization", $"Bearer {Token}");
               })
               .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
                .Build();
             WriteToLog("Connection Initalized..");
            connection.Closed += Connection_Closed;
            connection.On<string>(HubMessageType.LoginFailed, HandleFailedLogin);
            connection.On<ServerResponse>(HubMessageType.InvokeClientAction, HandleClientAction);
            WriteToLog("Starting connection....");
            await connection.StartAsync();
            WriteToLog("Connection started....");
        }
        public ClientApp(string token = "")
        {
            InitializeComponent();
            Token = token;
            WriteToLogSync($"Token:{token}");
            txtUrl.Text = "https://localhost:7109/connectionhub";
            //InitializeConnection();
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
             WriteToLog(msg);

        }
        private async Task Connection_Closed(Exception ex)
        {
            WriteToLog("Connection closed....");
            await Task.Delay(new Random().Next(0, 5) * 1000);
            if(CurrentAction== ActionType.Step3)
            {
                return; //marks the end of the process
            }
            await connection.StartAsync();
            WriteToLog("Connection restarted....");
        }
        private async Task HandleClientAction(ServerResponse response)
        {
                WriteToLog($"Action:{response.data} Message:{response.Message}");
            if (response.Success)
            {
                switch(response.CurrentAction)
                {
                    case ActionType.Step1:
                        WriteToLog("Step1 Done.");
                        CurrentAction = ActionType.Step2;

                        break;
                    case ActionType.Step2:
                        WriteToLog("Step2 Done.");
                        CurrentAction = ActionType.Step3;
                        break;
                    case ActionType.Step3:
                        WriteToLog("Step3 Done.");
                        break;
                }
                if (response.CurrentAction == ActionType.Step3)
                {
                    WriteToLog("Process Complete...");
                    await connection.DisposeAsync();

                    return;
                }
                await PerformNextStep();
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            
            ConnectionUrl = txtUrl.Text;
            //Task.Run(() => InitializeConnection());
            InitializeConnection();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            WriteToLog("Starting the process....");
            await connection.SendAsync(HubMessageType.InvokeServerAction, new RequestPayload { ActionType = CurrentAction, data = "Step1" });

        }
        private async Task PerformNextStep()
        {
            WriteToLog("Performing next step....");
            await connection.SendAsync(HubMessageType.InvokeServerAction, new RequestPayload { ActionType = CurrentAction, data = CurrentAction.ToString() });
        }
        private  void WriteToLog(string message)
        {
            txtLog.Invoke(new Action(() => txtLog.Text += message + Environment.NewLine));

        }
        private  void WriteToLogSync(string message)
        {
            txtLog.Text += message + Environment.NewLine;

        }
    }
}
