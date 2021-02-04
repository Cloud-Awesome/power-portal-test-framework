# CloudAwesome.PortalTestFramework

Fluent API to test MS Power Apps Portals. Supports multiple personas (user accounts) in config to allow testing of different security roles and scenarios.

Can use in conjunction with CloudAwesome.Xrm.Core to set up/tear down scenario data and query/assert appropriate outcomes in CDS.

```csharp
[Test]
[Category("Case Management")]
[Description("User can navigate to support and create a new case. " +
             "The created case's reference is appended to the URL")]
public void CreateNewCase()
{
    // Arrange
    var config = new PortalConfiguration(personaName:"arthur");

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
```
