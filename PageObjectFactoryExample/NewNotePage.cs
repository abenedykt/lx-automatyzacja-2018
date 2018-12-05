using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace PageObjectFactoryExample
{
    internal class NewNotePage
    {
        private IWebDriver _browser;

        public NewNotePage(IWebDriver browser)
        {
            _browser = browser;
            PageFactory.InitElements(_browser, this);
        }

        [FindsBy(How = How.Id, Using ="title")]
        public IWebElement Title { get; set; }

        [FindsBy(How = How.Id, Using = "content-html")]
        public IWebElement SwitchToHtml { get; set; }

        [FindsBy(How = How.Id, Using ="content")]
        public IWebElement Content { get; set; }

        [FindsBy(How = How.Id, Using ="publish")]
        public IWebElement Publish { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sample-permalink>a")]
        public IWebElement Slug { get; set; }

        internal Uri Create(string title, string content)
        {
            Title.SendKeys(title);
            SwitchToHtml.Click();
            Content.SendKeys(content);

            WaitForClickable(By.CssSelector(".edit-slug.button"), 3);

            Publish.Click();
            WaitForClickable(By.Id("publish"), 5);

            return new Uri(Slug.GetAttribute("href"));
        }

        protected void WaitForClickable(By by, int seconds)
        {
            var wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        protected void WaitForClickable(IWebElement element, int seconds)
        {
            var wait = new WebDriverWait(_browser, TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }
    }
}