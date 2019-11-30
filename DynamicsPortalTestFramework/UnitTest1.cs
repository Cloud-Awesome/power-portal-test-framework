using Microsoft.VisualStudio.TestTools.UnitTesting;

using CloudAwesome.PortalTestFramework;
using CloudAwesome.PortalTestFramework.Models;

namespace CloudAwesome.PortalTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        [TestCategory("Initial Project Creation")]
        public void FirstTestDuringSetUp()
        {
            var config = new PortalConfiguration()
            {
                BaseUrl = "tester.microsoftcrmportals.com",
                UserCredentials = new UserCredentials()
                {
                    UserName = "pedro@user.test",
                    UserPassword = "PassWord123",
                    AuthenticationType = AuthenticationType.ActiveDirectory
                }
            };
            
            // Arrange
            var portal = new Portal();
            portal.Login(config);

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
