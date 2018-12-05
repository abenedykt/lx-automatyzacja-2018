using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectFactoryExample
{
    internal class NotePage : BasePage
    {
        public NotePage(IWebDriver browser, Uri url) : base(browser)
        {
            _browser.Navigate().GoToUrl(url);
        }

        [FindsBy(How = How.CssSelector, Using = ".entry-title")]
        private IWebElement NoteTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".entry-content")]
        private IWebElement NoteContent { get; set; }

        public string Title => NoteTitle.Text;
        public string Content => NoteContent.Text;
    }
}