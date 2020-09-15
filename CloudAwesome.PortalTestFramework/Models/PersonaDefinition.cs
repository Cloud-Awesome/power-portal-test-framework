using System.Configuration;

namespace CloudAwesome.D365Portal.TestFramework.Models
{
    public class PersonaDefinition: ConfigurationSection
    {
        [ConfigurationProperty("Personae")]
        public UserCredentialCollection Personae => (UserCredentialCollection) this["Personae"];
    }
}
