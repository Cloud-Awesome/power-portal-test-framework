namespace CloudAwesome.PowerPortal.TestFramework
{
    public enum PageValidatorStatus
    {
        Success = 0, 
        Warning = 1,
        Error = 2
    }
    
    public class PageValidatorOutput
    {
        public PageValidatorStatus Status { get; set; }
        public string Message { get; set; }
    }
}