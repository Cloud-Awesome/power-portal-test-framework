using System;
using CloudAwesome.PortalTestFramework.Models;

namespace CloudAwesome.PortalTestFramework
{
    public class Portal
    {
        public Portal()
        {
            // Any constructor required?
        }


        public bool Login(PortalConfiguration configuration)
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
