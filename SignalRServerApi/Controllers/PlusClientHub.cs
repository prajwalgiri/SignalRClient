using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using SignalRServerApi.Helpers;
using SignalRServerApi.PlusClientHelpers;
using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SignalRServerApi.Controllers
{
    [Authorize]
    public class PlusClientHub : Hub
    {
        private readonly ILogger<object> _logger;
        private readonly IUserService _userService;
        public PlusClientHub(ILogger<object> logger,IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }
        private static readonly ConcurrentDictionary<string, string> UserConnections = new();
        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("Client Connected, Context:{0}", JsonSerializer.Serialize(this.Context.User));
            HttpContext httpContext = Context.GetHttpContext()!;
            //need to identify the connected client for logging only 
            object authUser = httpContext.Items["User"]!;
            _logger.LogInformation($"Connected User:{authUser}");

            return base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (user != null)
            {
                UserConnections.TryRemove(user, out _);
                await Clients.All.SendAsync("UserDisconnected", user);
            }
            await base.OnDisconnectedAsync(exception);
            _logger.LogInformation($"Disconnected :{exception}");
        }
        public async Task<string> ProcessLogin(string sUserName, string sPassword)
        {
            cSecurityLoginDR loginResponse = new cSecurityLoginDR();

            string sourceIp = Context.GetHttpContext()!.Connection.RemoteIpAddress.ToString();
            AuthenticateRequest authenticateRequest = new AuthenticateRequest { Username = sUserName, Password = sPassword };
            AuthenticateResponse? authenticateResponse =  _userService.Authenticate(authenticateRequest);
            if (authenticateResponse == null)
            {
                loginResponse.Success = false;
                loginResponse.Message = "Invalid Username or Password";
            }
            else
            {
                loginResponse.Success = true;
                loginResponse.Message = "Login Successful";
            }
            return JsonSerializer.Serialize(loginResponse);

        }
        public async Task<bool> IID_VerifyUnit_Exists(string sSessionToken, string sSerial, int nJurID)
        {
            if (!CheckSession(sSessionToken))
            {
                return false;
            }
            //Check that the session is valid.
            //Checks that the serial number exists in the Unit table.

            return true;

        }
        public async Task<bool> QS_ClientIDInThisJur(long lClientID, int iJurisdictionID)
        {
            //Check if ClientID is in this jurisdiction’s client table, if so, return true
            return true;
        }
        public async Task<bool> QS_CheckForRecall(int JurisdictionID, string sSerial)
        {
            //	Check the unit table for the serial number and return the Recall field.
            //	If the Recall field is NULL, then return false.
            return true;
        }
        public async Task<string> IID_GetClientListByLocation(string sSessionToken, int iLocationID, int nJurID)
        {
            if (!CheckSession(sSessionToken))
            {
                return "";
            }
         //   Gets a list of cTupleLong of all clients in location.

         //   Sets cTupleLong.ID to the ClientID.

         //   Sets cTupleLong.Description to “LastName, FirstName MiddleName(ClientID)”
	        //The list is sorted by Description and returned as JSON

            return "";
        }
        public async Task<string> IID_GetClientVehicleVerify(string sSessionToken, long iClientID, string sSerial, int nJurID)
        {
            if (!CheckSession(sSessionToken))
            {
                return "";
            }
            //Gets the client’s information and the currently installed vehicle info if the serial is installed.
            //Returns a cClientVehicleVerifyDR object as JSON.
            return "";

        }
        public async Task<string> IID_GetAppointmentList(string sSessionToken, long iVehicleID, int nJurID)
        {
            if (!CheckSession(sSessionToken))
            {
                return "";
            }
            //Unkept appointments with a begin date before the last kept appointment
            //should be marked ‘Missed’ and saved back to the db.
            //Return list of appointments for vehicleID as JSON.
            //List should be ordered by BeginDT descending
            return "";
        }
        public async Task<string> IID_GetOptionSettingFile(string sSessionToken, int optionSettingID, int nJurID)
        {
            if (!CheckSession(sSessionToken))
            {
                return "";
            }
            //Get the OptionSettingDO object for the optionSettingID
            //Get option settings from the db by the optionSettingID.
            //Returns OptionSettingDO object as JSON.

            return "";
        }
        public async Task<string> QS_GetUnifiedOptionFileForClient(long iClientID, int iJurisdictionID)
        {
            //Using the clientID we get the UnifiedOptionSettingID from the client record in the client table.
            //We then use the UnifiedOptionSettingID to pull records from the Unified.jur.UnitOptionSetting_UnitOption table.
            //We then create a SortedDictionary<string, object> and load the dictionary with default UnifiedOptions that have a default value and do not have “Not used” in the DeviceOptionName.
            //We then convert the values from the UnifiedOptionSetting_UnitOption records and replace the values in the dictionary.
            //We serialize the dictionary and return a JSON string.
            return "";

        }
        public async Task<string> QS_GetClientOptionsForInstall(long lClientID, int iJurisdictionID, int iWebUserID)
        {
            //Get the client options from the ClientOptions table by ClientID.
            //Returns cClientOptionDR as JSON.
            return "";
        }
        public async Task<string> QS_GetStepsForService(int QSServiceID, int JurisdictionID, int LocationID)
        {
            //Using the QSServiceID, we return a list of cQSServiceStepDR objects ordered by 
            //cQSServiceStepDR.SortOrder as JSON from the QSService_ServiceStep table in base.
            //We remove calibration steps during an Early Recall service if the location does
            //not have the CalibrateDuringEarlyRecall flag set to true.
            return "";
        }
        private bool CheckSession(string
            sSessionToken)
        {
            return true;
        }


    }
}
