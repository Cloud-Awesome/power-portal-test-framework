using NUnit.Framework;

using CloudAwesome.PortalTestFramework;
using CloudAwesome.PortalTestFramework.Models;

namespace CloudAwesome.PortalTests
{
    [TestFixture]
    public class SolutionCreationTests
    {

        [Test]
        [Category("Initial Project Creation")]
        [Category("Firefox")]
        public void FirstTestDuringSetUp()
        {
            //TODO - Extract config details out of the test class
            //TODO - Support multiple signins for different personas (constructor to determine config which to use?)
            //TODO - Support for multiple browsers
            //TODO - Initialisation helper - set up and tear down of local user account
            // Each class should be as test small as possible and include as little set up as possible.
            // ^^ On average, 10-20 lines max. would be great :)
            var config = new PortalConfiguration()
            {
                BaseUrl = "https://awesome-sandbox.microsoftcrmportals.com",
                UserCredentials = new UserCredentials()
                {
                    UserName = "arthur",
                    UserPassword = "zxasq1!",
                    AuthenticationType = AuthenticationType.Local
                },
                BrowserSettings = new BrowserSettings()
                {
                    BrowserType = BrowserType.Firefox
                }
            };

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
                .SetValue("test", "testValue")
                .Click("submit")
                .Wait(1000)
                .GetValue("testElement");

            // Assert
            Assert.AreEqual("Test Response", result, 
                "Test result is happy ;)");

            //TODO - Extract this out into a generic teardown method so doesn't need to be in every test class?
            portal.Quit();

        }
    }
}
