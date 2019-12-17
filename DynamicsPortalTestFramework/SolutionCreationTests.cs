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
        public void FirstTestDuringSetUp()
        {
            var config = new PortalConfiguration()
            {
                BaseUrl = "tester.microsoftcrmportals.com",
                UserCredentials = new UserCredentials()
                {
                    UserName = "pedro@user.test",
                    UserPassword = "PassWord123",
                    AuthenticationType = AuthenticationType.Local
                },
                BrowserSettings = new BrowserSettings()
                {
                    BrowserType = BrowserType.Firefox
                }
            };

            // Arrange
            var portal = new Portal(config);
            portal.Login();

            // Act
            var result = 
                portal
                .SetValue("test", 2)
                .Click("submit")
                .Wait(1000)
                .GetValue("testElement");

            // Assert
            Assert.AreEqual("Test Response", result, 
                "Test result is happy ;)");
            
        }
    }
}
