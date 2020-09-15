using NUnit.Framework;

using CloudAwesome.D365Portal.TestFramework;
using CloudAwesome.D365Portal.TestFramework.Models;

namespace CloudAwesome.PortalTests
{
    [TestFixture]
    public class SolutionCreationTests
    {
        [Test]
        [Category("Initial Project Creation")]
        [Description("Tests that the multi-persona PortalConfiguration returns a valid model from app.config")]
        public void TestConfigSettings()
        {
            var config = new PortalConfiguration("arthur");

            Assert.AreEqual("arthur", config.UserCredentials.UserName);
            Assert.AreEqual("https://awesome-sandbox.microsoftcrmportals.com/", config.BaseUrl);
            //Assert.AreEqual();
        }

        [Test]
        [Category("Initial Project Creation")]
        [Description("User can log in to the portal and is routed back to home page")]
        public void CanLogIn()
        {
            var config = new PortalConfiguration("arthur");

            // Arrange
            var portal = new Portal(config);
            if (!portal.Login())
            {
                Assert.Fail("Failed to authenticate");
            }

            Assert.True(true, "Succeeded in logging in!");
            Assert.AreEqual(config.BaseUrl, portal.GetCurrentUrl());

            portal.Quit();
        }

        [Test]
        [Category("Initial Project Creation")]
        [Description("Tests that invalid details returns a useful assertion as opposed to throwing a horrible exception")]
        [Ignore("Test not yet written")]
        public void CantLoginWithInvalidDetails()
        {
            //TODO - Write test to login with user "peter" who isn't a real user set up in the portal
            var config = new PortalConfiguration("peter");
        }

        [Test]
        [Category("Initial Project Creation")]
        [Description("User can update their Nickname in the profile")]
        [Ignore("Test authoring is in progress...")]
        public void UserCanUpdateNickname()
        {
            var config = new PortalConfiguration("arthur");

            // Arrange
            var portal = new Portal(config);
            if (!portal.Login())
            {
                Assert.Fail("Failed to authenticate");
            }

            var oldNickName =
                portal
                    .ClickByLinkText("Close") // close the Admin floating menu
                    .ClickByClassName("username")
                    .ClickByLinkText("Profile")
                    .Wait(2000)
                    .GetValue("nickname");

            // TODO - write test
            // Set new nick name
            // Submit
            // Refresh
            // Get and assert
            // Reset to original nickname and assert

        }

        [Test]
        [Category("Initial Project Creation")]
        [Description("User can navigate to their profile and read email address")]
        public void NavigateToProfileFromHeader()
        {
            var config = new PortalConfiguration("arthur");

            // Arrange
            var portal = new Portal(config);
            if (!portal.Login())
            {
                Assert.Fail("Failed to authenticate");
            }

            var profileUrl=
                portal
                    .ClickByLinkText("Close") // close the Admin floating menu
                    .ClickByClassName("username")
                    .ClickByLinkText("Profile")
                    .Wait(2000)
                    .GetCurrentUrl();

            var profileEmailAddress =
                portal
                    .GetValue("emailaddress1");

            Assert.AreEqual($"{config.BaseUrl}profile/", profileUrl);
            Assert.AreEqual("arthur@cloudawesome.yxz", profileEmailAddress);

            portal.Quit();
        }

        [Test]
        [Category("Initial Project Creation")]
        [Ignore("Test authoring is in progress...")]
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
