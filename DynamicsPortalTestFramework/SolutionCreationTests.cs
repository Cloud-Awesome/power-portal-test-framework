using System.Configuration;
using System.Linq;

using NUnit.Framework;

using CloudAwesome.PortalTestFramework;
using CloudAwesome.PortalTestFramework.Models;

namespace CloudAwesome.PortalTests
{
    [TestFixture]
    public class SolutionCreationTests
    {
        [Test]
        public void TestConfigSettings()
        {
            var config = new PortalConfiguration("arthur");

            Assert.AreEqual("arthur", config.UserCredentials.UserName);
            Assert.AreEqual("https://awesome-sandbox.microsoftcrmportals.com", config.BaseUrl);
            //Assert.AreEqual();
        }
        
        [Test]
        [Category("Initial Project Creation")]
        [Category("Firefox")]
        public void FirstTestDuringSetUp()
        {
            #region actions to do 
            //TODO - Support for multiple browsers
            //TODO - Initialisation helper - set up and tear down of local user account
            // Each class should be as test small as possible and include as little set up as possible.
            // ^^ On average, 10-20 lines max. would be great :)
            #endregion actions to do
            var config = new PortalConfiguration("arthur");

            // Arrange
            var portal = new Portal(config);
            if (!portal.Login())
            {
                //TODO - extract this out into the Login method, not in the test class
                Assert.Fail("Failed to authenticate");
            }

            // Act
            var result = 
                portal
                    .SetValue("firstname", "Arthur") // TODO - Extract magic strings out into DataModel
                    .SetValue("lastname", "Nicholson-Gumuła")
                    .Click("submit")
                    .Wait(1000)
                    .GetValue("name");

            // Assert
            Assert.AreEqual("Arthur Nicholson-Gumuła", result, 
                "Record name has been set correctly");

            portal.Quit();

        }
    }
}
