namespace SignalRServerApi.PlusClientHelpers
{
    public record cSecurityLoginDR
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public record cClientVehicleVerifyDR : cSecurityLoginDR
    {
      
    }
    public record OptionSettingDO : cSecurityLoginDR
    {

    }
    public  record cClientOptionDR : cSecurityLoginDR
    {
    }
    public record cQSServiceStepDR : cSecurityLoginDR
    {
        public int SortOrder { get; set; }
    }
}
