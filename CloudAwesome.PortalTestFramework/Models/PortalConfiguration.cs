using System;
using System.Configuration;
using System.Linq;

namespace CloudAwesome.D365Portal.TestFramework.Models
{
    public class PortalConfiguration
    {
        /// <summary>
        /// Defines the node in app.config configuration node instead of setting per test.
        /// Any settings from this configuration can be overridden manually in test
        /// </summary>
        public string PersonaName { get; }

        /// <summary>
        /// UserName, Password and AuthenticationType to be used in login
        /// </summary>
        public UserCredentials UserCredentials { get; set; }
        
        /// <summary>
        /// BaseUrl of the Portal Instance. Navigation in test is appended to this Base
        /// </summary>
        public string BaseUrl { get; set; }
        
        /// <summary>
        /// Details of browser and browser type in which to execute tests
        /// </summary>
        public BrowserSettings BrowserSettings { get; set; }

        /// <summary>
        /// Empty constructor. Sets any parameters set in appSettings but can be overriden in test
        /// </summary>
        public PortalConfiguration()
        {
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];

            bool.TryParse(ConfigurationManager.AppSettings["RunBrowserHeadless"], out bool runHeadless);
            BrowserSettings = new BrowserSettings()
            {
                BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType),
                    ConfigurationManager.AppSettings["BrowserType"]),
                Headless = runHeadless
            };
        }

        /// <summary>
        /// Constructor to get user configuration from app.config
        /// </summary>
        /// <param name="personaName"></param>
        public PortalConfiguration(string personaName): this()
        {
            PersonaName = personaName;
            
            var personaObject = (PersonaDefinition)ConfigurationManager.GetSection("PersonaDefinitions");
            var persona =
                (from object value in personaObject.Personae
                    where ((UserCredentials) value).PersonaId == personaName
                    select (UserCredentials) value)
                .FirstOrDefault();

            UserCredentials = persona;

            // TODO - Do plenty of validation to ensure (a) the node exists in app.config and (b) the node has a valid schema
        }
    }
}
