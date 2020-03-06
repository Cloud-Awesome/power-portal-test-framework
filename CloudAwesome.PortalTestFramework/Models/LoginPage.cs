using System;
using System.Collections.Generic;
using System.Text;

namespace CloudAwesome.PortalTestFramework.Models
{
    public static class LoginPage
    {
        public static string PageUrl { get { return "signin"; } }
        public static string UserName { get { return "Username"; } }
        public static string Password { get { return "Password"; } }
        public static string LocalSubmitButton {  get { return "submit-signin-local"; } }
    }
}
