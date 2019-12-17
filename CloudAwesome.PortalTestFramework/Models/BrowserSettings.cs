namespace CloudAwesome.PortalTestFramework.Models
{
    public enum BrowserType
    {
        Chrome, Firefox, Edge, Safari, All
    }

    public class BrowserSettings
    {
        public BrowserType BrowserType { get; set; }

    }
}