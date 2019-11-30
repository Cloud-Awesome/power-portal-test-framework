namespace CloudAwesome.PortalTestFramework.Models
{
    public enum AuthenticationType
    {
        ActiveDirectory, Local, Facebook, Google
    }

    public class UserCredentials
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public AuthenticationType AuthenticationType { get; set; }
    }
}