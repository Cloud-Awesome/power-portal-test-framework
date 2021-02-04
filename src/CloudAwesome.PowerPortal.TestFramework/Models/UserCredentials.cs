using System;
using System.Configuration;
using System.Collections.Generic;

namespace CloudAwesome.PowerPortal.TestFramework.Models
{
    public enum AuthenticationType
    {
        ActiveDirectory, Local, Facebook, Google
    }

    public class UserCredentials: ConfigurationElement
    {
        [ConfigurationProperty("Id", IsRequired = true, IsKey = true)]
        public string PersonaId => (string) this["Id"];

        [ConfigurationProperty("UserName", IsRequired = true)]
        public string UserName => (string) this["UserName"];

        [ConfigurationProperty("UserPassword", IsRequired = true)]
        public string UserPassword => (string) this["UserPassword"];

        [ConfigurationProperty("AuthenticationType", IsRequired = true)]
        public AuthenticationType AuthenticationType => (AuthenticationType) this["AuthenticationType"];
    }

    public class UserCredentialCollection: ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new UserCredentials();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((UserCredentials) element).PersonaId;
        }
    }
}