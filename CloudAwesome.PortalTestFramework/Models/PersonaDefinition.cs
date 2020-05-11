using System.Configuration;

namespace CloudAwesome.PortalTestFramework.Models
{
    public class PersonaDefinition: ConfigurationSection
    {
        [ConfigurationProperty("Personae")]
        public UserCredentialCollection Personae => (UserCredentialCollection) this["Personae"];
    }
}
