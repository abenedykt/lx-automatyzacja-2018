using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace PageObjectFactoryExample
{
    internal class NewNotePage : BasePage
    {
        public NewNotePage(IWebDriver browser) : base(browser)
        {
        }

        internal Uri Create(string title, string content)
        {
            Title.SendKeys(title);
            SwitchToHtml.Click();
            Content.SendKeys(content);

            WaitForClickable(SlugButton, 3);

            PublishButton.Click();
            WaitForClickable(PublishButton, 5);

            return new Uri(Slug.GetAttribute("href"));
        }

        [FindsBy(How = How.Id, Using = "title")]
        private IWebElement Title { get; set; }

        [FindsBy(How = How.Id, Using = "content-html")]
        private IWebElement SwitchToHtml { get; set; }

        [FindsBy(How = How.Id, Using = "content")]
        private IWebElement Content { get; set; }

        [FindsBy(How = How.Id, Using = "publish")]
        private IWebElement PublishButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".edit-slug.button")]
        private IWebElement SlugButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#sample-permalink>a")]
        private IWebElement Slug { get; set; }
    }
}