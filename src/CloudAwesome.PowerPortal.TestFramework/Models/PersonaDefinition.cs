using System.Configuration;

namespace CloudAwesome.PowerPortal.TestFramework.Models
{
    public class PersonaDefinition: ConfigurationSection
    {
        [ConfigurationProperty("Personae")]
        public UserCredentialCollection Personae => (UserCredentialCollection) this["Personae"];
    }
}
