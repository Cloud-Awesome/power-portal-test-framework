using System;
using System.Collections.Generic;
using NUnit.Framework;
using CloudAwesome.PowerPortal.TestFramework.Models;
using CloudAwesome.PowerPortal.TestFramework.PageValidators;

namespace CloudAwesome.PowerPortal.TestFramework.Samples
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
            Assert.AreEqual("https://awesome-portal.powerappsportals.com/", config.BaseUrl);
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
                portal.Quit();
                Assert.Fail("Failed to authenticate");
            }

            Assert.True(true, "Succeeded in logging in!");
            Assert.AreEqual(config.BaseUrl, portal.GetCurrentUrl());

            portal.Quit();
        }

        [Test]
        public void WalksThroughContactDetailsInGdsPortal()
        {
            const int standardWaitTime = 2000;
            var config = new PortalConfiguration("arthur");

            var portal = new Portal(config);
            if (!portal.Login())
            {
                portal.Quit();
                Assert.Fail("Failed to authenticate");
            }

            var actionUrl =
                portal.Navigate("task-list", standardWaitTime)
                    .ClickByLinkText("Contact details")
                    .Click("UpdateButton", standardWaitTime)
                    .Clear("telephone2")
                    .Clear("telephone1")
                    .SetValue("telephone2", "0131 618 618 4")
                    .SetValue("telephone1", "07789 456 123")
                    .ValidatePage(
                        // TODO - Probably want to validate every page, add option to include in PortalConfiguration?
                        new List<IPageValidator>()
                        {
                            new IsEnabled("tester"),
                            new IsEnabled("tester2"),
                            new ValidateHeaders(),
                            new ValidateUrlFormat()
                        }, Assert.Fail, Assert.Warn)
                    .Click("UpdateButton", standardWaitTime)
                    .Clear("emailaddress2")
                    .SetValue("emailaddress2", "myemailaddress@personal.test")
                    .Click("UpdateButton", standardWaitTime)
                    .Click("UpdateButton", standardWaitTime)
                    .Click("UpdateButton", standardWaitTime)
                    .GetCurrentUrl();

            var signedOutUrl =
                portal
                    .ClickByLinkText("Close") // Shouldn't be required!
                                              // This is just if you're logged in as an Admin.
                                              // Tests shouldn't normally be run as Admins ;)
                    .ClickByLinkText("Sign Out", standardWaitTime)
                    .GetCurrentUrl();

            portal.Quit();

            Assert.AreEqual("https://awesome-portal.powerappsportals.com/task-list/", actionUrl);
            Assert.AreEqual("https://awesome-portal.powerappsportals.com/", signedOutUrl);


            /*
             * TODO - Actions....
             * 2. Implement SetValue for other data types than string ;)
             * 3. Extract the common set up into a base class...
             * 4. Add helpers to extract magic strings etc...
             * 5. Add other test functions that allow you to 
             * 6. How to handle if an element doesn't exist
             * 7. Implement a ClickIfExists? (e.g. for the Admin dialog)
             */

        }



        #region Ignored Tests

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
        [Ignore("Ignored while working with sample GDS portal")]
        public void NavigateToProfileFromHeader()
        {
            var config = new PortalConfiguration("arthur");

            // Arrange
            var portal = new Portal(config);
            if (!portal.Login())
            {
                portal.Quit();
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
        [Ignore("Ignored while working with sample GDS portal")]
        public void FirstTestDuringSetUp()
        {
            #region actions to do 
            //TODO - Support for multiple browsers
            //TODO - Initialisation helper - set up and tear down of local user account
            //TODO - Provide several different base classes, e.g. LoginAndQuitAfterTest(personaName), BasePortalTest()
            // Each class should be as test small as possible and include as little set up as possible.
            // ^^ On average, 10-20 lines max. would be great :)
            #endregion actions to do
            var config = new PortalConfiguration("arthur");

            // Arrange
            var portal = new Portal(config);
            if (!portal.Login())
            {
                //TODO - extract this out into the Login method, not in the test class
                portal.Quit();
                Assert.Fail("Failed to authenticate");
            }

            // Act
            var result = 
                portal
                    .SetValue("firstname", "Arthur") // TODO - Extract magic strings out into overridable DataModel
                    .SetValue("lastname", "Nicholson-Gumuła")
                    .Click("submit")
                    .Wait(1000)
                    .GetValue("name");

            // Assert
            Assert.AreEqual("Arthur Nicholson-Gumuła", result, 
                "Record name has been set correctly");

            portal.Quit();

        }

        #endregion Ignored Tests
    }
}
