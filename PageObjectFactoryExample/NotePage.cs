using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectFactoryExample
{
    internal class NotePage
    {
        private IWebDriver _browser;

        public NotePage(IWebDriver browser, Uri url)
        {
            _browser = browser;
            _browser.Navigate().GoToUrl(url);
            PageFactory.InitElements(_browser, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".entry-title")]
        public IWebElement NoteTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".entry-content")]
        public IWebElement NoteContent { get; set; }

        public string Title => NoteTitle.Text;
        public string Content => NoteContent.Text;
    }
}