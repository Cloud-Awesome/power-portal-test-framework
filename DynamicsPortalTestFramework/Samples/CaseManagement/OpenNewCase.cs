using System;
using NUnit.Framework;

using CloudAwesome.D365Portal.TestFramework;
using CloudAwesome.D365Portal.TestFramework.Models;

namespace CloudAwesome.PortalTests.Samples.CaseManagement
{
    [TestFixture]
    public class OpenNewCase
    {
        [Test]
        [Category("Case Management")]
        [Description("Tests that the multi-persona PortalConfiguration returns a valid model from app.config")]
        public void CreateNewCase()
        {
            // Arrange
            var config = new PortalConfiguration("arthur");

            var portal = new Portal(config);
            if (!portal.Login())
            {
                Assert.Fail("Failed to authenticate");
            }

            // Act
            var createdCaseUrl =
                portal
                    .Navigate(SupportPage.PageUrl)
                    .Wait(1000)
                    .ClickByLinkText(SupportPage.CreateNewCase)
                    .Wait(1000)
                    .SetValue(SupportPage.Title, $"New Test Case - {DateTime.Now}")
                    //.SetValue(SupportPage.CaseType, )
                    .SetValue(SupportPage.Description,
                        "This is a new case because I'm having problems setting OptionSets in the portal")
                    .Click(SupportPage.Submit)
                    .Wait(2000)
                    .GetCurrentUrl();

            portal.Quit();

            // Assert
            Console.WriteLine($"URL of generated Case is {createdCaseUrl}");
            Assert.IsTrue(createdCaseUrl.Contains("/?created"));
        }
    }
}
