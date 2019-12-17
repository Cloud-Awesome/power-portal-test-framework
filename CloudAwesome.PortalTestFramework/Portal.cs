using System;
using CloudAwesome.PortalTestFramework.Models;

namespace CloudAwesome.PortalTestFramework
{
    public class Portal
    {
        public Portal(PortalConfiguration configuration)
        {
            // Implement
        }

        public bool Login()
        {
            // Implement
            return true;
        }

        public Portal Click(string element)
        {
            // Implement
            return this;
        }

        public Portal Wait(int milliseconds)
        {
            // Implement
            return this;
        }

        public Portal Navigate()
        {
            // Implement
            return this;
        }

        public Portal SetValue(string element, object value)
        {
            // Implement
            return this;
        }

        public Object GetValue(string element)
        {
            // Implement
            return "Test Response";
        }
    }
}
