namespace SignalRServerApi
{
    public static class HubMessageType
    {
        
        public const string LoginFailed = "LoginFailed";
        public const string InvokeServerAction = "HandleActionAsync";
        public const string InvokeClientAction = "HandleClientActionAsync";
        public const string NotifyServer = "HandleNotificationFromClient";
        
    }
    public  enum ActionType
    {
        Step1,
        Step2,
        Step3
    }
    public  class RequestPayload 
    {
        public ActionType ActionType { get; set; }
        
        public object data  { get; set; }
    }
    public class ServerResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object data { get; set; }
    }
}
