namespace CloudAwesome.PortalTestFramework.Models
{
    // Currently only Firefox is supported
    // (Phantom will come second)
    public enum BrowserType
    {
        Chrome, Firefox, Edge
    }

    public class BrowserSettings
    {
        public BrowserType BrowserType { get; set; }
        public bool Headless { get; set; }
    }
}