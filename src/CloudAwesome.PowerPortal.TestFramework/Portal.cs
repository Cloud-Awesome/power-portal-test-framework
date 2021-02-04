using System;
using System.Collections.Generic;
using System.Threading;
using CloudAwesome.PowerPortal.TestFramework.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Edge;
using Microsoft.Edge.SeleniumTools;

namespace CloudAwesome.PowerPortal.TestFramework
{
    public class Portal
    {
        private readonly IWebDriver _driver;
        private readonly PortalConfiguration _config;

        public Portal(PortalConfiguration configuration)
        {
            _config = configuration;

            switch (_config.BrowserSettings.BrowserType)
            {
                case BrowserType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    if (_config.BrowserSettings.Headless)
                    {
                        chromeOptions.AddArguments("headless");
                    }

                    _driver = new ChromeDriver(chromeOptions);
                    
                    break;
                case BrowserType.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    if (_config.BrowserSettings.Headless)
                    {
                        firefoxOptions.AddArguments("--headless");
                    }

                    _driver = new FirefoxDriver(firefoxOptions);
                    
                    break;
                case BrowserType.Edge:

                    var edgeOptions = new EdgeOptions();
                    edgeOptions.UseChromium = true;

                    if (_config.BrowserSettings.Headless)
                    {
                        edgeOptions.AddArguments("headless");
                    }

                    _driver = new EdgeDriver(edgeOptions);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //_driver = new FirefoxDriver();
            this.Navigate(_config.BaseUrl);
        }

        public string GetCurrentUrl()
        {
            return _driver.Url;
        }

        public bool Login()
        {
            switch (_config.UserCredentials.AuthenticationType)
            {
                case AuthenticationType.Local:
                    // TODO - use the wrapper methods in this class for all of these direct calls to _driver and Thread.Sleep
                    // Don't use direct Selenium calls
                    _driver.Navigate().GoToUrl($"{_driver.Url}{LoginPage.PageUrl}");
                    _driver.FindElement(By.Id(LoginPage.UserName)).SendKeys(_config.UserCredentials.UserName);
                    _driver.FindElement(By.Id(LoginPage.Password)).SendKeys(_config.UserCredentials.UserPassword);
                    _driver.FindElement(By.Id(LoginPage.LocalSubmitButton)).Click();
                    Thread.Sleep(1000);

                    break;

                case AuthenticationType.ActiveDirectory:
                    _driver.Navigate().GoToUrl($"{_driver.Url}{LoginPage.PageUrl}");
                    _driver.FindElement(By.Name("Azure AD")).Click();
                    // ?? How best to handle using the MS login page......

                    throw new NotImplementedException("Only Local Authentication is currently supported");
                    
                case AuthenticationType.Facebook:
                    throw new NotImplementedException("Only Local Authentication is currently supported");

                case AuthenticationType.Google:
                    throw new NotImplementedException("Only Local Authentication is currently supported");

                default:
                    throw new NotImplementedException("Only Local Authentication is currently supported");

            }
            
            return true;

            //TODO - general helper: wrap FindElement By.Id to throw a good error if element not found
            //TODO - general helper: single(?) resx or class of magic strings for IDs and CSS classes
            //TODO - If the user is a Portal Admin, get rid of the Admin toolbar? (unfortunately reappears on every page load...)

        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }

        public Portal Click(string element, int waitTime = 0)
        {
            _driver.FindElement(By.Id(element)).Click();
            if (waitTime > 0)
            {
                this.Wait(waitTime);
            }

            return this;

            //TODO Click by ID; Click by CSS ClassName; Selector; Click by Title; Click by Href; Link Text; XPath; 
            //
            //public Portal Click(By by)
            //{
            //    _driver.FindElement(by).Click();
            //    return this;
            //}
            //
            // Something like this ^^ but preferably not needing a reference to OpenQA in the test classes...
            // Probably no point in recreating the By.cs that is already provided if possible...
        }

        public Portal ClickByClassName(string className, int waitTime = 0)
        {
            _driver.FindElement(By.ClassName(className)).Click();
            if (waitTime > 0)
            {
                this.Wait(waitTime);
            }
            return this;
        }

        public Portal ClickByLinkText(string linkText, int waitTime = 0)
        {
            _driver.FindElement(By.LinkText(linkText)).Click();
            if (waitTime > 0)
            {
                this.Wait(waitTime);
            }
            return this;
        }

        public Portal Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            return this;
        }

        public Portal Navigate(string url, int waitTime = 0)
        {
            _driver.Navigate().GoToUrl(url == _config.BaseUrl ? 
                $"{url}" : 
                $"{_driver.Url}{url}");

            if (waitTime > 0)
            {
                this.Wait(waitTime);
            }

            return this;

            //TODO - Include an Assert if navigation fails
            // ^^ Could there be different reasons, e.g. 404, unauthorised, etc..?
        }

        public Portal SetValue(string element, string value)
        {
            _driver.FindElement(By.Id(element)).SendKeys(value);
            return this;

            //TODO - Assert.Fail if can't find element
            //TODO - Data types: String; lookup; datetime; option set; currency; tickbox
            //TODO - Extract FindElement(By....) to enable multiple selectors for each method (SetValue, GetValue, Click, Clear, etc...)
        }

        public string GetValue(string element)
        {
            return _driver.FindElement(By.Id(element)).GetAttribute("value");
        }

        public Portal Clear(string element)
        {
            _driver.FindElement(By.Id(element)).Clear();
            return this;
        }

        public Portal Assert()
        {
            //Assert.AreEqual...
            throw new NotImplementedException();

            //TODO - wrap some of the Assert methods to permit fluent chaining
            // ^^ Not best practice in unit testing, but perhaps useful in UI tests to gracefully fail when something is wrong?
        }

        public Portal ValidatePage<T>(List<T> validators)
        {
            // Doesn't do anything yet ;)
            return this;
        }

        public Portal ValidatePage(List<Validator> validators)
        {
            // Doesn't do anything yet ;)
            return this;
        }

        public bool IsEnabled(string element)
        {
            throw new NotImplementedException();
        }

        public bool IsDisplayed(string element)
        {
            throw new NotImplementedException();
        }

        public string GetElementDetail(string element)
        {
            throw new NotImplementedException();

            //TODO - Get non-form elements, e.g. H1 text, title, lede copy, assert breadcrumb values; image name/alt text, element colour, etc...
            //TODO - Assert is read-only, is visible, has focus
        }

        public bool AttachFile()
        {
            throw new NotImplementedException();

            //TODO - Select and attach file. What to return (bool? number of files attached? Support for multiple files?)
        }

        public void Quit()
        {
            _driver.Quit();
        }

        //TODO - deal with popovers, e.g. address on /contact-us/ form
        //TODO - Helper function for /search/ pop out, including search filter
        //TODO - Work with Rich text editor, e.g. in /forums/general-discussion/ (CKE editor)
        //TODO - Handle back and forward browser buttons (and any others, e.g. F5? _driver.Navigate().Refresh())
        //TODO - Verify cookie values? Cookie has been set, updated, etc..?
        //TODO - Support for expanding async SPA queries, e.g. Case Deflection search (/support/)
        // (MVP Only supports Local auth; Add AD etc. Post-MVP...)
    }
}
