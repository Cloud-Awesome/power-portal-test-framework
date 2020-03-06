using System;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

using CloudAwesome.PortalTestFramework.Models;

namespace CloudAwesome.PortalTestFramework
{
    public class Portal
    {
        private readonly IWebDriver _driver;
        private readonly PortalConfiguration _config;

        public Portal(PortalConfiguration configuration)
        {
            _config = configuration;

            _driver = new FirefoxDriver();
            this.Navigate(_config.BaseUrl);
            //_driver.Navigate().GoToUrl(_config.BaseUrl);
        }

        public bool Login()
        {
            // TODO - use the wrapper methods in this class for all of these direct calls to _driver and Thread.Sleep
            // Don't use direct calls
            _driver.Navigate().GoToUrl($"{_driver.Url}{LoginPage.PageUrl}");
            _driver.FindElement(By.Id(LoginPage.UserName)).SendKeys(_config.UserCredentials.UserName);
            _driver.FindElement(By.Id(LoginPage.Password)).SendKeys(_config.UserCredentials.UserPassword);
            _driver.FindElement(By.Id(LoginPage.LocalSubmitButton)).Click();
            Thread.Sleep(1000);
            
            return true;

            //TODO - general helper: wrap FindElement By.Id to throw a good error if element not found
            //TODO - general helper: single(?) resx or class of magic strings for IDs and CSS classes
            //TODO - Portals are charged per login - think about extracting this out into a single static login, somehow...
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }

        public Portal Click(string element)
        {
            _driver.FindElement(By.Id(element)).Click();
            return this;

            //TODO Click by ID; Click by CSS ClassName; Selector; Click by Title; Click by Href; Link Text; XPath; 
        }

        public Portal Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
            return this;
        }

        public Portal Navigate(string Url)
        {
            _driver.Navigate().GoToUrl($"{_driver.Url}{Url}");
            return this;

            //TODO - Include an Assert if navigation fails
            // ^^ Could there be different reasons, e.g. 404, unauthorised, etc..?
        }

        public Portal SetValue(string element, string value)
        {
            _driver.FindElement(By.Id(element)).SendKeys(value);
            return this;

            //TODO - Data types: String; lookup; datetime; option set; currency; tickbox
            //TODO - Extract FindElement(By....) to enable multiple selectors for each method (SetValue, GetValue, Click, Clear, etc...)
        }

        public string GetValue(string element)
        {
            return _driver.FindElement(By.Id(element)).Text;
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
